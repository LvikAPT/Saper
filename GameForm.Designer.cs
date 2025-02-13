namespace Saper.Forms
{
    partial class GameForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblFlags; // Добавляем поле для отображения флажков
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.DataGridView gameGrid;

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
            this.lblFlags = new System.Windows.Forms.Label(); // Инициализация lblFlags
            this.btnPause = new System.Windows.Forms.Button();
            this.gameGrid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gameGrid)).BeginInit();
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
            // lblFlags
            // 
            this.lblFlags.AutoSize = true;
            this.lblFlags.Location = new System.Drawing.Point(12, 30); // Позиция lblFlags
            this.lblFlags.Name = "lblFlags";
            this.lblFlags.Size = new System.Drawing.Size(0, 13);
            this.lblFlags.TabIndex = 1;
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(12, 50);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(75, 23);
            this.btnPause.TabIndex = 2;
            this.btnPause.Text = "Пауза";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // gameGrid
            // 
            this.gameGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gameGrid.Location = new System.Drawing.Point(12, 80);
            this.gameGrid.Name = "gameGrid";
            this.gameGrid.Size = new System.Drawing.Size(760, 400);
            this.gameGrid.TabIndex = 3;
            this.gameGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gameGrid_CellClick);
            this.gameGrid.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gameGrid_CellMouseDown);
            // 
            // GameForm
            // 
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.gameGrid);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.lblFlags); // Добавляем lblFlags на форму
            this.Controls.Add(this.lblTime);
            this.Name = "GameForm";
            ((System.ComponentModel.ISupportInitialize)(this.gameGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}