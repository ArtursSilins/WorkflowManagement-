using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Retrieve
{
    public class ItemsWithOperationsRepository:Repository.ConnectionString.Connection, Domain.Interfaces.IItemsWithOperationsRepository
    {
        public DataTable Items()
        {
            string query = "SELECT DISTINCT Items.ID, Items.Name FROM Items INNER JOIN ItemsOperations ON ItemsOperations.ItemId = Items.Id";
            DataTable dataTable = new DataTable();

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
