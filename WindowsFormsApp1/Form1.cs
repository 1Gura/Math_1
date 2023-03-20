using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        const int n = 5;
        double[] arrY;
        double[] arrX;
        double[] phi1;
        double[] phi2;

        public Form1()
        {
            InitializeComponent();
            SetDefaultValue();
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            double a0, a1, a2 = 0.0;
            arrX = new double[n];
            arrY = new double[n];
            GetDataForm();
            phi1 = SquareX(arrX);
            phi2 = Lnx(arrX);

            LeastSquares(arrX, arrY, phi1, phi2, out a0, out a1, out a2);
            double[] appY = CountApprY(a0, a1, a2, phi1, phi2, arrX.Length);

            OutData(a0, a1, a2);
            DrawChart(arrX, arrY, appY);
        }

        private void GetDataForm()
        {
            arrX[0] = Double.Parse(textBox1.Text);
            arrX[1] = Double.Parse(textBox2.Text);
            arrX[2] = Double.Parse(textBox3.Text);
            arrX[3] = Double.Parse(textBox4.Text);
            arrX[4] = Double.Parse(textBox5.Text);

            arrY[0] = Double.Parse(textBox10.Text);
            arrY[1] = Double.Parse(textBox9.Text);
            arrY[2] = Double.Parse(textBox8.Text);
            arrY[3] = Double.Parse(textBox7.Text);
            arrY[4] = Double.Parse(textBox6.Text);
        }

        private void SetDefaultValue()
        {
            textBox1.Text = "1";
            textBox2.Text = "1,2";
            textBox3.Text = "1,4";
            textBox4.Text = "1,6";
            textBox5.Text = "1,8";

            textBox10.Text = "11,9";
            textBox9.Text = "12,3";
            textBox8.Text = "12,5";
            textBox7.Text = "13,1";
            textBox6.Text = "13,3";
        }
        //Метод наименьших квадратов
        public void LeastSquares(double[] x, double[] y, double[] phi1, double[] phi2, out double a0, out double a1, out double a2)
        {
            // Построение матрицы X и вектора y на основе данных
            int n = x.Length;
            double[,] X = new double[n, 3];
            double[] Y = new double[n];
            for (int i = 0; i < n; i++)
            {
                X[i, 0] = 1.0;
                X[i, 1] = phi1[i];
                X[i, 2] = phi2[i];
                Y[i] = y[i];
            }

            // Решение системы линейных уравнений X'X*a = X'y
            double[,] Xt = Transpose(X);
            double[,] XtX = Multiply(Xt, X);
            double[] XtY = MultiplyVector(Xt, Y);
            double[,] invXtX = Inverse(XtX);
            double[] a = MultiplyVector(invXtX, XtY);

            // Извлечение коэффициентов
            a0 = Math.Round(a[0], 2);
            a1 = Math.Round(a[1], 2);
            a2 = Math.Round(a[2], 2);
        }

        public double[,] Transpose(double[,] A)
        {
            int m = A.GetLength(0);
            int n = A.GetLength(1);
            double[,] B = new double[n, m];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    B[j, i] = A[i, j];
                }
            }
            return B;
        }

        public double[,] Multiply(double[,] A, double[,] B)
        {
            int m = A.GetLength(0);
            int n = A.GetLength(1);
            int p = B.GetLength(1);
            double[,] C = new double[m, p];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < p; j++)
                {
                    for (int k = 0; k < n; k++)
                    {
                        C[i, j] += A[i, k] * B[k, j];
                    }
                }
            }
            return C;
        }

        public double[] MultiplyVector(double[,] A, double[] x)
        {
            int m = A.GetLength(0);
            int n = A.GetLength(1);
            double[] y = new double[m];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    y[i] += A[i, j] * x[j];
                }
            }
            return y;
        }

        public double[,] Inverse(double[,] A)
        {
            int n = A.GetLength(0);
            double[,] B = new double[n, n * 2];

            // Создание расширенной матрицы
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    B[i, j] = A[i, j];
                }
                B[i, n + i] = 1.0;
            }

            // Прямой ход метода Гаусса
            for (int i = 0; i < n; i++)
            {
                // Поиск максимального элемента в столбце i
                int maxRow = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (Math.Abs(B[j, i]) > Math.Abs(B[maxRow, i]))
                    {
                        maxRow = j;
                    }
                }

                // Обмен строк i и maxRow
                if (maxRow != i)
                {
                    for (int j = 0; j < n * 2; j++)
                    {
                        double temp = B[i, j];
                        B[i, j] = B[maxRow, j];
                        B[maxRow, j] = temp;
                    }
                }

                // Нормализация строки i
                double pivot = B[i, i];
                if (pivot == 0.0)
                {
                    return null;
                }
                for (int j = i; j < n * 2; j++)
                {
                    B[i, j] /= pivot;
                }

                // Вычитание i-й строки из всех строк, кроме i-й
                for (int j = 0; j < n; j++)
                {
                    if (j != i)
                    {
                        double factor = B[j, i];
                        for (int k = i; k < n * 2; k++)
                        {
                            B[j, k] -= factor * B[i, k];
                        }
                    }
                }
            }

            // Извлечение обратной матрицы
            double[,] invA = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    invA[i, j] = B[i, n + j];
                }
            }

            return invA;
        }

        private double[] CountApprY(double a0, double a1, double a2, double[] phi1, double[] phi2, int NumX)
        {
            double[] apprY = new double[NumX];
            for (int i = 0; i < NumX; i++)
            {
                apprY[i] = a0 + a1 * phi1[i] + a2 * phi2[i];
            }
            return apprY;
        }

        public void OutData(double a0, double a1, double a2)
        {
            textA0.Text = a0.ToString();
            textA1.Text = a1.ToString();
            textA2.Text = a2.ToString();
        }

        public void DrawChart(double[] X, double[] Y, double[] Y1)
        {

            // Устанавливаем размеры и местоположение Chart
            chart1.Size = new Size(800, 600);
            //chart1.Location = new Point(10, 10);

            // Создаем новую область Chart
            ChartArea chartArea1 = new ChartArea();
            if (chart1.ChartAreas.Count > 0)
            {
                chart1.ChartAreas.Clear();
            };
            chart1.ChartAreas.Add(chartArea1);

            // Создаем новый объект Series для хранения точек графика
            Series series1_Point = new Series();
            Series series1_Line = new Series();
            Series series2 = new Series();
            series1_Point.ChartType = SeriesChartType.Point;
            series1_Line.ChartType = SeriesChartType.Line;
            series2.ChartType = SeriesChartType.Line;
            series1_Point.Color = Color.Red;
            series1_Line.Color = Color.Red;
            series2.Color = Color.Blue;
            series1_Line.LegendText = "Эксперименты";
            series1_Point.LegendText = "Эксперименты(точки)";
            series2.LegendText = "Результаты аппроксимации";
            // Добавляем точки в Series
            for (int i = 0; i < X.Length; i++)
            {
                series1_Point.Points.AddXY(X[i], Y[i]);
                series1_Line.Points.AddXY(X[i], Y[i]);
                series2.Points.AddXY(X[i], Y1[i]);
            }

            if (chart1.Series.Count > 0)
            {
                chart1.Series.Clear();
            };

            // Добавляем Series в Chart
            chart1.Series.Add(series1_Point);
            chart1.Series.Add(series1_Line);
            chart1.Series.Add(series2);
        }

        #region Функции которые могут быть расчитаны в программе
        private double[] SquareX(double[] arX)
        {
            double[] phi = new double[arX.Length];
            for (int i = 0; i < arX.Length - 1; i++)
            {
                //здесь должна быть обработка на <0 но мне лень
                phi[i] = Math.Sqrt(arX[i]);
            }
            return phi;
        }

        private double[] MultX(double[] arX)
        {
            double[] phi = new double[arX.Length];
            for (int i = 0; i < arX.Length - 1; i++)
            {
                //здесь должна быть обработка на <0 но мне лень
                phi[i] = arX[i] * arX[i];
            }
            return phi;
        }
        private double[] DivX(double[] arX)
        {
            double[] phi = new double[arX.Length];
            for (int i = 0; i < arX.Length; i++)
            {
                phi[i] = 1.0 / (arX[i] + 1);
            }
            return phi;
        }

        private double[] Lnx(double[] arX)
        {
            double[] phi = new double[arX.Length];
            for (int i = 0; i < arX.Length - 1; i++)
            {
                phi[i] = Math.Log(arX[i]);
            }
            return phi;
        }

        #endregion
    }
}
