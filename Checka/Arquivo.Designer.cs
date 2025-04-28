namespace Checka
{
    partial class Arquivo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Arquivo));
            dataGridView1 = new DataGridView();
            Refresh = new Button();
            button1 = new Button();
            button2 = new Button();
            SearchBox = new TextBox();
            DeletarLinha = new Button();
            button3 = new Button();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 41);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(1129, 557);
            dataGridView1.TabIndex = 0;
            // 
            // Refresh
            // 
            Refresh.Location = new Point(12, 12);
            Refresh.Name = "Refresh";
            Refresh.Size = new Size(75, 23);
            Refresh.TabIndex = 1;
            Refresh.Text = "Refresh";
            Refresh.UseVisualStyleBackColor = true;
            Refresh.Click += Refresh_Click;
            // 
            // button1
            // 
            button1.Location = new Point(93, 12);
            button1.Name = "button1";
            button1.Size = new Size(91, 23);
            button1.TabIndex = 2;
            button1.Text = "LIMPAR TUDO";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(367, 11);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 3;
            button2.Text = "PESQUISAR";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // SearchBox
            // 
            SearchBox.Location = new Point(190, 12);
            SearchBox.Name = "SearchBox";
            SearchBox.Size = new Size(171, 23);
            SearchBox.TabIndex = 4;
            SearchBox.KeyDown += SearchBox_KeyDown;
            // 
            // DeletarLinha
            // 
            DeletarLinha.Location = new Point(448, 12);
            DeletarLinha.Name = "DeletarLinha";
            DeletarLinha.Size = new Size(100, 23);
            DeletarLinha.TabIndex = 5;
            DeletarLinha.Text = "LIMPAR LINHA";
            DeletarLinha.UseVisualStyleBackColor = true;
            DeletarLinha.Click += DeletarLinha_Click;
            // 
            // button3
            // 
            button3.Location = new Point(554, 11);
            button3.Name = "button3";
            button3.Size = new Size(100, 23);
            button3.TabIndex = 6;
            button3.Text = "RESTAURAR";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(660, 16);
            label1.Name = "label1";
            label1.Size = new Size(44, 15);
            label1.TabIndex = 12;
            label1.Text = "Linhas:";
            // 
            // Arquivo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1153, 610);
            Controls.Add(label1);
            Controls.Add(button3);
            Controls.Add(DeletarLinha);
            Controls.Add(SearchBox);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(Refresh);
            Controls.Add(dataGridView1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Arquivo";
            Text = "Arquivo";
            Resize += Arquivo_Resize;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Button Refresh;
        private Button button1;
        private Button button2;
        private TextBox SearchBox;
        private Button DeletarLinha;
        private Button button3;
        private Label label1;
    }
}