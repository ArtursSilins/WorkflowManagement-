using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Delete
{
    public class ItemsOperationsRepository:Repository.ConnectionString.Connection, Domain.Interfaces.IItemsOperationsRepository
    {
        public void DeleteOperationFromItemsOperations(string operation)
        {
            string query = "DELETE FROM ItemsOperations WHERE ItemsOperations.OperationId = @operation";

            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@operation", operation);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void DeleteOperationFromOperations(string operation)
        {
            string query = "DELETE FROM Operations WHERE Operations.Id = @operation";

            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@operation", operation);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void DeleteOperationFeomInLine(string operation)
        {
            string query = "DELETE FROM InLine WHERE InLine.OperationId = @operation";

            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@operation", operation);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void DeleteItemFromDisplay(string item)
        {
            string query = "DELETE FROM Display WHERE Display.Id = @item";

            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@item", item);
                command.ExecuteNonQuery(); 
                connection.Close();
            }
        }
        public void DeleteItemFromInLine(string item)
        {
            string query = "DELETE FROM InLine WHERE InLine.ItemId = @item";

            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@item", item);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void DeleteItemFromItemsOperations(string item)
        {
            string query = "DELETE FROM ItemsOperations WHERE ItemsOperations.ItemId = @item";

            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@item", item);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void DeleteItemFromItems(string item)
        {
            string query = "DELETE FROM Items WHERE Items.Id = @item";

            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@item", item);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void DeleteConnection(string item, string operation)
        {
            string query = "DELETE FROM ItemsOperations WHERE ItemsOperations.ItemId = @item AND ItemsOperations.OperationId = @operation ";

            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@item", item);
                command.Parameters.AddWithValue("@operation", operation);
                command.ExecuteNonQuery();
                connection.Close();
            }

        }
        public void DeleteDuplicateOperationFromItemsOperations(string duplicateId)
        {
            string query = "DELETE FROM ItemsOperations WHERE ItemsOperations.Duplicates = @duplicateId";

            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@duplicateId", duplicateId);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void DeleteDuplicateOperationFromOperations(string duplicateId)
        {
            string query = "DELETE FROM Operations WHERE Operations.DuplicateId = @duplicateId";

            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@duplicateId", duplicateId);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void DeleteFromDuplicate(string duplicateId)
        {
            string query = "DELETE FROM Duplicate WHERE Duplicate.Id = @duplicateId";

            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@duplicateId", duplicateId);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void DeleteDuplicateConnection(string item, string duplicateId)
        {
            string query = "DELETE FROM ItemsOperations WHERE ItemsOperations.ItemId = @itemId AND ItemsOperations.Duplicates = @duplicateId";

            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@itemId", item);
                command.Parameters.AddWithValue("@duplicateId", duplicateId);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
