using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Update
{
    public class Form2ProgressPictureRepository:ConnectionString.Connection, Domain.Interfaces.IForm2ProgressPictureRepository
    {     
        public void UpdatePicture(byte[] picture, DataRow row, string itemId)
        {
            string query = "UPDATE ItemsOperations SET ItemsOperations.Progress = @picture WHERE ItemsOperations.OperationId= @operationId AND ItemsOperations.ItemId = @itemId ";
            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@picture", picture);
                command.Parameters.AddWithValue("@operationId", row["OperationId"].ToString());
                command.Parameters.AddWithValue("@itemId", itemId);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void UpdatePicture(byte[] picture, DataRow row)
        {
            string query = "UPDATE ItemsOperations SET ItemsOperations.Progress = @picture WHERE ItemsOperations.OperationId= @operationId AND ItemsOperations.ItemId = @itemId ";
            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@picture", picture);
                command.Parameters.AddWithValue("@operationId", row["OperationId"].ToString());
                command.Parameters.AddWithValue("@itemId", row["ItemId"].ToString());
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
