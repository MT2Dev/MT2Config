using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace MT2Config
{
    // Standard controls that draw themselves in the dark theme. In the light theme they are untouched native controls.

    /// <summary>Drop-down list. Windows draws its closed state with the visual style and ignores BackColor.</summary>
    internal class ThemedComboBox : ComboBox
    {
        // Mono (also Wine without the .NET Framework) draws its controls itself and does not pass a device context in
        // WM_PAINT, so its own rendering is kept there.
        static readonly bool IsMono = Type.GetType("Mono.Runtime") != null;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == NativeMethods.WM_PAINT && Theme.Current.IsDark && !IsMono && PaintDark(m.WParam))
                return;

            base.WndProc(ref m);

            // WinForms skips MouseLeave when the opened list has taken the mouse; repaint the hover state anyway.
            if (m.Msg == NativeMethods.WM_MOUSELEAVE)
                Invalidate();
        }

        // Handles the whole WM_PAINT, so the native light face never flashes through. The opened list uses BackColor.
        bool PaintDark(IntPtr messageDc)
        {
            var paint = new NativeMethods.PAINTSTRUCT();
            IntPtr dc = messageDc;
            try
            {
                if (dc == IntPtr.Zero)
                    dc = NativeMethods.BeginPaint(Handle, out paint);
            }
            catch (Exception e) when (e is DllNotFoundException || e is EntryPointNotFoundException)
            {
                return false; // not running on Windows
            }

            try
            {
                using (Graphics target = Graphics.FromHdc(dc))
                using (BufferedGraphics buffer = BufferedGraphicsManager.Current.Allocate(target, ClientRectangle))
                {
                    Draw(buffer.Graphics);
                    buffer.Render(target);
                }
            }
            finally
            {
                if (messageDc == IntPtr.Zero)
                    NativeMethods.EndPaint(Handle, ref paint);
            }
            return true;
        }

        void Draw(Graphics g)
        {
            Theme theme = Theme.Current;
            Rectangle bounds = ClientRectangle;
            // From the cursor rather than MouseEnter/MouseLeave, which are not reliable around the opened list.
            bool hot = Enabled && bounds.Contains(PointToClient(Cursor.Position));
            Color back = !Enabled ? theme.Background : DroppedDown ? theme.SurfacePressed : hot ? theme.SurfaceHover : theme.Surface;
            Color border = !Enabled ? theme.Border : Focused || DroppedDown ? theme.Accent : hot ? theme.BorderHover : theme.Border;
            Color text = Enabled ? theme.Text : theme.DisabledText;

            using (var brush = new SolidBrush(back))
                g.FillRectangle(brush, bounds);
            using (var pen = new Pen(border))
                g.DrawRectangle(pen, 0, 0, bounds.Width - 1, bounds.Height - 1);

            int buttonWidth = SystemInformation.VerticalScrollBarWidth;
            int arrow = Math.Max(3, Font.Height / 4);
            float centerX = bounds.Width - 1 - buttonWidth / 2f;
            float centerY = bounds.Height / 2f;
            PointF[] chevron =
            {
                new PointF(centerX - arrow, centerY - arrow / 2f),
                new PointF(centerX, centerY + arrow / 2f),
                new PointF(centerX + arrow, centerY - arrow / 2f),
            };
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (var pen = new Pen(text, Math.Max(1f, Font.Height / 12f)))
                g.DrawLines(pen, chevron);
            g.SmoothingMode = SmoothingMode.Default;

            int padding = Math.Max(3, Font.Height / 4);
            var textBounds = new Rectangle(padding, 0, Math.Max(0, bounds.Width - buttonWidth - padding - 2), bounds.Height);
            TextRenderer.DrawText(g, Text, Font, textBounds, text, back,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix);
        }

        protected override void OnDropDown(EventArgs e)
        {
            base.OnDropDown(e);

            // Dark scroll bar for the opened list on Windows 10 1809+ (Windows ignores the theme name elsewhere).
            try
            {
                var info = new NativeMethods.COMBOBOXINFO { cbSize = Marshal.SizeOf(typeof(NativeMethods.COMBOBOXINFO)) };
                if (NativeMethods.GetComboBoxInfo(Handle, ref info))
                    NativeMethods.SetWindowTheme(info.hwndList, Theme.Current.IsDark ? "DarkMode_Explorer" : null, null);
            }
            catch (Exception ex) when (ex is DllNotFoundException || ex is EntryPointNotFoundException)
            {
            }
            Invalidate();
        }

        protected override void OnDropDownClosed(EventArgs e)
        {
            base.OnDropDownClosed(e);
            Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            Invalidate();
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            Invalidate();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            Invalidate();
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            Invalidate();
        }
    }

    internal class ThemedCheckBox : CheckBox
    {
        bool hot;

        protected override void OnPaint(PaintEventArgs e)
        {
            if (!Theme.Current.IsDark)
            {
                base.OnPaint(e);
                return;
            }

            Theme theme = Theme.Current;
            Graphics g = e.Graphics;
            g.Clear(BackColor);

            // Same box size as the native glyph, so the text sits where AutoSize expects it. Without visual styles
            // (Windows 7 classic theme) the renderer reports an unscaled 13x13 while the native box follows the DPI.
            int classic = (int)Math.Round(13 * g.DpiX / 96f);
            Size glyph = Application.RenderWithVisualStyles
                ? CheckBoxRenderer.GetGlyphSize(g, CheckBoxState.UncheckedNormal)
                : new Size(classic, classic);
            var box = new Rectangle(0, (Height - glyph.Height) / 2, glyph.Width - 1, glyph.Height - 1);
            Color fill = Checked ? theme.Accent : hot ? theme.SurfaceHover : theme.Surface;
            Color border = Checked ? theme.Accent : hot ? theme.BorderHover : theme.Border;
            using (var brush = new SolidBrush(fill))
                g.FillRectangle(brush, box);
            using (var pen = new Pen(border))
                g.DrawRectangle(pen, box);

            if (Checked)
            {
                PointF[] mark =
                {
                    new PointF(box.Left + box.Width * 0.22f, box.Top + box.Height * 0.52f),
                    new PointF(box.Left + box.Width * 0.42f, box.Top + box.Height * 0.72f),
                    new PointF(box.Left + box.Width * 0.78f, box.Top + box.Height * 0.30f),
                };
                g.SmoothingMode = SmoothingMode.AntiAlias;
                using (var pen = new Pen(theme.AccentText, Math.Max(1.5f, box.Width / 7f)))
                    g.DrawLines(pen, mark);
                g.SmoothingMode = SmoothingMode.Default;
            }

            int gap = Math.Max(3, glyph.Width / 4);
            var textBounds = new Rectangle(glyph.Width + gap, 0, Math.Max(0, Width - glyph.Width - gap), Height);
            TextFormatFlags flags = TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine
                | (ShowKeyboardCues ? TextFormatFlags.Default : TextFormatFlags.HidePrefix);
            TextRenderer.DrawText(g, Text, Font, textBounds, Enabled ? theme.Text : theme.DisabledText, flags);

            if (Focused && ShowFocusCues)
            {
                Size text = TextRenderer.MeasureText(g, Text, Font, textBounds.Size, flags);
                var focus = new Rectangle(textBounds.Left - 1, (Height - text.Height) / 2, Math.Min(text.Width + 2, textBounds.Width), text.Height);
                ControlPaint.DrawFocusRectangle(g, focus, theme.Text, BackColor);
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            hot = true;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            hot = false;
            Invalidate();
        }
    }

    internal class ThemedGroupBox : GroupBox
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            if (!Theme.Current.IsDark)
            {
                base.OnPaint(e);
                return;
            }

            Theme theme = Theme.Current;
            Graphics g = e.Graphics;
            const TextFormatFlags flags = TextFormatFlags.Left | TextFormatFlags.Top | TextFormatFlags.SingleLine | TextFormatFlags.NoPrefix;

            int top = Font.Height / 2;
            using (var pen = new Pen(theme.Border))
                g.DrawRectangle(pen, 0, top, Width - 1, Height - top - 1);

            if (Text.Length > 0)
            {
                // Caption over the top border line, like the native group box.
                Size text = TextRenderer.MeasureText(g, Text, Font, new Size(int.MaxValue, int.MaxValue), flags);
                var caption = new Rectangle(6, 0, Math.Min(text.Width, Math.Max(0, Width - 12)), Font.Height);
                using (var brush = new SolidBrush(BackColor))
                    g.FillRectangle(brush, caption);
                TextRenderer.DrawText(g, Text, Font, caption, Enabled ? theme.Text : theme.DisabledText, flags);
            }
        }
    }
}
