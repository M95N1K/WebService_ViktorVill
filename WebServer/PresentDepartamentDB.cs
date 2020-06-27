using Less5_DZ_Viktor_Vill.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Less5_DZ_Viktor_Vill.Present
{
    class PresentDepartamentDB
    {
        private SqlConnection connection;
        private SqlDataAdapter adapter;
        private string oldName;
        private IDepartamentView dv;

        public PresentDepartamentDB(IDepartamentView departamentView)
        {
            dv = departamentView;
            Initial();
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

            SqlCommand command = new SqlCommand("SELECT Name FROM Departament", connection);
            adapter.SelectCommand = command;

            command = new SqlCommand(@"INSERT INTO Departament (Name) VALUES (@Name)", connection);
            command.Parameters.Add("@Name", SqlDbType.NChar, 50, "Name");
            adapter.InsertCommand = command;

            

            command = new SqlCommand(@"DELETE FROM Departament WHERE Name = @Name", connection);
            command.Parameters.Add("@Name", SqlDbType.NChar, 50, "Name");
            adapter.DeleteCommand = command;

            dv.DV = new DataTable();
            adapter.Fill(dv.DV);
        }

        public bool AddRow(string departamentName)
        {
            try
            {
                DataRow dataRow = dv.DV.NewRow();
                dataRow["Name"] = departamentName;
                dv.DV.Rows.Add(dataRow);
                adapter.Update(dv.DV);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool DeleteRow(DataRowView dataRow)
        {
            try
            {
                dataRow.Row.Delete();
                adapter.Update(dv.DV);
            }
            catch 
            {
                return false;
            }
            return true;
        }

        public bool EditRow(string selectedName)
        {
            try
            {
                string com = $"UPDATE Departament SET Name = @Name WHERE Name = '{selectedName}'";
                SqlCommand command = new SqlCommand(com, connection);
                command.Parameters.Add("@Name", SqlDbType.NChar, 50, "Name");
                adapter.UpdateCommand = command;
                adapter.Update(dv.DV);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        public void Update()
        {
            dv.DV.Clear();
            adapter.Fill(dv.DV);
        }
    }
}
