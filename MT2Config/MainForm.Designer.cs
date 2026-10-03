namespace MT2Config
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.RootLayout = new System.Windows.Forms.TableLayoutPanel();
            this.DisplayBox = new System.Windows.Forms.GroupBox();
            this.DisplayLayout = new System.Windows.Forms.TableLayoutPanel();
            this.ScreenModeLabel = new System.Windows.Forms.Label();
            this.ScreenModeList = new System.Windows.Forms.ComboBox();
            this.ResolutionLabel = new System.Windows.Forms.Label();
            this.ResolutionList = new System.Windows.Forms.ComboBox();
            this.FrequencyLabel = new System.Windows.Forms.Label();
            this.FrequencyList = new System.Windows.Forms.ComboBox();
            this.GammaLabel = new System.Windows.Forms.Label();
            this.GammaList = new System.Windows.Forms.ComboBox();
            this.GraphicsBox = new System.Windows.Forms.GroupBox();
            this.GraphicsLayout = new System.Windows.Forms.TableLayoutPanel();
            this.VisibilityLabel = new System.Windows.Forms.Label();
            this.VisibilityList = new System.Windows.Forms.ComboBox();
            this.ShadowLabel = new System.Windows.Forms.Label();
            this.ShadowList = new System.Windows.Forms.ComboBox();
            this.TilingLabel = new System.Windows.Forms.Label();
            this.TilingList = new System.Windows.Forms.ComboBox();
            this.ObjectCullingCheck = new System.Windows.Forms.CheckBox();
            this.SoftwareCursorCheck = new System.Windows.Forms.CheckBox();
            this.DecompressedTextureCheck = new System.Windows.Forms.CheckBox();
            this.SoundBox = new System.Windows.Forms.GroupBox();
            this.SoundLayout = new System.Windows.Forms.TableLayoutPanel();
            this.MusicLabel = new System.Windows.Forms.Label();
            this.MusicBar = new System.Windows.Forms.TrackBar();
            this.MusicValueLabel = new System.Windows.Forms.Label();
            this.EffectsLabel = new System.Windows.Forms.Label();
            this.EffectsBar = new System.Windows.Forms.TrackBar();
            this.EffectsValueLabel = new System.Windows.Forms.Label();
            this.InterfaceBox = new System.Windows.Forms.GroupBox();
            this.InterfaceLayout = new System.Windows.Forms.TableLayoutPanel();
            this.ViewChatCheck = new System.Windows.Forms.CheckBox();
            this.AlwaysShowNameCheck = new System.Windows.Forms.CheckBox();
            this.ShowDamageCheck = new System.Windows.Forms.CheckBox();
            this.ShowSalesTextCheck = new System.Windows.Forms.CheckBox();
            this.DefaultImeCheck = new System.Windows.Forms.CheckBox();
            this.ButtonLayout = new System.Windows.Forms.TableLayoutPanel();
            this.LanguageLabel = new System.Windows.Forms.Label();
            this.LanguageList = new System.Windows.Forms.ComboBox();
            this.DefaultsButton = new System.Windows.Forms.Button();
            this.SaveAndPlayButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.ExitButton = new System.Windows.Forms.Button();
            this.RootLayout.SuspendLayout();
            this.DisplayBox.SuspendLayout();
            this.DisplayLayout.SuspendLayout();
            this.GraphicsBox.SuspendLayout();
            this.GraphicsLayout.SuspendLayout();
            this.SoundBox.SuspendLayout();
            this.SoundLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MusicBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EffectsBar)).BeginInit();
            this.InterfaceBox.SuspendLayout();
            this.InterfaceLayout.SuspendLayout();
            this.ButtonLayout.SuspendLayout();
            this.SuspendLayout();
            //
            // RootLayout
            //
            this.RootLayout.AutoSize = true;
            this.RootLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.RootLayout.ColumnCount = 2;
            this.RootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.RootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.RootLayout.Controls.Add(this.DisplayBox, 0, 0);
            this.RootLayout.Controls.Add(this.GraphicsBox, 1, 0);
            this.RootLayout.Controls.Add(this.SoundBox, 0, 1);
            this.RootLayout.Controls.Add(this.InterfaceBox, 1, 1);
            this.RootLayout.Controls.Add(this.ButtonLayout, 0, 2);
            this.RootLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RootLayout.Location = new System.Drawing.Point(9, 9);
            this.RootLayout.Name = "RootLayout";
            this.RootLayout.RowCount = 3;
            this.RootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.RootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.RootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.RootLayout.Size = new System.Drawing.Size(626, 360);
            this.RootLayout.TabIndex = 0;
            this.RootLayout.SetColumnSpan(this.ButtonLayout, 2);
            //
            // DisplayBox
            //
            this.DisplayBox.AutoSize = true;
            this.DisplayBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.DisplayBox.Controls.Add(this.DisplayLayout);
            this.DisplayBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DisplayBox.Location = new System.Drawing.Point(3, 3);
            this.DisplayBox.Name = "DisplayBox";
            this.DisplayBox.Padding = new System.Windows.Forms.Padding(6, 3, 6, 6);
            this.DisplayBox.Size = new System.Drawing.Size(307, 169);
            this.DisplayBox.TabIndex = 0;
            this.DisplayBox.TabStop = false;
            this.DisplayBox.Text = "Display";
            //
            // DisplayLayout
            //
            this.DisplayLayout.AutoSize = true;
            this.DisplayLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.DisplayLayout.ColumnCount = 2;
            this.DisplayLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.DisplayLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.DisplayLayout.Controls.Add(this.ScreenModeLabel, 0, 0);
            this.DisplayLayout.Controls.Add(this.ScreenModeList, 1, 0);
            this.DisplayLayout.Controls.Add(this.ResolutionLabel, 0, 1);
            this.DisplayLayout.Controls.Add(this.ResolutionList, 1, 1);
            this.DisplayLayout.Controls.Add(this.FrequencyLabel, 0, 2);
            this.DisplayLayout.Controls.Add(this.FrequencyList, 1, 2);
            this.DisplayLayout.Controls.Add(this.GammaLabel, 0, 3);
            this.DisplayLayout.Controls.Add(this.GammaList, 1, 3);
            this.DisplayLayout.Location = new System.Drawing.Point(6, 19);
            this.DisplayLayout.Name = "DisplayLayout";
            this.DisplayLayout.RowCount = 4;
            this.DisplayLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.DisplayLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.DisplayLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.DisplayLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.DisplayLayout.Size = new System.Drawing.Size(295, 144);
            this.DisplayLayout.TabIndex = 0;
            //
            // ScreenModeLabel
            //
            this.ScreenModeLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ScreenModeLabel.AutoSize = true;
            this.ScreenModeLabel.Location = new System.Drawing.Point(3, 7);
            this.ScreenModeLabel.Name = "ScreenModeLabel";
            this.ScreenModeLabel.Size = new System.Drawing.Size(73, 15);
            this.ScreenModeLabel.TabIndex = 0;
            this.ScreenModeLabel.Text = "Screen mode";
            //
            // ScreenModeList
            //
            this.ScreenModeList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ScreenModeList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ScreenModeList.FormattingEnabled = true;
            this.ScreenModeList.Location = new System.Drawing.Point(97, 3);
            this.ScreenModeList.Name = "ScreenModeList";
            this.ScreenModeList.Size = new System.Drawing.Size(180, 23);
            this.ScreenModeList.TabIndex = 1;
            this.ScreenModeList.SelectedIndexChanged += new System.EventHandler(this.ScreenModeList_SelectedIndexChanged);
            //
            // ResolutionLabel
            //
            this.ResolutionLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ResolutionLabel.AutoSize = true;
            this.ResolutionLabel.Location = new System.Drawing.Point(3, 36);
            this.ResolutionLabel.Name = "ResolutionLabel";
            this.ResolutionLabel.Size = new System.Drawing.Size(63, 15);
            this.ResolutionLabel.TabIndex = 2;
            this.ResolutionLabel.Text = "Resolution";
            //
            // ResolutionList
            //
            this.ResolutionList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ResolutionList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ResolutionList.FormattingEnabled = true;
            this.ResolutionList.Location = new System.Drawing.Point(97, 32);
            this.ResolutionList.Name = "ResolutionList";
            this.ResolutionList.Size = new System.Drawing.Size(180, 23);
            this.ResolutionList.TabIndex = 3;
            this.ResolutionList.SelectedIndexChanged += new System.EventHandler(this.ResolutionList_SelectedIndexChanged);
            //
            // FrequencyLabel
            //
            this.FrequencyLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.FrequencyLabel.AutoSize = true;
            this.FrequencyLabel.Location = new System.Drawing.Point(3, 65);
            this.FrequencyLabel.Name = "FrequencyLabel";
            this.FrequencyLabel.Size = new System.Drawing.Size(70, 15);
            this.FrequencyLabel.TabIndex = 4;
            this.FrequencyLabel.Text = "Refresh rate";
            //
            // FrequencyList
            //
            this.FrequencyList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FrequencyList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FrequencyList.FormattingEnabled = true;
            this.FrequencyList.Location = new System.Drawing.Point(97, 61);
            this.FrequencyList.Name = "FrequencyList";
            this.FrequencyList.Size = new System.Drawing.Size(180, 23);
            this.FrequencyList.TabIndex = 5;
            //
            // GammaLabel
            //
            this.GammaLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.GammaLabel.AutoSize = true;
            this.GammaLabel.Location = new System.Drawing.Point(3, 94);
            this.GammaLabel.Name = "GammaLabel";
            this.GammaLabel.Size = new System.Drawing.Size(46, 15);
            this.GammaLabel.TabIndex = 6;
            this.GammaLabel.Text = "Gamma";
            //
            // GammaList
            //
            this.GammaList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.GammaList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GammaList.FormattingEnabled = true;
            this.GammaList.Location = new System.Drawing.Point(97, 90);
            this.GammaList.Name = "GammaList";
            this.GammaList.Size = new System.Drawing.Size(180, 23);
            this.GammaList.TabIndex = 7;
            //
            // GraphicsBox
            //
            this.GraphicsBox.AutoSize = true;
            this.GraphicsBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.GraphicsBox.Controls.Add(this.GraphicsLayout);
            this.GraphicsBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GraphicsBox.Location = new System.Drawing.Point(316, 3);
            this.GraphicsBox.Name = "GraphicsBox";
            this.GraphicsBox.Padding = new System.Windows.Forms.Padding(6, 3, 6, 6);
            this.GraphicsBox.Size = new System.Drawing.Size(307, 169);
            this.GraphicsBox.TabIndex = 1;
            this.GraphicsBox.TabStop = false;
            this.GraphicsBox.Text = "Graphics";
            //
            // GraphicsLayout
            //
            this.GraphicsLayout.AutoSize = true;
            this.GraphicsLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.GraphicsLayout.ColumnCount = 2;
            this.GraphicsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.GraphicsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.GraphicsLayout.Controls.Add(this.VisibilityLabel, 0, 0);
            this.GraphicsLayout.Controls.Add(this.VisibilityList, 1, 0);
            this.GraphicsLayout.Controls.Add(this.ShadowLabel, 0, 1);
            this.GraphicsLayout.Controls.Add(this.ShadowList, 1, 1);
            this.GraphicsLayout.Controls.Add(this.TilingLabel, 0, 2);
            this.GraphicsLayout.Controls.Add(this.TilingList, 1, 2);
            this.GraphicsLayout.Controls.Add(this.ObjectCullingCheck, 0, 3);
            this.GraphicsLayout.Controls.Add(this.SoftwareCursorCheck, 0, 4);
            this.GraphicsLayout.Controls.Add(this.DecompressedTextureCheck, 0, 5);
            this.GraphicsLayout.Location = new System.Drawing.Point(6, 19);
            this.GraphicsLayout.Name = "GraphicsLayout";
            this.GraphicsLayout.RowCount = 6;
            this.GraphicsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.GraphicsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.GraphicsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.GraphicsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.GraphicsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.GraphicsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.GraphicsLayout.Size = new System.Drawing.Size(295, 144);
            this.GraphicsLayout.TabIndex = 0;
            this.GraphicsLayout.SetColumnSpan(this.ObjectCullingCheck, 2);
            this.GraphicsLayout.SetColumnSpan(this.SoftwareCursorCheck, 2);
            this.GraphicsLayout.SetColumnSpan(this.DecompressedTextureCheck, 2);
            //
            // VisibilityLabel
            //
            this.VisibilityLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.VisibilityLabel.AutoSize = true;
            this.VisibilityLabel.Location = new System.Drawing.Point(3, 7);
            this.VisibilityLabel.Name = "VisibilityLabel";
            this.VisibilityLabel.Size = new System.Drawing.Size(79, 15);
            this.VisibilityLabel.TabIndex = 0;
            this.VisibilityLabel.Text = "View distance";
            //
            // VisibilityList
            //
            this.VisibilityList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.VisibilityList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.VisibilityList.FormattingEnabled = true;
            this.VisibilityList.Location = new System.Drawing.Point(97, 3);
            this.VisibilityList.Name = "VisibilityList";
            this.VisibilityList.Size = new System.Drawing.Size(180, 23);
            this.VisibilityList.TabIndex = 1;
            //
            // ShadowLabel
            //
            this.ShadowLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ShadowLabel.AutoSize = true;
            this.ShadowLabel.Location = new System.Drawing.Point(3, 36);
            this.ShadowLabel.Name = "ShadowLabel";
            this.ShadowLabel.Size = new System.Drawing.Size(54, 15);
            this.ShadowLabel.TabIndex = 2;
            this.ShadowLabel.Text = "Shadows";
            //
            // ShadowList
            //
            this.ShadowList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ShadowList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ShadowList.FormattingEnabled = true;
            this.ShadowList.Location = new System.Drawing.Point(97, 32);
            this.ShadowList.Name = "ShadowList";
            this.ShadowList.Size = new System.Drawing.Size(180, 23);
            this.ShadowList.TabIndex = 3;
            //
            // TilingLabel
            //
            this.TilingLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.TilingLabel.AutoSize = true;
            this.TilingLabel.Location = new System.Drawing.Point(3, 65);
            this.TilingLabel.Name = "TilingLabel";
            this.TilingLabel.Size = new System.Drawing.Size(76, 15);
            this.TilingLabel.TabIndex = 4;
            this.TilingLabel.Text = "Terrain tiling";
            //
            // TilingList
            //
            this.TilingList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.TilingList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TilingList.FormattingEnabled = true;
            this.TilingList.Location = new System.Drawing.Point(97, 61);
            this.TilingList.Name = "TilingList";
            this.TilingList.Size = new System.Drawing.Size(180, 23);
            this.TilingList.TabIndex = 5;
            //
            // ObjectCullingCheck
            //
            this.ObjectCullingCheck.AutoSize = true;
            this.ObjectCullingCheck.Location = new System.Drawing.Point(3, 90);
            this.ObjectCullingCheck.Name = "ObjectCullingCheck";
            this.ObjectCullingCheck.Size = new System.Drawing.Size(103, 19);
            this.ObjectCullingCheck.TabIndex = 6;
            this.ObjectCullingCheck.Text = "Object culling";
            this.ObjectCullingCheck.UseVisualStyleBackColor = true;
            //
            // SoftwareCursorCheck
            //
            this.SoftwareCursorCheck.AutoSize = true;
            this.SoftwareCursorCheck.Location = new System.Drawing.Point(3, 115);
            this.SoftwareCursorCheck.Name = "SoftwareCursorCheck";
            this.SoftwareCursorCheck.Size = new System.Drawing.Size(111, 19);
            this.SoftwareCursorCheck.TabIndex = 7;
            this.SoftwareCursorCheck.Text = "Software cursor";
            this.SoftwareCursorCheck.UseVisualStyleBackColor = true;
            //
            // DecompressedTextureCheck
            //
            this.DecompressedTextureCheck.AutoSize = true;
            this.DecompressedTextureCheck.Location = new System.Drawing.Point(3, 140);
            this.DecompressedTextureCheck.Name = "DecompressedTextureCheck";
            this.DecompressedTextureCheck.Size = new System.Drawing.Size(150, 19);
            this.DecompressedTextureCheck.TabIndex = 8;
            this.DecompressedTextureCheck.Text = "Uncompressed textures";
            this.DecompressedTextureCheck.UseVisualStyleBackColor = true;
            //
            // SoundBox
            //
            this.SoundBox.AutoSize = true;
            this.SoundBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.SoundBox.Controls.Add(this.SoundLayout);
            this.SoundBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SoundBox.Location = new System.Drawing.Point(3, 178);
            this.SoundBox.Name = "SoundBox";
            this.SoundBox.Padding = new System.Windows.Forms.Padding(6, 3, 6, 6);
            this.SoundBox.Size = new System.Drawing.Size(307, 140);
            this.SoundBox.TabIndex = 2;
            this.SoundBox.TabStop = false;
            this.SoundBox.Text = "Sound";
            //
            // SoundLayout
            //
            this.SoundLayout.AutoSize = true;
            this.SoundLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.SoundLayout.ColumnCount = 3;
            this.SoundLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.SoundLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.SoundLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.SoundLayout.Controls.Add(this.MusicLabel, 0, 0);
            this.SoundLayout.Controls.Add(this.MusicBar, 1, 0);
            this.SoundLayout.Controls.Add(this.MusicValueLabel, 2, 0);
            this.SoundLayout.Controls.Add(this.EffectsLabel, 0, 1);
            this.SoundLayout.Controls.Add(this.EffectsBar, 1, 1);
            this.SoundLayout.Controls.Add(this.EffectsValueLabel, 2, 1);
            this.SoundLayout.Location = new System.Drawing.Point(6, 19);
            this.SoundLayout.Name = "SoundLayout";
            this.SoundLayout.RowCount = 2;
            this.SoundLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.SoundLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.SoundLayout.Size = new System.Drawing.Size(295, 115);
            this.SoundLayout.TabIndex = 0;
            //
            // MusicLabel
            //
            this.MusicLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.MusicLabel.AutoSize = true;
            this.MusicLabel.Location = new System.Drawing.Point(3, 15);
            this.MusicLabel.Name = "MusicLabel";
            this.MusicLabel.Size = new System.Drawing.Size(39, 15);
            this.MusicLabel.TabIndex = 0;
            this.MusicLabel.Text = "Music";
            //
            // MusicBar
            //
            this.MusicBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.MusicBar.LargeChange = 10;
            this.MusicBar.Location = new System.Drawing.Point(52, 3);
            this.MusicBar.Maximum = 100;
            this.MusicBar.Name = "MusicBar";
            this.MusicBar.Size = new System.Drawing.Size(180, 45);
            this.MusicBar.TabIndex = 1;
            this.MusicBar.TickFrequency = 10;
            this.MusicBar.ValueChanged += new System.EventHandler(this.MusicBar_ValueChanged);
            //
            // MusicValueLabel
            //
            this.MusicValueLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.MusicValueLabel.AutoSize = true;
            this.MusicValueLabel.Location = new System.Drawing.Point(255, 15);
            this.MusicValueLabel.MinimumSize = new System.Drawing.Size(37, 0);
            this.MusicValueLabel.Name = "MusicValueLabel";
            this.MusicValueLabel.Size = new System.Drawing.Size(37, 15);
            this.MusicValueLabel.TabIndex = 2;
            this.MusicValueLabel.Text = "100%";
            this.MusicValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // EffectsLabel
            //
            this.EffectsLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.EffectsLabel.AutoSize = true;
            this.EffectsLabel.Location = new System.Drawing.Point(3, 66);
            this.EffectsLabel.Name = "EffectsLabel";
            this.EffectsLabel.Size = new System.Drawing.Size(43, 15);
            this.EffectsLabel.TabIndex = 3;
            this.EffectsLabel.Text = "Effects";
            //
            // EffectsBar
            //
            this.EffectsBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.EffectsBar.LargeChange = 1;
            this.EffectsBar.Location = new System.Drawing.Point(52, 54);
            this.EffectsBar.Maximum = 5;
            this.EffectsBar.Name = "EffectsBar";
            this.EffectsBar.Size = new System.Drawing.Size(180, 45);
            this.EffectsBar.TabIndex = 4;
            this.EffectsBar.ValueChanged += new System.EventHandler(this.EffectsBar_ValueChanged);
            //
            // EffectsValueLabel
            //
            this.EffectsValueLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.EffectsValueLabel.AutoSize = true;
            this.EffectsValueLabel.Location = new System.Drawing.Point(255, 66);
            this.EffectsValueLabel.MinimumSize = new System.Drawing.Size(37, 0);
            this.EffectsValueLabel.Name = "EffectsValueLabel";
            this.EffectsValueLabel.Size = new System.Drawing.Size(37, 15);
            this.EffectsValueLabel.TabIndex = 5;
            this.EffectsValueLabel.Text = "100%";
            this.EffectsValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // InterfaceBox
            //
            this.InterfaceBox.AutoSize = true;
            this.InterfaceBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.InterfaceBox.Controls.Add(this.InterfaceLayout);
            this.InterfaceBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InterfaceBox.Location = new System.Drawing.Point(316, 178);
            this.InterfaceBox.Name = "InterfaceBox";
            this.InterfaceBox.Padding = new System.Windows.Forms.Padding(6, 3, 6, 6);
            this.InterfaceBox.Size = new System.Drawing.Size(307, 140);
            this.InterfaceBox.TabIndex = 3;
            this.InterfaceBox.TabStop = false;
            this.InterfaceBox.Text = "Interface";
            //
            // InterfaceLayout
            //
            this.InterfaceLayout.AutoSize = true;
            this.InterfaceLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.InterfaceLayout.ColumnCount = 1;
            this.InterfaceLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.InterfaceLayout.Controls.Add(this.ViewChatCheck, 0, 0);
            this.InterfaceLayout.Controls.Add(this.AlwaysShowNameCheck, 0, 1);
            this.InterfaceLayout.Controls.Add(this.ShowDamageCheck, 0, 2);
            this.InterfaceLayout.Controls.Add(this.ShowSalesTextCheck, 0, 3);
            this.InterfaceLayout.Controls.Add(this.DefaultImeCheck, 0, 4);
            this.InterfaceLayout.Location = new System.Drawing.Point(6, 19);
            this.InterfaceLayout.Name = "InterfaceLayout";
            this.InterfaceLayout.RowCount = 5;
            this.InterfaceLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.InterfaceLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.InterfaceLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.InterfaceLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.InterfaceLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.InterfaceLayout.Size = new System.Drawing.Size(295, 115);
            this.InterfaceLayout.TabIndex = 0;
            //
            // ViewChatCheck
            //
            this.ViewChatCheck.AutoSize = true;
            this.ViewChatCheck.Location = new System.Drawing.Point(3, 3);
            this.ViewChatCheck.Name = "ViewChatCheck";
            this.ViewChatCheck.Size = new System.Drawing.Size(80, 19);
            this.ViewChatCheck.TabIndex = 0;
            this.ViewChatCheck.Text = "Show chat";
            this.ViewChatCheck.UseVisualStyleBackColor = true;
            //
            // AlwaysShowNameCheck
            //
            this.AlwaysShowNameCheck.AutoSize = true;
            this.AlwaysShowNameCheck.Location = new System.Drawing.Point(3, 28);
            this.AlwaysShowNameCheck.Name = "AlwaysShowNameCheck";
            this.AlwaysShowNameCheck.Size = new System.Drawing.Size(129, 19);
            this.AlwaysShowNameCheck.TabIndex = 1;
            this.AlwaysShowNameCheck.Text = "Always show names";
            this.AlwaysShowNameCheck.UseVisualStyleBackColor = true;
            //
            // ShowDamageCheck
            //
            this.ShowDamageCheck.AutoSize = true;
            this.ShowDamageCheck.Location = new System.Drawing.Point(3, 53);
            this.ShowDamageCheck.Name = "ShowDamageCheck";
            this.ShowDamageCheck.Size = new System.Drawing.Size(98, 19);
            this.ShowDamageCheck.TabIndex = 2;
            this.ShowDamageCheck.Text = "Show damage";
            this.ShowDamageCheck.UseVisualStyleBackColor = true;
            //
            // ShowSalesTextCheck
            //
            this.ShowSalesTextCheck.AutoSize = true;
            this.ShowSalesTextCheck.Location = new System.Drawing.Point(3, 78);
            this.ShowSalesTextCheck.Name = "ShowSalesTextCheck";
            this.ShowSalesTextCheck.Size = new System.Drawing.Size(113, 19);
            this.ShowSalesTextCheck.TabIndex = 3;
            this.ShowSalesTextCheck.Text = "Show shop titles";
            this.ShowSalesTextCheck.UseVisualStyleBackColor = true;
            //
            // DefaultImeCheck
            //
            this.DefaultImeCheck.AutoSize = true;
            this.DefaultImeCheck.Location = new System.Drawing.Point(3, 103);
            this.DefaultImeCheck.Name = "DefaultImeCheck";
            this.DefaultImeCheck.Size = new System.Drawing.Size(116, 19);
            this.DefaultImeCheck.TabIndex = 4;
            this.DefaultImeCheck.Text = "Use Windows IME";
            this.DefaultImeCheck.UseVisualStyleBackColor = true;
            //
            // ButtonLayout
            //
            this.ButtonLayout.AutoSize = true;
            this.ButtonLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ButtonLayout.ColumnCount = 7;
            this.ButtonLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.ButtonLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.ButtonLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ButtonLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.ButtonLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.ButtonLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.ButtonLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.ButtonLayout.Controls.Add(this.LanguageLabel, 0, 0);
            this.ButtonLayout.Controls.Add(this.LanguageList, 1, 0);
            this.ButtonLayout.Controls.Add(this.DefaultsButton, 3, 0);
            this.ButtonLayout.Controls.Add(this.SaveAndPlayButton, 4, 0);
            this.ButtonLayout.Controls.Add(this.SaveButton, 5, 0);
            this.ButtonLayout.Controls.Add(this.ExitButton, 6, 0);
            this.ButtonLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ButtonLayout.Location = new System.Drawing.Point(0, 327);
            this.ButtonLayout.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.ButtonLayout.Name = "ButtonLayout";
            this.ButtonLayout.RowCount = 1;
            this.ButtonLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.ButtonLayout.Size = new System.Drawing.Size(626, 33);
            this.ButtonLayout.TabIndex = 4;
            //
            // LanguageLabel
            //
            this.LanguageLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LanguageLabel.AutoSize = true;
            this.LanguageLabel.Location = new System.Drawing.Point(3, 9);
            this.LanguageLabel.Name = "LanguageLabel";
            this.LanguageLabel.Size = new System.Drawing.Size(59, 15);
            this.LanguageLabel.TabIndex = 0;
            this.LanguageLabel.Text = "Language";
            //
            // LanguageList
            //
            this.LanguageList.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LanguageList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LanguageList.FormattingEnabled = true;
            this.LanguageList.Location = new System.Drawing.Point(68, 5);
            this.LanguageList.Name = "LanguageList";
            this.LanguageList.Size = new System.Drawing.Size(100, 23);
            this.LanguageList.TabIndex = 1;
            this.LanguageList.SelectedIndexChanged += new System.EventHandler(this.LanguageList_SelectedIndexChanged);
            //
            // DefaultsButton
            //
            this.DefaultsButton.AutoSize = true;
            this.DefaultsButton.Location = new System.Drawing.Point(239, 3);
            this.DefaultsButton.MinimumSize = new System.Drawing.Size(90, 27);
            this.DefaultsButton.Name = "DefaultsButton";
            this.DefaultsButton.Size = new System.Drawing.Size(90, 27);
            this.DefaultsButton.TabIndex = 2;
            this.DefaultsButton.Text = "Defaults";
            this.DefaultsButton.UseVisualStyleBackColor = true;
            this.DefaultsButton.Click += new System.EventHandler(this.DefaultsButton_Click);
            //
            // SaveAndPlayButton
            //
            this.SaveAndPlayButton.AutoSize = true;
            this.SaveAndPlayButton.Location = new System.Drawing.Point(335, 3);
            this.SaveAndPlayButton.MinimumSize = new System.Drawing.Size(90, 27);
            this.SaveAndPlayButton.Name = "SaveAndPlayButton";
            this.SaveAndPlayButton.Size = new System.Drawing.Size(96, 27);
            this.SaveAndPlayButton.TabIndex = 3;
            this.SaveAndPlayButton.Text = "Save and Play";
            this.SaveAndPlayButton.UseVisualStyleBackColor = true;
            this.SaveAndPlayButton.Click += new System.EventHandler(this.SaveAndPlayButton_Click);
            //
            // SaveButton
            //
            this.SaveButton.AutoSize = true;
            this.SaveButton.Location = new System.Drawing.Point(437, 3);
            this.SaveButton.MinimumSize = new System.Drawing.Size(90, 27);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(90, 27);
            this.SaveButton.TabIndex = 4;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            //
            // ExitButton
            //
            this.ExitButton.AutoSize = true;
            this.ExitButton.Location = new System.Drawing.Point(533, 3);
            this.ExitButton.MinimumSize = new System.Drawing.Size(90, 27);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(90, 27);
            this.ExitButton.TabIndex = 5;
            this.ExitButton.Text = "Cancel";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            //
            // MainForm
            //
            this.AcceptButton = this.SaveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.ExitButton;
            this.ClientSize = new System.Drawing.Size(644, 378);
            this.Controls.Add(this.RootLayout);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Metin2 - Settings";
            this.RootLayout.ResumeLayout(false);
            this.RootLayout.PerformLayout();
            this.DisplayBox.ResumeLayout(false);
            this.DisplayBox.PerformLayout();
            this.DisplayLayout.ResumeLayout(false);
            this.DisplayLayout.PerformLayout();
            this.GraphicsBox.ResumeLayout(false);
            this.GraphicsBox.PerformLayout();
            this.GraphicsLayout.ResumeLayout(false);
            this.GraphicsLayout.PerformLayout();
            this.SoundBox.ResumeLayout(false);
            this.SoundBox.PerformLayout();
            this.SoundLayout.ResumeLayout(false);
            this.SoundLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MusicBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EffectsBar)).EndInit();
            this.InterfaceBox.ResumeLayout(false);
            this.InterfaceBox.PerformLayout();
            this.InterfaceLayout.ResumeLayout(false);
            this.InterfaceLayout.PerformLayout();
            this.ButtonLayout.ResumeLayout(false);
            this.ButtonLayout.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel RootLayout;
        private System.Windows.Forms.GroupBox DisplayBox;
        private System.Windows.Forms.TableLayoutPanel DisplayLayout;
        private System.Windows.Forms.Label ScreenModeLabel;
        private System.Windows.Forms.ComboBox ScreenModeList;
        private System.Windows.Forms.Label ResolutionLabel;
        private System.Windows.Forms.ComboBox ResolutionList;
        private System.Windows.Forms.Label FrequencyLabel;
        private System.Windows.Forms.ComboBox FrequencyList;
        private System.Windows.Forms.Label GammaLabel;
        private System.Windows.Forms.ComboBox GammaList;
        private System.Windows.Forms.GroupBox GraphicsBox;
        private System.Windows.Forms.TableLayoutPanel GraphicsLayout;
        private System.Windows.Forms.Label VisibilityLabel;
        private System.Windows.Forms.ComboBox VisibilityList;
        private System.Windows.Forms.Label ShadowLabel;
        private System.Windows.Forms.ComboBox ShadowList;
        private System.Windows.Forms.Label TilingLabel;
        private System.Windows.Forms.ComboBox TilingList;
        private System.Windows.Forms.CheckBox ObjectCullingCheck;
        private System.Windows.Forms.CheckBox SoftwareCursorCheck;
        private System.Windows.Forms.CheckBox DecompressedTextureCheck;
        private System.Windows.Forms.GroupBox SoundBox;
        private System.Windows.Forms.TableLayoutPanel SoundLayout;
        private System.Windows.Forms.Label MusicLabel;
        private System.Windows.Forms.TrackBar MusicBar;
        private System.Windows.Forms.Label MusicValueLabel;
        private System.Windows.Forms.Label EffectsLabel;
        private System.Windows.Forms.TrackBar EffectsBar;
        private System.Windows.Forms.Label EffectsValueLabel;
        private System.Windows.Forms.GroupBox InterfaceBox;
        private System.Windows.Forms.TableLayoutPanel InterfaceLayout;
        private System.Windows.Forms.CheckBox ViewChatCheck;
        private System.Windows.Forms.CheckBox AlwaysShowNameCheck;
        private System.Windows.Forms.CheckBox ShowDamageCheck;
        private System.Windows.Forms.CheckBox ShowSalesTextCheck;
        private System.Windows.Forms.CheckBox DefaultImeCheck;
        private System.Windows.Forms.TableLayoutPanel ButtonLayout;
        private System.Windows.Forms.Label LanguageLabel;
        private System.Windows.Forms.ComboBox LanguageList;
        private System.Windows.Forms.Button DefaultsButton;
        private System.Windows.Forms.Button SaveAndPlayButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button ExitButton;
    }
}
