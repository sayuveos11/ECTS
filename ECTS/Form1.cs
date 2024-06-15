using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Npgsql;
using Word = Microsoft.Office.Interop.Word;

namespace ECTS
{
    public partial class Form1 : Form
    {
        private string templatePath;
        private string outputFolderPath;
        private string connectionString;
        private int fileCounter = 1;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Initialize TabControl and Tabs
            TabControl tabControl = new TabControl();
            tabControl.Dock = DockStyle.Fill;

            TabPage tabPage1 = new TabPage("Database Connection");
            TabPage tabPage2 = new TabPage("Data Management");

            tabControl.TabPages.Add(tabPage1);
            tabControl.TabPages.Add(tabPage2);

            this.Controls.Add(tabControl);

            // Database Connection Tab
            InitializeDatabaseConnectionTab(tabPage1);

            // Data Management Tab
            InitializeDataManagementTab(tabPage2);
        }

        private void InitializeDatabaseConnectionTab(TabPage tabPage)
        {
            Label lblHost = new Label { Text = "Host:", Location = new System.Drawing.Point(20, 20) };
            TextBox txtHost = new TextBox { Name = "txtHost", Location = new System.Drawing.Point(120, 20) };
            Label lblUsername = new Label { Text = "Username:", Location = new System.Drawing.Point(20, 60) };
            TextBox txtUsername = new TextBox { Name = "txtUsername", Location = new System.Drawing.Point(120, 60) };
            Label lblPassword = new Label { Text = "Password:", Location = new System.Drawing.Point(20, 100) };
            TextBox txtPassword = new TextBox { Name = "txtPassword", Location = new System.Drawing.Point(120, 100) };
            Label lblDatabase = new Label { Text = "Database:", Location = new System.Drawing.Point(20, 140) };
            TextBox txtDatabase = new TextBox { Name = "txtDatabase", Location = new System.Drawing.Point(120, 140) };
            Button btnSaveConnection = new Button { Text = "Save Connection", Location = new System.Drawing.Point(120, 180) };
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
            Label lblSchema = new Label { Text = "Schema:", Location = new System.Drawing.Point(20, 20) };
            TextBox txtSchemaName = new TextBox { Name = "txtSchemaName", Location = new System.Drawing.Point(120, 20) };
            Label lblTable = new Label { Text = "Table:", Location = new System.Drawing.Point(20, 60) };
            TextBox txtTableName = new TextBox { Name = "txtTableName", Location = new System.Drawing.Point(120, 60) };
            Button btnLoadData = new Button { Text = "Load Data", Location = new System.Drawing.Point(120, 100) };
            btnLoadData.Click += btnLoadData_Click;

            DataGridView dataGridView1 = new DataGridView { Name = "dataGridView1", Location = new System.Drawing.Point(20, 140), Width = 500, Height = 200 };
            Button btnAddRow = new Button { Text = "Add Row", Location = new System.Drawing.Point(20, 360) };
            btnAddRow.Click += btnAddRow_Click;
            Button btnDeleteRow = new Button { Text = "Delete Row", Location = new System.Drawing.Point(120, 360) };
            btnDeleteRow.Click += btnDeleteRow_Click;
            Button btnUpdateRow = new Button { Text = "Update Row", Location = new System.Drawing.Point(220, 360) };
            btnUpdateRow.Click += btnUpdateRow_Click;

            Button btnSelectTemplate = new Button { Text = "Select Template", Location = new System.Drawing.Point(20, 400) };
            btnSelectTemplate.Click += btnSelectTemplate_Click;
            Button btnSelectOutputFolder = new Button { Text = "Select Output Folder", Location = new System.Drawing.Point(140, 400) };
            btnSelectOutputFolder.Click += btnSelectOutputFolder_Click;
            Button btnGenerateDocuments = new Button { Text = "Generate Documents", Location = new System.Drawing.Point(260, 400) };
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
        private void btnSaveConnection_Click(object sender, EventArgs e)
        {
            var tabPage = ((Button)sender).Parent as TabPage;
            var txtHost = tabPage.Controls["txtHost"] as TextBox;
            var txtUsername = tabPage.Controls["txtUsername"] as TextBox;
            var txtPassword = tabPage.Controls["txtPassword"] as TextBox;
            var txtDatabase = tabPage.Controls["txtDatabase"] as TextBox;

            connectionString = $"Host={txtHost.Text};Username={txtUsername.Text};Password={txtPassword.Text};Database={txtDatabase.Text}";
            MessageBox.Show("Database connection settings saved.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(connectionString))
                {
                    MessageBox.Show("Please save the database connection settings first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (dataGridView1 == null)
                {
                    MessageBox.Show("DataGridView is not initialized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Proceed with loading data into DataGridView
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
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LoadData(string schemaName, string tableName, DataGridView dataGridView)
        {
            try
            {
                if (string.IsNullOrEmpty(connectionString))
                {
                    MessageBox.Show("Please save the database connection settings first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrEmpty(schemaName) || string.IsNullOrEmpty(tableName))
                {
                    MessageBox.Show("Please enter the table and schema names.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    var cmdText = $"SELECT * FROM {schemaName}.{tableName}";
                    using (var cmd = new NpgsqlCommand(cmdText, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        var dataTable = new DataTable();
                        dataTable.Load(reader);
                        dataGridView.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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

        private void btnUpdateRow_Click(object sender, EventArgs e)
        {
            var tabPage = ((Button)sender).Parent as TabPage;
            var txtSchemaName = tabPage.Controls["txtSchemaName"] as TextBox;
            var txtTableName = tabPage.Controls["txtTableName"] as TextBox;
            var dataGridView1 = tabPage.Controls["dataGridView1"] as DataGridView;

            UpdateData(txtSchemaName.Text, txtTableName.Text, dataGridView1);
        }

        private void UpdateData(string schemaName, string tableName, DataGridView dataGridView)
        {
            try
            {
                if (string.IsNullOrEmpty(connectionString))
                {
                    MessageBox.Show("Please save the database connection settings first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();

                    var dataTable = dataGridView.DataSource as DataTable;
                    if (dataTable != null)
                    {
                        var cmdText = $"DELETE FROM {schemaName}.{tableName}";
                        using (var cmd = new NpgsqlCommand(cmdText, conn))
                        {
                            cmd.ExecuteNonQuery();
                        }

                        foreach (DataRow row in dataTable.Rows)
                        {
                            var columns = string.Join(",", dataTable.Columns.Cast<DataColumn>().Select(c => c.ColumnName));
                            var values = string.Join(",", row.ItemArray.Select(val => $"'{val}'"));

                            cmdText = $"INSERT INTO {schemaName}.{tableName} ({columns}) VALUES ({values})";
                            using (var cmd = new NpgsqlCommand(cmdText, conn))
                            {
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
                MessageBox.Show("Data updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSelectTemplate_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Word Documents|*.docx";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    templatePath = openFileDialog.FileName;
                    MessageBox.Show("Template selected: " + templatePath, "Template Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnSelectOutputFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    outputFolderPath = folderBrowserDialog.SelectedPath;
                    MessageBox.Show("Output folder selected: " + outputFolderPath, "Folder Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnGenerateDocuments_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(templatePath) || string.IsNullOrEmpty(outputFolderPath))
            {
                MessageBox.Show("Please select the template and output folder first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var tabPage = ((Button)sender).Parent as TabPage;
            var dataGridView1 = tabPage.Controls["dataGridView1"] as DataGridView;

            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.IsNewRow) continue;

                    var wordApp = new Word.Application();
                    var wordDoc = wordApp.Documents.Open(templatePath);

                    foreach (DataGridViewColumn column in dataGridView1.Columns)
                    {
                        string columnName = column.Name;
                        string value = row.Cells[columnName].Value?.ToString() ?? string.Empty;

                        ReplaceBookmarkWithValue(wordDoc, columnName, value);
                    }

                    string outputFileName = GetNumberedFileName(outputFolderPath, "docx");

                    wordDoc.SaveAs2(outputFileName);
                    wordDoc.Close(SaveChanges: false);
                    wordApp.Quit();

                    Marshal.ReleaseComObject(wordDoc);
                    Marshal.ReleaseComObject(wordApp);

                    fileCounter++;
                }
                MessageBox.Show("Documents generated successfully.", "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetNumberedFileName(string folderPath, string extension)
        {
            string fileName = Path.Combine(folderPath, $"{fileCounter}.{extension}");
            while (File.Exists(fileName))
            {
                fileCounter++;
                fileName = Path.Combine(folderPath, $"{fileCounter}.{extension}");
            }
            return fileName;
        }

        private void ReplaceBookmarkWithValue(Word.Document doc, string bookmarkName, string value)
        {
            if (doc.Bookmarks.Exists(bookmarkName))
            {
                Word.Range range = doc.Bookmarks[bookmarkName].Range;
                range.Text = value;
                doc.Bookmarks.Add(bookmarkName, range);
            }
        }
    }
}
