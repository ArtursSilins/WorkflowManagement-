using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Retrieve
{
    public class TimeRepository : Repository.ConnectionString.Connection, Domain.Interfaces.ITimeRepository
    {
        public double GetTime(DataRow row)
        {
            double time = 0;
            DataTable dataTable = new DataTable();
            string query = "SELECT ItemsOperations.Time FROM ItemsOperations WHERE ItemsOperations.ItemId = @itemId AND ItemsOperations.OperationId = @operationId";
            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@itemId", row["ItemId"]);
                command.Parameters.AddWithValue("@operationId", row["OperationId"]);
                dataAdapter.Fill(dataTable);
                foreach (DataRow item in dataTable.Rows)
                {
                    time = double.Parse(item["Time"].ToString());
                }
                command.ExecuteNonQuery();
                connection.Close();
            }
            return time;
        }
        public double GetTime(string itemId, string operationId)
        {
            double time = 0;
            DataTable dataTable = new DataTable();

            string query = "SELECT ItemsOperations.Time FROM ItemsOperations WHERE ItemsOperations.ItemId = @itemId AND ItemsOperations.OperationId = @operationId";
            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@itemId", itemId);
                command.Parameters.AddWithValue("@operationId", operationId);
                dataAdapter.Fill(dataTable);
                foreach (DataRow item in dataTable.Rows)
                {
                    time = double.Parse(item["Time"].ToString());
                }
                command.ExecuteNonQuery();
                connection.Close();
            }
            return time;
        }
        public double GetSetupTime(DataRow row)
        {
            double time = 0;
            DataTable dataTable = new DataTable();

            string query = "SELECT ItemsOperations.SetupTime FROM ItemsOperations WHERE ItemsOperations.ItemId = @itemId AND ItemsOperations.OperationId = @operationId";
            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@itemId", row["ItemId"]);
                command.Parameters.AddWithValue("@operationId", row["OperationId"]);
                dataAdapter.Fill(dataTable);
                foreach (DataRow item in dataTable.Rows)
                {
                    time = double.Parse(item["SetupTime"].ToString());
                }
                command.ExecuteNonQuery();
                connection.Close();
            }
            return time;
        }
        public double GetSetupTime(string itemId, string operationId)
        {
            double time = 0;
            DataTable dataTable = new DataTable();

            string query = "SELECT ItemsOperations.SetupTime FROM ItemsOperations WHERE ItemsOperations.ItemId = @itemId AND ItemsOperations.OperationId = @operationId";
            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@itemId", itemId);
                command.Parameters.AddWithValue("@operationId", operationId);
                dataAdapter.Fill(dataTable);
                foreach (DataRow item in dataTable.Rows)
                {
                    time = double.Parse(item["SetupTime"].ToString());
                }
                command.ExecuteNonQuery();
                connection.Close();
            }
            return time;
        }
        public DataTable GetMinStartTime(DataRow dataRow)
        {
            string query = "SELECT MIN (ItemsOperations.StartTime) as time FROM ItemsOperations WHERE ItemsOperations.ItemId = @itemId ";

            DataTable dataTable = new DataTable();

            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@itemId", dataRow["Id"]);
                adapter.Fill(dataTable);
               
                command.ExecuteNonQuery();
                connection.Close();

                return dataTable;
            }
        }
        public DataTable GetMaxEndTimeFromInLine(DataRow dataRow)
        {
            string query = "SELECT MAX (InLine.EndTime) as time FROM InLine " +
                "WHERE InLine.ItemId = @itemId";

            DataTable dataTable = new DataTable();

            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@itemId", dataRow["Id"]);
                adapter.Fill(dataTable);
               
                command.ExecuteNonQuery();
                connection.Close();

                return dataTable;
            }

        }
        
        public DateTime GetMinStartTimeFromInLine(DataRow dataRow)
        {
            string query = "SELECT MIN (InLine.StartTime) as time FROM InLine WHERE InLine.ItemId = @itemId ";
            DataTable startRindaTime = new DataTable();
            DateTime time = new DateTime();
            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@itemId", dataRow["Id"]);
                adapter.Fill(startRindaTime);
                foreach (DataRow row in startRindaTime.Rows)
                {
                    time = Convert.ToDateTime(row["time"]);
                }
                command.ExecuteNonQuery();
                connection.Close();

                return time;
            }

        }
        public DateTime GetMaxEndTimeFromItemsOperations(DataRow dataRow)
        {
            string query = "SELECT MAX (ItemsOperations.EndTime) as time FROM ItemsOperations WHERE ItemsOperations.ItemId = @itemId ";
            DataTable endTime = new DataTable();
            DateTime time = new DateTime();
            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@itemId", dataRow["ItemId"]);
                adapter.Fill(endTime);
                foreach (DataRow row in endTime.Rows)
                {
                    time = Convert.ToDateTime(row["time"]);
                }
                command.ExecuteNonQuery();
                connection.Close();

                return time;
            }
        }
        public DateTime GetMaxEndTimeFromItemsOperationsDisplay(DataRow dataRow)
        {
            string query = "SELECT MAX (ItemsOperations.EndTime) as time FROM ItemsOperations WHERE ItemsOperations.ItemId = @itemId ";
            DataTable endTime = new DataTable();
            DateTime time = new DateTime();
            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@itemId", dataRow["Id"]);
                adapter.Fill(endTime);
                foreach (DataRow row in endTime.Rows)
                {
                    time = Convert.ToDateTime(row["time"]);
                }
                command.ExecuteNonQuery();
                connection.Close();

                return time;
            }
        }
    }
}
