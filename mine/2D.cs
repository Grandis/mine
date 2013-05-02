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
        double[,] data2D;
        int segmentsAmount = 5; // Количество делений сетки.
        int margin = 60; // Отступ графика от краев формы.

        public _2D(double[,] data2D)
        {
            InitializeComponent();
            this.ResizeRedraw = true;
            this.data2D = data2D;
        }

        private void _2D_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;
            Pen axis = new Pen(Color.Black, 3);
            
            Point yMax = new Point(margin, margin / 2);
            Point xMax = new Point(ClientSize.Width - margin / 2, ClientSize.Height - margin);
            Point nullPoint = new Point(margin, ClientSize.Height - margin);

            // Рисуем оси координат и подписи к ним:
            gr.DrawLine(axis, yMax, nullPoint);
            gr.DrawString("Y", new Font(SystemFonts.DefaultFont, FontStyle.Bold), Brushes.Black, margin - 15, 15); 
            gr.DrawLine(axis, nullPoint, xMax);
            gr.DrawString("X", new Font(SystemFonts.DefaultFont, FontStyle.Bold), Brushes.Black, ClientSize.Width - 20, ClientSize.Height - margin); 
            // ------------------------------------

            drawLines(gr, margin); // Рисуем сетку.
            drawPoints(gr, margin);
        }

        // Сетка на графике:
        public void drawLines(Graphics gr, int margin)
        {
            Pen grid = new Pen(Color.Gray, 1);
            float width = ClientSize.Width - margin * 2;
            float height = ClientSize.Height - margin * 2;

            float startHor = margin, startVert = margin;
            for (int i = 0; i < segmentsAmount; i++)
            {
                startHor += width / segmentsAmount;
                gr.DrawLine(grid, startHor, startVert - 10, startHor, startVert + height + 10);
            }

            startHor = margin;
            for (int i = 0; i < segmentsAmount; i++)
            {
                gr.DrawLine(grid, startHor - 10, startVert, startHor + width + 10, startVert);
                startVert += height / segmentsAmount;
            }
        }
        // ------------------------------------------

        // Наносим на график координаты скважин:
        public void drawPoints(Graphics gr, int margin)
        {
            float x;
            float y;
            int width = ClientSize.Width - margin * 2;
            int height = ClientSize.Height - margin * 2;
            
            int minX = int.MaxValue, maxX = 0, minY = int.MaxValue, maxY = 0;

            for (int i = 0; i < data2D.GetLength(0); i++)
            {
                if ((int)data2D[i, 0] / 100 > maxX) maxX = (int)data2D[i, 0] /100;
                if ((int)data2D[i, 0] / 100 < minX) minX = (int)data2D[i, 0] / 100;
                if ((int)data2D[i, 1] / 100 > maxY) maxY = (int)data2D[i, 1] / 100;
                if ((int)data2D[i, 1] / 100 < minY) minY = (int)data2D[i, 1] / 100;
            }

            int segmentsX = (maxX - minX) + 1;
            int segmentsY = (maxY - minY) + 1;

            float scaleX = width / (segmentsX * 100.00f);
            float scaleY = height / (segmentsY * 100.00f);

            for (int i = 0; i < data2D.GetLength(0); i++)
            {
                x = (float)(data2D[i,0] - minX * 100) * scaleX + margin - 1;
                y = (float)(data2D[i,1] - minY * 100) * scaleY + margin - 1;
                gr.FillRectangle(Brushes.Red, x, ClientSize.Height - y, 3, 3);
            }

            // Подписываем иксы:
            for (int i = 0; i <= segmentsAmount; i++)
            {
                gr.DrawString((minX * 100 + segmentsX * (100 / segmentsAmount) * i).ToString(), new Font(SystemFonts.DefaultFont, FontStyle.Regular), Brushes.Black, width / segmentsAmount * i + 45, ClientSize.Height - 40);
            }
            // -----------------

            // Подписываем игреки:
            for (int i = 0; i <= segmentsAmount; i++)
            {
                gr.DrawString((minY * 100 + segmentsY * (100 - i * (100 / segmentsAmount))).ToString(), new Font(SystemFonts.DefaultFont, FontStyle.Regular), Brushes.Black, 10, height / segmentsAmount * i + margin - 5);
            }
            // -----------------
        }
        // -------------------------------------
    }
}
