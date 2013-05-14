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
    public partial class Simplex : Form
    {
        public Simplex()
        {
            InitializeComponent();
        }

        double L;
        int x, y, tempMinColumn = -1, tempMinRow = -1;
        int variables = 0;
        int bounds = 0;
        int lap;
        String[] eqData = { "<=", "=>" };
        double[,] temporaryMatrix;
        double[,] mainMatrix = null;
        double[,] firstMatrix;
        bool check = true;
        bool error;
        int[] xRows;
        int[] checkRows;
        bool checkOver; // если есть ограничение с "=>", то становится TRUE

        private void button1_Click(object sender, EventArgs e)
        {
            tempMinColumn = -1;
            tempMinRow = -1;
            for (int i = 0; i < xRows.Length; i++) xRows[i] = -1;
            mainMatrix = null;
            lap = 0;
            error = false;

            checkRows = new int[Convert.ToInt32(boundsNumber.Text)];
            for (int i = 0; i < checkRows.Length; i++) checkRows[i] = -1;
            checkOver = false;

            checking();
            if (error) return;

            if (checkOver) temporaryCount();

            firstMatrixFilling();
            simplexCount();
        }

        // Проверяем, есть ли в ограничениях ">=":
        public void checking()
        {
            try
            {
                variables = Convert.ToInt32(variablesNumber.Text);
                bounds = Convert.ToInt32(boundsNumber.Text);
                x = bounds + 1;
                y = x + variables;

                for (int i = 0; i < bounds; i++)
                {
                    for (int j = 0; j < variables; j++)
                    {
                        if (dataGridView1.Rows[i].Cells[variables].Value.ToString() == "=>")
                        {
                            checkRows[i] = 1;
                            checkOver = true;
                        }
                    }
                }

                if (checkOver)
                {
                    temporaryMatrix = new double[x + 1, y];

                    // Добавляем в массив коэфициенты переменных ограничений.
                    for (int i = 0; i < bounds; i++)
                    {
                        for (int j = 0; j < variables; j++)
                        {
                            temporaryMatrix[i, j] = Convert.ToDouble(dataGridView1.Rows[i].Cells[j].Value);
                            if (checkRows[i] == 1)
                                temporaryMatrix[x, j] += Convert.ToDouble(dataGridView1.Rows[i].Cells[j].Value);
                        }
                    }
                    //--------------------------------------------------------------------------------------

                    // Добавляем в массив единицы базисных переменных
                    for (int i = 0; i < bounds; i++)
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
                    for (int i = 0; i < bounds; i++)
                    {
                        temporaryMatrix[i, y - 1] = Convert.ToDouble(dataGridView1.Rows[i].Cells[dataGridView1.Columns.Count - 1].Value);
                        if (checkRows[i] == 1) temporaryMatrix[x, y - 1] += Convert.ToDouble(dataGridView1.Rows[i].Cells[dataGridView1.Columns.Count - 1].Value);
                    }
                    //--------------------------------------------------------------

                    // Добавляем в массив исходное уравнение (с обратными знаками!).
                    for (int j = 0; j < variables; j++)
                    {
                        if (comboBox1.SelectedItem.Equals("Min")) temporaryMatrix[x - 1, j] = Convert.ToDouble(dataGridView2.Rows[0].Cells[j].Value);
                        else temporaryMatrix[x - 1, j] = Convert.ToDouble(dataGridView2.Rows[0].Cells[j].Value) * -1;
                    }
                    //---------------------------------------------------------------

                    //
                    for (int j = 0; j < variables; j++)
                        temporaryMatrix[x, j] *= -1;
                    temporaryMatrix[x, y - 1] *= -1;
                    //-----------------------------------

                    String matrix = "Начальная матрица W\n";
                    for (int i = 0; i <= x; i++)
                    {
                        for (int j = 0; j < y; j++)
                            matrix += temporaryMatrix[i, j] + " ";
                        matrix += "\n";
                    }
                    MessageBox.Show(matrix);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                error = true;
            }
        }
        //****************************************************

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

            tempMinColumn = minCollumnIndex;
            tempMinRow = minRowIndex;

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
            }

            // Если нет, то повторяем функцию temporaryCount().
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
                    temporaryCount();
                }
            }

        }

        public void firstMatrixFilling()
        {
            // Узнаём необходимые размеры массива и создаем его.
            variables = Convert.ToInt32(variablesNumber.Text);
            bounds = Convert.ToInt32(boundsNumber.Text);
            x = bounds + 1;
            y = x + variables;

            firstMatrix = new double[x, y];

            try
            {
                if (mainMatrix == null)
                {
                    // Добавляем в массив коэфициенты переменных ограничений.
                    for (int i = 0; i < bounds; i++)
                    {
                        for (int j = 0; j < variables; j++)
                        {
                            if (dataGridView1.Rows[i].Cells[variables].Value.ToString() == "<=")
                            {
                                firstMatrix[i, j] = Convert.ToDouble(dataGridView1.Rows[i].Cells[j].Value);
                            }
                            else
                            {
                                firstMatrix[i, j] = Convert.ToDouble(dataGridView1.Rows[i].Cells[j].Value) * -1;
                            }
                        }
                    }

                    // Добавляем в массив единицы базисных переменных
                    for (int i = 0; i < bounds; i++)
                    {
                        firstMatrix[i, variables + i] = 1;
                    }

                    // Добавляем в массив ограничения.
                    for (int i = 0; i < bounds; i++)
                    {
                        if (dataGridView1.Rows[i].Cells[variables].Value.ToString() == "<=")
                        {
                            firstMatrix[i, y - 1] = Convert.ToDouble(dataGridView1.Rows[i].Cells[dataGridView1.Columns.Count - 1].Value);
                        }
                        else
                        {
                            firstMatrix[i, y - 1] = Convert.ToDouble(dataGridView1.Rows[i].Cells[dataGridView1.Columns.Count - 1].Value) * -1;
                        }
                    }

                    // Добавляем в массив исходное уравнение (с обратными знаками!).
                    for (int j = 0; j < variables; j++)
                    {
                        if (comboBox1.SelectedItem.Equals("Min")) firstMatrix[x - 1, j] = Convert.ToDouble(dataGridView2.Rows[0].Cells[j].Value);
                        else firstMatrix[x - 1, j] = Convert.ToDouble(dataGridView2.Rows[0].Cells[j].Value) * -1;
                    }

                    String matrix = "Начальная матрица\n";
                    for (int i = 0; i < x; i++)
                    {
                        for (int j = 0; j < y; j++)
                            matrix += firstMatrix[i, j] + " ";
                        matrix += "\n";
                    }
                    MessageBox.Show(matrix);

                    // Проверяем, все ли элементы функции L положительны.
                    for (int j = 0; j < y - 1; j++)
                    {
                        if (firstMatrix[x - 1, j] < 0) check = false;
                    }
                    if (check)
                    {
                        MessageBox.Show("Целевая функция L = 0.");
                        return;
                    }
                }

                else firstMatrix = mainMatrix;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

                tempMinColumn = minCollumnIndex;
                tempMinRow = minRowIndex;

                // Проверяем, все ли элементы функции L положительны.
                check = true;
                for (int j = 0; j < y - 1; j++)
                {
                    if (firstMatrix[x - 1, j] < 0) check = false;
                }


                // Если да, то задача решена.
                if (check)
                {
                    if (comboBox1.SelectedItem.Equals("Min")) L = firstMatrix[x - 1, y - 1] * -1;
                    else L = firstMatrix[x - 1, y - 1];
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

        // Указываем количество переменных.
        private void variablesNumber_TextChanged(object sender, EventArgs e)
        {
            DataGridViewComboBoxColumn cbEqual = new DataGridViewComboBoxColumn();
            cbEqual.DataSource = eqData;
            try
            {
                if (variablesNumber.Text != "" && Convert.ToInt32(variablesNumber.Text) > 0)
                {
                    dataGridView1.Columns.Clear();
                    dataGridView2.Columns.Clear();

                    for (int i = 0; i < Convert.ToInt32(variablesNumber.Text); i++)
                    {
                        string name = "x" + (i + 1);
                        dataGridView1.Columns.Add(name, name);
                        dataGridView2.Columns.Add(name, name);
                        dataGridView2.Rows.Clear();
                        dataGridView2.Rows.Add(1);
                    }
                    dataGridView1.Columns.Add(cbEqual);
                    dataGridView1.Columns.Add("result", "");
                }
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].Width = 50;
                }
                for (int i = 0; i < dataGridView2.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].Width = 50;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                variablesNumber.Text = "";
            }
        }

        // Указываем количество ограничений.
        private void boundsNumber_TextChanged(object sender, EventArgs e)
        {
            try
            {
                xRows = new int[Convert.ToInt32(variablesNumber.Text) + Convert.ToInt32(boundsNumber.Text) + 1];
                if (variablesNumber.Text != "" && Convert.ToInt32(variablesNumber.Text) > 0 && boundsNumber.Text != "" && Convert.ToInt32(boundsNumber.Text) > 0)
                {
                    dataGridView1.Rows.Clear();
                    dataGridView1.Rows.Add(Convert.ToInt32(boundsNumber.Text));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                boundsNumber.Text = "";
            }
        }
    }
}
