namespace Gestor_De_Pule
{
    partial class Main
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
            menuStrip1 = new MenuStrip();
            arquivoToolStripMenuItem = new ToolStripMenuItem();
            animalToolStripMenuItem = new ToolStripMenuItem();
            apostadorToolStripMenuItem = new ToolStripMenuItem();
            puleToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { arquivoToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // arquivoToolStripMenuItem
            // 
            arquivoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { animalToolStripMenuItem, apostadorToolStripMenuItem, puleToolStripMenuItem });
            arquivoToolStripMenuItem.Name = "arquivoToolStripMenuItem";
            arquivoToolStripMenuItem.Size = new Size(71, 20);
            arquivoToolStripMenuItem.Text = "Cadastros";
            // 
            // animalToolStripMenuItem
            // 
            animalToolStripMenuItem.Name = "animalToolStripMenuItem";
            animalToolStripMenuItem.Size = new Size(180, 22);
            animalToolStripMenuItem.Text = "Animal";
            animalToolStripMenuItem.Click += JanelaCadastro;
            // 
            // apostadorToolStripMenuItem
            // 
            apostadorToolStripMenuItem.Name = "apostadorToolStripMenuItem";
            apostadorToolStripMenuItem.Size = new Size(180, 22);
            apostadorToolStripMenuItem.Text = "Apostador";
            apostadorToolStripMenuItem.Click += OpenWindowApostadoresCadastrados;
            // 
            // puleToolStripMenuItem
            // 
            puleToolStripMenuItem.Name = "puleToolStripMenuItem";
            puleToolStripMenuItem.Size = new Size(180, 22);
            puleToolStripMenuItem.Text = "Pule";
            puleToolStripMenuItem.Click += WindowPuleCadastrados;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Main";
            Text = "Gestor Pule";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem arquivoToolStripMenuItem;
        private ToolStripMenuItem animalToolStripMenuItem;
        private ToolStripMenuItem apostadorToolStripMenuItem;
        private ToolStripMenuItem puleToolStripMenuItem;
    }
}
