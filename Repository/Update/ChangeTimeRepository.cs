using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Update
{
    public class ChangeTimeRepository:Repository.ConnectionString.Connection, Domain.Interfaces.IChangeTimeRepository
    {
        public void SetTime(string time, string item, string operation)
        {
            string query = "UPDATE ItemsOperations SET Time = @time WHERE ItemId = @item AND OperationId = @operation";
            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@time", time);
                command.Parameters.AddWithValue("@item", item);
                command.Parameters.AddWithValue("@operation", operation);
                command.ExecuteNonQuery();
                connection.Close();
            }

        }
        public void SetSetupTime(string setupTime, string item, string operation)
        {
            string query = "UPDATE ItemsOperations SET SetupTime = @setupTime WHERE ItemId = @item AND OperationId = @operation";
            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@setupTime", setupTime);
                command.Parameters.AddWithValue("@item", item);
                command.Parameters.AddWithValue("@operation", operation);
                command.ExecuteNonQuery();
                connection.Close();
            }

        }
    }
}
