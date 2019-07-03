using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Retrieve
{
    public class PauseTimesRepository: Repository.ConnectionString.Connection, Domain.Interfaces.IPauseTimesRepository
    {
        public DataTable GetPauseTimes()
        {
            DataTable dataTable = new DataTable();
            string query = "SELECT * FROM Pause";

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
