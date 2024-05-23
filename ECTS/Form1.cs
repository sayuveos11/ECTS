using System;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Npgsql;
using Word = Microsoft.Office.Interop.Word;

namespace ECTS
{
    public partial class Form1 : Form
    {
        // Рядок підключення до бази даних PostgreSQL
        private string ConnectionString = "Host=localhost;Username=postgres;Password=1234;Database=ECTS";

        public Form1()
        {
            InitializeComponent();
        }
        // Обробник події для кнопки "Завантажити дані"
        private void btnLoadData_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        // Метод для завантаження даних з бази даних у DataGridView
        private void LoadData()
        {
            try
            {
                // Підключення до бази даних PostgreSQL
                using (var conn = new NpgsqlConnection(ConnectionString))
                {
                    conn.Open();
                    // Виконання SQL-запиту для отримання даних
                    using (var cmd = new NpgsqlCommand("SELECT course, speciality_number, speciality_name, surname, name, national_scale, quantity, ects_scale FROM ectsstudents", conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        var dataTable = new DataTable();
                        dataTable.Load(reader); // Завантаження даних у DataTable
                        dataGridView1.DataSource = dataTable; // Призначення джерела даних для DataGridView
                    }
                }
            }
            catch (Exception ex)
            {
                // Відображення повідомлення про помилку у разі невдалої операції
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Обробник події для кнопки "Сгенерувати документи"
        private void btnGenerateDocuments_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.IsNewRow) continue; // Пропуск нових (порожніх) рядків

                    // Створення нового екземпляра Word і відкриття шаблону документа
                    var wordApp = new Word.Application();
                    var wordDoc = wordApp.Documents.Open(@"C:\Users\kkall\Desktop\students.docx");

                    // Заповнення закладок у документі Word даними з DataGridView
                    ReplaceBookmarkWithValue(wordDoc, "Course", row.Cells["course"].Value.ToString());
                    ReplaceBookmarkWithValue(wordDoc, "SpecialityNumber", row.Cells["speciality_number"].Value.ToString());
                    ReplaceBookmarkWithValue(wordDoc, "SpecialityName", row.Cells["speciality_name"].Value.ToString());
                    ReplaceBookmarkWithValue(wordDoc, "Surname", row.Cells["surname"].Value.ToString());
                    ReplaceBookmarkWithValue(wordDoc, "Name", row.Cells["name"].Value.ToString());
                    ReplaceBookmarkWithValue(wordDoc, "NationalScale", row.Cells["national_scale"].Value.ToString());
                    ReplaceBookmarkWithValue(wordDoc, "Quantity", row.Cells["quantity"].Value.ToString());
                    ReplaceBookmarkWithValue(wordDoc, "EctsScale", row.Cells["ects_scale"].Value.ToString());

                    // Генерація імені вихідного файлу на основі прізвища
                    string surname = row.Cells["surname"].Value.ToString();
                    string outputFileName = $"C:\\Users\\kkall\\Desktop\\ects\\{surname}.docx";

                    // Перевірка наявності файлу і генерація унікального імені файлу у разі необхідності
                    while (File.Exists(outputFileName))
                    {
                        outputFileName = Path.Combine(
                            Path.GetDirectoryName(outputFileName),
                            $"{Path.GetFileNameWithoutExtension(outputFileName)}_{Guid.NewGuid().ToString().Substring(0, 8)}.docx");
                    }

                    // Збереження документа з згенерованим ім'ям файлу
                    wordDoc.SaveAs2(outputFileName);
                    wordDoc.Close(SaveChanges: false);
                    wordApp.Quit();

                    // Звільнення об'єктів Word з пам'яті
                    Marshal.ReleaseComObject(wordDoc);
                    Marshal.ReleaseComObject(wordApp);
                }
                // Відображення повідомлення про успішне створення документів
                MessageBox.Show("Документи згенеровані успішно", "Виконано", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Відображення повідомлення про помилку у разі невдалої операції
                MessageBox.Show("Помилка: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Метод для заміни закладки у документі Word на задане значення
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
