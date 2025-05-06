namespace Checka
{
    partial class LimparMes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LimparMes));
            monthCalendar1 = new MonthCalendar();
            monthCalendar2 = new MonthCalendar();
            label1 = new Label();
            Limpar = new Button();
            SuspendLayout();
            // 
            // monthCalendar1
            // 
            monthCalendar1.Location = new Point(18, 33);
            monthCalendar1.Name = "monthCalendar1";
            monthCalendar1.TabIndex = 0;
            // 
            // monthCalendar2
            // 
            monthCalendar2.Location = new Point(263, 33);
            monthCalendar2.Name = "monthCalendar2";
            monthCalendar2.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(18, 9);
            label1.Name = "label1";
            label1.Size = new Size(119, 15);
            label1.TabIndex = 2;
            label1.Text = "Selecione o intervalo:";
            // 
            // Limpar
            // 
            Limpar.Location = new Point(415, 207);
            Limpar.Name = "Limpar";
            Limpar.Size = new Size(75, 23);
            Limpar.TabIndex = 3;
            Limpar.Text = "Deletar";
            Limpar.UseVisualStyleBackColor = true;
            Limpar.Click += button1_Click;
            // 
            // LimparMes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(508, 285);
            Controls.Add(Limpar);
            Controls.Add(label1);
            Controls.Add(monthCalendar2);
            Controls.Add(monthCalendar1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "LimparMes";
            Text = "Limpar por data";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MonthCalendar monthCalendar1;
        private MonthCalendar monthCalendar2;
        private Label label1;
        private Button Limpar;
    }
}