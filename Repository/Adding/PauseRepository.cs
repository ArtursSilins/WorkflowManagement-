using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Adding
{
    public class PauseRepository:Repository.ConnectionString.Connection, Domain.Interfaces.IPauseRepository
    {
        public void addPause(string pauseName, DateTime pauseStart, DateTime pauseEnd)
        {
            string query = "INSERT INTO Pause VALUES (@Name, @pauseStart, @pauseEnd) ";

            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {

                connection.Open();

                command.Parameters.AddWithValue("@Name", pauseName);
                command.Parameters.AddWithValue("@pauseStart", pauseStart);
                command.Parameters.AddWithValue("@pauseEnd", pauseEnd);
                command.ExecuteNonQuery();

                connection.Close();

            }
        }
    }
}
