namespace Checka
{
    partial class BSoft
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BSoft));
            BSoftDataGrid = new DataGridView();
            PortalDataGrid = new DataGridView();
            AcertoDataGrid = new DataGridView();
            FreteBotao = new Button();
            RefreshBsoft = new Button();
            RefreshAcerto = new Button();
            RefreshPortal = new Button();
            SalvarBsoft = new Button();
            CrJoao = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            textBox1 = new TextBox();
            Pesquisar = new Button();
            CheckCRBsoft = new CheckBox();
            CheckAcerto = new CheckBox();
            CheckPortal = new CheckBox();
            panel1 = new Panel();
            checkBoxDtEmbarque = new CheckBox();
            checkBoxFaturamento = new CheckBox();
            checkBoxVencimento = new CheckBox();
            label4 = new Label();
            checkBoxAno = new CheckBox();
            checkBoxObs = new CheckBox();
            checkBoxTotalFrete = new CheckBox();
            checkBoxCentral = new CheckBox();
            checkBoxCodRevenda = new CheckBox();
            checkBoxTransportadora = new CheckBox();
            checkBoxFrete = new CheckBox();
            checkBoxID = new CheckBox();
            checkBoxCarga = new CheckBox();
            button1 = new Button();
            Descontos = new TextBox();
            label5 = new Label();
            button2 = new Button();
            button3 = new Button();
            ValorSomadoCRBsoft = new Label();
            ValorSomadoPortal = new Label();
            ValorSomadoAcerto = new Label();
            ValorDiff = new Label();
            button4 = new Button();
            LimparCRBsoft = new Button();
            ((System.ComponentModel.ISupportInitialize)BSoftDataGrid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PortalDataGrid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)AcertoDataGrid).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // BSoftDataGrid
            // 
            BSoftDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            BSoftDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(BSoftDataGrid, "BSoftDataGrid");
            BSoftDataGrid.Name = "BSoftDataGrid";
            BSoftDataGrid.CellClick += BSoftDataGrid_CellClick;
            BSoftDataGrid.CellDoubleClick += BSoftDataGrid_CellDoubleClick;
            BSoftDataGrid.DataBindingComplete += BSoftDataGrid_DataBindingComplete;
            // 
            // PortalDataGrid
            // 
            PortalDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            PortalDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(PortalDataGrid, "PortalDataGrid");
            PortalDataGrid.Name = "PortalDataGrid";
            PortalDataGrid.CellContentClick += PortalDataGrid_CellContentClick;
            PortalDataGrid.DataBindingComplete += PortalDataGrid_DataBindingComplete;
            // 
            // AcertoDataGrid
            // 
            AcertoDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            AcertoDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(AcertoDataGrid, "AcertoDataGrid");
            AcertoDataGrid.Name = "AcertoDataGrid";
            AcertoDataGrid.DataBindingComplete += AcertoDataGrid_DataBindingComplete;
            // 
            // FreteBotao
            // 
            FreteBotao.BackgroundImage = Properties.Resources.caminhao1;
            resources.ApplyResources(FreteBotao, "FreteBotao");
            FreteBotao.Name = "FreteBotao";
            FreteBotao.UseVisualStyleBackColor = true;
            FreteBotao.Click += FreteBotao2_Click;
            // 
            // RefreshBsoft
            // 
            RefreshBsoft.Image = Properties.Resources.Refresh;
            resources.ApplyResources(RefreshBsoft, "RefreshBsoft");
            RefreshBsoft.Name = "RefreshBsoft";
            RefreshBsoft.UseVisualStyleBackColor = true;
            RefreshBsoft.Click += RefreshBsoft_Click;
            // 
            // RefreshAcerto
            // 
            RefreshAcerto.Image = Properties.Resources.Refresh;
            resources.ApplyResources(RefreshAcerto, "RefreshAcerto");
            RefreshAcerto.Name = "RefreshAcerto";
            RefreshAcerto.UseVisualStyleBackColor = true;
            RefreshAcerto.Click += RefreshAcerto_Click;
            // 
            // RefreshPortal
            // 
            RefreshPortal.BackgroundImage = Properties.Resources.Refresh;
            resources.ApplyResources(RefreshPortal, "RefreshPortal");
            RefreshPortal.Name = "RefreshPortal";
            RefreshPortal.UseVisualStyleBackColor = true;
            RefreshPortal.Click += RefreshPortal_Click;
            // 
            // SalvarBsoft
            // 
            SalvarBsoft.Image = Properties.Resources.Salvar;
            resources.ApplyResources(SalvarBsoft, "SalvarBsoft");
            SalvarBsoft.Name = "SalvarBsoft";
            SalvarBsoft.UseVisualStyleBackColor = true;
            SalvarBsoft.Click += SalvarBsoft_Click;
            // 
            // CrJoao
            // 
            resources.ApplyResources(CrJoao, "CrJoao");
            CrJoao.Name = "CrJoao";
            CrJoao.UseVisualStyleBackColor = true;
            CrJoao.Click += CrJoao_Click;
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            // 
            // textBox1
            // 
            resources.ApplyResources(textBox1, "textBox1");
            textBox1.Name = "textBox1";
            // 
            // Pesquisar
            // 
            resources.ApplyResources(Pesquisar, "Pesquisar");
            Pesquisar.Name = "Pesquisar";
            Pesquisar.UseVisualStyleBackColor = true;
            Pesquisar.Click += Pesquisar_Click;
            // 
            // CheckCRBsoft
            // 
            resources.ApplyResources(CheckCRBsoft, "CheckCRBsoft");
            CheckCRBsoft.Name = "CheckCRBsoft";
            CheckCRBsoft.UseVisualStyleBackColor = true;
            // 
            // CheckAcerto
            // 
            resources.ApplyResources(CheckAcerto, "CheckAcerto");
            CheckAcerto.Name = "CheckAcerto";
            CheckAcerto.UseVisualStyleBackColor = true;
            // 
            // CheckPortal
            // 
            resources.ApplyResources(CheckPortal, "CheckPortal");
            CheckPortal.Name = "CheckPortal";
            CheckPortal.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Controls.Add(checkBoxDtEmbarque);
            panel1.Controls.Add(checkBoxFaturamento);
            panel1.Controls.Add(checkBoxVencimento);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(checkBoxAno);
            panel1.Controls.Add(checkBoxObs);
            panel1.Controls.Add(checkBoxTotalFrete);
            panel1.Controls.Add(checkBoxCentral);
            panel1.Controls.Add(checkBoxCodRevenda);
            panel1.Controls.Add(checkBoxTransportadora);
            panel1.Controls.Add(checkBoxFrete);
            panel1.Controls.Add(checkBoxID);
            panel1.Controls.Add(checkBoxCarga);
            resources.ApplyResources(panel1, "panel1");
            panel1.Name = "panel1";
            // 
            // checkBoxDtEmbarque
            // 
            resources.ApplyResources(checkBoxDtEmbarque, "checkBoxDtEmbarque");
            checkBoxDtEmbarque.Name = "checkBoxDtEmbarque";
            checkBoxDtEmbarque.UseVisualStyleBackColor = true;
            // 
            // checkBoxFaturamento
            // 
            resources.ApplyResources(checkBoxFaturamento, "checkBoxFaturamento");
            checkBoxFaturamento.Name = "checkBoxFaturamento";
            checkBoxFaturamento.UseVisualStyleBackColor = true;
            // 
            // checkBoxVencimento
            // 
            resources.ApplyResources(checkBoxVencimento, "checkBoxVencimento");
            checkBoxVencimento.Name = "checkBoxVencimento";
            checkBoxVencimento.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            label4.Name = "label4";
            // 
            // checkBoxAno
            // 
            resources.ApplyResources(checkBoxAno, "checkBoxAno");
            checkBoxAno.Name = "checkBoxAno";
            checkBoxAno.UseVisualStyleBackColor = true;
            // 
            // checkBoxObs
            // 
            resources.ApplyResources(checkBoxObs, "checkBoxObs");
            checkBoxObs.Name = "checkBoxObs";
            checkBoxObs.UseVisualStyleBackColor = true;
            // 
            // checkBoxTotalFrete
            // 
            resources.ApplyResources(checkBoxTotalFrete, "checkBoxTotalFrete");
            checkBoxTotalFrete.Checked = true;
            checkBoxTotalFrete.CheckState = CheckState.Checked;
            checkBoxTotalFrete.Name = "checkBoxTotalFrete";
            checkBoxTotalFrete.UseVisualStyleBackColor = true;
            // 
            // checkBoxCentral
            // 
            resources.ApplyResources(checkBoxCentral, "checkBoxCentral");
            checkBoxCentral.Checked = true;
            checkBoxCentral.CheckState = CheckState.Checked;
            checkBoxCentral.Name = "checkBoxCentral";
            checkBoxCentral.UseVisualStyleBackColor = true;
            // 
            // checkBoxCodRevenda
            // 
            resources.ApplyResources(checkBoxCodRevenda, "checkBoxCodRevenda");
            checkBoxCodRevenda.Name = "checkBoxCodRevenda";
            checkBoxCodRevenda.UseVisualStyleBackColor = true;
            // 
            // checkBoxTransportadora
            // 
            resources.ApplyResources(checkBoxTransportadora, "checkBoxTransportadora");
            checkBoxTransportadora.Checked = true;
            checkBoxTransportadora.CheckState = CheckState.Checked;
            checkBoxTransportadora.Name = "checkBoxTransportadora";
            checkBoxTransportadora.UseVisualStyleBackColor = true;
            // 
            // checkBoxFrete
            // 
            resources.ApplyResources(checkBoxFrete, "checkBoxFrete");
            checkBoxFrete.Checked = true;
            checkBoxFrete.CheckState = CheckState.Checked;
            checkBoxFrete.Name = "checkBoxFrete";
            checkBoxFrete.UseVisualStyleBackColor = true;
            // 
            // checkBoxID
            // 
            resources.ApplyResources(checkBoxID, "checkBoxID");
            checkBoxID.Name = "checkBoxID";
            checkBoxID.UseVisualStyleBackColor = true;
            // 
            // checkBoxCarga
            // 
            resources.ApplyResources(checkBoxCarga, "checkBoxCarga");
            checkBoxCarga.Checked = true;
            checkBoxCarga.CheckState = CheckState.Checked;
            checkBoxCarga.Name = "checkBoxCarga";
            checkBoxCarga.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            resources.ApplyResources(button1, "button1");
            button1.Name = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Descontos
            // 
            resources.ApplyResources(Descontos, "Descontos");
            Descontos.Name = "Descontos";
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            label5.Name = "label5";
            // 
            // button2
            // 
            button2.Image = Properties.Resources.joao;
            resources.ApplyResources(button2, "button2");
            button2.Name = "button2";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Image = Properties.Resources.joao;
            resources.ApplyResources(button3, "button3");
            button3.Name = "button3";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // ValorSomadoCRBsoft
            // 
            resources.ApplyResources(ValorSomadoCRBsoft, "ValorSomadoCRBsoft");
            ValorSomadoCRBsoft.Name = "ValorSomadoCRBsoft";
            // 
            // ValorSomadoPortal
            // 
            resources.ApplyResources(ValorSomadoPortal, "ValorSomadoPortal");
            ValorSomadoPortal.Name = "ValorSomadoPortal";
            // 
            // ValorSomadoAcerto
            // 
            resources.ApplyResources(ValorSomadoAcerto, "ValorSomadoAcerto");
            ValorSomadoAcerto.Name = "ValorSomadoAcerto";
            // 
            // ValorDiff
            // 
            resources.ApplyResources(ValorDiff, "ValorDiff");
            ValorDiff.Name = "ValorDiff";
            // 
            // button4
            // 
            resources.ApplyResources(button4, "button4");
            button4.Name = "button4";
            button4.UseVisualStyleBackColor = true;
            button4.Click += FreteBotao_Click;
            // 
            // LimparCRBsoft
            // 
            resources.ApplyResources(LimparCRBsoft, "LimparCRBsoft");
            LimparCRBsoft.Name = "LimparCRBsoft";
            LimparCRBsoft.UseVisualStyleBackColor = true;
            LimparCRBsoft.Click += LimparCRBsoft_Click;
            // 
            // BSoft
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(LimparCRBsoft);
            Controls.Add(button4);
            Controls.Add(ValorDiff);
            Controls.Add(ValorSomadoAcerto);
            Controls.Add(ValorSomadoPortal);
            Controls.Add(ValorSomadoCRBsoft);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(label5);
            Controls.Add(Descontos);
            Controls.Add(button1);
            Controls.Add(panel1);
            Controls.Add(CheckPortal);
            Controls.Add(CheckAcerto);
            Controls.Add(CheckCRBsoft);
            Controls.Add(Pesquisar);
            Controls.Add(textBox1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(CrJoao);
            Controls.Add(SalvarBsoft);
            Controls.Add(RefreshPortal);
            Controls.Add(RefreshAcerto);
            Controls.Add(RefreshBsoft);
            Controls.Add(FreteBotao);
            Controls.Add(AcertoDataGrid);
            Controls.Add(PortalDataGrid);
            Controls.Add(BSoftDataGrid);
            Name = "BSoft";
            Load += BSoft_Load;
            ((System.ComponentModel.ISupportInitialize)BSoftDataGrid).EndInit();
            ((System.ComponentModel.ISupportInitialize)PortalDataGrid).EndInit();
            ((System.ComponentModel.ISupportInitialize)AcertoDataGrid).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView BSoftDataGrid;
        private DataGridView PortalDataGrid;
        private DataGridView AcertoDataGrid;
        private Button FreteBotao;
        private Button RefreshBsoft;
        private Button RefreshAcerto;
        private Button RefreshPortal;
        private Button SalvarBsoft;
        private Button CrJoao;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox textBox1;
        private Button Pesquisar;
        private CheckBox CheckCRBsoft;
        private CheckBox CheckAcerto;
        private CheckBox CheckPortal;
        private Panel panel1;
        private CheckBox checkBoxAno;
        private CheckBox checkBoxObs;
        private CheckBox checkBoxTotalFrete;
        private CheckBox checkBoxCentral;
        private CheckBox checkBoxCodRevenda;
        private CheckBox checkBoxTransportadora;
        private CheckBox checkBoxFrete;
        private CheckBox checkBoxID;
        private CheckBox checkBoxCarga;
        private Label label4;
        private Button button1;
        private CheckBox checkBoxDtEmbarque;
        private CheckBox checkBoxFaturamento;
        private CheckBox checkBoxVencimento;
        private TextBox Descontos;
        private Label label5;
        private Button button2;
        private Button button3;
        private Label ValorSomadoCRBsoft;
        private Label ValorSomadoPortal;
        private Label ValorSomadoAcerto;
        private Label ValorDiff;
        private Button button4;
        private Button LimparCRBsoft;
    }
}