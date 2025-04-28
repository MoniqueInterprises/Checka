namespace Checka
{
    partial class Home
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Home));
            dataGridView1 = new DataGridView();
            Refresh = new Button();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            pictureBox1 = new PictureBox();
            SearchBox = new TextBox();
            Pesquisar = new Button();
            label1 = new Label();
            button7 = new Button();
            monthCalendar1 = new MonthCalendar();
            monthCalendar2 = new MonthCalendar();
            Data = new Button();
            EmissaoVencimentoCheck = new CheckBox();
            BuscarData = new Button();
            button8 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 164);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(1240, 413);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.CellValueChanged += dataGridView1_CellValueChanged;
            // 
            // Refresh
            // 
            Refresh.Image = Properties.Resources.Refresh;
            Refresh.Location = new Point(12, 12);
            Refresh.Name = "Refresh";
            Refresh.Size = new Size(75, 75);
            Refresh.TabIndex = 1;
            Refresh.Text = "Refresh";
            Refresh.TextAlign = ContentAlignment.BottomCenter;
            Refresh.UseVisualStyleBackColor = true;
            Refresh.Click += Refresh_Click;
            // 
            // button1
            // 
            button1.BackgroundImage = Properties.Resources.Importar;
            button1.BackgroundImageLayout = ImageLayout.Center;
            button1.Location = new Point(93, 12);
            button1.Name = "button1";
            button1.Size = new Size(75, 75);
            button1.TabIndex = 2;
            button1.Text = "IMPORTAR";
            button1.TextAlign = ContentAlignment.BottomCenter;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Image = Properties.Resources.Frete;
            button2.Location = new Point(255, 12);
            button2.Name = "button2";
            button2.Size = new Size(75, 75);
            button2.TabIndex = 3;
            button2.Text = "Frete";
            button2.TextAlign = ContentAlignment.BottomCenter;
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.BackgroundImageLayout = ImageLayout.Center;
            button3.Image = Properties.Resources.Salvar;
            button3.Location = new Point(336, 12);
            button3.Name = "button3";
            button3.Size = new Size(75, 75);
            button3.TabIndex = 4;
            button3.Text = "Salvar Alt";
            button3.TextAlign = ContentAlignment.BottomCenter;
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Image = Properties.Resources.DeletarLinha;
            button4.Location = new Point(417, 12);
            button4.Name = "button4";
            button4.Size = new Size(75, 75);
            button4.TabIndex = 5;
            button4.Text = "Del Linha";
            button4.TextAlign = ContentAlignment.BottomCenter;
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.BackgroundImage = Properties.Resources.Arquivo;
            button5.BackgroundImageLayout = ImageLayout.Center;
            button5.Location = new Point(174, 12);
            button5.Name = "button5";
            button5.Size = new Size(75, 75);
            button5.TabIndex = 6;
            button5.Text = "Arquivo";
            button5.TextAlign = ContentAlignment.BottomCenter;
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button6
            // 
            button6.Image = Properties.Resources.LimparTudo;
            button6.Location = new Point(498, 12);
            button6.Name = "button6";
            button6.Size = new Size(100, 75);
            button6.TabIndex = 7;
            button6.Text = "APAGAR TUDO";
            button6.TextAlign = ContentAlignment.BottomCenter;
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.CheckaLogo_removebg_preview;
            pictureBox1.Location = new Point(790, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(462, 146);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 8;
            pictureBox1.TabStop = false;
            // 
            // SearchBox
            // 
            SearchBox.Location = new Point(12, 135);
            SearchBox.Name = "SearchBox";
            SearchBox.Size = new Size(237, 23);
            SearchBox.TabIndex = 9;
            SearchBox.KeyDown += SearchBox_KeyDown;
            // 
            // Pesquisar
            // 
            Pesquisar.Location = new Point(255, 135);
            Pesquisar.Name = "Pesquisar";
            Pesquisar.Size = new Size(75, 23);
            Pesquisar.TabIndex = 10;
            Pesquisar.Text = "PESQUISAR";
            Pesquisar.UseVisualStyleBackColor = true;
            Pesquisar.Click += Pesquisar_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(336, 138);
            label1.Name = "label1";
            label1.Size = new Size(44, 15);
            label1.TabIndex = 11;
            label1.Text = "Linhas:";
            // 
            // button7
            // 
            button7.Location = new Point(336, 93);
            button7.Name = "button7";
            button7.Size = new Size(75, 23);
            button7.TabIndex = 12;
            button7.Text = "Pintar OK";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // monthCalendar1
            // 
            monthCalendar1.Location = new Point(417, 196);
            monthCalendar1.Name = "monthCalendar1";
            monthCalendar1.TabIndex = 13;
            monthCalendar1.Visible = false;
            // 
            // monthCalendar2
            // 
            monthCalendar2.Location = new Point(662, 196);
            monthCalendar2.Name = "monthCalendar2";
            monthCalendar2.TabIndex = 14;
            monthCalendar2.Visible = false;
            // 
            // Data
            // 
            Data.Location = new Point(417, 135);
            Data.Name = "Data";
            Data.Size = new Size(75, 23);
            Data.TabIndex = 15;
            Data.Text = "Data";
            Data.UseVisualStyleBackColor = true;
            Data.Click += Data_Click;
            // 
            // EmissaoVencimentoCheck
            // 
            EmissaoVencimentoCheck.AutoSize = true;
            EmissaoVencimentoCheck.Location = new Point(498, 137);
            EmissaoVencimentoCheck.Name = "EmissaoVencimentoCheck";
            EmissaoVencimentoCheck.Size = new Size(137, 19);
            EmissaoVencimentoCheck.TabIndex = 16;
            EmissaoVencimentoCheck.Text = "Emissão/Vencimento";
            EmissaoVencimentoCheck.UseVisualStyleBackColor = true;
            EmissaoVencimentoCheck.Visible = false;
            // 
            // BuscarData
            // 
            BuscarData.Location = new Point(814, 370);
            BuscarData.Name = "BuscarData";
            BuscarData.Size = new Size(75, 23);
            BuscarData.TabIndex = 17;
            BuscarData.Text = "Buscar";
            BuscarData.UseVisualStyleBackColor = true;
            BuscarData.Visible = false;
            BuscarData.Click += BuscarData_Click;
            // 
            // button8
            // 
            button8.BackgroundImageLayout = ImageLayout.None;
            button8.Image = Properties.Resources.pastas4;
            button8.ImageAlign = ContentAlignment.TopCenter;
            button8.Location = new Point(604, 12);
            button8.Name = "button8";
            button8.Size = new Size(75, 75);
            button8.TabIndex = 18;
            button8.Text = "BSoft";
            button8.TextAlign = ContentAlignment.BottomCenter;
            button8.UseVisualStyleBackColor = true;
            button8.Click += button8_Click;
            // 
            // Home
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1264, 589);
            Controls.Add(button8);
            Controls.Add(BuscarData);
            Controls.Add(EmissaoVencimentoCheck);
            Controls.Add(Data);
            Controls.Add(monthCalendar2);
            Controls.Add(monthCalendar1);
            Controls.Add(button7);
            Controls.Add(label1);
            Controls.Add(Pesquisar);
            Controls.Add(SearchBox);
            Controls.Add(pictureBox1);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(Refresh);
            Controls.Add(dataGridView1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Home";
            Text = "Home";
            Load += Home_Load;
            Resize += Home_Resize;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Button Refresh;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private PictureBox pictureBox1;
        private TextBox SearchBox;
        private Button Pesquisar;
        private Label label1;
        private Button button7;
        private MonthCalendar monthCalendar1;
        private MonthCalendar monthCalendar2;
        private Button Data;
        private CheckBox EmissaoVencimentoCheck;
        private Button BuscarData;
        private Button button8;
    }
}
