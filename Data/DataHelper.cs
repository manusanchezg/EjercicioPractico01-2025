using Microsoft.Data.SqlClient;
using System.Data;
using EjercicioPractico01_2025.Domain;

namespace EjercicioPractico01_2025.Data
{
    internal class DataHelper
    {
        private static DataHelper instance;
        private SqlConnection connection;

        private DataHelper()
        {
            // Inicializar la conexión a la base de datos
            connection = new SqlConnection(Properties.Resources.ConexionLocal);
        }

        public static DataHelper GetInstance()
        {
            if (instance == null)
            {
                instance = new DataHelper();
            }
            return instance;
        }

        public DataTable ExecuteSPQuery(string sp)
        {
            DataTable dataTable = new DataTable();
            try
            {

                OpenConnection();

                var cmd = new SqlCommand(sp, connection)
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = sp
                };

                dataTable.Load(cmd.ExecuteReader());
            }
            catch (SqlException ex)
            {
                // Registrar el error
                Console.WriteLine($"Error al ejecutar el SP: {ex.Message}");
                throw; // Re-lanzar la excepción si es necesario
            }
            finally
            {
                CloseConnection();
            }
            return dataTable;
        }

        public DataTable ExecuteSPQuery(string sp, List<Parameter> parameters)
        {
            DataTable dataTable = new DataTable();
            try
            {
                OpenConnection();

                var cmd = new SqlCommand(sp, connection)
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = sp
                };

                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        cmd.Parameters.AddWithValue(param.Name, param.Value ?? DBNull.Value);
                    }
                }

                dataTable.Load(cmd.ExecuteReader());
            }
            catch (SqlException ex)
            {
                // Registrar el error
                Console.WriteLine($"Error al ejecutar el SP: {ex.Message}");
                throw; // Re-lanzar la excepción si es necesario
            }
            finally
            {
                CloseConnection();
            }
            return dataTable;
        }

        public SqlConnection OpenConnection()
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            return connection;
        }

        public void CloseConnection()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }
}
