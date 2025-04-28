namespace Checka
{
    partial class Importar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Importar));
            textBox1 = new TextBox();
            Enviar = new Button();
            label1 = new Label();
            checkBox1 = new CheckBox();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 27);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(277, 23);
            textBox1.TabIndex = 0;
            textBox1.Text = "C:\\Users\\moniq\\desktop\\Info.xlsx";
            // 
            // Enviar
            // 
            Enviar.Location = new Point(295, 9);
            Enviar.Name = "Enviar";
            Enviar.Size = new Size(97, 41);
            Enviar.TabIndex = 1;
            Enviar.Text = "Enviar";
            Enviar.UseVisualStyleBackColor = true;
            Enviar.Click += Enviar_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(194, 15);
            label1.TabIndex = 2;
            label1.Text = "CAMINHO DO ARQUIVO DO EXCEL\r\n";
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(12, 56);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(165, 19);
            checkBox1.TabIndex = 3;
            checkBox1.Text = "Apagar antes de importar?";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // Importar
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(404, 79);
            Controls.Add(checkBox1);
            Controls.Add(label1);
            Controls.Add(Enviar);
            Controls.Add(textBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Importar";
            Text = "Importar";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private Button Enviar;
        private Label label1;
        private CheckBox checkBox1;
    }
}