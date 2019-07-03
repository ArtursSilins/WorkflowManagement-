using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Retrieve
{
    public class Form2TextRepository:ConnectionString.Connection, Domain.Interfaces.IForm2TextRepository
    {
        public DataTable Get(string itemId)
        {
            DataTable table = new DataTable();
            string query = "SELECT Items.Name FROM Items WHERE Items.Id = @itemId ";

            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@itemId", itemId);
                adapter.Fill(table).ToString();
                command.ExecuteNonQuery();
                connection.Close();
            }
            return table;
        }
    }
}
