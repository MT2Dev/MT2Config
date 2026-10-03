using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security;
using Microsoft.Win32;

namespace MT2Config
{
    /// <summary>
    /// UI texts. They are compiled into config.exe instead of satellite resource DLLs, so the tool stays a single file.
    /// To add a language, copy one of the instances below, translate it and add it to <see cref="All"/>.
    /// </summary>
    internal sealed class Language
    {
        const string RegistryPath = @"Software\MT2Config";
        const string RegistryValue = "Language";

        public static readonly Language English = new Language
        {
            Code = "en",
            Name = "English",
            Title = "Settings",
            Display = "Display",
            ScreenMode = "Screen mode",
            Windowed = "Windowed",
            Fullscreen = "Fullscreen",
            Resolution = "Resolution",
            RefreshRate = "Refresh rate",
            Gamma = "Gamma",
            Graphics = "Graphics",
            ViewDistance = "View distance",
            ViewDistances = new[] { "Near", "Medium", "Far" },
            Shadows = "Shadows",
            ShadowLevels = new[] { "Off", "Ground only", "Ground and own character", "All", "All (high)", "All (maximum)" },
            Tiling = "Terrain tiling",
            TilingModes = new[] { "Auto", "CPU (software)", "GPU (hardware)" },
            ObjectCulling = "Object culling",
            SoftwareCursor = "Software cursor",
            DecompressedTextures = "Uncompressed textures",
            Sound = "Sound",
            Music = "Music",
            Effects = "Effects",
            Interface = "Interface",
            ShowChat = "Show chat",
            AlwaysShowNames = "Always show names",
            ShowDamage = "Show damage",
            ShowShopTitles = "Show shop titles",
            WindowsIme = "Use Windows IME",
            LanguageLabel = "Language",
            Defaults = "Defaults",
            SaveAndPlay = "Save and Play",
            Save = "Save",
            Cancel = "Cancel",
            Custom = "custom",
            Error = "Error",
            LoadFailed = "{0} could not be read, default settings are shown.\n\n{1}",
            SaveFailed = "The settings could not be saved:\n{0}\n\n{1}\n\nIf the game is installed under \"Program Files\", run config.exe as administrator.",
            LaunchFailed = "The game could not be started:\n{0}\n\n{1}",
            UnexpectedError = "Unexpected error:\n\n{0}",
        };

        public static readonly Language Turkish = new Language
        {
            Code = "tr",
            Name = "Türkçe",
            Title = "Ayarlar",
            Display = "Ekran",
            ScreenMode = "Ekran modu",
            Windowed = "Pencere",
            Fullscreen = "Tam ekran",
            Resolution = "Çözünürlük",
            RefreshRate = "Yenileme hızı",
            Gamma = "Gama",
            Graphics = "Grafik",
            ViewDistance = "Görüş mesafesi",
            ViewDistances = new[] { "Yakın", "Orta", "Uzak" },
            Shadows = "Gölgeler",
            ShadowLevels = new[] { "Kapalı", "Sadece zemin", "Zemin ve karakterin", "Tümü", "Tümü (yüksek)", "Tümü (maksimum)" },
            Tiling = "Arazi işleme",
            TilingModes = new[] { "Otomatik", "CPU (yazılım)", "GPU (donanım)" },
            ObjectCulling = "Görünmeyen nesneleri gizle",
            SoftwareCursor = "Yazılımsal imleç",
            DecompressedTextures = "Sıkıştırılmamış dokular",
            Sound = "Ses",
            Music = "Müzik",
            Effects = "Efektler",
            Interface = "Arayüz",
            ShowChat = "Sohbeti göster",
            AlwaysShowNames = "İsimleri her zaman göster",
            ShowDamage = "Hasarı göster",
            ShowShopTitles = "Pazar başlıklarını göster",
            WindowsIme = "Windows IME kullan",
            LanguageLabel = "Dil",
            Defaults = "Varsayılan",
            SaveAndPlay = "Kaydet ve Oyna",
            Save = "Kaydet",
            Cancel = "İptal",
            Custom = "özel",
            Error = "Hata",
            LoadFailed = "{0} okunamadı, varsayılan ayarlar gösteriliyor.\n\n{1}",
            SaveFailed = "Ayarlar kaydedilemedi:\n{0}\n\n{1}\n\nOyun \"Program Files\" altında kuruluysa config.exe'yi yönetici olarak çalıştırın.",
            LaunchFailed = "Oyun başlatılamadı:\n{0}\n\n{1}",
            UnexpectedError = "Beklenmeyen hata:\n\n{0}",
        };

        public static readonly Language[] All = { Turkish, English };

        // The language of the open window, also used by the global error handler.
        public static Language Current { get; set; } = English;

        public string Code { get; private set; }
        public string Name { get; private set; }
        public string Title { get; private set; }
        public string Display { get; private set; }
        public string ScreenMode { get; private set; }
        public string Windowed { get; private set; }
        public string Fullscreen { get; private set; }
        public string Resolution { get; private set; }
        public string RefreshRate { get; private set; }
        public string Gamma { get; private set; }
        public string Graphics { get; private set; }
        public string ViewDistance { get; private set; }
        public string[] ViewDistances { get; private set; } // VISIBILITY 1, 2, 3
        public string Shadows { get; private set; }
        public string[] ShadowLevels { get; private set; }  // SHADOW_LEVEL 0 - 5 (SHADOW_NONE ... SHADOW_ALL_MAX)
        public string Tiling { get; private set; }
        public string[] TilingModes { get; private set; }   // SOFTWARE_TILING 0, 1, 2
        public string ObjectCulling { get; private set; }
        public string SoftwareCursor { get; private set; }
        public string DecompressedTextures { get; private set; }
        public string Sound { get; private set; }
        public string Music { get; private set; }
        public string Effects { get; private set; }
        public string Interface { get; private set; }
        public string ShowChat { get; private set; }
        public string AlwaysShowNames { get; private set; }
        public string ShowDamage { get; private set; }
        public string ShowShopTitles { get; private set; }
        public string WindowsIme { get; private set; }
        public string LanguageLabel { get; private set; }
        public string Defaults { get; private set; }
        public string SaveAndPlay { get; private set; }
        public string Save { get; private set; }
        public string Cancel { get; private set; }
        public string Custom { get; private set; }
        public string Error { get; private set; }
        public string LoadFailed { get; private set; }      // {0} path, {1} error
        public string SaveFailed { get; private set; }      // {0} path, {1} error
        public string LaunchFailed { get; private set; }    // {0} path, {1} error
        public string UnexpectedError { get; private set; } // {0} exception

        public static Language Find(string code)
        {
            return All.FirstOrDefault(language => string.Equals(language.Code, code, StringComparison.OrdinalIgnoreCase));
        }

        // The player's last choice, otherwise the Windows display language, otherwise the regional format.
        public static Language LoadPreferred()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RegistryPath))
                {
                    Language saved = Find(key?.GetValue(RegistryValue) as string);
                    if (saved != null)
                        return saved;
                }
            }
            catch (Exception e) when (e is SecurityException || e is UnauthorizedAccessException || e is IOException)
            {
            }

            return Find(CultureInfo.CurrentUICulture.TwoLetterISOLanguageName)
                ?? Find(CultureInfo.CurrentCulture.TwoLetterISOLanguageName)
                ?? English;
        }

        // Stored per Windows user (HKCU), needs no administrator rights.
        public void SaveAsPreferred()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey(RegistryPath))
                    key.SetValue(RegistryValue, Code);
            }
            catch (Exception e) when (e is SecurityException || e is UnauthorizedAccessException || e is IOException)
            {
            }
        }

        public override string ToString() => Name;
    }
}
