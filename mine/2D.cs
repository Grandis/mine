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
        string title = "Номер скважины";
        String[] names = { "Номер скважины", "", "", "Отметка Z", "CUOB", "CUOK", "MOOB", "MOSF" };
        int count;
        int caption = 0;
        
        Point PointBegin = Point.Empty;
        Point PointEnd = Point.Empty;

        int KDX = 10;
        int KDY = 6;
        double CD = 1.0; //  любое положительное число
        double N = 1; // кратность цены деления


        double[] X;//  
        double[] Y;//  координаты исходных точек
        double[] Z;//

        double Xmin, Xmax, Ymin, Ymax;         // текущие пределы  
        double Xmin_d, Xmax_d, Ymin_d, Ymax_d; // пределы исходных данных 
        int ots_lev1, ots_lev2, ots_pra1, ots_pra2;
        int ots_ver1, ots_ver2, ots_niz1, ots_niz2;
        double K_e_px, K_e_py;
        bool flag = false; // для колеса
        Font font_os;
        Font font_nad;
        Font font_pok;
        StringFormat sf;
        bool oprobFlag = false;

        public _2D(double[,] data2D, int count)
        {
            InitializeComponent();
            this.data2D = data2D;
            this.count = count;
            DoubleBuffered = true; // двойная буфферизация

            Cursor = Cursors.Arrow; // курсор ро стрелкой

            sf = new StringFormat();
            ResizeRedraw = true;
            X = new double[count];
            Y = new double[count];
            Z = new double[count];

            font_os = new Font("Arial", 14);
            font_nad = new Font("Arial", 20);
            font_pok = new Font("Arial", 10);

            Size ek = SystemInformation.PrimaryMonitorSize; // Размеры экрана
            Size = new Size((int)(0.8 * ek.Width), (int)(0.8 * ek.Height));

            Form_mas();
            Nastroyka(); // Настройка	
            Max_Min();    //Max-Min

            Top = (int)(0.1 * ek.Height);
            Left = (int)(0.1 * ek.Width);

        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////

        void Form_mas()
        {
            int kol = 0;
            while (kol < count)
            {
                X[kol] = data2D[kol, 1];
                Y[kol] = data2D[kol,2];
                Z[kol] = data2D[kol,caption];
                kol++;
            }
        }

        void Nastroyka()
        {

            Graphics gr = CreateGraphics();

            //Для определения правильной длины строки в MeasureString - StringFormat.GenericTypographic

            SizeF pr_po_os = gr.MeasureString("9", font_os, PointF.Empty, StringFormat.GenericTypographic);
            SizeF pr_po_nad = gr.MeasureString("9", font_nad, PointF.Empty, StringFormat.GenericTypographic);
            SizeF pr_po_pok = gr.MeasureString("9", font_pok, PointF.Empty, StringFormat.GenericTypographic);

            ots_lev1 = (int)(4 * pr_po_os.Width);
            ots_lev2 = (int)(2 * pr_po_pok.Width);

            ots_pra1 = (int)(2 * pr_po_pok.Width);
            ots_pra2 = (int)(2 * pr_po_pok.Width);

            ots_ver1 = (int)(2 * pr_po_nad.Height) + menuStrip1.Height + toolStrip1.Height;
            ots_ver2 = (int)(2 * pr_po_pok.Height);

            ots_niz1 = (int)(2 * pr_po_os.Height);
            ots_niz2 = (int)(0.5 * pr_po_os.Height);
            gr.Dispose();
        }

        void Max_Min()
        {

            Xmax = Xmin = X[0];
            Ymax = Ymin = Y[0];

            for (int i = 1; i < count; i++)
            {
                if (X[i] > Xmax) Xmax = X[i];
                if (X[i] < Xmin) Xmin = X[i];
                if (Y[i] > Ymax) Ymax = Y[i];
                if (Y[i] < Ymin) Ymin = Y[i];
            }

            Xmax = Xmax_d = Math.Ceiling(Xmax);
            Ymax = Ymax_d = Math.Ceiling(Ymax);
            Xmin = Xmin_d = Math.Floor(Xmin);
            Ymin = Ymin_d = Math.Floor(Ymin);
        }

        private void _2D_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;
            int i, koor_x, koor_y;
            double pr;
            string buf;

            if (flag == false)
            {
                Xmin = Math.Floor(Xmin / N) * N;
                Ymin = Math.Floor(Ymin / N) * N;
            }

            if (CD >= 0)
            {
                pr = CD = 0;
                while ((Xmin + CD * KDX) < Xmax || (Ymin + CD * KDY) < Ymax)
                {
                    pr++;
                    CD = N * pr;
                }

                if (CD == 0) CD = N;
            }
            else
                CD *= -1.0;

            Xmax = Xmin + KDX * CD;
            Ymax = Ymin + KDY * CD;

            K_e_px = (Xmax - Xmin) /
                (ClientSize.Width - ots_lev1 - ots_lev2 - ots_pra1 - ots_pra2);
            K_e_py = (Ymax - Ymin) /
                (ClientSize.Height - ots_niz1 - ots_niz2 - ots_ver1 - ots_ver2);


            // Вывод заголовка 
            sf.Alignment = sf.LineAlignment = StringAlignment.Center;
            gr.DrawString(title, font_nad, Brushes.Black,
            ClientSize.Width / 2, (menuStrip1.Height + toolStrip1.Height + ots_ver1) / 2, sf);


            Pen pen = new Pen(Color.Black, 1);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            sf.LineAlignment = sf.Alignment = StringAlignment.Center;

            // Вывод горизонтальных осей 

            for (i = 0; i < KDY + 1; i++)
            {
                koor_y = (int)((Ymax - Ymin - CD * i) / K_e_py + ots_ver1 + ots_ver2);

                if (flag == false)
                {
                    buf = string.Format("{0:F0}", Ymin + CD * i);
                    gr.DrawString(buf, font_os, Brushes.Black, ots_lev1 / 2, koor_y, sf);
                }

                gr.DrawLine(pen, ots_lev1, koor_y, ots_lev1 + ots_lev2 +
                    (int)((Xmax - Xmin) / K_e_px), koor_y);
            }

            // Вывод вертикальных осей 

            sf.LineAlignment = StringAlignment.Near;
            sf.Alignment = StringAlignment.Center;

            for (i = 0; i < KDX + 1; i++)
            {
                koor_y = ots_ver1 + ots_ver2;
                koor_x = (int)((Xmax - Xmin - CD * i) / K_e_px + ots_lev1 + ots_lev2);
                gr.DrawLine(pen, koor_x, koor_y, koor_x, koor_y + (int)((Ymax - Ymin) /
                K_e_py + ots_niz2));
                koor_y += (int)((Ymax - Ymin) / K_e_py + ots_niz2);

                if (flag == false)
                    if ((i & 1) == 0)//четные
                    {
                        buf = string.Format("{0:F0}", Xmax - CD * i);
                        gr.DrawString(buf, font_os, Brushes.Black, koor_x, koor_y, sf);
                    }
            }

            //Вывод показателей

            sf.LineAlignment = StringAlignment.Far;
            sf.Alignment = StringAlignment.Center;

            for (i = 0; i < count; i++)
            {

                if (X[i] > Xmax || X[i] < Xmin || Y[i] > Ymax || Y[i] < Ymin) continue;
                koor_x = (int)((X[i] - Xmin) / K_e_px + ots_lev1 + ots_lev2);
                koor_y = (int)((Ymax - Y[i]) / K_e_py + ots_ver1 + ots_ver2);
                if (caption == 0) buf = string.Format("{0:F0}", Z[i]);
                else buf = string.Format("{0:F3}", Z[i]);
                if (oprobFlag == true)
                {
                    if (Z[i] > 0)
                    {
                        gr.DrawString(buf, font_pok, Brushes.Black, koor_x, koor_y, sf);
                        gr.FillEllipse(new SolidBrush(Color.Red), koor_x - 3, koor_y - 3, 6, 6);
                    }
                }
                else
                {
                    gr.DrawString(buf, font_pok, Brushes.Black, koor_x, koor_y, sf);
                    gr.FillEllipse(new SolidBrush(Color.Red), koor_x - 3, koor_y - 3, 6, 6);
                }
            }
        }

        private void _2D_MouseDown(object sender, MouseEventArgs e)
        {
           // if (e.Button == MouseButtons.Middle)
           //   if (flag == true) toolStripButton1_Click(null, null);
           // else toolStripButton2_Click(null, null);

                if (flag == true) return;

            PointEnd = PointBegin = e.Location;
            Cursor = Cursors.Cross;

        }

        private void _2D_MouseUp(object sender, MouseEventArgs e)
        {
            if (flag == true) return;
            int x1, x2, y1, y2;
            Cursor = Cursors.Arrow;

            if (e.Button == MouseButtons.Left)
            {
                x1 = PointBegin.X < PointEnd.X ? PointBegin.X : PointEnd.X;
                x2 = PointBegin.X > PointEnd.X ? PointBegin.X : PointEnd.X;
                y1 = PointBegin.Y < PointEnd.Y ? PointBegin.Y : PointEnd.Y;
                y2 = PointBegin.Y > PointEnd.Y ? PointBegin.Y : PointEnd.Y;

                if (x1 == x2 || y1 == y2) return;

                Xmax = (x2 - ots_lev1 - ots_lev2) * K_e_px + Xmin;
                Xmin = (x1 - ots_lev1 - ots_lev2) * K_e_px + Xmin;
                Ymin = Ymax - (y2 - ots_ver1 - ots_ver2) * K_e_py;
                Ymax = Ymax - (y1 - ots_ver1 - ots_ver2) * K_e_py;
                Invalidate();
                return;

            }

            if (e.Button != MouseButtons.Right) return;
            if (PointBegin == PointEnd) return;


            Xmin = Xmin - (PointEnd.X - PointBegin.X) * K_e_px;
            Ymin = Ymin - (PointBegin.Y - PointEnd.Y) * K_e_py;
            CD = -1 * CD; // для сохранения CD в функции SCROLL
            Invalidate();
        }

        private void _2D_MouseMove(object sender, MouseEventArgs e)
        {
            if (flag == true) return;
            double x, y;
            Point pt = e.Location;
            x = (pt.X - ots_lev1 - ots_lev2) * K_e_px + Xmin;
            y = Ymax - (pt.Y - ots_ver1 - ots_ver2) * K_e_py;
            Text = string.Format("X = {0:F2} Y = {1:F2} ", x, y);


            if (e.Button == MouseButtons.Left)
            {
                Rectangle rect;
                Point pt1, pt2;
                pt1 = PointToScreen(PointBegin);
                Peres(ref PointEnd);
                pt2 = PointToScreen(PointEnd);

                rect = new Rectangle(pt1.X, pt1.Y, pt2.X - pt1.X, pt2.Y - pt1.Y);
                ControlPaint.DrawReversibleFrame(rect, Color.FromArgb(255, 255, 0), FrameStyle.Dashed);
                PointEnd = e.Location;
                Peres(ref PointEnd);
                pt2 = PointToScreen(PointEnd);
                rect = new Rectangle(pt1.X, pt1.Y, pt2.X - pt1.X, pt2.Y - pt1.Y);
                ControlPaint.DrawReversibleFrame(rect, Color.FromArgb(255, 255, 0), FrameStyle.Dashed);
                return;
            }
            if (e.Button == MouseButtons.Right)
            {
                Peres(ref PointEnd);
                ControlPaint.DrawReversibleLine(PointToScreen(PointBegin), PointToScreen(PointEnd), Color.FromArgb(255, 255, 0));
                PointEnd = e.Location;
                Peres(ref PointEnd);
                ControlPaint.DrawReversibleLine(PointToScreen(PointBegin), PointToScreen(PointEnd), Color.FromArgb(255, 255, 0));
                return;
            }

        }

        //Функция пересчета координаты точек при выходе за границы окна
        private void Peres(ref Point pt)
        {
            pt.X = (pt.X < ClientRectangle.Left) ? ClientRectangle.Left : pt.X;
            pt.X = (pt.X > ClientRectangle.Right) ? ClientRectangle.Right : pt.X;
            pt.Y = (pt.Y < ClientRectangle.Top + menuStrip1.Height + toolStrip1.Height) ?
                ClientRectangle.Top + menuStrip1.Height + toolStrip1.Height : pt.Y;
            pt.Y = (pt.Y > ClientRectangle.Bottom) ? ClientRectangle.Bottom : pt.Y;
        }


        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (flag == false) return;
            base.OnMouseWheel(e);

            double x, y, k_uv, vr_cd;
            Text = "Работает колесико";
            Point pt = e.Location;

            x = (pt.X - ots_lev1 - ots_lev2) * K_e_px + Xmin;
            y = Ymax - (pt.Y - ots_ver1 - ots_ver2) * K_e_py;

            // ----------------------------------------------------------
            k_uv = (e.Delta > 0) ? 1.1 : 0.9;
            vr_cd = Math.Floor(CD * k_uv / N) * N;

            if (Math.Abs(CD - vr_cd) < 0.001)
                vr_cd = (k_uv > 1) ? vr_cd += N : vr_cd -= N;
            if (vr_cd <= N) vr_cd = N;

            if (vr_cd > (Xmax_d - Xmin_d) &&
                vr_cd > (Ymax_d - Ymin_d))
                return;

            // -----------------------------------------------------------

            Xmin = x - (x - Xmin) / CD * vr_cd;
            Ymin = y - (y - Ymin) / CD * vr_cd;

            CD = -vr_cd;

            Invalidate();
        }

        //Обновить
        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            caption = 0;
            title = "Номер скважины";

            Form_mas();
            Nastroyka();  // Настройка	
            Max_Min();    // Max-Min
            Invalidate();
        }

        //Переключение в режим двумерной графики
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            toolStripButton1.Checked = true;
            toolStripButton2.Checked = false;
            flag = false;
            Cursor = Cursors.Arrow;
            Invalidate();
        }

        //Переключение в режим колесика
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            toolStripButton1.Checked = false;
            toolStripButton2.Checked = true;
            flag = true;
            Cursor = Cursors.Cross;
            Invalidate();
        }

        private void cUOBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            caption = 4;
            title = "CUOB";

            Form_mas();
            Nastroyka();  // Настройка	
            Max_Min();    // Max-Min
            Invalidate();
        }

        private void cUOKToolStripMenuItem_Click(object sender, EventArgs e)
        {
            caption = 5;
            title = "CUOK";

            Form_mas();
            Nastroyka();  // Настройка	
            Max_Min();    // Max-Min
            Invalidate();

        }

        private void mOOBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            caption = 6;
            title = "MOOB";

            Form_mas();
            Nastroyka();  // Настройка	
            Max_Min();    // Max-Min
            Invalidate();

        }

        private void mOSFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            caption = 7;
            title = "MOSF";

            Form_mas();
            Nastroyka();  // Настройка	
            Max_Min();    // Max-Min
            Invalidate();

        }

        private void отметкаZToolStripMenuItem_Click(object sender, EventArgs e)
        {
            caption = 3;
            title = "Отметка Z";

            Form_mas();
            Nastroyka();  // Настройка	
            Max_Min();    // Max-Min
            Invalidate();

        }

        private void номерСкважиныToolStripMenuItem_Click(object sender, EventArgs e)
        {
            caption = 0;
            title = "Номер скважины";

            Form_mas();
            Nastroyka();  // Настройка	
            Max_Min();    // Max-Min
            Invalidate();

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (caption == 0 || caption == 3)
                caption = 7;
            else caption--;

            title = names[caption];
            Form_mas();
            Nastroyka();  // Настройка	
            Max_Min();    // Max-Min
            Invalidate();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (caption == 0 || caption == 7)
                caption = 3;
            else 
                caption++;

            title = names[caption];
            Form_mas();
            Nastroyka();  // Настройка	
            Max_Min();    // Max-Min
            Invalidate();

        }

        private void oprobSKToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (oprobFlag == false)
            {
                allSKToolStripMenuItem.Checked = false;
                oprobSKToolStripMenuItem.Checked = true;
                oprobFlag = true;
                Form_mas();
                Nastroyka();  // Настройка	
                Max_Min();    // Max-Min
                Invalidate();
            }
        }

        private void allSKToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (oprobFlag == true)
            {
                oprobSKToolStripMenuItem.Checked = false;
                allSKToolStripMenuItem.Checked = true;
                oprobFlag = false;
                Form_mas();
                Nastroyka();  // Настройка	
                Max_Min();    // Max-Min
                Invalidate();
            }
        }

    }
}
