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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

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
            InitilizeValue();
        }

        private void InitilizeValue()
        {
            //addColumn(8);
            
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            arrX = new double[dataX.ColumnCount];
            arrY = new double[dataX.ColumnCount];

            for (int i = 0; i < dataX.RowCount; i++)
            {
                for (int j = 0; j < dataX.ColumnCount; j++)
                {
                    if (i != 1)
                        arrX[j] = Convert.ToDouble((dataX[j, i].Value.ToString()));
                    else
                        arrY[j] = Convert.ToDouble((dataX[j, i].Value.ToString()));
                }
            }

            // Срееднее по Х и Y
            var averageX = arrX.Average();
            var averageY = arrY.Average();

            var multyAverageXY = averageX * averageY;

            var averageXxY = SumAllXY(arrX, arrY);

            var sumX2 = Average2Collection(arrX);
            var sumY2 = Average2Collection(arrY);

            var standardDeviationX = Math.Sqrt(sumX2 - averageX * averageX);
            var standardDeviationY = Math.Sqrt(sumY2 - averageY * averageY);

            var correlationCoefficient = (averageXxY - multyAverageXY) / (standardDeviationX * standardDeviationY);

            // Посчитали по Y*X
            var PyxResult = Pyx(averageXxY, multyAverageXY, standardDeviationX);
            var ByxResult = Byx(averageY, averageX, PyxResult);

            // Посчитали по X*Y
            var PxyResult = Pyx(averageXxY, multyAverageXY, standardDeviationY);
            var BxyResult = Byx(averageX, averageY, PxyResult);

            var Yx = LinearRegrassion(PyxResult, ByxResult, arrX);
            var Xy = LinearRegrassion(PxyResult, BxyResult, arrY);

            OutData(correlationCoefficient);
            OutResultFunctionYx(PyxResult, ByxResult);
            OutResultFunctionXy(PxyResult, BxyResult);
            DrawChart(arrX, Yx, Xy, arrY);
        }

        private void OutResultFunctionYx(double P, double B)
        {
            Yx.Text = String.Format("{0:F3}*y + ({1:F3})", P, B);
        }

        private void OutResultFunctionXy(double P, double B)
        {
            Xy.Text = String.Format("{0:F3}*y + ({1:F3})", P, B);
        }

        //Значения линейной регрессии
        private double[] LinearRegrassion(double P, double B, double[] X)
        {
            var valueLinearRegression = new double[X.Length];
            for (var i = 0; i < X.Length; i++)
            {
                valueLinearRegression[i] = P * X[i] + B;
            }
            return valueLinearRegression;
        }


        // Коэффициент Р линейной регрессии
        private double Pyx(double averageXxY, double multyAverageXY, double standardDeviation)
        {
            return (averageXxY - multyAverageXY) / (standardDeviation * standardDeviation);
        }

        // Коэффициент B линейной регрессии
        private double Byx(double averageY, double averageX, double PLinear)
        {
            return averageY - averageX * PLinear;
        }

        // Произведение XY
        private double SumAllXY(double[] arrX, double[] arrY)
        {
            var XxY = new double[arrX.Length];
            for (var i = 0; i < arrX.Length; i++)
            {
                XxY[i] = arrX[i] * arrY[i];
            }

            return XxY.Average();
        }

        //Средне квадратическое отклонение
        private double Average2Collection(double[] arr)
        {
            var arr2 = new double[arr.Length];
            for (var i = 0; i < arr.Length; i++)
            {
                arr2[i] = arr[i] * arr[i];
            }

            return arr2.Average();
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

        public void OutData(double coefrValue)
        {
            coefr.Text = String.Format("{0:F3}", coefrValue);
        }

        public void DrawChart(double[] X, double[] Yx, double[] Xy, double[] Y)
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
            Series XY_Point = new Series();
            Series Yx_line = new Series();
            Series Xy_line = new Series();
            XY_Point.ChartType = SeriesChartType.Point;
            Yx_line.ChartType = SeriesChartType.Line;
            Xy_line.ChartType = SeriesChartType.Line;
            XY_Point.Color = Color.Red;
            Yx_line.Color = Color.Green;
            Xy_line.Color = Color.Blue;
            Yx_line.LegendText = "Линейная регрессия Yx";
            XY_Point.LegendText = "Эксперименты(точки)";
            Xy_line.LegendText = "Линейная регрессия Xy";
            // Добавляем точки в Series
            for (int i = 0; i < X.Length; i++)
            {
                XY_Point.Points.AddXY(X[i], Y[i]);
                Yx_line.Points.AddXY(X[i], Yx[i]);
                Xy_line.Points.AddXY(Xy[i], Y[i]);
            }

            if (chart1.Series.Count > 0)
            {
                chart1.Series.Clear();
            };

            // Добавляем Series в Chart
            chart1.Series.Add(XY_Point);
            chart1.Series.Add(Yx_line);
            chart1.Series.Add(Xy_line);
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

        private void addColumn(int colNum)
        {
            if (colNum != 0)
            {
                for (int i = 0; i < colNum; i++)
                {
                    DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                    column.HeaderText = "I" + (i + 1);
                    column.Width = 40;
                    dataX.Columns.Add(column);
                };
                // Добавляем две строки в DataGridView
                DataGridViewRow rowX = new DataGridViewRow();
                DataGridViewRow rowY = new DataGridViewRow();
                rowX.CreateCells(dataX);
                rowX.HeaderCell.Value = "X";
                rowY.CreateCells(dataX);
                rowY.HeaderCell.Value = "Y";
                dataX.Rows.Add(rowX);
                dataX.Rows.Add(rowY);
            }
        }

        private void ExpNumCount_ValueChanged(object sender, EventArgs e)
        {
            int ColNum = Convert.ToInt32(ExpNumCount.Value.ToString());
            // Удаляем все столбцы из dataX
            for (int i = dataX.Columns.Count - 1; i >= 0; i--)
            {
                dataX.Columns.Remove(dataX.Columns[i]);
            }
            addColumn(ColNum);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
