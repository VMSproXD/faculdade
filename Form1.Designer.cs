namespace Automatização_De_Folha_De_Pagamento
{
    partial class TeladeAcesso
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TeladeAcesso));
            bttEntrar = new Button();
            SuspendLayout();
            // 
            // bttEntrar
            // 
            bttEntrar.Anchor = AnchorStyles.None;
            bttEntrar.AutoSize = true;
            bttEntrar.BackColor = Color.RoyalBlue;
            bttEntrar.FlatStyle = FlatStyle.Popup;
            bttEntrar.Font = new Font("Calisto MT", 12F, FontStyle.Regular, GraphicsUnit.Point);
            bttEntrar.Location = new Point(490, 429);
            bttEntrar.Name = "bttEntrar";
            bttEntrar.RightToLeft = RightToLeft.No;
            bttEntrar.Size = new Size(220, 33);
            bttEntrar.TabIndex = 0;
            bttEntrar.Text = "Entrar";
            bttEntrar.UseVisualStyleBackColor = false;
            // 
            // TeladeAcesso
            // 
            AutoScaleMode = AutoScaleMode.None;
            AutoSize = true;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1180, 685);
            Controls.Add(bttEntrar);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "TeladeAcesso";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Telade Acesso";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button bttEntrar;
    }
}