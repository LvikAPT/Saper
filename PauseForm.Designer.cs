namespace Saper.Forms
{
    partial class PauseForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnResume;
        private System.Windows.Forms.Button btnMainMenu;

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
            this.btnResume = new System.Windows.Forms.Button();
            this.btnMainMenu = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnResume
            // 
            this.btnResume.Location = new System.Drawing.Point(50, 50);
            this.btnResume.Size = new System.Drawing.Size(100, 30);
            this.btnResume.TabIndex = 0;
            this.btnResume.Text = "Возобновить";
            this.btnResume.UseVisualStyleBackColor = true;
            this.btnResume.Click += new System.EventHandler(this.btnResume_Click);
            // 
            // btnMainMenu
            // 
            this.btnMainMenu.Location = new System.Drawing.Point(50, 100);
            this.btnMainMenu.Name = "btnMainMenu";
            this.btnMainMenu.Size = new System.Drawing.Size(100, 30);
            this.btnMainMenu.TabIndex = 1;
            this.btnMainMenu.Text = "Главное меню";
            this.btnMainMenu.UseVisualStyleBackColor = true;
            this.btnMainMenu.Click += new System.EventHandler(this.btnMainMenu_Click);
            // 
            // PauseForm
            // 
            this.ClientSize = new System.Drawing.Size(200, 200);
            this.Controls.Add(this.btnResume);
            this.Controls.Add(this.btnMainMenu);
            this.Name = "PauseForm";
            this.ResumeLayout(false);
        }
    }
}