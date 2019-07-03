using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Retrieve
{
    public class IsBusyCheckRepository:Repository.ConnectionString.Connection, Domain.Interfaces.IIsBusyCheckRepository
    {
        public DataTable GetTable(string operation)
        {
            DataTable dataTable = new DataTable();
            string query = "SELECT Operations.Busy FROM Operations WHERE Operations.Id = @operation";
            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@operation", operation);
                dataAdapter.Fill(dataTable);
              
                command.ExecuteNonQuery();
                connection.Close();
            }
            return dataTable;
        }
        public DataTable GetItemTable(string item)
        {
            DataTable dataTable = new DataTable();
            string query = "SELECT Operations.ItemId FROM Operations WHERE Operations.ItemId = @item";
            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@item", item);
                dataAdapter.Fill(dataTable);

                command.ExecuteNonQuery();
                connection.Close();
            }
            return dataTable;
        }
        public DataTable GetDuplicateTable(string duplicateId)
        {
            DataTable dataTable = new DataTable();
            string query = "SELECT Operations.Busy FROM Operations WHERE Operations.DuplicateId = @duplicateId";
            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@duplicateId", duplicateId);
                dataAdapter.Fill(dataTable);

                command.ExecuteNonQuery();
                connection.Close();
            }
            return dataTable;
        }
    }
}
