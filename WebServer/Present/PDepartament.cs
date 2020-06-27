using Less5_DZ_Viktor_Vill;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebServer.Present
{
    public class PDepartament
    {
        private SqlConnection connection;

        public PDepartament()
        {
            var connectionString = new SqlConnectionStringBuilder
            {
                DataSource = @"(localdb)\MSSQLLocalDB",
                InitialCatalog = "VictorSlaves",
                Pooling = true
            };

            connection = new SqlConnection(connectionString.ConnectionString);
            connection.Open();
        }

        public IEnumerable<Departament> GetDepartaments()
        {
            List<Departament> result = new List<Departament>();

            string sql = @"SELECT * FROM Departament";

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new Departament()
                        {
                            DepartamentName = reader["Name"].ToString()
                        });
                    }
                }
            }

            return result;
        }

        public bool AddDepartament(Departament departament)
        {
            try
            {
                string command = $@"INSERT INTO Departament (Name) VALUES (N'{departament.DepartamentName}')";
                using (SqlCommand com = new SqlCommand(command,connection))
                {
                    com.ExecuteNonQuery();
                }
            }
            catch 
            {
                return false;
            }
            return true;
        }
    }
}