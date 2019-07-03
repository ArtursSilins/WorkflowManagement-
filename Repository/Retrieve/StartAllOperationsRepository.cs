using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Retrieve
{
    public class StartAllOperationsRepository:Repository.ConnectionString.Connection, Domain.Interfaces.IStartAllOperations
    {
        public DataTable Operations(string runItem)
        {
            DataTable dataTable = new DataTable();
            string query = "SELECT io.ItemId, io.OperationId, io.SetupTime, io.Time FROM ItemsOperations io WHERE io.ItemId = @runItem AND io.Duplicates IS NULL";
            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@runItem", runItem);
                adapter.Fill(dataTable);
                command.ExecuteNonQuery();
                connection.Close();
            }
            return dataTable;
        }
        public DataTable OperationsWithDuplicates(string runItem)
        {
            DataTable dataTable = new DataTable();
            string query = "SELECT DISTINCT io.Duplicates, MAX(io.ItemId), MAX(io.OperationId) FROM ItemsOperations io WHERE io.ItemId = @runItem AND io.Duplicates <> 'NULL' GROUP BY io.Duplicates ";
            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@runItem", runItem);
                adapter.Fill(dataTable);
                command.ExecuteNonQuery();
                connection.Close();
            }
            return dataTable;
        }
        public DataTable SpecificOperationsWithDuplicates(DataRow row, string runItem)
        {
            DataTable dataTable = new DataTable();
            string query = "SELECT io.OperationId, io.ItemId FROM ItemsOperations io WHERE io.Duplicates = @duplicates AND io.ItemId = @runItem  ";
            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@duplicates", row["Duplicates"].ToString());
                command.Parameters.AddWithValue("@runItem", runItem);
                adapter.Fill(dataTable);
                command.ExecuteNonQuery();
                connection.Close();
            }
            return dataTable;
        }
        public DataTable GetSpecificDuplicateItem(string runItem, int operationId)
        {
            DataTable dataTable = new DataTable();
            string query = "SELECT io.ItemId, io.OperationId, io.SetupTime, io.Time FROM ItemsOperations io WHERE io.ItemId = @runItem AND io.OperationId = @operationId";
            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@runItem", runItem);
                command.Parameters.AddWithValue("@operationId", operationId);
                adapter.Fill(dataTable);                
                command.ExecuteNonQuery();
                connection.Close();
            }
            return dataTable;
        }
    }
}
