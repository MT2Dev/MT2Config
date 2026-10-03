using System;
using System.Globalization;
using System.Linq;

namespace MT2Config
{
    /// <summary>
    /// UI texts, one file per language in the Languages folder. They are compiled into config.exe instead of satellite
    /// resource DLLs, so the tool stays a single file. To add a language: copy Languages/English.cs, translate it, and
    /// add a field below plus an entry in <see cref="All"/>.
    /// </summary>
    internal sealed partial class Language
    {
        const string SettingName = "Language";

        // Created through factory methods so the initialization order does not depend on how the partial files are
        // ordered by the compiler.
        public static readonly Language Turkish = CreateTurkish();
        public static readonly Language English = CreateEnglish();
        public static readonly Language Danish = CreateDanish();
        public static readonly Language German = CreateGerman();
        public static readonly Language Spanish = CreateSpanish();
        public static readonly Language French = CreateFrench();
        public static readonly Language Italian = CreateItalian();
        public static readonly Language Hungarian = CreateHungarian();
        public static readonly Language Dutch = CreateDutch();
        public static readonly Language Portuguese = CreatePortuguese();
        public static readonly Language Romanian = CreateRomanian();
        public static readonly Language Greek = CreateGreek();
        public static readonly Language Russian = CreateRussian();

        // Order of the language list.
        public static readonly Language[] All =
        {
            Turkish, English, Danish, German, Spanish, French, Italian, Hungarian, Dutch, Portuguese, Romanian, Greek, Russian,
        };

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
        public string DarkMode { get; private set; }
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

        // Tooltips: what each option does.
        public string TipScreenMode { get; private set; }
        public string TipResolution { get; private set; }
        public string TipRefreshRate { get; private set; }   // {0} GameClient.MaxRefreshRate
        public string TipGamma { get; private set; }         // {0} default gamma (ClientConfig)
        public string TipViewDistance { get; private set; }
        public string TipShadows { get; private set; }
        public string TipTiling { get; private set; }
        public string TipObjectCulling { get; private set; }
        public string TipSoftwareCursor { get; private set; }
        public string TipDecompressedTextures { get; private set; }
        public string TipMusic { get; private set; }
        public string TipEffects { get; private set; }
        public string TipShowChat { get; private set; }
        public string TipAlwaysShowNames { get; private set; }
        public string TipShowDamage { get; private set; }
        public string TipShowShopTitles { get; private set; }
        public string TipWindowsIme { get; private set; }
        public string TipLanguage { get; private set; }
        public string TipDarkMode { get; private set; }
        public string TipDefaults { get; private set; }
        public string TipSaveAndPlay { get; private set; }   // {0} game executable file name
        public string TipSave { get; private set; }
        public string TipCancel { get; private set; }

        public static Language Find(string code)
        {
            return All.FirstOrDefault(language => string.Equals(language.Code, code, StringComparison.OrdinalIgnoreCase));
        }

        // The player's last choice, otherwise the Windows display language, otherwise the regional format.
        public static Language LoadPreferred()
        {
            return Find(UserSettings.Get(SettingName) as string)
                ?? Find(CultureInfo.CurrentUICulture.TwoLetterISOLanguageName)
                ?? Find(CultureInfo.CurrentCulture.TwoLetterISOLanguageName)
                ?? English;
        }

        public void SaveAsPreferred()
        {
            UserSettings.Set(SettingName, Code);
        }

        public override string ToString() => Name;
    }
}
