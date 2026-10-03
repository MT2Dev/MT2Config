using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace MT2Config
{
    /// <summary>
    /// metin2.cfg, read and written the same way as the client's CPythonSystem::LoadConfig() / SaveConfig().
    /// Keys this tool does not edit (BPP, SAVE_ID, custom keys of a modified client, ...) are kept as they are.
    /// </summary>
    internal sealed class ClientConfig
    {
        public const string FileName = "metin2.cfg";

        // Key order of CPythonSystem::SaveConfig(); other keys found in the file are written after these.
        static readonly string[] SaveOrder =
        {
            "WIDTH", "HEIGHT", "BPP", "FREQUENCY", "SOFTWARE_CURSOR", "OBJECT_CULLING", "VISIBILITY",
            "MUSIC_VOLUME", "VOICE_VOLUME", "GAMMA", "IS_SAVE_ID", "SAVE_ID", "PRE_LOADING_DELAY_TIME",
            "DECOMPRESSED_TEXTURE", "WINDOWED", "VIEW_CHAT", "ALWAYS_VIEW_NAME", "SHOW_DAMAGE",
            "SHOW_SALESTEXT", "USE_DEFAULT_IME", "SOFTWARE_TILING", "SHADOW_LEVEL", "NO_SOUND_CARD",
        };

        // The client compares keys with stricmp(). OrdinalIgnoreCase does the same and, unlike ToUpper() or
        // culture-aware comparisons, is not broken by the Turkish i/İ casing rules.
        static readonly HashSet<string> SaveOrderKeys = new HashSet<string>(SaveOrder, StringComparer.OrdinalIgnoreCase);

        // Latin-1 maps every byte to exactly one char and back, so values we never touch
        // (e.g. SAVE_ID in the player's ANSI code page) are written back byte for byte.
        static readonly Encoding FileEncoding = Encoding.GetEncoding(28591);

        const int KeyColumnWidth = 24;

        readonly Dictionary<string, string> entries = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        readonly List<string> entryOrder = new List<string>();

        // Values used when a key is missing from the file (and by the "Defaults" button). They must match the client's
        // CPythonSystem::SetDefaultConfig(): SaveConfig() leaves WINDOWED, VIEW_CHAT, ALWAYS_VIEW_NAME, SHOW_DAMAGE and
        // SHOW_SALESTEXT out of the file while they hold their default value.
        public int Width { get; set; } = 1024;
        public int Height { get; set; } = 768;
        public int Frequency { get; set; } = 60;
        public bool Windowed { get; set; } = false;
        public int Gamma { get; set; } = 3;
        public int Visibility { get; set; } = 3;
        public int ShadowLevel { get; set; } = 3;
        public int SoftwareTiling { get; set; } = 0;
        public bool SoftwareCursor { get; set; } = false;
        public bool ObjectCulling { get; set; } = true;
        public bool DecompressedTexture { get; set; } = false;
        public float MusicVolume { get; set; } = 1.0f; // 0.0 - 1.0
        public int VoiceVolume { get; set; } = 5;      // 0 - 5
        public bool UseDefaultIme { get; set; } = false;
        public bool ViewChat { get; set; } = true;
        public bool AlwaysShowName { get; set; } = true; // DEFAULT_VALUE_ALWAYS_SHOW_NAME (true in the client)
        public bool ShowDamage { get; set; } = true;
        public bool ShowSalesText { get; set; } = true;

        public static ClientConfig Load(string path)
        {
            return File.Exists(path) ? Parse(File.ReadAllBytes(path)) : new ClientConfig();
        }

        public void Save(string path)
        {
            File.WriteAllBytes(path, ToBytes());
        }

        public static ClientConfig Parse(byte[] data)
        {
            var config = new ClientConfig();

            // A UTF-8 BOM (e.g. after saving the file with Notepad) would become part of the first key in the
            // client and silently disable that setting. Skip it here; ToBytes() never writes one.
            int start = data.Length >= 3 && data[0] == 0xEF && data[1] == 0xBB && data[2] == 0xBF ? 3 : 0;

            string value = "";
            foreach (string line in FileEncoding.GetString(data, start, data.Length - start).Split('\n'))
            {
                // sscanf(buf, " %s %s\n", command, value)
                string[] tokens = line.Split(CRuntime.Whitespace, StringSplitOptions.RemoveEmptyEntries);
                if (tokens.Length == 0)
                    break; // sscanf() returns EOF for an empty line and the client stops reading the file there

                // When the line has no value, sscanf() leaves `value` untouched and the client reuses the previous one.
                if (tokens.Length > 1)
                    value = tokens[1];

                config.Set(tokens[0], value);
            }

            config.ReadEntries();
            return config;
        }

        public byte[] ToBytes()
        {
            WriteEntries();

            var text = new StringBuilder();
            foreach (string key in SaveOrder)
            {
                if (entries.ContainsKey(key))
                    AppendLine(text, key, entries[key]);
            }
            foreach (string key in entryOrder)
            {
                if (!SaveOrderKeys.Contains(key))
                    AppendLine(text, key, entries[key]);
            }
            // SaveConfig() ends the file with an empty line as well. There must be no other empty line:
            // the client stops reading at the first one.
            text.Append("\r\n");

            return FileEncoding.GetBytes(text.ToString());
        }

        // Same conversions as CPythonSystem::LoadConfig().
        void ReadEntries()
        {
            string v;
            if (entries.TryGetValue("WIDTH", out v)) Width = CRuntime.Atoi(v);
            if (entries.TryGetValue("HEIGHT", out v)) Height = CRuntime.Atoi(v);
            if (entries.TryGetValue("FREQUENCY", out v)) Frequency = CRuntime.Atoi(v);
            if (entries.TryGetValue("SOFTWARE_CURSOR", out v)) SoftwareCursor = CRuntime.Atoi(v) != 0;
            if (entries.TryGetValue("OBJECT_CULLING", out v)) ObjectCulling = CRuntime.Atoi(v) != 0;
            if (entries.TryGetValue("VISIBILITY", out v)) Visibility = CRuntime.Atoi(v);
            if (entries.TryGetValue("MUSIC_VOLUME", out v)) MusicVolume = ParseMusicVolume(v);
            if (entries.TryGetValue("VOICE_VOLUME", out v)) VoiceVolume = CRuntime.Atoi(v);
            if (entries.TryGetValue("GAMMA", out v)) Gamma = CRuntime.Atoi(v);
            if (entries.TryGetValue("WINDOWED", out v)) Windowed = CRuntime.Atoi(v) == 1;
            if (entries.TryGetValue("USE_DEFAULT_IME", out v)) UseDefaultIme = CRuntime.Atoi(v) == 1;
            if (entries.TryGetValue("SOFTWARE_TILING", out v)) SoftwareTiling = CRuntime.Atoi(v);
            if (entries.TryGetValue("SHADOW_LEVEL", out v)) ShadowLevel = CRuntime.Atoi(v);
            if (entries.TryGetValue("DECOMPRESSED_TEXTURE", out v)) DecompressedTexture = CRuntime.Atoi(v) == 1;
            if (entries.TryGetValue("VIEW_CHAT", out v)) ViewChat = CRuntime.Atoi(v) == 1;
            if (entries.TryGetValue("ALWAYS_VIEW_NAME", out v)) AlwaysShowName = CRuntime.Atoi(v) == 1;
            if (entries.TryGetValue("SHOW_DAMAGE", out v)) ShowDamage = CRuntime.Atoi(v) == 1;
            if (entries.TryGetValue("SHOW_SALESTEXT", out v)) ShowSalesText = CRuntime.Atoi(v) == 1;
        }

        // Same formats as CPythonSystem::SaveConfig(). Unlike the client, every key is written explicitly
        // (SaveConfig() skips WINDOWED, VIEW_CHAT, ... when they hold their default value). Options hidden in the
        // window (GameClient.Show*) are not set here, so the value found in the file is written back unchanged.
        void WriteEntries()
        {
            Set("WIDTH", Width);
            Set("HEIGHT", Height);
            Set("FREQUENCY", Frequency);
            Set("SOFTWARE_CURSOR", SoftwareCursor);
            if (GameClient.ShowObjectCulling)
                Set("OBJECT_CULLING", ObjectCulling);
            if (GameClient.ShowViewDistance)
                Set("VISIBILITY", Visibility);
            // "%.3f": a value without a '.' is read as the old 0-5 scale ("1" would mean ~16% volume).
            Set("MUSIC_VOLUME", MusicVolume.ToString("0.000", CultureInfo.InvariantCulture));
            Set("VOICE_VOLUME", VoiceVolume);
            if (GameClient.ShowGamma)
                Set("GAMMA", Gamma);
            if (GameClient.ShowDecompressedTextures)
                Set("DECOMPRESSED_TEXTURE", DecompressedTexture);
            Set("WINDOWED", Windowed);
            Set("VIEW_CHAT", ViewChat);
            Set("ALWAYS_VIEW_NAME", AlwaysShowName);
            Set("SHOW_DAMAGE", ShowDamage);
            Set("SHOW_SALESTEXT", ShowSalesText);
            Set("USE_DEFAULT_IME", UseDefaultIme);
            Set("SOFTWARE_TILING", SoftwareTiling);
            Set("SHADOW_LEVEL", ShadowLevel);
        }

        static float ParseMusicVolume(string value)
        {
            if (value.IndexOf('.') < 0) // "Old compatiability": 0-5 scale
            {
                int level = CRuntime.Atoi(value);
                return level == 0 ? 0.0f : (float)Math.Pow(10.0, -1.0 + level / 5.0);
            }
            return (float)CRuntime.Atof(value);
        }

        void Set(string key, string value)
        {
            if (!entries.ContainsKey(key))
                entryOrder.Add(key);
            entries[key] = value;
        }

        void Set(string key, int value) => Set(key, value.ToString(CultureInfo.InvariantCulture));

        void Set(string key, bool value) => Set(key, value ? "1" : "0");

        static void AppendLine(StringBuilder text, string key, string value)
        {
            text.Append(key.PadRight(KeyColumnWidth - 1)).Append(' ').Append(value).Append("\r\n");
        }
    }
}
