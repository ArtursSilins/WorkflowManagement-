using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.ConnectionString;

namespace Repository.Retrieve
{
    public class ItemForGridRepository:Connection, IItemForGridRepository
    {
        public DataTable View(string item)
        {
            DataTable table = new DataTable();
            string query = "SELECT Operations.Name, ItemsOperations.Time as Time, ItemsOperations.SetupTime, Duplicate.Name as Duplicates FROM" +
                " ItemsOperations INNER JOIN Operations ON Operations.Id = ItemsOperations.OperationId " +
                " LEFT JOIN Duplicate ON Duplicate.Id = ItemsOperations.Duplicates WHERE ItemsOperations.ItemId = @item";

            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@item", item);
                adapter.Fill(table);                
                command.ExecuteNonQuery();
                connection.Close();

            }
            return table;
        }
        public DataTable ItemsWhenDisplayGridClicked(string item)
        {
            DataTable dataTable = new DataTable();
            string query = "SELECT Operations.Name, io.Time, io.Progress, io.Count, io.StartTime, io.EndTime FROM" +
                " ItemsOperations io INNER JOIN Operations ON Operations.Id = io.OperationId WHERE io.ItemId = @item ";

            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@item", item);
                adapter.Fill(dataTable).ToString();

                command.ExecuteNonQuery();
                connection.Close();
            }

            return dataTable;
        }
        public DataTable DuplicateView(string item)
        {
            DataTable dataTable = new DataTable();
            string query = "SELECT Duplicate.Name as Duplicate, Operations.Name as Operations FROM Duplicate, Operations WHERE Duplicate.Id = @item AND Operations.DuplicateId = @item";

            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@item", item);
                adapter.Fill(dataTable).ToString();

                command.ExecuteNonQuery();
                connection.Close();
            }

            return dataTable;
        }
    }
}
