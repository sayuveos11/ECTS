using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace ECTS
{
    public static class DocumentHelper
    {

        // Генерація документу 
        public static void GenerateDocuments(string templatePath, string outputFolderPath, DataGridView dataGridView, ref int fileCounter)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    if (row.IsNewRow) continue;

                    var wordApp = new Word.Application();
                    var wordDoc = wordApp.Documents.Open(templatePath);

                    foreach (DataGridViewColumn column in dataGridView.Columns)
                    {
                        string columnName = column.Name;
                        string value = row.Cells[columnName].Value?.ToString() ?? string.Empty;

                        ReplaceBookmarkWithValue(wordDoc, columnName, value);
                    }

                    string outputFileName = GetNumberedFileName(outputFolderPath, "docx", ref fileCounter);

                    wordDoc.SaveAs2(outputFileName);
                    wordDoc.Close(SaveChanges: false);
                    wordApp.Quit();

                    Marshal.ReleaseComObject(wordDoc);
                    Marshal.ReleaseComObject(wordApp);

                    fileCounter++;
                }
                MessageBox.Show("Документи успішно згенеровано.", "Завершено", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Генерація назви для готових документів
        private static string GetNumberedFileName(string folderPath, string extension, ref int fileCounter)
        {
            string fileName = Path.Combine(folderPath, $"{fileCounter}.{extension}");
            while (File.Exists(fileName))
            {
                fileCounter++;
                fileName = Path.Combine(folderPath, $"{fileCounter}.{extension}");
            }
            return fileName;
        }

        // Заміна закладок у шаблоні на дані з БД

        private static void ReplaceBookmarkWithValue(Word.Document doc, string bookmarkName, string value)
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
