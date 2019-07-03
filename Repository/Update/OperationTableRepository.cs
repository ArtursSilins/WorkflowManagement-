using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.ConnectionString;

namespace Repository.Update
{
    public class OperationTableRepository:Connection, IOperationTableRepository
    {
        public void AddToOperations(DataRow row)
        {
            string query = "UPDATE  IO SET IO.StartTime = '@startTime' IO.EndTime = '@endTime' IO.Count = @count " +
                "FROM ItemsOperations IO INNER JOIN InLine IL ON IO.ItemId = IL.ItemId AND IO.OperationId = IL.OperationId";

            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();               
                command.Parameters.AddWithValue("@startTime", Convert.ToDateTime(row["StartTime"]).ToString("yyyy-MM-dd HH:mm:ss"));
                command.Parameters.AddWithValue("@endTime", Convert.ToDateTime(row["EndTime"]).ToString("yyyy-MM-dd HH:mm:ss"));
                command.Parameters.AddWithValue("@count", row["Count"]);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        
    }
}
