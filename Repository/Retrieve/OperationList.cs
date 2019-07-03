using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Repository.ConnectionString;

namespace Repository.Retrieve
{
    public class OperationList:Connection, IOperationList
    {
        public DataTable List(string id)
        {
            string query = "SELECT * FROM Operations, ItemsOperations WHERE ItemsOperations.ItemId = @id AND ItemsOperations.OperationId = Operations.Id";
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {

                connection.Open();
                command.Parameters.AddWithValue("@id", id);
                adapter.Fill(dataTable);
                command.ExecuteNonQuery();
                connection.Close();

            }
            return dataTable;
        }
        public DataTable DuplicateList(string duplicateId)
        {
            string query = "SELECT * FROM Operations WHERE Operations.DuplicateId = @duplicateId ";
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
