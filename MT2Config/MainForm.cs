///////////////////////////////////////////////////////////////
//FileName: MainForm.cs (config.cs in the original version)
//FileType: Visual C# Source file
//Author : Takuma <work.takuma@gmail.com>
//Copy Rights : Takuma
//Description : Basic c# config.exe : metin2
//Modified : 2026 MT2Dev - client compatible metin2.cfg handling,
//           monitor display modes, Turkish/English UI
///////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Windows.Forms;

namespace MT2Config
{
    public partial class MainForm : Form
    {
        static readonly int[] VisibilityValues = { 1, 2, 3 };
        static readonly int[] ShadowValues = { 0, 1, 2, 3, 4, 5 };
        static readonly int[] TilingValues = { 0, 1, 2 };
        static readonly int[] GammaValues = { 0, 1, 2, 3, 4, 5 };

        // The client opens "metin2.cfg" from its working directory, which is the client folder config.exe lives in.
        readonly string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ClientConfig.FileName);
        readonly string gamePath = GameClient.FindExecutable(AppDomain.CurrentDomain.BaseDirectory);
        readonly DisplayModes displayModes = DisplayModes.Query();

        ClientConfig config = new ClientConfig();
        Language language = Language.LoadPreferred();
        bool updating; // set while the controls are filled from code, the change handlers ignore those changes

        public MainForm()
        {
            InitializeComponent();

            using (Stream icon = typeof(MainForm).Assembly.GetManifestResourceStream("MT2Config.app.ico"))
                Icon = new Icon(icon);

            SaveAndPlayButton.Visible = gamePath != null;
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
            LanguageList.SelectedItem = language;
            updating = false;

            ApplyLanguage();
            ShowConfig(config);
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
            DefaultsButton.Text = language.Defaults;
            SaveAndPlayButton.Text = language.SaveAndPlay;
            SaveButton.Text = language.Save;
            ExitButton.Text = language.Cancel;
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

        void ReadConfig(ClientConfig c)
        {
            Resolution resolution = SelectedValue<Resolution>(ResolutionList);
            c.Width = resolution.Width;
            c.Height = resolution.Height;
            c.Frequency = SelectedValue<int>(FrequencyList);
            c.Windowed = SelectedValue<bool>(ScreenModeList);
            c.Gamma = SelectedValue<int>(GammaList);
            c.Visibility = SelectedValue<int>(VisibilityList);
            c.ShadowLevel = SelectedValue<int>(ShadowList);
            c.SoftwareTiling = SelectedValue<int>(TilingList);

            c.ObjectCulling = ObjectCullingCheck.Checked;
            c.SoftwareCursor = SoftwareCursorCheck.Checked;
            c.DecompressedTexture = DecompressedTextureCheck.Checked;
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
            List<int> supported = displayModes.FrequenciesOf(SelectedValue<Resolution>(ResolutionList)).ToList();

            // After a resolution change: keep the rate if the new resolution has it, otherwise 60 Hz, otherwise the highest.
            if (!keepUnsupported && supported.Count > 0 && !supported.Contains(frequency))
                frequency = supported.Contains(60) ? 60 : supported.Max();

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
            // The refresh rate is only used in fullscreen mode.
            bool fullscreen = !SelectedValue<bool>(ScreenModeList);
            FrequencyLabel.Enabled = fullscreen;
            FrequencyList.Enabled = fullscreen;

            MusicValueLabel.Text = MusicBar.Value + "%";
            EffectsValueLabel.Text = EffectsBar.Value * 100 / EffectsBar.Maximum + "%";
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
