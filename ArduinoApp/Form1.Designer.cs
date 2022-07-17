namespace ArduinoApp
{
    partial class Form1
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
            this.btnEncender = new System.Windows.Forms.Button();
            this.btnApagar = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.lblEstado = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEncender
            // 
            this.btnEncender.BackColor = System.Drawing.Color.PaleGreen;
            this.btnEncender.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEncender.Font = new System.Drawing.Font("Cascadia Mono", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEncender.Location = new System.Drawing.Point(12, 68);
            this.btnEncender.Name = "btnEncender";
            this.btnEncender.Size = new System.Drawing.Size(91, 40);
            this.btnEncender.TabIndex = 0;
            this.btnEncender.Text = "Encender";
            this.btnEncender.UseVisualStyleBackColor = false;
            this.btnEncender.Click += new System.EventHandler(this.btnEncender_Click);
            // 
            // btnApagar
            // 
            this.btnApagar.BackColor = System.Drawing.Color.PaleGreen;
            this.btnApagar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApagar.Font = new System.Drawing.Font("Cascadia Mono", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApagar.Location = new System.Drawing.Point(12, 114);
            this.btnApagar.Name = "btnApagar";
            this.btnApagar.Size = new System.Drawing.Size(91, 40);
            this.btnApagar.TabIndex = 1;
            this.btnApagar.Text = "Apagar";
            this.btnApagar.UseVisualStyleBackColor = false;
            this.btnApagar.Click += new System.EventHandler(this.btnApagar_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(240, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(314, 239);
            this.dataGridView1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cascadia Code", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 262);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(210, 25);
            this.label1.TabIndex = 4;
            this.label1.Text = "Estado del sensor:";
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Font = new System.Drawing.Font("Cascadia Code", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstado.ForeColor = System.Drawing.Color.Red;
            this.lblEstado.Location = new System.Drawing.Point(218, 262);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(89, 25);
            this.lblEstado.TabIndex = 5;
            this.lblEstado.Text = "APAGADO";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(563, 302);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnApagar);
            this.Controls.Add(this.btnEncender);
            this.Name = "Form1";
            this.Text = "Sensor de humedad";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnEncender;
        private System.Windows.Forms.Button btnApagar;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblEstado;
    }
}

