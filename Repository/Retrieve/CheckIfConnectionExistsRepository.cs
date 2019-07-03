using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Retrieve
{
    public class CheckIfConnectionExistsRepository:ConnectionString.Connection, Domain.Interfaces.ICheckIfConnectionExistsRepository
    {
        public DataTable GetTable(string item, string alloperations)
        {
            DataTable dataTable = new DataTable();

            string query = "SELECT io.ItemId, io.OperationId FROM ItemsOperations io WHERE io.ItemId = @item AND io.OperationId = @alloperations ";
            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@item", item);
                command.Parameters.AddWithValue("@alloperations", alloperations);
                adapter.Fill(dataTable);
                command.ExecuteNonQuery();
                connection.Close();
            }
            return dataTable;
        }
        public DataTable GetDuplicateTable(string item, string duplicateId)
        {
            DataTable dataTable = new DataTable();

            string query = "SELECT io.ItemId, io.Duplicates FROM ItemsOperations io WHERE io.ItemId = @item AND io.Duplicates = @duplicateId ";
            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@item", item);
                command.Parameters.AddWithValue("@duplicateId", duplicateId);
                adapter.Fill(dataTable);
                command.ExecuteNonQuery();
                connection.Close();
            }
            return dataTable;
        }
    }
}
