partial class PauseForm
{
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.Button btnContinue;
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
        this.btnContinue = new System.Windows.Forms.Button();
        this.btnMainMenu = new System.Windows.Forms.Button();
        this.SuspendLayout();

        // 
        // btnContinue
        // 
        this.btnContinue.Location = new System.Drawing.Point(50, 30);
        this.btnContinue.Name = "btnContinue";
        this.btnContinue.Size = new System.Drawing.Size(200, 30);
        this.btnContinue.TabIndex = 0;
        this.btnContinue.Text = "Продолжить игру";
        this.btnContinue.UseVisualStyleBackColor = true;
        this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);

        // 
        // btnMainMenu
        // 
        this.btnMainMenu.Location = new System.Drawing.Point(50, 70);
        this.btnMainMenu.Name = "btnMainMenu";
        this.btnMainMenu.Size = new System.Drawing.Size(200, 30);
        this.btnMainMenu.TabIndex = 1;
        this.btnMainMenu.Text = "В главное меню";
        this.btnMainMenu.UseVisualStyleBackColor = true;
        this.btnMainMenu.Click += new System.EventHandler(this.btnMainMenu_Click);

        // 
        // PauseForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(300, 150);
        this.Controls.Add(this.btnMainMenu);
        this.Controls.Add(this.btnContinue);
        this.Name = "PauseForm";
        this.Text = "Пауза";
        this.ResumeLayout(false);
    }
}