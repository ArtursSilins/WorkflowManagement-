using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.ConnectionString;

namespace Repository.Retrieve
{
    public class ItemsOperationsForTrueFalseCheckRepository:Connection, IItemsOperationsForTrueFalseCheckReposytory
    {
        public DataTable GetItemsOperationsTable()
        {
            DataTable dataTable = new DataTable();
            string query = "SELECT ItemsOperations.ItemId, ItemsOperations.OperationId, ItemsOperations.Time, ItemsOperations.EndTime, ItemsOperations.StartTime, ItemsOperations.Count, ItemsOperations.SetupTime FROM ItemsOperations  ";
            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();               
                adapter.Fill(dataTable);
                command.ExecuteNonQuery();
                connection.Close();
            }
            return dataTable;
        }
        public DataTable GetItemsOperationsTableRunAll(string item)
        {
            DataTable dataTable = new DataTable();
            string query = "SELECT ItemsOperations.ItemId, ItemsOperations.OperationId, ItemsOperations.Time, ItemsOperations.EndTime, ItemsOperations.StartTime, ItemsOperations.Count, ItemsOperations.SetupTime FROM ItemsOperations WHERE ItemsOperations.ItemId = @item ";
            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@item", item);
                adapter.Fill(dataTable);
                command.ExecuteNonQuery();
                connection.Close();
            }
            return dataTable;
        }
    }
}
