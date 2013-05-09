namespace mine
{
    partial class _2D
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
            this.SuspendLayout();
            // 
            // _2D
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Name = "_2D";
            this.Text = "2D";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this._2D_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this._2D_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this._2D_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this._2D_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion

    }
}