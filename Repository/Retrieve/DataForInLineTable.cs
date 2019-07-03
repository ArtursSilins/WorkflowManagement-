using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Retrieve
{
    public class DataForInLineTable:Repository.ConnectionString.Connection, Domain.Interfaces.IDataForInLineTable
    {
        public DataTable GetFromItemsOperations(string operation)
        {
            string query = "SELECT IO.EndTime, IO.OperationId, IO.SetupTime, IO.Time, IO.Count FROM ItemsOperations IO WHERE IO.OperationId = @operation AND IO.EndTime = (SELECT DISTINCT MAX(ItemsOperations.EndTime) FROM ItemsOperations WHERE ItemsOperations.OperationId = @operation)";
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@operation", operation);
                adapter.Fill(dataTable);
                command.ExecuteNonQuery();
                connection.Close();
            }
            return dataTable;
        }
        public DataTable GetFromInLine(string item, string operation)
        {
            string query = "select InLine.EndTime, IO.Time, IO.SetupTime FROM InLine, ItemsOperations IO WHERE" +
                " InLine.ItemId = @item AND InLine.OperationId = @operation AND IO.ItemId = @item AND IO.OperationId = @operation";
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@item", item);
                command.Parameters.AddWithValue("@operation", operation);
                adapter.Fill(dataTable);
                command.ExecuteNonQuery();
                connection.Close();
            }
            return dataTable;
        }
        public DataTable GetOperationsId(int operation)
        {
            DataTable dataTable = new DataTable();

            string query = "SELECT InLine.OperationId FROM InLine ";
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
    }
}
