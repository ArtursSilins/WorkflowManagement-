using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Retrieve
{
    public class DuplicateOperationsRepository:ConnectionString.Connection, IDuplicateOperationsRepository
    {
        public DataTable Get(string duplicateId)
        {
            string query = "SELECT * FROM Operations WHERE Operations.DuplicateId = @duplicateId";
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {

                connection.Open();
                command.Parameters.AddWithValue("@duplicateId", duplicateId);
                adapter.Fill(dataTable);
                command.ExecuteNonQuery();
                connection.Close();

            }
            return dataTable;
        }
        public DataTable ExistingDuplicateConnections(string duplicateId)
        {
            string query = "SELECT DISTINCT io.ItemId, io.Time, io.SetupTime FROM ItemsOperations io WHERE" +
                " io.Duplicates = @duplicateId AND io.Time = (SELECT MIN(io.Time) FROM ItemsOperations io WHERE io.Duplicates = @duplicateId) AND" +
                " io.SetupTime = (SELECT MIN(io.SetupTime) FROM ItemsOperations io WHERE io.Duplicates = @duplicateId)";
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {

                connection.Open();
                command.Parameters.AddWithValue("@duplicateId", duplicateId);
                adapter.Fill(dataTable);
                command.ExecuteNonQuery();
                connection.Close();

            }
            return dataTable;
        }
    }
}
