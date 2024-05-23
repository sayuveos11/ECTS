namespace ECTS
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnLoadData;
        private System.Windows.Forms.Button btnGenerateDocuments;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnLoadData = new System.Windows.Forms.Button();
            this.btnGenerateDocuments = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(760, 350);
            this.dataGridView1.TabIndex = 0;
            // 
            // btnLoadData
            // 
            this.btnLoadData.Location = new System.Drawing.Point(12, 380);
            this.btnLoadData.Name = "btnLoadData";
            this.btnLoadData.Size = new System.Drawing.Size(150, 30);
            this.btnLoadData.TabIndex = 1;
            this.btnLoadData.Text = "Завантажити дані";
            this.btnLoadData.UseVisualStyleBackColor = true;
            this.btnLoadData.Click += new System.EventHandler(this.btnLoadData_Click);
            // 
            // btnGenerateDocuments
            // 
            this.btnGenerateDocuments.Location = new System.Drawing.Point(622, 380);
            this.btnGenerateDocuments.Name = "btnGenerateDocuments";
            this.btnGenerateDocuments.Size = new System.Drawing.Size(150, 30);
            this.btnGenerateDocuments.TabIndex = 2;
            this.btnGenerateDocuments.Text = "Згенерувати";
            this.btnGenerateDocuments.UseVisualStyleBackColor = true;
            this.btnGenerateDocuments.Click += new System.EventHandler(this.btnGenerateDocuments_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(784, 421);
            this.Controls.Add(this.btnGenerateDocuments);
            this.Controls.Add(this.btnLoadData);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "ECTS Document Generator";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
