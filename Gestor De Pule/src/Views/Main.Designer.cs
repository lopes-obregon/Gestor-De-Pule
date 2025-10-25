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
            arquivoToolStripMenuItem1 = new ToolStripMenuItem();
            sairToolStripMenuItem = new ToolStripMenuItem();
            arquivoToolStripMenuItem = new ToolStripMenuItem();
            animalToolStripMenuItem = new ToolStripMenuItem();
            apostadorToolStripMenuItem = new ToolStripMenuItem();
            puleToolStripMenuItem = new ToolStripMenuItem();
            relatóriosToolStripMenuItem = new ToolStripMenuItem();
            apostadorToolStripMenuItem1 = new ToolStripMenuItem();
            animalToolStripMenuItem1 = new ToolStripMenuItem();
            puleToolStripMenuItem1 = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { arquivoToolStripMenuItem1, arquivoToolStripMenuItem, relatóriosToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1081, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // arquivoToolStripMenuItem1
            // 
            arquivoToolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { sairToolStripMenuItem });
            arquivoToolStripMenuItem1.Name = "arquivoToolStripMenuItem1";
            arquivoToolStripMenuItem1.Size = new Size(61, 20);
            arquivoToolStripMenuItem1.Text = "Arquivo";
            // 
            // sairToolStripMenuItem
            // 
            sairToolStripMenuItem.Name = "sairToolStripMenuItem";
            sairToolStripMenuItem.Size = new Size(93, 22);
            sairToolStripMenuItem.Text = "Sair";
            sairToolStripMenuItem.Click += CloseSystem;
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
            animalToolStripMenuItem.Size = new Size(129, 22);
            animalToolStripMenuItem.Text = "Animal";
            animalToolStripMenuItem.Click += JanelaCadastro;
            // 
            // apostadorToolStripMenuItem
            // 
            apostadorToolStripMenuItem.Name = "apostadorToolStripMenuItem";
            apostadorToolStripMenuItem.Size = new Size(129, 22);
            apostadorToolStripMenuItem.Text = "Apostador";
            apostadorToolStripMenuItem.Click += OpenWindowApostadoresCadastrados;
            // 
            // puleToolStripMenuItem
            // 
            puleToolStripMenuItem.Name = "puleToolStripMenuItem";
            puleToolStripMenuItem.Size = new Size(129, 22);
            puleToolStripMenuItem.Text = "Pule";
            puleToolStripMenuItem.Click += WindowPuleCadastrados;
            // 
            // relatóriosToolStripMenuItem
            // 
            relatóriosToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { apostadorToolStripMenuItem1, animalToolStripMenuItem1, puleToolStripMenuItem1 });
            relatóriosToolStripMenuItem.Name = "relatóriosToolStripMenuItem";
            relatóriosToolStripMenuItem.Size = new Size(71, 20);
            relatóriosToolStripMenuItem.Text = "Relatórios";
            // 
            // apostadorToolStripMenuItem1
            // 
            apostadorToolStripMenuItem1.Name = "apostadorToolStripMenuItem1";
            apostadorToolStripMenuItem1.Size = new Size(180, 22);
            apostadorToolStripMenuItem1.Text = "Apostador";
            apostadorToolStripMenuItem1.Click += ApostadorWindow;
            // 
            // animalToolStripMenuItem1
            // 
            animalToolStripMenuItem1.Name = "animalToolStripMenuItem1";
            animalToolStripMenuItem1.Size = new Size(180, 22);
            animalToolStripMenuItem1.Text = "Animal";
            animalToolStripMenuItem1.Click += AnimalForm;
            // 
            // puleToolStripMenuItem1
            // 
            puleToolStripMenuItem1.Name = "puleToolStripMenuItem1";
            puleToolStripMenuItem1.Size = new Size(180, 22);
            puleToolStripMenuItem1.Text = "Pule";
            puleToolStripMenuItem1.Click += WindowRelatórioPule;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1081, 593);
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
        private ColumnHeader columnHeaderNúmero;
        private ToolStripMenuItem relatóriosToolStripMenuItem;
        private ToolStripMenuItem apostadorToolStripMenuItem1;
        private ToolStripMenuItem animalToolStripMenuItem1;
        private ToolStripMenuItem arquivoToolStripMenuItem1;
        private ToolStripMenuItem sairToolStripMenuItem;
        private ToolStripMenuItem puleToolStripMenuItem1;
    }
}
