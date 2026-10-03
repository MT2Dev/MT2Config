using System.IO;
using System.Linq;

namespace MT2Config
{
    /// <summary>Server specific settings, change them for your own client.</summary>
    internal static class GameClient
    {
        // Window title: "Metin2 - Settings".
        public const string Name = "Metin2";

        // Highest refresh rate the client supports. Faster monitor modes are not offered and a higher FREQUENCY
        // found in metin2.cfg is lowered.
        public const int MaxRefreshRate = 60;

        // Options the client reads from metin2.cfg but never applies (as in the vanilla client). They are hidden in the
        // window, and their lines in metin2.cfg are not touched. Set one to true once your client uses it.
        public static readonly bool ShowGamma = false;              // GAMMA
        public static readonly bool ShowViewDistance = false;       // VISIBILITY
        public static readonly bool ShowObjectCulling = false;      // OBJECT_CULLING
        public static readonly bool ShowDecompressedTextures = false; // DECOMPRESSED_TEXTURE

        // "Save and Play" starts the first of these found next to config.exe; if none is found the button is hidden.
        // Put your client's file name here, or empty the list if the game has to be started from a patcher.
        static readonly string[] ExecutableNames = { "MT2DevCore.exe", "metin2client.exe", "Metin2Release.exe", "Metin2Distribute.exe" };

        public static string FindExecutable(string directory)
        {
            return ExecutableNames.Select(name => Path.Combine(directory, name)).FirstOrDefault(File.Exists);
        }
    }
}
