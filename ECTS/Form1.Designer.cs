namespace ECTS
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

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
            // Ініціалізація компонентів форми
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtDatabase = new System.Windows.Forms.TextBox();
            this.txtTableName = new System.Windows.Forms.TextBox();
            this.txtSchemaName = new System.Windows.Forms.TextBox();
            this.btnSaveConnection = new System.Windows.Forms.Button();
            this.btnLoadData = new System.Windows.Forms.Button();
            this.btnSelectTemplate = new System.Windows.Forms.Button();
            this.btnSelectOutputFolder = new System.Windows.Forms.Button();
            this.btnGenerateDocuments = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lblHost = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblDatabase = new System.Windows.Forms.Label();
            this.lblTableName = new System.Windows.Forms.Label();
            this.lblSchemaName = new System.Windows.Forms.Label();

            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();

            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(776, 426);
            this.tabControl1.TabIndex = 0;

            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lblHost);
            this.tabPage1.Controls.Add(this.txtHost);
            this.tabPage1.Controls.Add(this.lblUsername);
            this.tabPage1.Controls.Add(this.txtUsername);
            this.tabPage1.Controls.Add(this.lblPassword);
            this.tabPage1.Controls.Add(this.txtPassword);
            this.tabPage1.Controls.Add(this.lblDatabase);
            this.tabPage1.Controls.Add(this.txtDatabase);
            this.tabPage1.Controls.Add(this.lblTableName);
            this.tabPage1.Controls.Add(this.txtTableName);
            this.tabPage1.Controls.Add(this.lblSchemaName);
            this.tabPage1.Controls.Add(this.txtSchemaName);
            this.tabPage1.Controls.Add(this.btnSaveConnection);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(768, 400);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Підключення до бази даних";
            this.tabPage1.UseVisualStyleBackColor = true;

            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnLoadData);
            this.tabPage2.Controls.Add(this.btnSelectTemplate);
            this.tabPage2.Controls.Add(this.btnSelectOutputFolder);
            this.tabPage2.Controls.Add(this.btnGenerateDocuments);
            this.tabPage2.Controls.Add(this.dataGridView1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(768, 400);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Управління даними";
            this.tabPage2.UseVisualStyleBackColor = true;

            // 
            // txtHost
            // 
            this.txtHost.Location = new System.Drawing.Point(120, 15);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(200, 20);
            this.txtHost.TabIndex = 0;

            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(120, 45);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(200, 20);
            this.txtUsername.TabIndex = 1;

            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(120, 75);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(200, 20);
            this.txtPassword.TabIndex = 2;
            this.txtPassword.UseSystemPasswordChar = true;

            // 
            // txtDatabase
            // 
            this.txtDatabase.Location = new System.Drawing.Point(120, 105);
            this.txtDatabase.Name = "txtDatabase";
            this.txtDatabase.Size = new System.Drawing.Size(200, 20);
            this.txtDatabase.TabIndex = 3;

            // 
            // txtTableName
            // 
            this.txtTableName.Location = new System.Drawing.Point(120, 135);
            this.txtTableName.Name = "txtTableName";
            this.txtTableName.Size = new System.Drawing.Size(200, 20);
            this.txtTableName.TabIndex = 4;

            // 
            // txtSchemaName
            // 
            this.txtSchemaName.Location = new System.Drawing.Point(120, 165);
            this.txtSchemaName.Name = "txtSchemaName";
            this.txtSchemaName.Size = new System.Drawing.Size(200, 20);
            this.txtSchemaName.TabIndex = 5;

            // 
            // btnSaveConnection
            // 
            this.btnSaveConnection.Location = new System.Drawing.Point(15, 195);
            this.btnSaveConnection.Name = "btnSaveConnection";
            this.btnSaveConnection.Size = new System.Drawing.Size(120, 30);
            this.btnSaveConnection.TabIndex = 6;
            this.btnSaveConnection.Text = "Зберегти з'єднання";
            this.btnSaveConnection.UseVisualStyleBackColor = true;
            this.btnSaveConnection.Click += new System.EventHandler(this.btnSaveConnection_Click);

            // 
            // btnLoadData
            // 
            this.btnLoadData.Location = new System.Drawing.Point(6, 15);
            this.btnLoadData.Name = "btnLoadData";
            this.btnLoadData.Size = new System.Drawing.Size(120, 30);
            this.btnLoadData.TabIndex = 7;
            this.btnLoadData.Text = "Завантажити дані";
            this.btnLoadData.UseVisualStyleBackColor = true;
            this.btnLoadData.Click += new System.EventHandler(this.btnLoadData_Click);

            // 
            // btnSelectTemplate
            // 
            this.btnSelectTemplate.Location = new System.Drawing.Point(6, 55);
            this.btnSelectTemplate.Name = "btnSelectTemplate";
            this.btnSelectTemplate.Size = new System.Drawing.Size(120, 30);
            this.btnSelectTemplate.TabIndex = 8;
            this.btnSelectTemplate.Text = "Вибрати шаблон";
            this.btnSelectTemplate.UseVisualStyleBackColor = true;
            this.btnSelectTemplate.Click += new System.EventHandler(this.btnSelectTemplate_Click);

            // 
            // btnSelectOutputFolder
            // 
            this.btnSelectOutputFolder.Location = new System.Drawing.Point(6, 95);
            this.btnSelectOutputFolder.Name = "btnSelectOutputFolder";
            this.btnSelectOutputFolder.Size = new System.Drawing.Size(120, 30);
            this.btnSelectOutputFolder.TabIndex = 9;
            this.btnSelectOutputFolder.Text = "Вибрати папку виводу";
            this.btnSelectOutputFolder.UseVisualStyleBackColor = true;
            this.btnSelectOutputFolder.Click += new System.EventHandler(this.btnSelectOutputFolder_Click);

            // 
            // btnGenerateDocuments
            // 
            this.btnGenerateDocuments.Location = new System.Drawing.Point(6, 135);
            this.btnGenerateDocuments.Name = "btnGenerateDocuments";
            this.btnGenerateDocuments.Size = new System.Drawing.Size(250, 30);
            this.btnGenerateDocuments.TabIndex = 10;
            this.btnGenerateDocuments.Text = "Генерувати документи";
            this.btnGenerateDocuments.UseVisualStyleBackColor = true;
            this.btnGenerateDocuments.Click += new System.EventHandler(this.btnGenerateDocuments_Click);

            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 175);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(756, 219);
            this.dataGridView1.TabIndex = 11;

            // 
            // lblHost
            // 
            this.lblHost.AutoSize = true;
            this.lblHost.Location = new System.Drawing.Point(15, 18);
            this.lblHost.Name = "lblHost";
            this.lblHost.Size = new System.Drawing.Size(34, 13);
            this.lblHost.TabIndex = 12;
            this.lblHost.Text = "Хост:";
                        // Додавання коментарів до елементів форми та їх властивостей

            // 
            // lblHost
            // 
            this.lblHost.AutoSize = true;
            this.lblHost.Location = new System.Drawing.Point(15, 18);
            this.lblHost.Name = "lblHost";
            this.lblHost.Size = new System.Drawing.Size(34, 13);
            this.lblHost.TabIndex = 12;
            this.lblHost.Text = "Хост:";

            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(15, 48);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(58, 13);
            this.lblUsername.TabIndex = 13;
            this.lblUsername.Text = "Ім'я користувача:";

            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(15, 78);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(48, 13);
            this.lblPassword.TabIndex = 14;
            this.lblPassword.Text = "Пароль:";

            // 
            // lblDatabase
            // 
            this.lblDatabase.AutoSize = true;
            this.lblDatabase.Location = new System.Drawing.Point(15, 108);
            this.lblDatabase.Name = "lblDatabase";
            this.lblDatabase.Size = new System.Drawing.Size(48, 13);
            this.lblDatabase.TabIndex = 15;
            this.lblDatabase.Text = "База даних:";

            // 
            // lblTableName
            // 
            this.lblTableName.AutoSize = true;
            this.lblTableName.Location = new System.Drawing.Point(15, 138);
            this.lblTableName.Name = "lblTableName";
            this.lblTableName.Size = new System.Drawing.Size(77, 13);
            this.lblTableName.TabIndex = 16;
            this.lblTableName.Text = "Назва таблиці:";

            // 
            // lblSchemaName
            // 
            this.lblSchemaName.AutoSize = true;
            this.lblSchemaName.Location = new System.Drawing.Point(15, 168);
            this.lblSchemaName.Name = "lblSchemaName";
            this.lblSchemaName.Size = new System.Drawing.Size(88, 13);
            this.lblSchemaName.TabIndex = 17;
            this.lblSchemaName.Text = "Назва схеми:";

            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Генератор документів ECTS";

            // Включення розгортання компонентів
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
        }

        // Об'єкти управління віджетами на формі
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtHost;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtDatabase;
        private System.Windows.Forms.TextBox txtTableName;
        private System.Windows.Forms.TextBox txtSchemaName;
        private System.Windows.Forms.Button btnSaveConnection;
        private System.Windows.Forms.Button btnLoadData;
        private System.Windows.Forms.Button btnSelectTemplate;
        private System.Windows.Forms.Button btnSelectOutputFolder;
        private System.Windows.Forms.Button btnGenerateDocuments;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lblHost;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblDatabase;
        private System.Windows.Forms.Label lblTableName;
        private System.Windows.Forms.Label lblSchemaName;
    }
}
