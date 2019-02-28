using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DBI_ShuffleTool.UI.Progress_Bar
{
    class BlueProgressBar : ProgressBar
    {
        private SolidBrush brush = new SolidBrush(Color.FromArgb(255, 52, 152, 219));

        public BlueProgressBar()
        {
            SetStyle(ControlStyles.UserPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //if (brush == null || brush.Color != ForeColor)
            //    brush = new SolidBrush(Color.FromArgb(255, (byte)52, (byte)152, (byte)219));

            Rectangle rec = new Rectangle(0, 0, this.Width, this.Height);
            if (ProgressBarRenderer.IsSupported)
                ProgressBarRenderer.DrawHorizontalBar(e.Graphics, rec);
            rec.Width = (int)(rec.Width * ((double)Value / Maximum)) - 4;
            rec.Height = rec.Height - 4;
            e.Graphics.FillRectangle(brush, 2, 2, rec.Width, rec.Height);
        }
    }
}