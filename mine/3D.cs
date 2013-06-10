using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

// для работы с библиотекой OpenGL
using Tao.OpenGl;
// для работы с элементом управления SimpleOpenGLControl
using Tao.Platform.Windows;

namespace mine
{
    public partial class _3D : Form
    {
        double[,] data3D;
        int count;

        float xRot, yRot, zRot;
        int kod_figure = 1;

        //Массивы цветов для столбиковых диаграмм
        double[,] mas_color =
				{
				  { 0.0,0.0,0.0  }, // сорт 0
				  { 0.2,0.2,1.0  }, // сорт 1
				  { 1.0,0.2,0.2  }, // сорт 2
				  
				};

        double[,] mas_oten =
				{
				  { 0.0,0.0,0.0  }, // сорт 0
				  { 0.8,0.8,1.0  }, // сорт 1
				  { 1.0,0.8,0.8  }, // сорт 2
				  
				};

        string[] zag = {
                           "Отметка Z",
                            "CuOB",
                            "CuOK",
                            "MoOB",
                            "MoSF"
                       };



        public _3D(double[,] data3D, int count)
        {
            InitializeComponent();
            this.data3D = data3D;
            this.count = count;

            simpleOpenGlControl1.InitializeContexts(); // /Инициализация тао
            Gl.glEnable(Gl.GL_DEPTH_TEST);
            xRot = yRot = zRot = 0;

        }

        private void _3D_Load(object sender, EventArgs e)
        {
            IntPtr hDC = Wgl.wglGetCurrentDC();

            Gdi.SelectObject(hDC, Font.ToHfont());//Выбрать шрифт, созданный нами.  
            Wgl.wglUseFontOutlinesA(Wgl.wglGetCurrentDC(), 0, 255,
            1000, 0, 0, Wgl.WGL_FONT_POLYGONS, null);

        }


        private void simpleOpenGlControl1_Load(object sender, EventArgs e)
        {
            simpleOpenGlControl1_SizeChanged(null, null);
        }

        private void simpleOpenGlControl1_SizeChanged(object sender, EventArgs e)
        {
            Gl.glViewport(0, 0, simpleOpenGlControl1.Width, simpleOpenGlControl1.Height);

            // настройка проекции 
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();

            Glu.gluPerspective(30.0f, 1.0f * simpleOpenGlControl1.Width / simpleOpenGlControl1.Height, 0.1f, 200.0f);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();

            Gl.glTranslatef(0.0f, 0.0f, -4.0f);

            simpleOpenGlControl1.Invalidate();
        }

        private void simpleOpenGlControl1_Paint(object sender, PaintEventArgs e)
        {
            Gl.glClearColor(0.50f, 0.50f, 0.50f, 1.0f);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

            Gl.glPushMatrix();
            Model();
            Gl.glPopMatrix();

        }

        private void simpleOpenGlControl1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;
        }

        private void simpleOpenGlControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                xRot -= 5.0f;
            }
            if (e.KeyCode == Keys.Down)
            {
                xRot += 5.0f;
            }

            if (e.KeyCode == Keys.Left)
                yRot -= 5.0f;

            if (e.KeyCode == Keys.Right)
                yRot += 5.0f;

            if (xRot > 356.0f) xRot = 0.0f;
            if (xRot < -1.0f) xRot = 355.0f;

            if (yRot > 356.0f) yRot = 0.0f;
            if (yRot < -1.0f) yRot = 355.0f;


            if (zRot < -100.0f) zRot = -100.0f;
            if (zRot > 4.0f) zRot = 4.0f;


            simpleOpenGlControl1.Invalidate(false);
        }

        //Нажатие кнопки вверх
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            kod_figure++;
            if (kod_figure > 5) kod_figure = 1;
            simpleOpenGlControl1.Invalidate(false);
        }

        //Нажатие кнопки вниз
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            kod_figure--;
            if (kod_figure < 1) kod_figure = 5;
            simpleOpenGlControl1.Invalidate(false);
        }

        //Пользовательские функции
        
        //Вывод текста
        void OutText(string text)
        {

            Gl.glListBase(1000);
            byte[] textbytes = new byte[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                textbytes[i] = (byte)text[i];
                if (text[i] > 1039 && text[i] < 1104)
                    textbytes[i] = (byte)(text[i] + 176);
            }
            Gl.glCallLists(text.Length, Gl.GL_UNSIGNED_BYTE, textbytes);
        }
        //Вывод модели
        void Model()
        {
            try
            {
                int i;
                if (count == 0) return;
                double maxx, minx, maxy, miny, maxz, minz;
                double xx1, yy1, prir, dxy, z_t;
                double zb, xb, yb, delta, vr;

                int pok = kod_figure + 2;

                minx = maxx = data3D[0, 1];
                miny = maxy = data3D[0, 2];
                minz = maxz = data3D[0, pok];

                for (i = 0; i < count;i++ )
                {
                    xb = data3D[i, 1];
                    yb = data3D[i, 2];
                    zb = data3D[i, pok];

                    if (xb < minx) minx = xb;
                    if (yb < miny) miny = yb;
                    if (zb < minz) minz = zb;

                    if (xb > maxx) maxx = xb;
                    if (yb > maxy) maxy = yb;
                    if (zb > maxz) maxz = zb;
                }


                //Шаг для блока
                delta = 3;

                if (Math.Abs(maxz - minz) < 0.0001) minz = 0;
                dxy = (maxx - minx > maxy - miny) ? maxx - minx : maxy - miny;
                // формирование размеров призм
                prir = delta / dxy / 2.0;
                minz = minz - (maxz - minz) / 9.0; //пересчет для нулевых значений

                Gl.glEnable(Gl.GL_POLYGON_SMOOTH);

                Gl.glTranslatef(0.0f, 0.0f, zRot);
                Gl.glRotatef(xRot - 90, 1.0f, 0.0f, 0.0f);
                Gl.glRotatef(yRot - 110, 0.0f, 0.0f, 1.0f);

                // Вывод с к в а ж и н
                Gl.glBegin(Gl.GL_QUADS);
                Gl.glLineWidth(1);

                for (i = 0; i < count; i++)
                {
                    xb = data3D[i, 1];
                    yb = data3D[i, 2];
                    vr = data3D[i, pok];

                    if (vr.ToString() == "") continue;
                    zb = vr;
                    if (zb < 0.0001) continue;

                    xx1 = (xb - minx) / dxy - 0.5;
                    yy1 = (yb - miny) / dxy - 0.5;
                    z_t = (zb - minz) / (maxz - minz) - 0.5;

                    //  Построение и заливка вертикальных призм         
                    truba_z(xx1, yy1, -0.5, z_t, prir, 2);

                    // Заливка верхних поверхностей вертикальных призм
                    //  Установка  цветов сортов руд

                    Gl.glVertex3f((float)(xx1 - prir), (float)(yy1 + prir),
                        (float)z_t);
                    Gl.glVertex3f((float)(xx1 + prir), (float)(yy1 + prir),
                        (float)z_t);
                    Gl.glVertex3f((float)(xx1 + prir), (float)(yy1 - prir),
                        (float)z_t);
                    Gl.glVertex3f((float)(xx1 - prir), (float)(yy1 - prir),
                        (float)z_t);


                }


                Gl.glEnd();

                if (kod_figure == 1)
                    Scena(minx, maxx, miny, maxy, minz, maxz);
                else
                    Scena(minx, maxx, miny, maxy, minz, maxz, 4, 10);

                //Вывод надписей
                Gl.glColor3f(1, 1, 1);
                Gl.glDisable(Gl.GL_DEPTH_TEST);
                Gl.glPushMatrix();
                Gl.glLoadIdentity();
                Gl.glTranslatef(0, 0, -4);
                Gl.glScalef(0.1f, 0.1f, 0.1f);
                Gl.glTranslatef(0, 8.5f, 0);
                OutText(zag[kod_figure-1]);
                Gl.glPopMatrix();
                Gl.glEnable(Gl.GL_DEPTH_TEST);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        void gltDrawUnitAxes(double hx, double hy, double hz, double k)
        {
            Glu.GLUquadric pObj;	// Temporary, used for quadrics


            double max, fAxisRadius, fArrowRadius, fArrowHeight;
            max = (hx > hy) ? hx : hy;
            max = (hz > max) ? hz : max;

            fAxisRadius = max / 50 / k;
            fArrowRadius = max / 20 / k;
            fArrowHeight = max / 13 / k;

            // Setup the quadric object
            pObj = Glu.gluNewQuadric();

            Glu.gluQuadricDrawStyle(pObj, Glu.GLU_FILL);
            Glu.gluQuadricNormals(pObj, Glu.GLU_SMOOTH);
            Glu.gluQuadricOrientation(pObj, Glu.GLU_OUTSIDE);
            Glu.gluQuadricTexture(pObj, Glu.GLU_FALSE);

            ///////////////////////////////////////////////////////
            // Draw the blue Z axis first, with arrowed head
            Gl.glColor3f(0.0f, 0.0f, 1.0f);
            Glu.gluCylinder(pObj, fAxisRadius, fAxisRadius, hz, 10, 1);
            Gl.glPushMatrix();
            Gl.glTranslated(0.0, 0.0, hz);
            Glu.gluCylinder(pObj, fArrowRadius, 0.0f, fArrowHeight, 10, 1);
            Gl.glRotatef(180.0f, 1.0f, 0.0f, 0.0f);
            Glu.gluDisk(pObj, fAxisRadius, fArrowRadius, 10, 1);
            Gl.glPopMatrix();

            ///////////////////////////////////////////////////////
            // Draw the Red X axis 2nd, with arrowed head
            Gl.glColor3f(1.0f, 0.0f, 0.0f);
            Gl.glPushMatrix();
            Gl.glRotatef(90.0f, 0.0f, 1.0f, 0.0f);
            Glu.gluCylinder(pObj, fAxisRadius, fAxisRadius, hx, 10, 1);
            Gl.glPushMatrix();
            Gl.glTranslated(0.0, 0.0, hx);//
            Glu.gluCylinder(pObj, fArrowRadius, 0.0f, fArrowHeight, 10, 1);
            Gl.glRotatef(180.0f, 0.0f, 1.0f, 0.0f);
            Glu.gluDisk(pObj, fAxisRadius, fArrowRadius, 10, 1);
            Gl.glPopMatrix();
            Gl.glPopMatrix();

            ///////////////////////////////////////////////////////
            // Draw the Green Y axis 3rd, with arrowed head
            Gl.glColor3f(0.0f, 1.0f, 0.0f);
            Gl.glPushMatrix();
            Gl.glRotatef(-90.0f, 1.0f, 0.0f, 0.0f);
            Glu.gluCylinder(pObj, fAxisRadius, fAxisRadius, hy, 10, 1);
            Gl.glPushMatrix();
            Gl.glTranslated(0.0, 0.0, hy);
            Glu.gluCylinder(pObj, fArrowRadius, 0.0f, fArrowHeight, 10, 1);
            Gl.glRotatef(180.0f, 1.0f, 0.0f, 0.0f);
            Glu.gluDisk(pObj, fAxisRadius, fArrowRadius, 10, 1);
            Gl.glPopMatrix();
            Gl.glPopMatrix();

            ////////////////////////////////////////////////////////
            // White Sphere at origin
            Gl.glColor3f(1.0f, 1.0f, 1.0f);
            Glu.gluSphere(pObj, 2 * fAxisRadius, 15, 15);

            // Delete the quadric
            Glu.gluDeleteQuadric(pObj);
        }


        void truba_x(double z1, double y1, double x1, double x2, double prir)
        {
            // построение призмы вокруг прямой
            double[] z_prir = { 1, -1, -1, -1, -1, 1, 1, 1 };
            double[] y_prir = { 1, 1, 1, -1, -1, -1, -1, 1 };
            double y, z;

            for (int j = 0; j < 8; j++)
            {

                z = z1 + prir * z_prir[j];
                y = y1 + prir * y_prir[j];
                if (j % 2 == 1)
                {
                    Gl.glVertex3d(x1, y, z);
                    Gl.glVertex3d(x2, y, z);
                    continue;
                }
                Gl.glVertex3d(x2, y, z);
                Gl.glVertex3d(x1, y, z);
            }

        }



        void truba_y(double z1, double x1, double y1, double y2, double prir)
        {
            // построение призмы вокруг прямой
            double[] z_prir = { 1, -1, -1, -1, -1, 1, 1, 1 };
            double[] y_prir = { 1, 1, 1, -1, -1, -1, -1, 1 };
            double x, z;

            for (int j = 0; j < 8; j++)
            {

                z = z1 + prir * z_prir[j];
                x = x1 + prir * y_prir[j];
                if (j % 2 == 1)
                {
                    Gl.glVertex3d(x, y1, z);
                    Gl.glVertex3d(x, y2, z);
                    continue;
                }
                Gl.glVertex3d(x, y2, z);
                Gl.glVertex3d(x, y1, z);
            }
        }


        void truba_z(double x1, double y1, double z1, double z2, double prir, int sort)
        {

            // построение призмы вокруг прямой
            double[] x_prir = { 1, -1, -1, -1, -1, 1, 1, 1 };
            double[] y_prir = { 1, 1, 1, -1, -1, -1, -1, 1 };
            double x, y;
            double c1, c2, c3;
            double co1, co2, co3;

            c1 = mas_color[sort, 0];
            c2 = mas_color[sort, 1];
            c3 = mas_color[sort, 2];

            co1 = mas_oten[sort, 0];
            co2 = mas_oten[sort, 1];
            co3 = mas_oten[sort, 2];

            for (int j = 0; j < 8; j++)
            {
                x = x1 + prir * x_prir[j];
                y = y1 + prir * y_prir[j];

                if (j % 2 == 1)
                {

                    if (sort == 1) Gl.glColor3d(co1, co2, co3);

                    Gl.glVertex3d(x, y, z1);

                    if (sort == 1) Gl.glColor3d(c1, c2, c3);

                    Gl.glVertex3d(x, y, z2);
                    continue;
                }


                Gl.glColor3d(co1, co2, co3);
                Gl.glVertex3d(x, y, z2);

                Gl.glColor3d(c1, c2, c3);
                Gl.glVertex3d(x, y, z1);
            }
        }
        // Формирование сцены относительно единичной матрицы
        // minx,miny,minz - минимальные координаты по каждой из осей
        // maxx,maxy,maxz - максимальные координаты по каждой из осей
        // dec - количество знаков после запятой для вывода надписей (по умолчанию 1)
        // kd - количество делений по оси OZ (по умолчанию 10)

        void Scena(double minx, double maxx, double miny, double maxy,
            double minz, double maxz, int dec = 1, int kd = 10)
        {

            int i, kol_b, ots_b;
            double x1, y1, shir_b, dop_dl;
            double xn, yn, zn, dx, dy;
            double per, dxy, xc, yc, ots_pov, cena_del;
            string buf, shabl;

            //------------------------

            //количество символов для отступа вывода надписей
            ots_b = 2;

            xn = yn = zn = -0.5;
            ots_pov = 0.015;
            cena_del = (maxz - minz) / kd;

            //Коэфициент высоты шрифта для 10 делений

            per = 100.0 / 7.0 * kd / 10;

            //Ширина одной цифры
            shir_b = 0.03 * 10 / kd;
            shabl = "{0,0:F" + dec.ToString() + "}";
            buf = string.Format(shabl, minz);

            //Максимальное количество букв
            kol_b = buf.Length;

            buf = string.Format(shabl, maxz);

            kol_b = (buf.Length > kol_b) ? buf.Length : kol_b;

            dop_dl = (kol_b + 2 * ots_b) * shir_b;

            // ----------------------------------------------------------------------------

            dxy = ((maxx - minx) > (maxy - miny)) ? maxx - minx : maxy - miny;

            x1 = xn + (maxx - minx) / dxy;
            y1 = yn + (maxy - miny) / dxy;

            xc = zn - dop_dl;
            yc = zn - dop_dl;


            Gl.glPolygonMode(Gl.GL_FRONT_AND_BACK, Gl.GL_FILL);
            Gl.glLineWidth(1);

            // Построение осей	
            Gl.glTranslated(xc, yc, zn);

            gltDrawUnitAxes(x1 - xc + dop_dl, y1 - yc + dop_dl, 1 + 1.0 / kd, 1.4);

            Gl.glTranslated(-xc, -yc, -zn);

            // Построение прозрачных плоскостей

            Gl.glEnable(Gl.GL_BLEND);

            Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);

            Gl.glPolygonMode(Gl.GL_FRONT_AND_BACK, Gl.GL_FILL);

            Gl.glColor4f(0.0f, 0.0f, 1.0f, 0.2f);

            Gl.glBegin(Gl.GL_QUADS);

            Gl.glVertex3d(xc, yc, zn);
            Gl.glVertex3d(xc, yc, 0.5 + 1.0 / kd);
            Gl.glVertex3d(x1 + dop_dl, yc, 0.5 + 1.0 / kd);
            Gl.glVertex3d(x1 + dop_dl, yc, zn);


            Gl.glVertex3d(xc, yc, zn);
            Gl.glVertex3d(xc, yc, 0.5 + 1.0 / kd);
            Gl.glVertex3d(xc, y1 + dop_dl, 0.5 + 1.0 / kd);
            Gl.glVertex3d(xc, y1 + dop_dl, zn);

            /////////////////////////////////////
            Gl.glVertex3d(xc + 0.05, yc + 0.05, zn);
            Gl.glVertex3d(xc + 0.05, y1 + dop_dl, zn);
            Gl.glVertex3d(x1 + dop_dl, y1 + dop_dl, zn);
            Gl.glVertex3d(x1 + dop_dl, yc + 0.05, zn);


            Gl.glEnd();
            Gl.glDisable(Gl.GL_BLEND); // конец прозрачности

            // Построение осей для удлиненных плоскостей

            Gl.glColor3f(0.3f, 0.6f, 0.5f);

            Gl.glPolygonMode(Gl.GL_FRONT_AND_BACK, Gl.GL_LINE);
            Gl.glLineWidth(1);


            Gl.glBegin(Gl.GL_LINES);
            Gl.glColor3f(0.7f, 0.7f, 0.7f);

            for (i = 1; i < (kd + 1); i++) // построение штрихов
            {
                Gl.glVertex3d(xn - dop_dl + 0.05, yc + ots_pov, zn + i * 1.0 / kd);
                Gl.glVertex3d(x1, yc + ots_pov, zn + i * 1.0 / kd);
                Gl.glVertex3d(xc + ots_pov, yn - dop_dl + 0.05, zn + i * 1.0 / kd);
                Gl.glVertex3d(xc + ots_pov, y1, zn + i * 1.0 / kd);
            }

            Gl.glEnd();
            //-----------------------------------------------------
            //glColor3f(0.3f,0.8f,0.3f);
            Gl.glColor3f(0.0f, 0.0f, 0.0f);
            Gl.glPolygonMode(Gl.GL_FRONT_AND_BACK, Gl.GL_FILL);

            Gl.glPushMatrix();//сохранение текущей матрицы
            Gl.glScalef((float)(1.0 / per), (float)(1.0 / per), (float)(1.0 / per));

            // вывод показателей по 1-й плоскости сценария
            Gl.glTranslatef((float)((xc + ots_pov) * per),
                (float)((y1 + ots_b * shir_b) * per), (float)(zn * per));

            for (i = 0; i < (kd + 1); i++)
            {
                Gl.glPushMatrix();//сохранение текущей матрицы
                Gl.glTranslatef(0.0f, 0.0f, (float)(i * 1.0 / kd * per));

                if (i == 0)
                {
                    Gl.glTranslated(0.2f, (kol_b - 1) / 2.0 * shir_b * per, 0.0f);
                    buf = "Y";
                }
                else
                {
                    shabl = "{0,0:F" + dec.ToString() + "}";
                    buf = string.Format(shabl, minz + cena_del * i);
                }

                Gl.glRotatef(90, 0, 0, 1);
                Gl.glRotatef(90, 1, 0, 0);

                OutText(buf);
                Gl.glPopMatrix();//восстановление матрицы
            }
            Gl.glPopMatrix();//восстановление матрицы

            // вывод показателей по 2-й плоскости сценария     

            Gl.glPushMatrix();//сохранение текущей матрицы

            Gl.glScalef((float)(1.0 / per), (float)(1.0 / per), (float)(1.0 / per));
            Gl.glTranslatef((float)((x1 + dop_dl - ots_b * shir_b) * per),
                (float)((yc + ots_pov) * per), (float)(zn * per));

            for (i = 0; i < (kd + 1); i++)
            {
                Gl.glPushMatrix();//сохранение текущей матрицы
                Gl.glTranslatef(0.0f, 0.0f, (float)(i * 1.0 / kd * per));


                if (i == 0)
                {
                    Gl.glTranslated(-(kol_b - 1) / 2.0 * shir_b * per, 0.2f, 0.0f);
                    buf = "X";
                }
                else
                {
                    shabl = "{0,0:F" + dec.ToString() + "}";
                    buf = string.Format(shabl, minz + cena_del * i);
                }

                Gl.glRotatef(180, 0, 0, 1);
                Gl.glRotatef(90, 1, 0, 0);

                OutText(buf);
                Gl.glPopMatrix();//восстановление матрицы
            }
            Gl.glPopMatrix();//восстановление матрицы

            //Область минимакса в виде труб

            Gl.glBegin(Gl.GL_QUADS);
            Gl.glColor3f(0.0f, 1.0f, 0.0f);

            //Доля стороны для 5 отрезков
            dx = (x1 - xn) / 9.0;
            dy = (y1 - yn) / 9.0;

            for (i = 0; i < 5; i++)
            {
                truba_x(zn, yn, xn + 2 * dx * i, xn + 2 * dx * i + dx, 0.005);
                truba_x(zn, y1, xn + 2 * dx * i, xn + 2 * dx * i + dx, 0.005);
                truba_y(zn, xn, yn + 2 * dy * i, yn + 2 * dy * i + dy, 0.005);
                truba_y(zn, x1, yn + 2 * dy * i, yn + 2 * dy * i + dy, 0.005);
            }
            Gl.glEnd();

        }


    }
}
