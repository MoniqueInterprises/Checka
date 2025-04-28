namespace Checka
{
    partial class CheckJoao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheckJoao));
            dataGridView1 = new DataGridView();
            dataGridView2 = new DataGridView();
            label1 = new Label();
            label2 = new Label();
            BuscarData = new Button();
            Data = new Button();
            monthCalendar2 = new MonthCalendar();
            monthCalendar1 = new MonthCalendar();
            label3 = new Label();
            Pesquisar = new Button();
            SearchBox = new TextBox();
            label4 = new Label();
            Refresh = new Button();
            LoadJoao = new Button();
            TotalJoao = new Label();
            TotalPortal = new Label();
            Diff = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 256);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(931, 907);
            dataGridView1.TabIndex = 0;
            dataGridView1.DataBindingComplete += dataGridView1_DataBindingComplete;
            dataGridView1.Sorted += dataGridView1_Sorted;
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(1035, 256);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.Size = new Size(777, 907);
            dataGridView2.TabIndex = 1;
            dataGridView2.Sorted += dataGridView2_Sorted;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 213);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 2;
            label1.Text = "Portal";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(1040, 213);
            label2.Name = "label2";
            label2.Size = new Size(31, 15);
            label2.TabIndex = 3;
            label2.Text = "João";
            // 
            // BuscarData
            // 
            BuscarData.Location = new Point(820, 186);
            BuscarData.Name = "BuscarData";
            BuscarData.Size = new Size(75, 23);
            BuscarData.TabIndex = 25;
            BuscarData.Text = "Buscar";
            BuscarData.UseVisualStyleBackColor = true;
            BuscarData.Visible = false;
            BuscarData.Click += BuscarData_Click;
            // 
            // Data
            // 
            Data.Location = new Point(336, 12);
            Data.Name = "Data";
            Data.Size = new Size(75, 23);
            Data.TabIndex = 23;
            Data.Text = "Data";
            Data.UseVisualStyleBackColor = true;
            Data.Click += Data_Click;
            // 
            // monthCalendar2
            // 
            monthCalendar2.Location = new Point(668, 12);
            monthCalendar2.Name = "monthCalendar2";
            monthCalendar2.TabIndex = 22;
            monthCalendar2.Visible = false;
            // 
            // monthCalendar1
            // 
            monthCalendar1.Location = new Point(423, 12);
            monthCalendar1.Name = "monthCalendar1";
            monthCalendar1.TabIndex = 21;
            monthCalendar1.Visible = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 238);
            label3.Name = "label3";
            label3.Size = new Size(44, 15);
            label3.TabIndex = 20;
            label3.Text = "Linhas:";
            // 
            // Pesquisar
            // 
            Pesquisar.Location = new Point(255, 12);
            Pesquisar.Name = "Pesquisar";
            Pesquisar.Size = new Size(75, 23);
            Pesquisar.TabIndex = 19;
            Pesquisar.Text = "PESQUISAR";
            Pesquisar.UseVisualStyleBackColor = true;
            Pesquisar.Click += Pesquisar_Click;
            // 
            // SearchBox
            // 
            SearchBox.Location = new Point(12, 12);
            SearchBox.Name = "SearchBox";
            SearchBox.Size = new Size(237, 23);
            SearchBox.TabIndex = 18;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(1040, 238);
            label4.Name = "label4";
            label4.Size = new Size(44, 15);
            label4.TabIndex = 26;
            label4.Text = "Linhas:";
            // 
            // Refresh
            // 
            Refresh.Image = Properties.Resources.Refresh;
            Refresh.Location = new Point(12, 41);
            Refresh.Name = "Refresh";
            Refresh.Size = new Size(75, 75);
            Refresh.TabIndex = 27;
            Refresh.Text = "Refresh";
            Refresh.TextAlign = ContentAlignment.BottomCenter;
            Refresh.UseVisualStyleBackColor = true;
            Refresh.Click += Refresh_Click;
            // 
            // LoadJoao
            // 
            LoadJoao.Image = Properties.Resources.Refresh;
            LoadJoao.Location = new Point(949, 256);
            LoadJoao.Name = "LoadJoao";
            LoadJoao.Size = new Size(80, 75);
            LoadJoao.TabIndex = 28;
            LoadJoao.Text = "LOAD JOÃO";
            LoadJoao.TextAlign = ContentAlignment.BottomCenter;
            LoadJoao.UseVisualStyleBackColor = true;
            LoadJoao.Click += LoadJoao_Click;
            // 
            // TotalJoao
            // 
            TotalJoao.AutoSize = true;
            TotalJoao.Location = new Point(1139, 238);
            TotalJoao.Name = "TotalJoao";
            TotalJoao.Size = new Size(36, 15);
            TotalJoao.TabIndex = 29;
            TotalJoao.Text = "Total:";
            // 
            // TotalPortal
            // 
            TotalPortal.AutoSize = true;
            TotalPortal.Location = new Point(111, 238);
            TotalPortal.Name = "TotalPortal";
            TotalPortal.Size = new Size(36, 15);
            TotalPortal.TabIndex = 30;
            TotalPortal.Text = "Total:";
            // 
            // Diff
            // 
            Diff.AutoSize = true;
            Diff.Location = new Point(531, 238);
            Diff.Name = "Diff";
            Diff.Size = new Size(60, 15);
            Diff.TabIndex = 31;
            Diff.Text = "Diferença:";
            // 
            // CheckJoao
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1887, 1175);
            Controls.Add(Diff);
            Controls.Add(TotalPortal);
            Controls.Add(TotalJoao);
            Controls.Add(LoadJoao);
            Controls.Add(Refresh);
            Controls.Add(label4);
            Controls.Add(BuscarData);
            Controls.Add(Data);
            Controls.Add(monthCalendar2);
            Controls.Add(monthCalendar1);
            Controls.Add(label3);
            Controls.Add(Pesquisar);
            Controls.Add(SearchBox);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dataGridView2);
            Controls.Add(dataGridView1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "CheckJoao";
            Text = "CheckJoao";
            Load += CheckJoao_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private DataGridView dataGridView2;
        private Label label1;
        private Label label2;
        private Button BuscarData;
        private Button Data;
        private MonthCalendar monthCalendar2;
        private MonthCalendar monthCalendar1;
        private Label label3;
        private Button Pesquisar;
        private TextBox SearchBox;
        private Label label4;
        private Button Refresh;
        private Button LoadJoao;
        private Label TotalJoao;
        private Label TotalPortal;
        private Label Diff;
    }
}