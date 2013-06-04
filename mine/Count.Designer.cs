namespace mine
{
    partial class Count
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.результатыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.исходныеДанныеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.staticDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.NAIM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.INFO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VAL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dGOR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dZATR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dPPL1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dPPL2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dQ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dALPHA1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.staticDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.результатыToolStripMenuItem,
            this.исходныеДанныеToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(798, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // результатыToolStripMenuItem
            // 
            this.результатыToolStripMenuItem.Name = "результатыToolStripMenuItem";
            this.результатыToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.результатыToolStripMenuItem.Text = "Результаты";
            this.результатыToolStripMenuItem.Click += new System.EventHandler(this.результатыToolStripMenuItem_Click);
            // 
            // исходныеДанныеToolStripMenuItem
            // 
            this.исходныеДанныеToolStripMenuItem.Name = "исходныеДанныеToolStripMenuItem";
            this.исходныеДанныеToolStripMenuItem.Size = new System.Drawing.Size(118, 20);
            this.исходныеДанныеToolStripMenuItem.Text = "Исходные данные";
            this.исходныеДанныеToolStripMenuItem.Click += new System.EventHandler(this.исходныеДанныеToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.staticDataGridView);
            this.groupBox1.Location = new System.Drawing.Point(12, 261);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(565, 156);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Таблица STATIC";
            this.groupBox1.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Location = new System.Drawing.Point(12, 27);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(774, 228);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Таблица DATA";
            this.groupBox2.Visible = false;
            // 
            // staticDataGridView
            // 
            this.staticDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.staticDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.staticDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NAIM,
            this.INFO,
            this.VAL});
            this.staticDataGridView.Location = new System.Drawing.Point(7, 20);
            this.staticDataGridView.Name = "staticDataGridView";
            this.staticDataGridView.Size = new System.Drawing.Size(551, 130);
            this.staticDataGridView.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dGOR,
            this.BL,
            this.dZATR,
            this.dPPL1,
            this.dPPL2,
            this.dQ,
            this.dALPHA1});
            this.dataGridView1.Location = new System.Drawing.Point(7, 20);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(761, 202);
            this.dataGridView1.TabIndex = 0;
            // 
            // NAIM
            // 
            this.NAIM.HeaderText = "Наименование";
            this.NAIM.Name = "NAIM";
            // 
            // INFO
            // 
            this.INFO.HeaderText = "Описание";
            this.INFO.Name = "INFO";
            this.INFO.Width = 300;
            // 
            // VAL
            // 
            this.VAL.HeaderText = "Значение";
            this.VAL.Name = "VAL";
            this.VAL.Width = 90;
            // 
            // dGOR
            // 
            this.dGOR.HeaderText = "Номер горизонта";
            this.dGOR.Name = "dGOR";
            // 
            // BL
            // 
            this.BL.HeaderText = "Номер блока";
            this.BL.Name = "BL";
            // 
            // dZATR
            // 
            this.dZATR.HeaderText = "Затраты";
            this.dZATR.Name = "dZATR";
            // 
            // dPPL1
            // 
            this.dPPL1.HeaderText = "Нижняя граница";
            this.dPPL1.Name = "dPPL1";
            // 
            // dPPL2
            // 
            this.dPPL2.HeaderText = "Верхняя граница";
            this.dPPL2.Name = "dPPL2";
            // 
            // dQ
            // 
            this.dQ.HeaderText = "Добыча одним экскаватором";
            this.dQ.Name = "dQ";
            // 
            // dALPHA1
            // 
            this.dALPHA1.HeaderText = "Качество";
            this.dALPHA1.Name = "dALPHA1";
            // 
            // Count
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(798, 424);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Count";
            this.Text = "Оперативный план";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Count_Paint);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.staticDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem результатыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem исходныеДанныеToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView staticDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn NAIM;
        private System.Windows.Forms.DataGridViewTextBoxColumn INFO;
        private System.Windows.Forms.DataGridViewTextBoxColumn VAL;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dGOR;
        private System.Windows.Forms.DataGridViewTextBoxColumn BL;
        private System.Windows.Forms.DataGridViewTextBoxColumn dZATR;
        private System.Windows.Forms.DataGridViewTextBoxColumn dPPL1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dPPL2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dQ;
        private System.Windows.Forms.DataGridViewTextBoxColumn dALPHA1;
    }
}