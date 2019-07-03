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
    public class CheckIfExistsRepository:Connection, ICheckIfExistRepository
    {
        public DataTable GetItemTable(string id)
        {
            DataTable dataTable = new DataTable();

            string query = "SELECT Items.Id from Items WHERE Items.Id = @id ";
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
        public DataTable GetOperationTable(string id)
        {
            DataTable dataTable = new DataTable();

            string query = "SELECT Operations.Id from Operations WHERE Operations.Id = @id ";
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
        public DataTable GetInLineItemOperation(string itemId, string operationId)
        {
            DataTable dataTable = new DataTable();
            string query = "SELECT InLine.OperationId FROM InLine WHERE InLine.ItemId = @itemId AND InLine.OperationId = @operationId ";
            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@itemId", itemId);
                command.Parameters.AddWithValue("@operationId", operationId);
                adapter.Fill(dataTable);
                command.ExecuteNonQuery();
                connection.Close();
            }
            return dataTable;
        }
    }
}
