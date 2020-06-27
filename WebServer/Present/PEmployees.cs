using Less5_DZ_Viktor_Vill;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace WebServer.Present
{
    public class PEmployees
    {
        private SqlConnection connection;

        public PEmployees()
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

        public IEnumerable<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();

            string sql = @"SELECT * FROM Workers";

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        employees.Add(
                            new Employee()
                            {
                                ID = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                                FirstName = reader["Firstname"].ToString(),
                                Departament = reader["Departament"].ToString(),
                                Position = reader["Position"].ToString(),
                                BirthDay = Convert.ToDateTime(reader["Birthday"])
                            });
                }
            }

            return employees;
        }

        public Employee GetEmployeeById(int id)
        {
            Employee employee = new Employee();

            string sql = $@"SELECT * FROM Workers WHERE Id={id}";


            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employee = new Employee()
                        {
                            ID = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            FirstName = reader["Firstname"].ToString(),
                            Departament = reader["Departament"].ToString(),
                            Position = reader["Position"].ToString(),
                            BirthDay = Convert.ToDateTime(reader["Birthday"])
                        };
                    }
                }
            }
            if (employee.ID == 0 && employee.Departament == null)
            {
                employee = new Employee()
                {
                    ID = -1,
                    Name = "",
                    FirstName = "",
                    Departament = "",
                    Position = "",
                    BirthDay = DateTime.Today
                };
            }

            return employee;
        }

        public bool AddEmployee(Employee employee)
        {
            try
            {
                string command = $@"INSERT INTO Workers(Name, Firstname, Departament, Position, Birthday) 
                                VALUES(N'{employee.Name}',
                                       N'{employee.FirstName}',
                                       N'{employee.Departament}',
                                       N'{employee.Position}',
                                       N'{employee.BirthDay.Year}-{employee.BirthDay.Month}-{employee.BirthDay.Day}')";

                using (var com = new SqlCommand(command, connection))
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