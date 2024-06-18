using System;
using System.Data;
using Npgsql;
using System.Windows.Forms;
using System.Linq;

namespace ECTS
{
    public static class DatabaseHelper
    {

        // Завантаження даних з БД
        public static DataTable LoadData(string connectionString, string schemaName, string tableName)
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    var cmdText = $"SELECT * FROM {schemaName}.{tableName}";
                    using (var cmd = new NpgsqlCommand(cmdText, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        var dataTable = new DataTable();
                        dataTable.Load(reader);
                        return dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        // Редагування БД у програмі
        public static void UpdateData(string connectionString, string schemaName, string tableName, DataTable dataTable)
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();

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
                MessageBox.Show("Дані успішно оновлено.", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
