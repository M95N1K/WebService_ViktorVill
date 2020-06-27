using Less5_DZ_Viktor_Vill.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Less5_DZ_Viktor_Vill.Present
{
    class PresentWorkersDB
    {
        private SqlConnection connection;
        private SqlDataAdapter adapter;

        private IDataTableView dt;

        public PresentWorkersDB(IDataTableView dt)
        {
            this.dt = dt;
            // Initial();
        }

        public void Initial()
        {
            var connectionString = new SqlConnectionStringBuilder
            {
                DataSource = @"(localdb)\MSSQLLocalDB",
                InitialCatalog = "VictorSlaves",
                Pooling = true
            };

            connection = new SqlConnection(connectionString.ConnectionString);
            adapter = new SqlDataAdapter();

            SqlCommand command = new SqlCommand("SELECT Id, Name, Firstname, Departament, Position, Birthday FROM Workers", connection);
            adapter.SelectCommand = command;

            #region Добавим
            command = new SqlCommand(@"INSERT INTO Workers (Name, Firstname, Departament, Position, Birthday)
                                        VALUES (@Name, @Firstname, @Departament, @Position, @Birthday); 
                                        SET @Id = @@IDENTITY;", connection);

            command.Parameters.Add("@Name", SqlDbType.NChar, -1, "Name");
            command.Parameters.Add("@Firstname", SqlDbType.NChar, 50, "Firstname");
            command.Parameters.Add("@Departament", SqlDbType.NChar, 50, "Departament");
            command.Parameters.Add("@Position", SqlDbType.NChar, 50, "Position");
            command.Parameters.Add("@Birthday", SqlDbType.Date, 0, "Birthday");

            SqlParameter param = command.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
            param.Direction = ParameterDirection.Output;
            adapter.InsertCommand = command;
            #endregion

            #region Меняем
            command = new SqlCommand(@"UPDATE Workers SET   Name = @Name,
                                                            Firstname = @Firstname,
                                                            Departament = @Departament,
                                                            Position = @Position,
                                                            Birthday = @Birthday
                                        WHERE Id = @Id", connection);
            command.Parameters.Add("@Name", SqlDbType.NChar, 50, "Name");
            command.Parameters.Add("@Firstname", SqlDbType.NChar, 50, "Firstname");
            command.Parameters.Add("@Departament", SqlDbType.NChar, 50, "Departament");
            command.Parameters.Add("@Position", SqlDbType.NChar, 50, "Position");
            command.Parameters.Add("@Birthday", SqlDbType.Date, 0, "Birthday");
            //param = 
            command.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");

            adapter.UpdateCommand = command;
            #endregion

            #region Удаляем
            command = new SqlCommand("DELETE FROM Workers WHERE Id = @Id", connection);
            //param = 
            command.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
            adapter.DeleteCommand = command;
            #endregion
            dt.DT = new DataTable();
            adapter.Fill(dt.DT);
        }

        public void AddRow(DataRow dataRow)
        {
            dt.DT.Rows.Add(dataRow);
            adapter.Update(dt.DT);
        }

        public void UpdateData()
        {
            adapter.Update(dt.DT);
        }

        public void DeleteRow(DataRowView dataRow)
        {
            dataRow.Row.Delete();
            adapter.Update(dt.DT);
        }

        public void SetDepartamentFilter(string departament, bool enable)
        {
            
            if (!enable)
            {
                SqlCommand command = new SqlCommand("SELECT Id, Name, Firstname, Departament, Position, Birthday FROM Workers", connection);
                adapter.SelectCommand = command;
            }
            else
            {
                string com = $"SELECT Id, Name, Firstname, Departament, Position, Birthday FROM Workers WHERE Departament = '{departament}'";
                SqlCommand command = new SqlCommand(com, connection);
                adapter.SelectCommand = command;
            }
            dt.DT.Clear();
            adapter.Fill(dt.DT);
        }
    }
}
