namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btn1 = new System.Windows.Forms.Button();
            this.ExpNumCount = new System.Windows.Forms.NumericUpDown();
            this.lbCounter = new System.Windows.Forms.Label();
            this.grExpData = new System.Windows.Forms.GroupBox();
            this.dataX = new System.Windows.Forms.DataGridView();
            this.ExpNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.r = new System.Windows.Forms.Label();
            this.coefr = new System.Windows.Forms.TextBox();
            this.Yx = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Xy = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExpNumCount)).BeginInit();
            this.grExpData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataX)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(567, 12);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(919, 511);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // btn1
            // 
            this.btn1.Location = new System.Drawing.Point(12, 223);
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(75, 23);
            this.btn1.TabIndex = 27;
            this.btn1.Text = "Посчитать";
            this.btn1.UseVisualStyleBackColor = true;
            this.btn1.Click += new System.EventHandler(this.btn1_Click);
            // 
            // ExpNumCount
            // 
            this.ExpNumCount.Location = new System.Drawing.Point(138, 18);
            this.ExpNumCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ExpNumCount.Name = "ExpNumCount";
            this.ExpNumCount.Size = new System.Drawing.Size(51, 20);
            this.ExpNumCount.TabIndex = 29;
            this.ExpNumCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ExpNumCount.ValueChanged += new System.EventHandler(this.ExpNumCount_ValueChanged);
            // 
            // lbCounter
            // 
            this.lbCounter.AutoSize = true;
            this.lbCounter.Cursor = System.Windows.Forms.Cursors.Default;
            this.lbCounter.Location = new System.Drawing.Point(12, 12);
            this.lbCounter.Name = "lbCounter";
            this.lbCounter.Size = new System.Drawing.Size(120, 26);
            this.lbCounter.TabIndex = 28;
            this.lbCounter.Text = "Количество \r\nданных эксперимента";
            // 
            // grExpData
            // 
            this.grExpData.Controls.Add(this.dataX);
            this.grExpData.Font = new System.Drawing.Font("Georgia", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.grExpData.Location = new System.Drawing.Point(12, 56);
            this.grExpData.Name = "grExpData";
            this.grExpData.Size = new System.Drawing.Size(535, 161);
            this.grExpData.TabIndex = 30;
            this.grExpData.TabStop = false;
            this.grExpData.Text = "Данные эксперимента";
            // 
            // dataX
            // 
            this.dataX.AllowUserToAddRows = false;
            this.dataX.AllowUserToDeleteRows = false;
            this.dataX.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataX.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ExpNum});
            this.dataX.Location = new System.Drawing.Point(6, 31);
            this.dataX.Name = "dataX";
            this.dataX.Size = new System.Drawing.Size(509, 114);
            this.dataX.TabIndex = 0;
            // 
            // ExpNum
            // 
            this.ExpNum.Frozen = true;
            this.ExpNum.HeaderText = "i";
            this.ExpNum.Name = "ExpNum";
            this.ExpNum.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ExpNum.ToolTipText = "i-е значение";
            this.ExpNum.Width = 25;
            // 
            // r
            // 
            this.r.AutoSize = true;
            this.r.Location = new System.Drawing.Point(9, 273);
            this.r.Name = "r";
            this.r.Size = new System.Drawing.Size(140, 13);
            this.r.TabIndex = 31;
            this.r.Text = "Коэффициент корреляции";
            // 
            // coefr
            // 
            this.coefr.Location = new System.Drawing.Point(12, 289);
            this.coefr.Name = "coefr";
            this.coefr.Size = new System.Drawing.Size(137, 20);
            this.coefr.TabIndex = 32;
            // 
            // Yx
            // 
            this.Yx.Location = new System.Drawing.Point(12, 344);
            this.Yx.Name = "Yx";
            this.Yx.Size = new System.Drawing.Size(137, 20);
            this.Yx.TabIndex = 34;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 328);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 13);
            this.label1.TabIndex = 33;
            this.label1.Text = "Уравнение Лин. Рег. Yx";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // Xy
            // 
            this.Xy.Location = new System.Drawing.Point(12, 394);
            this.Xy.Name = "Xy";
            this.Xy.Size = new System.Drawing.Size(137, 20);
            this.Xy.TabIndex = 36;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 378);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 13);
            this.label2.TabIndex = 35;
            this.label2.Text = "Уравнение Лин. Рег. Xy";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1522, 673);
            this.Controls.Add(this.Xy);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Yx);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.coefr);
            this.Controls.Add(this.r);
            this.Controls.Add(this.grExpData);
            this.Controls.Add(this.ExpNumCount);
            this.Controls.Add(this.lbCounter);
            this.Controls.Add(this.btn1);
            this.Controls.Add(this.chart1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExpNumCount)).EndInit();
            this.grExpData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataX)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button btn1;
        private System.Windows.Forms.NumericUpDown ExpNumCount;
        private System.Windows.Forms.Label lbCounter;
        private System.Windows.Forms.GroupBox grExpData;
        private System.Windows.Forms.DataGridView dataX;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExpNum;
        private System.Windows.Forms.Label r;
        private System.Windows.Forms.TextBox coefr;
        private System.Windows.Forms.TextBox Yx;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Xy;
        private System.Windows.Forms.Label label2;
    }
}

