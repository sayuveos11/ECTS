using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Npgsql;

namespace ECTS
{
    public partial class Form1 : Form
    {
        private string templatePath;  // шлях до шаблону документа
        private string outputFolderPath;  // шлях до папки для збереження результатів
        private string connectionString;  // рядок підключення до бази даних
        private int fileCounter = 1;  // лічильник файлів для нумерації

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Ініціалізація TabControl та вкладок
            TabControl tabControl = new TabControl();
            tabControl.Dock = DockStyle.Fill;

            TabPage tabPage1 = new TabPage("Підключення до БД");
            TabPage tabPage2 = new TabPage("Управління даними");

            tabControl.TabPages.Add(tabPage1);
            tabControl.TabPages.Add(tabPage2);

            this.Controls.Add(tabControl);

            // Вкладка "Підключення до БД"
            InitializeDatabaseConnectionTab(tabPage1);

            // Вкладка "Управління даними"
            InitializeDataManagementTab(tabPage2);
        }

        private void InitializeDatabaseConnectionTab(TabPage tabPage)
        {
            Label lblHost = new Label { Text = "Хост:", Location = new System.Drawing.Point(20, 20) };
            TextBox txtHost = new TextBox { Name = "txtHost", Location = new System.Drawing.Point(120, 20) };
            Label lblUsername = new Label { Text = "Користувач:", Location = new System.Drawing.Point(20, 60) };
            TextBox txtUsername = new TextBox { Name = "txtUsername", Location = new System.Drawing.Point(120, 60) };
            Label lblPassword = new Label { Text = "Пароль:", Location = new System.Drawing.Point(20, 100) };
            TextBox txtPassword = new TextBox { Name = "txtPassword", Location = new System.Drawing.Point(120, 100) };
            Label lblDatabase = new Label { Text = "База даних:", Location = new System.Drawing.Point(20, 140) };
            TextBox txtDatabase = new TextBox { Name = "txtDatabase", Location = new System.Drawing.Point(120, 140) };
            Button btnSaveConnection = new Button { Text = "Зберегти з'єднання", Location = new System.Drawing.Point(120, 180) };
            btnSaveConnection.Click += btnSaveConnection_Click;

            tabPage.Controls.Add(lblHost);
            tabPage.Controls.Add(txtHost);
            tabPage.Controls.Add(lblUsername);
            tabPage.Controls.Add(txtUsername);
            tabPage.Controls.Add(lblPassword);
            tabPage.Controls.Add(txtPassword);
            tabPage.Controls.Add(lblDatabase);
            tabPage.Controls.Add(txtDatabase);
            tabPage.Controls.Add(btnSaveConnection);
        }

        private void InitializeDataManagementTab(TabPage tabPage)
        {
            Label lblSchema = new Label { Text = "Схема:", Location = new System.Drawing.Point(20, 20) };
            TextBox txtSchemaName = new TextBox { Name = "txtSchemaName", Location = new System.Drawing.Point(120, 20) };
            Label lblTable = new Label { Text = "Таблиця:", Location = new System.Drawing.Point(20, 60) };
            TextBox txtTableName = new TextBox { Name = "txtTableName", Location = new System.Drawing.Point(120, 60) };
            Button btnLoadData = new Button { Text = "Завантажити дані", Location = new System.Drawing.Point(120, 100) };
            btnLoadData.Click += btnLoadData_Click;

            DataGridView dataGridView1 = new DataGridView { Name = "dataGridView1", Location = new System.Drawing.Point(20, 140), Width = 500, Height = 200 };
            Button btnAddRow = new Button { Text = "Додати рядок", Location = new System.Drawing.Point(20, 360) };
            btnAddRow.Click += btnAddRow_Click;
            Button btnDeleteRow = new Button { Text = "Видалити рядок", Location = new System.Drawing.Point(120, 360) };
            btnDeleteRow.Click += btnDeleteRow_Click;
            Button btnUpdateRow = new Button { Text = "Оновити рядок", Location = new System.Drawing.Point(220, 360) };
            btnUpdateRow.Click += btnUpdateRow_Click;

            Button btnSelectTemplate = new Button { Text = "Вибрати шаблон", Location = new System.Drawing.Point(20, 400) };
            btnSelectTemplate.Click += btnSelectTemplate_Click;
            Button btnSelectOutputFolder = new Button { Text = "Вибрати папку виводу", Location = new System.Drawing.Point(140, 400) };
            btnSelectOutputFolder.Click += btnSelectOutputFolder_Click;
            Button btnGenerateDocuments = new Button { Text = "Створити документи", Location = new System.Drawing.Point(260, 400) };
            btnGenerateDocuments.Click += btnGenerateDocuments_Click;

            tabPage.Controls.Add(lblSchema);
            tabPage.Controls.Add(txtSchemaName);
            tabPage.Controls.Add(lblTable);
            tabPage.Controls.Add(txtTableName);
            tabPage.Controls.Add(btnLoadData);
            tabPage.Controls.Add(dataGridView1);
            tabPage.Controls.Add(btnAddRow);
            tabPage.Controls.Add(btnDeleteRow);
            tabPage.Controls.Add(btnUpdateRow);
            tabPage.Controls.Add(btnSelectTemplate);
            tabPage.Controls.Add(btnSelectOutputFolder);
            tabPage.Controls.Add(btnGenerateDocuments);
        }

        // Збереження параметрів підключення до БД
        private void btnSaveConnection_Click(object sender, EventArgs e)
        {
            var tabPage = ((Button)sender).Parent as TabPage;
            var txtHost = tabPage.Controls["txtHost"] as TextBox;
            var txtUsername = tabPage.Controls["txtUsername"] as TextBox;
            var txtPassword = tabPage.Controls["txtPassword"] as TextBox;
            var txtDatabase = tabPage.Controls["txtDatabase"] as TextBox;

            connectionString = $"Host={txtHost.Text};Username={txtUsername.Text};Password={txtPassword.Text};Database={txtDatabase.Text}";
            MessageBox.Show("Налаштування підключення до бази даних збережено.", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Завантаження даних з бази до DataGridView
        private void btnLoadData_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(connectionString))
                {
                    MessageBox.Show("Спочатку збережіть налаштування підключення до бази даних.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (dataGridView1 == null)
                {
                    MessageBox.Show("DataGridView не ініціалізована.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Продовжуємо з завантаженням даних до DataGridView
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    var cmdText = $"SELECT * FROM {txtSchemaName.Text}.{txtTableName.Text}";
                    using (var cmd = new NpgsqlCommand(cmdText, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        var dataTable = new DataTable();
                        dataTable.Load(reader);
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Додавання нового рядка в DataGridView
        private void btnAddRow_Click(object sender, EventArgs e)
        {
            var tabPage = ((Button)sender).Parent as TabPage;
            var dataGridView1 = tabPage.Controls["dataGridView1"] as DataGridView;
            var dataTable = dataGridView1.DataSource as DataTable;

            if (dataTable != null)
            {
                dataTable.Rows.Add(dataTable.NewRow());
            }
        }

        // Видалення вибраного рядка з DataGridView
        private void btnDeleteRow_Click(object sender, EventArgs e)
        {
            var tabPage = ((Button)sender).Parent as TabPage;
            var dataGridView1 = tabPage.Controls["dataGridView1"] as DataGridView;
            var dataTable = dataGridView1.DataSource as DataTable;

            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                if (!row.IsNewRow)
                {
                    dataGridView1.Rows.Remove(row);
                }
            }
        }

        // Оновлення даних в базі з DataGridView
        private void btnUpdateRow_Click(object sender, EventArgs e)
        {
            var tabPage = ((Button)sender).Parent as TabPage;
            var txtSchemaName = tabPage.Controls["txtSchemaName"] as TextBox;
            var txtTableName = tabPage.Controls["txtTableName"] as TextBox;
            var dataGridView1 = tabPage.Controls["dataGridView1"] as DataGridView;
            DatabaseHelper.UpdateData(connectionString, txtSchemaName.Text, txtTableName.Text, dataGridView1.DataSource as DataTable);
        }

        // Вибір шаблону документа
        private void btnSelectTemplate_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Документи Word|*.docx";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    templatePath = openFileDialog.FileName;
                    MessageBox.Show("Вибраний шаблон: " + templatePath, "Вибір шаблону", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        // Вибір папки для збереження результатів
        private void btnSelectOutputFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    outputFolderPath = folderBrowserDialog.SelectedPath;
                    MessageBox.Show("Вибрана папка для виводу: " + outputFolderPath, "Вибір папки", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        // Генерація документів на основі вибраного шаблону та даних з DataGridView
        private void btnGenerateDocuments_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(templatePath) || string.IsNullOrEmpty(outputFolderPath))
            {
                MessageBox.Show("Спочатку виберіть шаблон та папку для виводу.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var tabPage = ((Button)sender).Parent as TabPage;
            var dataGridView1 = tabPage.Controls["dataGridView1"] as DataGridView;
            DocumentHelper.GenerateDocuments(templatePath, outputFolderPath, dataGridView1, ref fileCounter);
        }
    }
}
