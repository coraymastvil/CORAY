using System;
using System.Collections.Generic;
using System.Data.SQLite;
using GymAppADO.Database;
using GymAppADO.Models;

namespace GymAppADO.Repository
{
    public class MiembroRepository : IMiembroRepository
    {
        private readonly DatabaseConfig _dbConfig;

        public MiembroRepository(DatabaseConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }

        public void Add(Miembro miembro)
        {
            using (var conn = new SQLiteConnection(_dbConfig.GetConnectionString()))
            {
                conn.Open();
                string query = "INSERT INTO Miembro (nombre_completo, cedula, telefono) VALUES (@nombre, @cedula, @telefono)";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nombre", miembro.NombreCompleto);
                    cmd.Parameters.AddWithValue("@cedula", miembro.Cedula);
                    cmd.Parameters.AddWithValue("@telefono", miembro.Telefono);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Miembro> GetAll()
        {
            var list = new List<Miembro>();
            using (var conn = new SQLiteConnection(_dbConfig.GetConnectionString()))
            {
                conn.Open();
                string query = "SELECT nombre_completo, cedula, telefono FROM Miembro";
                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Miembro
                        {
                            NombreCompleto = reader["nombre_completo"].ToString(),
                            Cedula = reader["cedula"].ToString(),
                            Telefono = reader["telefono"].ToString()
                        });
                    }
                }
            }
            return list;
        }

        public Miembro GetByCedula(string cedula)
        {
            using (var conn = new SQLiteConnection(_dbConfig.GetConnectionString()))
            {
                conn.Open();
                string query = "SELECT nombre_completo, cedula, telefono FROM Miembro WHERE cedula = @cedula";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@cedula", cedula);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Miembro
                            {
                                NombreCompleto = reader["nombre_completo"].ToString(),
                                Cedula = reader["cedula"].ToString(),
                                Telefono = reader["telefono"].ToString()
                            };
                        }
                    }
                }
            }
            return null;
        }

        public void UpdateTelefono(string cedula, string telefono)
        {
            using (var conn = new SQLiteConnection(_dbConfig.GetConnectionString()))
            {
                conn.Open();
                string query = "UPDATE Miembro SET telefono = @telefono WHERE cedula = @cedula";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@telefono", telefono);
                    cmd.Parameters.AddWithValue("@cedula", cedula);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(string cedula)
        {
            using (var conn = new SQLiteConnection(_dbConfig.GetConnectionString()))
            {
                conn.Open();
                string query = "DELETE FROM Miembro WHERE cedula = @cedula";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@cedula", cedula);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
