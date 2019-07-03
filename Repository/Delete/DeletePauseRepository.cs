using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Delete
{
    public class DeletePauseRepository:Repository.ConnectionString.Connection, Domain.Interfaces.IDeletePauseRepository
    {
        public void DeletPause(string pauseName)
        {
            string query = "DELETE FROM Pause WHERE Pause.PauseName = @Name ";

            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@Name", pauseName);
                command.ExecuteNonQuery();
                connection.Close();

            }
        }
    }
}
