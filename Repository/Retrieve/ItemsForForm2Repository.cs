using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Retrieve
{
    public class ItemsForForm2Repository:Repository.ConnectionString.Connection, Domain.Interfaces.IItemsForForm2Repository
    {
        public DataTable Get(string itemId)
        {
            DataTable dataTable = new DataTable();
            string query = "SELECT io.SetupTime, io.OperationId, io.EndTime, io.Count, io.Time, io.StartTime FROM" +
                " ItemsOperations io WHERE io.ItemId = @itemId ";

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
    }
}
