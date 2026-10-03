///////////////////////////////////////////////////////////////
//FileName: MainForm.cs (config.cs in the original version)
//FileType: Visual C# Source file
//Author : Takuma <work.takuma@gmail.com>
//Copy Rights : Takuma
//Description : Basic c# config.exe : metin2
//Modified : 2026 MT2Dev - client compatible metin2.cfg handling,
//           monitor display modes, 13 UI languages, dark mode
///////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Windows.Forms;

namespace MT2Config
{
    public partial class MainForm : Form
    {
        static readonly int[] VisibilityValues = { 1, 2, 3 };
        static readonly int[] ShadowValues = { 0, 1, 2, 3, 4, 5 };
        static readonly int[] TilingValues = { 0, 1, 2 };
        static readonly int[] GammaValues = { 0, 1, 2, 3, 4, 5 };

        // Tooltips do not wrap long texts by themselves.
        const int TipLineLength = 60;
        const int TipPadding = 5;

        // The client opens "metin2.cfg" from its working directory, which is the client folder config.exe lives in.
        readonly string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ClientConfig.FileName);
        readonly string gamePath = GameClient.FindExecutable(AppDomain.CurrentDomain.BaseDirectory);
        readonly DisplayModes displayModes = DisplayModes.Query();

        ClientConfig config = new ClientConfig();
        Language language = Language.LoadPreferred();
        bool updating; // set while the controls are filled from code, the change handlers ignore those changes

        public MainForm()
        {
            // Before the window handle exists, so the title bar starts in the right theme.
            Theme.Current = Theme.LoadPreferred();

            InitializeComponent();

            using (Stream icon = typeof(MainForm).Assembly.GetManifestResourceStream("MT2Config.app.ico"))
                Icon = new Icon(icon);

            SaveAndPlayButton.Visible = gamePath != null;

            // Options the client does not apply (see GameClient); hidden layout rows take no space.
            GammaLabel.Visible = GammaList.Visible = GameClient.ShowGamma;
            VisibilityLabel.Visible = VisibilityList.Visible = GameClient.ShowViewDistance;
            ObjectCullingCheck.Visible = GameClient.ShowObjectCulling;
            DecompressedTextureCheck.Visible = GameClient.ShowDecompressedTextures;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            try
            {
                config = ClientConfig.Load(configPath);
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException || ex is SecurityException)
            {
                ShowError(language.LoadFailed, configPath, ex.Message);
            }

            updating = true;
            LanguageList.Items.AddRange(Language.All);
            LanguageList.MaxDropDownItems = Language.All.Length;
            LanguageList.SelectedItem = language;
            DarkModeCheck.Checked = Theme.Current.IsDark;
            DarkModeCheck.Enabled = Theme.IsAvailable;
            updating = false;

            ApplyLanguage();
            ShowConfig(config);
            ApplyTheme();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            ApplyTitleBarTheme();
        }

        void ApplyLanguage()
        {
            Language.Current = language;

            Text = GameClient.Name + " - " + language.Title;
            DisplayBox.Text = language.Display;
            ScreenModeLabel.Text = language.ScreenMode;
            ResolutionLabel.Text = language.Resolution;
            FrequencyLabel.Text = language.RefreshRate;
            GammaLabel.Text = language.Gamma;
            GraphicsBox.Text = language.Graphics;
            VisibilityLabel.Text = language.ViewDistance;
            ShadowLabel.Text = language.Shadows;
            TilingLabel.Text = language.Tiling;
            ObjectCullingCheck.Text = language.ObjectCulling;
            SoftwareCursorCheck.Text = language.SoftwareCursor;
            DecompressedTextureCheck.Text = language.DecompressedTextures;
            SoundBox.Text = language.Sound;
            MusicLabel.Text = language.Music;
            EffectsLabel.Text = language.Effects;
            InterfaceBox.Text = language.Interface;
            ViewChatCheck.Text = language.ShowChat;
            AlwaysShowNameCheck.Text = language.AlwaysShowNames;
            ShowDamageCheck.Text = language.ShowDamage;
            ShowSalesTextCheck.Text = language.ShowShopTitles;
            DefaultImeCheck.Text = language.WindowsIme;
            LanguageLabel.Text = language.LanguageLabel;
            DarkModeCheck.Text = language.DarkMode;
            DefaultsButton.Text = language.Defaults;
            SaveAndPlayButton.Text = language.SaveAndPlay;
            SaveButton.Text = language.Save;
            ExitButton.Text = language.Cancel;

            ApplyTips();
        }

        // The label and the control of an option show the same tooltip.
        void ApplyTips()
        {
            SetTip(language.TipScreenMode, ScreenModeLabel, ScreenModeList);
            SetTip(language.TipResolution, ResolutionLabel, ResolutionList);
            SetTip(string.Format(language.TipRefreshRate, GameClient.MaxRefreshRate), FrequencyLabel, FrequencyList);
            SetTip(string.Format(language.TipGamma, new ClientConfig().Gamma), GammaLabel, GammaList);
            SetTip(language.TipViewDistance, VisibilityLabel, VisibilityList);
            SetTip(language.TipShadows, ShadowLabel, ShadowList);
            SetTip(language.TipTiling, TilingLabel, TilingList);
            SetTip(language.TipObjectCulling, ObjectCullingCheck);
            SetTip(language.TipSoftwareCursor, SoftwareCursorCheck);
            SetTip(language.TipDecompressedTextures, DecompressedTextureCheck);
            SetTip(language.TipMusic, MusicLabel, MusicBar, MusicValueLabel);
            SetTip(language.TipEffects, EffectsLabel, EffectsBar, EffectsValueLabel);
            SetTip(language.TipShowChat, ViewChatCheck);
            SetTip(language.TipAlwaysShowNames, AlwaysShowNameCheck);
            SetTip(language.TipShowDamage, ShowDamageCheck);
            SetTip(language.TipShowShopTitles, ShowSalesTextCheck);
            SetTip(language.TipWindowsIme, DefaultImeCheck);
            SetTip(language.TipLanguage, LanguageLabel, LanguageList);
            SetTip(language.TipDarkMode, DarkModeCheck);
            SetTip(language.TipDefaults, DefaultsButton);
            if (gamePath != null)
                SetTip(string.Format(language.TipSaveAndPlay, Path.GetFileName(gamePath)), SaveAndPlayButton);
            SetTip(language.TipSave, SaveButton);
            SetTip(language.TipCancel, ExitButton);
        }

        void SetTip(string text, params Control[] controls)
        {
            string wrapped = WrapText(text, TipLineLength);
            foreach (Control control in controls)
                HelpToolTip.SetToolTip(control, wrapped);
        }

        static string WrapText(string text, int lineLength)
        {
            var result = new StringBuilder(text.Length + 8);
            int lineStart = 0;
            foreach (string word in text.Split(' '))
            {
                if (result.Length > lineStart && result.Length - lineStart + 1 + word.Length > lineLength)
                {
                    result.Append('\n');
                    lineStart = result.Length;
                }
                else if (result.Length > lineStart)
                {
                    result.Append(' ');
                }
                result.Append(word);
            }
            return result.ToString();
        }

        void ShowConfig(ClientConfig c)
        {
            updating = true;

            FillList(ScreenModeList, new[] { true, false }, c.Windowed, windowed => windowed ? language.Windowed : language.Fullscreen);
            FillList(ResolutionList, displayModes.Resolutions, new Resolution(c.Width, c.Height), resolution => resolution.ToString());
            FillFrequencies(c.Frequency, keepUnsupported: true);
            FillList(GammaList, GammaValues, c.Gamma, value => value.ToString());
            FillList(VisibilityList, VisibilityValues, c.Visibility, value => OptionText(language.ViewDistances, value - 1, value));
            FillList(ShadowList, ShadowValues, c.ShadowLevel, value => OptionText(language.ShadowLevels, value, value));
            FillList(TilingList, TilingValues, c.SoftwareTiling, value => OptionText(language.TilingModes, value, value));

            ObjectCullingCheck.Checked = c.ObjectCulling;
            SoftwareCursorCheck.Checked = c.SoftwareCursor;
            DecompressedTextureCheck.Checked = c.DecompressedTexture;
            MusicBar.Value = (int)Math.Round(Math.Max(0.0, Math.Min(1.0, c.MusicVolume)) * 100);
            EffectsBar.Value = Math.Max(EffectsBar.Minimum, Math.Min(EffectsBar.Maximum, c.VoiceVolume));
            ViewChatCheck.Checked = c.ViewChat;
            AlwaysShowNameCheck.Checked = c.AlwaysShowName;
            ShowDamageCheck.Checked = c.ShowDamage;
            ShowSalesTextCheck.Checked = c.ShowSalesText;
            DefaultImeCheck.Checked = c.UseDefaultIme;

            updating = false;
            UpdateControlStates();
        }

        // Hidden options are not read back (and ClientConfig does not write them), so Defaults cannot change them.
        void ReadConfig(ClientConfig c)
        {
            Resolution resolution = SelectedValue<Resolution>(ResolutionList);
            c.Width = resolution.Width;
            c.Height = resolution.Height;
            c.Frequency = SelectedValue<int>(FrequencyList);
            c.Windowed = SelectedValue<bool>(ScreenModeList);
            c.ShadowLevel = SelectedValue<int>(ShadowList);
            c.SoftwareTiling = SelectedValue<int>(TilingList);
            if (GameClient.ShowGamma)
                c.Gamma = SelectedValue<int>(GammaList);
            if (GameClient.ShowViewDistance)
                c.Visibility = SelectedValue<int>(VisibilityList);

            if (GameClient.ShowObjectCulling)
                c.ObjectCulling = ObjectCullingCheck.Checked;
            if (GameClient.ShowDecompressedTextures)
                c.DecompressedTexture = DecompressedTextureCheck.Checked;
            c.SoftwareCursor = SoftwareCursorCheck.Checked;
            c.MusicVolume = MusicBar.Value / 100f;
            c.VoiceVolume = EffectsBar.Value;
            c.ViewChat = ViewChatCheck.Checked;
            c.AlwaysShowName = AlwaysShowNameCheck.Checked;
            c.ShowDamage = ShowDamageCheck.Checked;
            c.ShowSalesText = ShowSalesTextCheck.Checked;
            c.UseDefaultIme = DefaultImeCheck.Checked;
        }

        void FillFrequencies(int frequency, bool keepUnsupported)
        {
            // Only rates up to GameClient.MaxRefreshRate are listed.
            List<int> supported = displayModes.FrequenciesOf(SelectedValue<Resolution>(ResolutionList)).ToList();

            // A rate the client cannot use (above the limit, or 0 for "monitor default") is replaced by the highest
            // supported one, as is the old rate after a resolution change that does not offer it.
            bool usable = frequency > 1 && frequency <= GameClient.MaxRefreshRate;
            if (!usable || (!keepUnsupported && supported.Count > 0 && !supported.Contains(frequency)))
                frequency = supported.Count > 0 ? supported.Max() : GameClient.MaxRefreshRate;

            FillList(FrequencyList, supported, frequency, hz => hz + " Hz");
        }

        // Fills the list and selects `selected`. A value that is not offered (hand-edited file, modified client, a mode of
        // another monitor, ...) is added as a "custom" entry, so saving never changes it behind the player's back.
        void FillList<T>(ComboBox list, IEnumerable<T> values, T selected, Func<T, string> text)
        {
            list.BeginUpdate();
            list.Items.Clear();
            foreach (T value in values)
                list.Items.Add(new Choice<T>(value, text(value)));

            int index = IndexOf(list, selected);
            if (index < 0)
                index = list.Items.Add(new Choice<T>(selected, text(selected) + " (" + language.Custom + ")"));

            list.SelectedIndex = index;
            list.EndUpdate();
        }

        void UpdateControlStates()
        {
            // The refresh rate is only used in fullscreen mode. The label stays enabled, so its tooltip ("not used in
            // windowed mode") can still be shown, and is greyed out by color: in the light theme with the color WinForms
            // uses for disabled labels; in the dark theme with our own, as WinForms' would be darker than the background.
            bool fullscreen = !SelectedValue<bool>(ScreenModeList);
            Color disabled = Theme.Current.IsDark ? Theme.Current.DisabledText
                : SystemInformation.HighContrast ? SystemColors.GrayText : SystemColors.ControlDark;
            FrequencyList.Enabled = fullscreen;
            FrequencyLabel.ForeColor = fullscreen ? Color.Empty : disabled;

            MusicValueLabel.Text = MusicBar.Value + "%";
            EffectsValueLabel.Text = EffectsBar.Value * 100 / EffectsBar.Maximum + "%";
        }

        void ApplyTheme()
        {
            Theme theme = Theme.Current;

            SuspendLayout();
            // Color.Empty gives the controls back their default (inherited or system) colors.
            BackColor = theme.IsDark ? theme.Background : Color.Empty;
            ForeColor = theme.IsDark ? theme.Text : Color.Empty;
            ApplyTheme(Controls, theme);
            ResumeLayout();

            // Native tooltips ignore BackColor; in the dark theme they are drawn here.
            HelpToolTip.OwnerDraw = theme.IsDark;

            UpdateControlStates();
            ApplyTitleBarTheme();
            Invalidate(true);
        }

        // Labels, group boxes, check boxes and layout panels take the form colors; these need more.
        static void ApplyTheme(Control.ControlCollection controls, Theme theme)
        {
            foreach (Control control in controls)
            {
                var button = control as Button;
                var list = control as ComboBox;
                if (button != null)
                {
                    if (theme.IsDark)
                    {
                        button.FlatStyle = FlatStyle.Flat;
                        button.FlatAppearance.BorderColor = theme.Border;
                        button.FlatAppearance.MouseOverBackColor = theme.SurfaceHover;
                        button.FlatAppearance.MouseDownBackColor = theme.SurfacePressed;
                        button.BackColor = theme.Surface;
                        button.UseVisualStyleBackColor = false;
                    }
                    else
                    {
                        button.FlatStyle = FlatStyle.Standard;
                        button.BackColor = Color.Empty;
                        button.UseVisualStyleBackColor = true;
                    }
                }
                else if (list != null)
                {
                    // Also the colors of the opened list. Light: the ComboBox defaults, set explicitly because an
                    // empty color would make some WinForms implementations inherit the form background.
                    list.BackColor = theme.IsDark ? theme.Surface : SystemColors.Window;
                    list.ForeColor = theme.IsDark ? theme.Text : SystemColors.WindowText;
                }
                else if (control is TrackBar)
                {
                    // Set directly: the native track bar does not always repaint when only the inherited color changes.
                    control.BackColor = theme.IsDark ? theme.Background : Color.Empty;
                }

                ApplyTheme(control.Controls, theme);
            }
        }

        // Dark title bar on Windows 10 1809+ and Windows 11; older Windows reject the attribute and keep theirs.
        void ApplyTitleBarTheme()
        {
            if (!IsHandleCreated)
                return;

            int dark = Theme.Current.IsDark ? 1 : 0;
            try
            {
                if (NativeMethods.DwmSetWindowAttribute(Handle, NativeMethods.DWMWA_USE_IMMERSIVE_DARK_MODE, ref dark, sizeof(int)) != 0)
                    NativeMethods.DwmSetWindowAttribute(Handle, NativeMethods.DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1, ref dark, sizeof(int));

                // Redraw the frame so a change shows while the window is open. Windows 10 keeps the old caption color
                // until the activation state changes, so the caption is also redrawn inactive and back.
                NativeMethods.SetWindowPos(Handle, IntPtr.Zero, 0, 0, 0, 0,
                    NativeMethods.SWP_NOMOVE | NativeMethods.SWP_NOSIZE | NativeMethods.SWP_NOZORDER | NativeMethods.SWP_NOACTIVATE | NativeMethods.SWP_FRAMECHANGED);
                if (Visible)
                {
                    bool active = ActiveForm == this;
                    NativeMethods.SendMessage(Handle, NativeMethods.WM_NCACTIVATE, active ? IntPtr.Zero : (IntPtr)1, IntPtr.Zero);
                    NativeMethods.SendMessage(Handle, NativeMethods.WM_NCACTIVATE, active ? (IntPtr)1 : IntPtr.Zero, IntPtr.Zero);
                }
            }
            catch (Exception e) when (e is DllNotFoundException || e is EntryPointNotFoundException)
            {
                // Not running on Windows.
            }
        }

        void HelpToolTip_Popup(object sender, PopupEventArgs e)
        {
            if (!HelpToolTip.OwnerDraw)
                return;

            Size text = TextRenderer.MeasureText(HelpToolTip.GetToolTip(e.AssociatedControl), Font, Size.Empty, TextFormatFlags.NoPrefix);
            e.ToolTipSize = new Size(text.Width + 2 * TipPadding, text.Height + 2 * TipPadding);
        }

        void HelpToolTip_Draw(object sender, DrawToolTipEventArgs e)
        {
            Theme theme = Theme.Current;
            using (var brush = new SolidBrush(theme.Surface))
                e.Graphics.FillRectangle(brush, e.Bounds);
            using (var pen = new Pen(theme.Border))
                e.Graphics.DrawRectangle(pen, e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height - 1);
            TextRenderer.DrawText(e.Graphics, e.ToolTipText, Font, Rectangle.Inflate(e.Bounds, -TipPadding, -TipPadding), theme.Text,
                TextFormatFlags.Left | TextFormatFlags.Top | TextFormatFlags.NoPrefix);
        }

        bool SaveConfig()
        {
            ReadConfig(config);
            try
            {
                config.Save(configPath);
                return true;
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException || ex is SecurityException)
            {
                ShowError(language.SaveFailed, configPath, ex.Message);
                return false;
            }
        }

        void ShowError(string format, params object[] args)
        {
            MessageBox.Show(this, string.Format(format, args), language.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        static string OptionText(string[] names, int index, int value)
        {
            return index >= 0 && index < names.Length ? names[index] : value.ToString();
        }

        static int IndexOf<T>(ComboBox list, T value)
        {
            for (int i = 0; i < list.Items.Count; i++)
            {
                if (EqualityComparer<T>.Default.Equals(((Choice<T>)list.Items[i]).Value, value))
                    return i;
            }
            return -1;
        }

        static T SelectedValue<T>(ComboBox list)
        {
            return ((Choice<T>)list.SelectedItem).Value;
        }

        void ScreenModeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!updating)
                UpdateControlStates();
        }

        void ResolutionList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!updating)
                FillFrequencies(SelectedValue<int>(FrequencyList), keepUnsupported: false);
        }

        void MusicBar_ValueChanged(object sender, EventArgs e)
        {
            if (!updating)
                UpdateControlStates();
        }

        void EffectsBar_ValueChanged(object sender, EventArgs e)
        {
            if (!updating)
                UpdateControlStates();
        }

        void LanguageList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (updating)
                return;

            language = (Language)LanguageList.SelectedItem;
            language.SaveAsPreferred();

            // Rebuild the lists with the new texts, keeping what is selected.
            var current = new ClientConfig();
            ReadConfig(current);
            ApplyLanguage();
            ShowConfig(current);
        }

        void DarkModeCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (updating)
                return;

            Theme.Current = DarkModeCheck.Checked ? Theme.Dark : Theme.Light;
            Theme.Current.SaveAsPreferred();
            ApplyTheme();
        }

        void DefaultsButton_Click(object sender, EventArgs e)
        {
            ShowConfig(new ClientConfig());
        }

        void SaveAndPlayButton_Click(object sender, EventArgs e)
        {
            if (!SaveConfig())
                return;

            try
            {
                Process.Start(new ProcessStartInfo(gamePath)
                {
                    WorkingDirectory = Path.GetDirectoryName(gamePath),
                    UseShellExecute = true, // shows the UAC prompt if the client requires administrator rights
                })?.Dispose();
                Close();
            }
            catch (Win32Exception ex)
            {
                ShowError(language.LaunchFailed, gamePath, ex.Message);
            }
        }

        void SaveButton_Click(object sender, EventArgs e)
        {
            if (SaveConfig())
                Close();
        }

        void ExitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        sealed class Choice<T>
        {
            public Choice(T value, string text)
            {
                Value = value;
                Text = text;
            }

            public T Value { get; }
            public string Text { get; }

            public override string ToString() => Text;
        }
    }
}
