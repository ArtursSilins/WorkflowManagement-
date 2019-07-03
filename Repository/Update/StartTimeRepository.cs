using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Update
{
    public class StartTimeRepository:Repository.ConnectionString.Connection, Domain.Interfaces.IStartTimeRepository
    {
        public void AddStartTime(DateTime time, DateTime endTime, string count, string item, string operation)
        {
            string query = "UPDATE ItemsOperations SET ItemsOperations.StartTime = @time" +
                ", ItemsOperations.EndTime = @endTime" +
                ", ItemsOperations.Count = @count " +
                "WHERE ItemsOperations.ItemId = @item AND ItemsOperations.OperationId = @operation";

            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@time", time);
                command.Parameters.AddWithValue("@endTime", endTime);
                command.Parameters.AddWithValue("@count", count);
                command.Parameters.AddWithValue("@item", item);
                command.Parameters.AddWithValue("@operation", operation);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void AddStartTime(DateTime time, DateTime endTime, DataRow row, string count)
        {
            string query = "UPDATE ItemsOperations SET ItemsOperations.StartTime = @time" +
                ", ItemsOperations.EndTime = @endTime" +
                ", ItemsOperations.Count = @count " +
                "WHERE ItemsOperations.ItemId = @item AND ItemsOperations.OperationId = @operation";

            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@time", time);
                command.Parameters.AddWithValue("@endTime", endTime);
                command.Parameters.AddWithValue("@count", count);
                command.Parameters.AddWithValue("@item", row["ItemId"].ToString());
                command.Parameters.AddWithValue("@operation", row["OperationId"].ToString());
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void AddInLineStartTime(int lineNr, DateTime startTime, DateTime endTime, string count, string item, string operation)
        {
            string query = "INSERT INTO  InLine VALUES ( @lineNr, @item, @operation, @endTime, @count, @startTime )";
            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@lineNr", lineNr);
                command.Parameters.AddWithValue("@startTime", startTime);
                command.Parameters.AddWithValue("@endTime", endTime);
                command.Parameters.AddWithValue("@count", count);
                command.Parameters.AddWithValue("@item", item);
                command.Parameters.AddWithValue("@operation", operation);
                command.ExecuteNonQuery();
                connection.Close();
            }

        }
    }
}
