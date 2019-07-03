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
    public class ItemsOperationsNullRepository:Connection, IItemsOperationsNullRepository
    {
        public void InsertNull(byte[] picture, DataRow row)
        {
            string query = "UPDATE ItemsOperations SET ItemsOperations.StartTime = NULL, ItemsOperations.EndTime = NULL, ItemsOperations.Count = NULL, ItemsOperations.Progress = @picture WHERE ItemsOperations.OperationId = @OperationId AND ItemsOperations.ItemId = @ItemId ";
            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {

                connection.Open();
                command.Parameters.AddWithValue("@picture", picture);
                command.Parameters.AddWithValue("@OperationId", row["OperationId"]);
                command.Parameters.AddWithValue("@ItemId", row["ItemId"]);
                command.ExecuteNonQuery();
                connection.Close();
            }

        }
        public void InsertNullWhereEndTimeExpired(byte[] picture, DataRow row)
        {
            string query = "UPDATE ItemsOperations SET ItemsOperations.StartTime = NULL, ItemsOperations.EndTime = NULL, ItemsOperations.Count = NULL, ItemsOperations.Progress = @picture  WHERE ItemsOperations.EndTime = @endTime ";
            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {

                connection.Open();
                command.Parameters.AddWithValue("@picture", picture);
                command.Parameters.AddWithValue("@endTime", Convert.ToDateTime(row["EndTime"]).ToString("yyyy-MM-dd HH:mm:ss"));
                command.ExecuteNonQuery();
                connection.Close();
            }

        }
    }
}
