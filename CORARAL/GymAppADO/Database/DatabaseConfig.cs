using System.Data.SQLite;

namespace GymAppADO.Database
{
    public class DatabaseConfig
    {
        private readonly string connectionString = "Data Source=gym_ado.db;Version=3;";

        public string GetConnectionString() => connectionString;

        public void InitializeDatabase()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string createTableQuery = @"
                    CREATE TABLE IF NOT EXISTS Miembro (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        nombre_completo TEXT NOT NULL,
                        cedula TEXT UNIQUE NOT NULL,
                        telefono TEXT
                    );";

                using (var command = new SQLiteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
