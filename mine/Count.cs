using System;
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
        int[] xRows;
        bool check = true;
        int lap = 0;

        String matrix = "";
        double L;

        int x; 
        int y; 

        public void temporaryMatrixFilling()
        {
            OleDbConnection con = new OleDbConnection(connect);
            con.Open();
            
            // Подсчет записей:
            OleDbCommand dataCount = new OleDbCommand("SELECT DATA.ID FROM DATA", con);
            OleDbDataReader dataCountReader = dataCount.ExecuteReader();
            int count = 0;
            while (dataCountReader.Read())
            {
                count++;
            }

            variables = count;
            bounds = variables * 4;

            x = bounds + 1;
            y = x + variables;

            alpha1 = new double[variables];
            pPl1 = new double[variables];
            pPl2 = new double[variables];
            zatr = new double[variables];
            checkRows = new int[x];
            xRows = new int[y];

            int i = 0;
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
                if (i < 20) temporaryMatrix[i, temp] = 1;
                else temporaryMatrix[i, temp] = alpha1[temp];
                if (checkRows[i] == 1)
                    temporaryMatrix[x, temp] += temporaryMatrix[i, temp];
                
                temp++;
                if (temp == variables) temp = 0;
                
            }

            // Формирование массива данных для начальной матрицы:
            OleDbCommand dataQuery = new OleDbCommand("SELECT DATA.ZATR, DATA.PPL1, DATA.PPL2, DATA.Q, DATA.ALPHA1, DATA.ALPHA2 FROM DATA", con);
            OleDbDataReader dataRead = dataQuery.ExecuteReader();

            //--------------------------------------------------------------------------------------
            
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

            String matrix = "Начальная матрица W\n";
            for (i = 0; i <= x; i++)
            {
                for (int j = 0; j < y; j++)
                    matrix += temporaryMatrix[i, j] + " ";
                matrix += "\n";
            }
            MessageBox.Show(matrix);
            

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
                
                matrix = "";
                for (int i = 0; i < x; i++)
                {
                    for (int j = 0; j < y; j++)
                        matrix += mainMatrix[i, j] + " ";
                    matrix += "\n";
                }
                MessageBox.Show(matrix);
                firstMatrix = mainMatrix;
            }

            // Если нет, то повторяем функцию temporaryCount().
            else
            {
                if (lap > variables * 2)
                {
                    MessageBox.Show("О_о");
                    mainMatrix = new double[x, y];
                    for (int i = 0; i < x; i++)
                    {
                        for (int j = 0; j < y; j++)
                        {
                            mainMatrix[i, j] = temporaryMatrix[i, j];
                        }
                    }

                    matrix = "";
                    for (int i = 0; i < x; i++)
                    {
                        for (int j = 0; j < y; j++)
                            matrix += mainMatrix[i, j] + " ";
                        matrix += "\n";
                    }
                    MessageBox.Show(matrix);
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
                    L = firstMatrix[x - 1, y - 1] * -1;
                    String xLast = "Переменные: X(";
                    for (int i = 0; i < variables; i++)
                    {
                        if (xRows[i] != -1)
                            xLast += firstMatrix[xRows[i], y - 1].ToString() + ";  ";
                        else
                            xLast += "0;  ";
                    }
                    xLast = xLast.Trim() + ").";
                    MessageBox.Show("Целевая функция равна " + L.ToString() + ".\n" + xLast);
                }

                // Если нет, то повторяем функцию simplexCount().
                else
                {
                    if (lap > variables * 2)
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

    }
}
