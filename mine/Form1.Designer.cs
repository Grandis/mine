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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.simplexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TwoDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
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
            this.listNbl.Size = new System.Drawing.Size(76, 134);
            this.listNbl.TabIndex = 1;
            this.listNbl.SelectedIndexChanged += new System.EventHandler(this.listNbl_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listHorizont);
            this.groupBox1.Location = new System.Drawing.Point(12, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(113, 158);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Номер горизонта";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listNbl);
            this.groupBox2.Location = new System.Drawing.Point(132, 27);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(88, 158);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Номер блока";
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.dataGridView1.Location = new System.Drawing.Point(12, 191);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(622, 177);
            this.dataGridView1.TabIndex = 4;
            // 
            // NSK
            // 
            this.NSK.HeaderText = "Скважина";
            this.NSK.Name = "NSK";
            this.NSK.ReadOnly = true;
            this.NSK.Width = 60;
            // 
            // X
            // 
            this.X.HeaderText = "X";
            this.X.Name = "X";
            this.X.ReadOnly = true;
            // 
            // Y
            // 
            this.Y.HeaderText = "Y";
            this.Y.Name = "Y";
            this.Y.ReadOnly = true;
            // 
            // Z
            // 
            this.Z.HeaderText = "Z";
            this.Z.Name = "Z";
            this.Z.ReadOnly = true;
            // 
            // CUOB
            // 
            this.CUOB.HeaderText = "CUOB";
            this.CUOB.Name = "CUOB";
            this.CUOB.ReadOnly = true;
            this.CUOB.Width = 50;
            // 
            // CUOK
            // 
            this.CUOK.HeaderText = "CUOK";
            this.CUOK.Name = "CUOK";
            this.CUOK.ReadOnly = true;
            this.CUOK.Width = 50;
            // 
            // MOOB
            // 
            this.MOOB.HeaderText = "MOOB";
            this.MOOB.Name = "MOOB";
            this.MOOB.ReadOnly = true;
            this.MOOB.Width = 50;
            // 
            // MOSF
            // 
            this.MOSF.HeaderText = "MOSF";
            this.MOSF.Name = "MOSF";
            this.MOSF.ReadOnly = true;
            this.MOSF.Width = 50;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TwoDToolStripMenuItem,
            this.dToolStripMenuItem1,
            this.simplexToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(646, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // simplexToolStripMenuItem
            // 
            this.simplexToolStripMenuItem.Image = global::mine.Properties.Resources.function;
            this.simplexToolStripMenuItem.Name = "simplexToolStripMenuItem";
            this.simplexToolStripMenuItem.Size = new System.Drawing.Size(91, 20);
            this.simplexToolStripMenuItem.Text = "Симплекс";
            this.simplexToolStripMenuItem.Click += new System.EventHandler(this.simplexToolStripMenuItem_Click);
            // 
            // TwoDToolStripMenuItem
            // 
            this.TwoDToolStripMenuItem.Image = global::mine.Properties.Resources._2d;
            this.TwoDToolStripMenuItem.Name = "TwoDToolStripMenuItem";
            this.TwoDToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.TwoDToolStripMenuItem.Text = "2D";
            this.TwoDToolStripMenuItem.Click += new System.EventHandler(this.TwoDToolStripMenuItem_Click);
            // 
            // dToolStripMenuItem1
            // 
            this.dToolStripMenuItem1.Image = global::mine.Properties.Resources._3d;
            this.dToolStripMenuItem1.Name = "dToolStripMenuItem1";
            this.dToolStripMenuItem1.Size = new System.Drawing.Size(49, 20);
            this.dToolStripMenuItem1.Text = "3D";
            this.dToolStripMenuItem1.Click += new System.EventHandler(this.dToolStripMenuItem1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(646, 380);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(662, 418);
            this.Name = "Form1";
            this.Text = "Работа с БД";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem TwoDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem simplexToolStripMenuItem;
    }
}

