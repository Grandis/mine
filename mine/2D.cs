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
            int margin = 30; // Отступ графика от краев формы.
            Point yMax = new Point(margin, margin);
            Point xMax = new Point(ClientSize.Width - margin, ClientSize.Height - margin);
            Point nullPoint = new Point(margin, ClientSize.Height - margin);

            // Рисуем оси координат и подписи к ним:
            gr.DrawLine(axis, yMax, nullPoint);
            gr.DrawString("Y", new Font(SystemFonts.DefaultFont, FontStyle.Bold), new SolidBrush(Color.Black), 10, 15); 
            gr.DrawLine(axis, nullPoint, xMax);
            gr.DrawString("X", new Font(SystemFonts.DefaultFont, FontStyle.Bold), new SolidBrush(Color.Black), ClientSize.Width - 20, ClientSize.Height - 25); 
            // ------------------------------------

            drawLines(gr, margin); // Рисуем сетку.
        }

        // Сетка на графике:
        public void drawLines(Graphics gr, int margin)
        {
            int segmentsAmount = 5;
            Pen grid = new Pen(Color.Gray, 1);
            float width = ClientSize.Width - margin * 2;
            float height = ClientSize.Height - margin * 2;

            float startHor = margin, startVert = margin;
            for (int i = 0; i < segmentsAmount; i++)
            {
                startHor += width / segmentsAmount;
                gr.DrawLine(grid, startHor, startVert, startHor, startVert + height);
            }

            startHor = margin;
            for (int i = 0; i < segmentsAmount; i++)
            {
                gr.DrawLine(grid, startHor, startVert, startHor + width, startVert);
                startVert += height / segmentsAmount;
            }
        }
        // ------------------------------------------
    }
}
