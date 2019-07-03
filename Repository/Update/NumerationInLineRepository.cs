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
    public class NumerationInLineRepository:Connection, INumerationInLineRepository
    {
        public void AddNewNr(DataRow row, int a)
        {
            string query = "UPDATE InLine SET InLine.Nr = @Nr WHERE Id = @Id AND OperationId = @operationId ";

            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@Nr", a);
                command.Parameters.AddWithValue("@Id", row["Id"]);
                command.Parameters.AddWithValue("@operationId", row["OperationId"]);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void ArrangeOneItemOperations(DataRow row)
        {
            string query = "UPDATE InLine SET InLine.Nr -= 1 WHERE InLine.ItemId = @itemId AND InLine.OperationId = @operationId";

            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@itemId", row["ItemId"]);
                command.Parameters.AddWithValue("@operationId", row["OperationId"]);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
