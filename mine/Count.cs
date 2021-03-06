﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace mine
{
    public partial class Count : Form
    {
        public Count()
        {
            InitializeComponent();
            temporaryMatrixFilling();
            temporaryCount();
            simplexCount();
        }

        String connect = "Provider=Microsoft.JET.OLEDB.4.0;data source= ../../mdb/CMMVS.MDB";
        int variables;
        int bounds;
        double[,] temporaryMatrix;
        double[,] mainMatrix = null;
        double[,] firstMatrix;
        int[] checkRows;
        double alphaPl;
        double deltaAlpha;
        double[] alpha1;
        double[] pPl1;
        double[] pPl2;
        double[] zatr;
        double[] t;
        double[] xArray;
        double sigma = 0;
        int nGor = 1385; // горизонт
        int[] nBl; // выбранные блоки
        double[] AlphaINbl; // средний показатель по каждому блоку
        int[] xRows;
        bool check = true;
        int lap = 0;
        int i = 0;

        String matrix = "";
        StringFormat sf = new StringFormat();
        double L;

        int x; 
        int y; 

        public void temporaryMatrixFilling()
        {
            OleDbConnection con = new OleDbConnection(connect);
            con.Open();

            if (xArray != null)
            {
                nBl = new int[variables];
                AlphaINbl = new double[variables];
                t = new double[variables];
                OleDbCommand nBlQuery = new OleDbCommand("SELECT DATA.NBL FROM DATA", con);
                OleDbDataReader nBlReader = nBlQuery.ExecuteReader();
                i = 0;
                while (nBlReader.Read())
                {
                    nBl[i] = Convert.ToInt32(nBlReader[0]);
                    i++;
                }

                for (i = 0; i < variables; i++)
                {
                    OleDbCommand oprobCount = new OleDbCommand("SELECT CMMVS.CUOB FROM CMMVS WHERE CMMVS.NGOR="+nGor+" AND CMMVS.NBL=" + nBl[i], con);
                    OleDbDataReader oprobCountReader = oprobCount.ExecuteReader();

                    double sum = 0.00;
                    int j = 0;
                    while (oprobCountReader.Read())
                    {
                        if (Convert.ToDouble(oprobCountReader[0]) > 0)
                        {
                            sum += Math.Pow((Convert.ToDouble(oprobCountReader[0]) - alpha1[i]), 2);
                            j++; 
                        }
                    }
                    t[i] = (sum / j) + Math.Pow((alpha1[i] - alphaPl), 2);
                    sigma += t[i] * xArray[i];
                }
                sigma /= xArray.Sum();

                //***********************


            }
                // Подсчет записей:
                OleDbCommand dataCount = new OleDbCommand("SELECT DATA.ID FROM DATA", con);
                OleDbDataReader dataCountReader = dataCount.ExecuteReader();
                int count = 0;
                while (dataCountReader.Read())
                {
                    count++;
                }

                variables = count;
                if(xArray != null)
                {
                    bounds = variables * 4 + 1;
                }
                else
                {
                bounds = variables * 4;
                }

                x = bounds + 1;
                y = x + variables;

                alpha1 = new double[variables];
                pPl1 = new double[variables];
                pPl2 = new double[variables];
                zatr = new double[variables];
                checkRows = new int[x];
                xRows = new int[y];

                for (i = 0; i < y; i++) xRows[i] = -1;
                for (i = 0; i < checkRows.Count(); i++) if (i < 10 || (i > 19 && i < 30) || i > 39) checkRows[i] = 1;
                // ----------------

                // Задаем АльфаПлановый (нормативный показатель качества):
                OleDbCommand dataAlphaPl = new OleDbCommand("SELECT STATIC.VAL FROM STATIC WHERE STATIC.ID=5", con);
                OleDbDataReader dataAlphaPlReader = dataAlphaPl.ExecuteReader();

                dataAlphaPlReader.Read();
                alphaPl = Convert.ToDouble(dataAlphaPlReader[0]);
                // ----------------

                // Задаем ДельтаАльфа:
                OleDbCommand dataDeltaAlpha = new OleDbCommand("SELECT STATIC.VAL FROM STATIC WHERE STATIC.ID=7", con);
                OleDbDataReader dataDeltaAlphaReader = dataDeltaAlpha.ExecuteReader();

                dataDeltaAlphaReader.Read();
                deltaAlpha = Convert.ToDouble(dataDeltaAlphaReader[0]);
                // ----------------

                temporaryMatrix = new double[x + 1, y];

                // Формирование массива данных ALPHA1:
                OleDbCommand dataAlpha1Query = new OleDbCommand("SELECT DATA.ZATR, DATA.PPL1, DATA.PPL2, DATA.ALPHA1 FROM DATA", con);
                OleDbDataReader dataAlpha1Reader = dataAlpha1Query.ExecuteReader();

                i = 0;
                while (dataAlpha1Reader.Read())
                {
                    zatr[i] = Convert.ToDouble(dataAlpha1Reader[0]);
                    pPl1[i] = Convert.ToDouble(dataAlpha1Reader[1]);
                    pPl2[i] = Convert.ToDouble(dataAlpha1Reader[2]);
                    alpha1[i] = Convert.ToDouble(dataAlpha1Reader[3]);
                    i++;
                }
            
            // Добавляем в массив коэфициенты переменных ограничений.
            int temp = 0;
            for (i = 0; i < bounds; i++)
            {
                if (i < 2 * variables) temporaryMatrix[i, temp] = 1;
                else if(i < 4 * variables) temporaryMatrix[i, temp] = alpha1[temp];
                else if (i == 4 * variables)
                {
                    for (int j = 0; j < variables; j++)
                    {
                        temporaryMatrix[i, j] = Math.Abs(sigma - t[j]);
                        temporaryMatrix[x, j] += temporaryMatrix[i, j];
                    }
                }
                if (i != 4 * variables && checkRows[i] == 1)
                    temporaryMatrix[x, temp] += temporaryMatrix[i, temp];
                
                temp++;
                if (temp == variables) temp = 0;
                
            }

            /*
            //
            OleDbCommand dataQuery = new OleDbCommand("SELECT DATA.ZATR, DATA.PPL1, DATA.PPL2, DATA.Q, DATA.ALPHA1, DATA.ALPHA2 FROM DATA", con);
            OleDbDataReader dataRead = dataQuery.ExecuteReader();
            //--------------------------------------------------------------------------------------
            */
            con.Close();

            // Добавляем в массив единицы базисных переменных
            for (i = 0; i < bounds; i++)
            {
                if (checkRows[i] == 1)
                {
                    temporaryMatrix[i, variables + i] = -1;
                    temporaryMatrix[x, variables + i] = 1;
                }
                else temporaryMatrix[i, variables + i] = 1;
            }
            //--------------------------------------------------------------
            
            // Добавляем в массив ограничения.
            temp = 0;
            for (i = 0; i < bounds; i++)
            {
                if (i < variables) temporaryMatrix[i, y - 1] = pPl1[temp];
                else if (i < 2 * variables) temporaryMatrix[i, y - 1] = pPl2[temp];
                else if (i < 3 * variables) temporaryMatrix[i, y - 1] = (alphaPl - deltaAlpha) * pPl1[temp];
                else if (i < 4 * variables) temporaryMatrix[i, y - 1] = (alphaPl + deltaAlpha) * pPl2[temp];
                else if (i == 4 * variables) temporaryMatrix[i, y - 1] = 0;

                if (checkRows[i] == 1) temporaryMatrix[x, y - 1] += temporaryMatrix[i, y - 1];

                temp++;
                if (temp == variables) temp = 0;
            }
            //--------------------------------------------------------------
            
            // Добавляем в массив исходное уравнение.
            for (int j = 0; j < variables; j++)
            {
                temporaryMatrix[x - 1, j] = zatr[j];
            }
            //---------------------------------------------------------------
            
            //
            for (int j = 0; j < variables; j++)
                temporaryMatrix[x, j] *= -1;
            temporaryMatrix[x, y - 1] *= -1;
            //-----------------------------------
            /*
            String matrix = "Начальная матрица W\n";
            for (i = 0; i <= x; i++)
            {
                for (int j = 0; j < y; j++)
                    matrix += temporaryMatrix[i, j] + " ";
                matrix += "\n";
            }
            MessageBox.Show(matrix);
            */

        }
        
        public void temporaryCount()
        {
            // наименьший элемент в L строке
            double minCollumn = temporaryMatrix[x, 0];
            int minCollumnIndex = 0;
            for (int j = 0; j < y - 1; j++)
            {
                //MessageBox.Show("Перебор наименьших элементов в строке W:  " + firstMatrix[bounds, j]);

                if (temporaryMatrix[x, j] < minCollumn)
                {
                    minCollumn = temporaryMatrix[x, j];
                    minCollumnIndex = j;
                }
            }
            //MessageBox.Show("Наименьший элемент в строке W:  " + minCollumn);

            // наименьший эллемент в найденном столбце
            int minRowIndex = 0;
            double minRow = Double.MaxValue;//firstMatrix[0, y-1] / firstMatrix[0, minCollumnIndex];
            for (int i = 0; i < bounds; i++)
            {
                if (temporaryMatrix[i, minCollumnIndex] > 0 && temporaryMatrix[i, y - 1] / temporaryMatrix[i, minCollumnIndex] < minRow)
                {
                    minRow = temporaryMatrix[i, y - 1] / temporaryMatrix[i, minCollumnIndex];
                    minRowIndex = i;
                }
            }
            //MessageBox.Show("наименьший эллемент в найденном столбце:  " + minRow);
            //MessageBox.Show("Indef of наименьший эллемент в найденном столбце:  " + minRowIndex);

            double minRowCol = temporaryMatrix[minRowIndex, minCollumnIndex];
            xRows[minCollumnIndex] = minRowIndex;

            // Делим элементы строки
            for (int j = 0; j < y; j++)
            {
                temporaryMatrix[minRowIndex, j] /= minRowCol;
            }

            // Считаем
            for (int i = 0; i <= x; i++)
            {
                if (i != minRowIndex)
                {
                    double tempMinCol = temporaryMatrix[i, minCollumnIndex];
                    for (int j = 0; j < y; j++)
                    {
                        temporaryMatrix[i, j] -= temporaryMatrix[minRowIndex, j] * tempMinCol;
                    }
                }
            }

            /*
            String matrix = "";
            for (int i = 0; i <= x; i++)
            {
                for (int j = 0; j < y; j++)
                    matrix += temporaryMatrix[i, j] + " ";
                matrix += "\n";
            }
            MessageBox.Show(matrix);
            */
        
            // Проверяем, все ли элементы строки W равны 0.
            check = true;
            for (int j = 0; j < y - 1; j++)
            {
                if (temporaryMatrix[x, j] != 0) check = false;
            }

            // Если да, то задача решена.
            if (check)
            {
                mainMatrix = new double[x, y];
                for (int i = 0; i < x; i++)
                {
                    for (int j = 0; j < y; j++)
                    {
                        mainMatrix[i, j] = temporaryMatrix[i, j];
                    }
                }
                /*
                matrix = "";
                for (int i = 0; i < x; i++)
                {
                    for (int j = 0; j < y; j++)
                        matrix += mainMatrix[i, j] + " ";
                    matrix += "\n";
                }
                MessageBox.Show(matrix);
                */
                firstMatrix = mainMatrix;
            }

            // Если нет, то повторяем функцию temporaryCount().
            else
            {
                if (lap > bounds * bounds)
                {
                    //MessageBox.Show("О_о");
                    mainMatrix = new double[x, y];
                    for (int i = 0; i < x; i++)
                    {
                        for (int j = 0; j < y; j++)
                        {
                            mainMatrix[i, j] = temporaryMatrix[i, j];
                        }
                    }
                    /*
                    matrix = "";
                    for (int i = 0; i < x; i++)
                    {
                        for (int j = 0; j < y; j++)
                            matrix += mainMatrix[i, j] + " ";
                        matrix += "\n";
                    }
                    MessageBox.Show(matrix);
                    */
                    firstMatrix = mainMatrix;
                }
                else
                {
                    lap++;
                    temporaryCount();
                }
            }

        }

        public void simplexCount()
        {
            
            try
            {
                // наименьший элемент в L строке
                double minCollumn = firstMatrix[bounds, 0];
                int minCollumnIndex = 0;
                for (int j = 0; j < y - 1; j++)
                {
                    //MessageBox.Show("Перебор наименьших элементов в строке L:  " + firstMatrix[bounds, j]);

                    if (firstMatrix[bounds, j] < minCollumn)
                    {
                        minCollumn = firstMatrix[bounds, j];
                        minCollumnIndex = j;
                    }
                }
                //MessageBox.Show("Наименьший элемент в строке L:  " + minCollumn);

                // наименьший эллемент в найденном столбце
                int minRowIndex = 0;
                double minRow = Double.MaxValue;//firstMatrix[0, y-1] / firstMatrix[0, minCollumnIndex];
                for (int i = 0; i < bounds; i++)
                {
                    if (firstMatrix[i, minCollumnIndex] > 0 && firstMatrix[i, y - 1] / firstMatrix[i, minCollumnIndex] < minRow)
                    {
                        minRow = firstMatrix[i, y - 1] / firstMatrix[i, minCollumnIndex];
                        minRowIndex = i;
                    }
                }
                //MessageBox.Show("наименьший эллемент в найденном столбце:  " + minRow);
                //MessageBox.Show("Indef of наименьший эллемент в найденном столбце:  " + minRowIndex);

                double minRowCol = firstMatrix[minRowIndex, minCollumnIndex];
                xRows[minCollumnIndex] = minRowIndex;

                // Делим элементы строки
                for (int j = 0; j < y; j++)
                {
                    firstMatrix[minRowIndex, j] /= minRowCol;
                }

                // Считаем
                for (int i = 0; i < x; i++)
                {
                    if (i != minRowIndex)
                    {
                        double tempMinCol = firstMatrix[i, minCollumnIndex];
                        for (int j = 0; j < y; j++)
                        {
                            firstMatrix[i, j] -= firstMatrix[minRowIndex, j] * tempMinCol;
                        }
                    }
                }

                /*
                String matrix = "";
                for (int i = 0; i < x; i++)
                {
                    for (int j = 0; j < y; j++)
                        matrix += firstMatrix[i, j] + " ";
                    matrix += "\n";
                }
                MessageBox.Show(matrix);
                */

                // Проверяем, все ли элементы функции L положительны.
                check = true;
                for (int j = 0; j < y - 1; j++)
                {
                    if (firstMatrix[x - 1, j] < 0) check = false;
                }


                // Если да, то задача решена.
                if (check)
                {
                    if (xArray == null)
                    {

                        xArray = new double[variables];
                        for (int i = 0; i < variables; i++)
                        {
                            if (xRows[i] != -1)
                            {
                                xArray[i] = firstMatrix[xRows[i], y - 1];
                            }
                            else
                            {
                                xArray[i] = 0;
                            }
                        }

                        L = firstMatrix[x - 1, y - 1] * -1;
                        String xLast = "Переменные: X(";
                        for (int i = 0; i < variables; i++)
                        {
                            if (xRows[i] != -1)
                            {
                                xLast += firstMatrix[xRows[i], y - 1].ToString() + ";  ";
                                xArray[i] = firstMatrix[xRows[i], y - 1];
                            }
                            else
                            {
                                xLast += "0;  ";
                                xArray[i] = 0;
                            }
                        }
                        xLast = xLast.Trim() + ").";
                        //MessageBox.Show("Без сигмы\nЦелевая функция равна " + L.ToString() + ".\n" + xLast);


                        
                        temporaryMatrixFilling();
                        temporaryCount();
                        simplexCount();
                    }
                    else
                    {
                        L = firstMatrix[x - 1, y - 1] * -1;
                        String xLast = "Переменные: X(";
                        for (int i = 0; i < variables; i++)
                        {
                            if (xRows[i] != -1)
                            {
                                xLast += firstMatrix[xRows[i], y - 1].ToString() + ";  ";
                                xArray[i] = firstMatrix[xRows[i], y - 1];
                            }
                            else
                            {
                                xLast += "0;  ";
                                xArray[i] = 0;
                            }
                        }
                        xLast = xLast.Trim() + ").";
                        //MessageBox.Show("Целевая функция равна " + L.ToString() + ".\n" + xLast);
                    }
                }

                // Если нет, то повторяем функцию simplexCount().
                else
                {
                    if (lap > bounds * bounds)
                    {
                        MessageBox.Show("Функция не ограничена на множестве допустимых решений.");
                        return;
                    }
                    else
                    {
                        lap++;
                        //mainMatrix = firstMatrix;
                        simplexCount();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Count_Paint(object sender, PaintEventArgs e)
        {
            sf.Alignment = sf.LineAlignment = StringAlignment.Center;
            Graphics gr = e.Graphics;
            gr.DrawString("Горизонт " + nGor, new Font("Arial", 14), Brushes.Black, ClientSize.Width / 2, menuStrip1.Height * 2, sf);
            gr.DrawString("Итоговые затраты составляют:   " + L + " грн.", new Font("Arial", 14), Brushes.Black, ClientSize.Width / 2, menuStrip1.Height * 2 + 30, sf);

            float j = menuStrip1.Height * 2F + 60F;
            for (int i = 0; i < variables; i++)
            {
                gr.DrawString("Добыча руды на блоке №" + nBl[i] + ":   " + xArray[i] + " т.", new Font("Arial", 14), Brushes.DarkGray, 230, j);
                j += 25;
            }

            gr.DrawString("Всего добыто:   " + xArray.Sum() + " т.", new Font("Arial", 14), Brushes.Black, ClientSize.Width / 2, j + 30, sf);

        }

        private void исходныеДанныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            groupBox2.Visible = true;

            OleDbConnection con = new OleDbConnection(connect);
            con.Open();

            staticDataGridView.Rows.Clear();
            dataGridView1.Rows.Clear();

            OleDbCommand staticQuery = new OleDbCommand("SELECT STATIC.NAIM, STATIC.INFO, STATIC.VAL FROM STATIC", con);
            OleDbDataReader staticGrid = staticQuery.ExecuteReader();
            OleDbCommand data = new OleDbCommand("SELECT DATA.NGOR, DATA.NBL, DATA.ZATR, DATA.PPL1, DATA.PPL2, DATA.Q, DATA.ALPHA1 FROM DATA", con);
            OleDbDataReader dataGrid = data.ExecuteReader();

            int i = 0;
            int colls = staticGrid.FieldCount;
            while (staticGrid.Read())
            {
                staticDataGridView.Rows.Add();
                for (int j = 0; j < colls; j++)
                {
                    staticDataGridView.Rows[i].Cells[j].Value = staticGrid[j];
                }
                i++;
            }

            i = 0;
            colls = dataGrid.FieldCount;
            while (dataGrid.Read())
            {
                dataGridView1.Rows.Add();
                for (int j = 0; j < colls; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = dataGrid[j];
                }
                i++;
            }

            con.Close();

        }

        private void результатыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = false;

        }

    }
}
