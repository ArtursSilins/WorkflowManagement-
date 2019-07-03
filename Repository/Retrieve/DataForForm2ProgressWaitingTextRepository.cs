using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Retrieve
{
    public class DataForForm2ProgressTextRepository:ConnectionString.Connection, Domain.Interfaces.IDataForForm2ProgressTxtRepository
    {
        public DataTable WaitingData(string itemId)
        {
            DataTable dataTable = new DataTable();
            string query = "SELECT  il.EndTime, il.ItemId, il.OperationId FROM InLine il WHERE il.ItemId = @itemId ";

            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@itemId", itemId);
                adapter.Fill(dataTable);              
                command.ExecuteNonQuery();
                connection.Close();
            }        
            return dataTable;
        }
        public DataTable CompletedData(string itemId, string operationId)
        {
            DataTable dataTable = new DataTable();
            string query = "SELECT io.EndTime FROM ItemsOperations io " +
                "WHERE io.ItemId = @itemId AND io.OperationId = @operationId AND io.EndTime IS NULL AND io.StartTime IS NULL "+
                " AND NOT EXISTS (SELECT il.EndTime, il.StartTime FROM InLine il WHERE il.ItemId = @itemId AND il.OperationId = @operationId) ";

            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@itemId", itemId);
                command.Parameters.AddWithValue("@operationId", operationId);
                adapter.Fill(dataTable);
                command.ExecuteNonQuery();
                connection.Close();
            }
            return dataTable;
        }
    }
}
