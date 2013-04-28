using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace mine
{
    public partial class _2D : Form
    {
        public _2D()
        {
            InitializeComponent();
            this.ResizeRedraw = true;
        }

        private void _2D_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;
            Pen axis = new Pen(Color.Black, 3);
            Point yMax = new Point(30, 30);
            Point xMax = new Point(ClientSize.Width - 30, ClientSize.Height - 30);
            Point nullPoint = new Point(30, ClientSize.Height - 30);

            // Рисуем оси координат и подписи к ним
            gr.DrawLine(axis, yMax, nullPoint);
            gr.DrawString("Y", new Font(SystemFonts.DefaultFont, FontStyle.Bold), new SolidBrush(Color.Black), 10, 15); 
            gr.DrawLine(axis, nullPoint, xMax);
            gr.DrawString("X", new Font(SystemFonts.DefaultFont, FontStyle.Bold), new SolidBrush(Color.Black), ClientSize.Width - 20, ClientSize.Height - 25); 
            // ------------------------------------
        }
    }
}
