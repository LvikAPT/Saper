namespace Saper.Forms
{
    partial class GameForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Button btnPause;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTime = new System.Windows.Forms.Label();
            this.btnPause = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(12, 9);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(0, 13);
            this.lblTime.TabIndex = 0;
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(12, 30);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(75, 23);
            this.btnPause.TabIndex = 1;
            this.btnPause.Text = "Пауза";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // GameForm
            // 
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.lblTime);
            this.Name = "GameForm";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}