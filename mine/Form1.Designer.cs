namespace mine
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
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
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.listHorizont = new System.Windows.Forms.ListBox();
            this.listNbl = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.NSK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.X = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Z = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CUOB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CUOK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MOOB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MOSF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // listHorizont
            // 
            this.listHorizont.FormattingEnabled = true;
            this.listHorizont.Location = new System.Drawing.Point(6, 18);
            this.listHorizont.Name = "listHorizont";
            this.listHorizont.Size = new System.Drawing.Size(101, 134);
            this.listHorizont.TabIndex = 0;
            this.listHorizont.SelectedIndexChanged += new System.EventHandler(this.listHorizont_SelectedIndexChanged);
            // 
            // listNbl
            // 
            this.listNbl.FormattingEnabled = true;
            this.listNbl.Location = new System.Drawing.Point(6, 18);
            this.listNbl.Name = "listNbl";
            this.listNbl.Size = new System.Drawing.Size(60, 134);
            this.listNbl.TabIndex = 1;
            this.listNbl.SelectedIndexChanged += new System.EventHandler(this.listNbl_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listHorizont);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(113, 158);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Номер горизонта";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listNbl);
            this.groupBox2.Location = new System.Drawing.Point(132, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(72, 158);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Номер бл";
            // 
            // dataGridView1
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NSK,
            this.X,
            this.Y,
            this.Z,
            this.CUOB,
            this.CUOK,
            this.MOOB,
            this.MOSF});
            this.dataGridView1.Location = new System.Drawing.Point(12, 176);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(622, 177);
            this.dataGridView1.TabIndex = 4;
            // 
            // NSK
            // 
            this.NSK.HeaderText = "Скважина";
            this.NSK.Name = "NSK";
            this.NSK.Width = 60;
            // 
            // X
            // 
            this.X.HeaderText = "X";
            this.X.Name = "X";
            // 
            // Y
            // 
            this.Y.HeaderText = "Y";
            this.Y.Name = "Y";
            // 
            // Z
            // 
            this.Z.HeaderText = "Z";
            this.Z.Name = "Z";
            // 
            // CUOB
            // 
            this.CUOB.HeaderText = "CUOB";
            this.CUOB.Name = "CUOB";
            this.CUOB.Width = 50;
            // 
            // CUOK
            // 
            this.CUOK.HeaderText = "CUOK";
            this.CUOK.Name = "CUOK";
            this.CUOK.Width = 50;
            // 
            // MOOB
            // 
            this.MOOB.HeaderText = "MOOB";
            this.MOOB.Name = "MOOB";
            this.MOOB.Width = 50;
            // 
            // MOSF
            // 
            this.MOSF.HeaderText = "MOSF";
            this.MOSF.Name = "MOSF";
            this.MOSF.Width = 50;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 365);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listHorizont;
        private System.Windows.Forms.ListBox listNbl;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn NSK;
        private System.Windows.Forms.DataGridViewTextBoxColumn X;
        private System.Windows.Forms.DataGridViewTextBoxColumn Y;
        private System.Windows.Forms.DataGridViewTextBoxColumn Z;
        private System.Windows.Forms.DataGridViewTextBoxColumn CUOB;
        private System.Windows.Forms.DataGridViewTextBoxColumn CUOK;
        private System.Windows.Forms.DataGridViewTextBoxColumn MOOB;
        private System.Windows.Forms.DataGridViewTextBoxColumn MOSF;
    }
}

