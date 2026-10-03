using System.Drawing;
using System.Windows.Forms;

namespace MT2Config
{
    /// <summary>
    /// Colors of the window. The light theme keeps the native Windows look; the dark theme is drawn by the Themed*
    /// controls and <see cref="MainForm"/>, because WinForms on .NET Framework has no dark mode of its own.
    /// </summary>
    internal sealed class Theme
    {
        const string SettingName = "DarkMode";

        public static readonly Theme Light = new Theme { IsDark = false };

        public static readonly Theme Dark = new Theme
        {
            IsDark = true,
            Background = Color.FromArgb(32, 32, 32),
            Surface = Color.FromArgb(45, 45, 45),
            SurfaceHover = Color.FromArgb(56, 56, 56),
            SurfacePressed = Color.FromArgb(68, 68, 68),
            Border = Color.FromArgb(85, 85, 85),
            BorderHover = Color.FromArgb(122, 122, 122),
            Text = Color.FromArgb(240, 240, 240),
            DisabledText = Color.FromArgb(140, 140, 140),
            Accent = Color.FromArgb(0, 120, 212),
            AccentText = Color.White,
        };

        public static Theme Current { get; set; } = Light;

        public bool IsDark { get; private set; }
        public Color Background { get; private set; }
        public Color Surface { get; private set; }        // inputs and buttons
        public Color SurfaceHover { get; private set; }
        public Color SurfacePressed { get; private set; }
        public Color Border { get; private set; }
        public Color BorderHover { get; private set; }
        public Color Text { get; private set; }
        public Color DisabledText { get; private set; }   // dark only, light uses the native disabled look
        public Color Accent { get; private set; }         // focus border, checked check boxes
        public Color AccentText { get; private set; }

        // Dark mode would replace the colors of a Windows high contrast theme, which players rely on for accessibility.
        public static bool IsAvailable => !SystemInformation.HighContrast;

        // The player's last choice, otherwise the "app mode" of Windows 10/11 (light on older Windows).
        public static Theme LoadPreferred()
        {
            if (!IsAvailable)
                return Light;

            object saved = UserSettings.Get(SettingName);
            if (saved is int)
                return (int)saved != 0 ? Dark : Light;

            object appsUseLightTheme = UserSettings.Read(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme");
            return appsUseLightTheme is int && (int)appsUseLightTheme == 0 ? Dark : Light;
        }

        public void SaveAsPreferred()
        {
            UserSettings.Set(SettingName, IsDark ? 1 : 0);
        }
    }
}
