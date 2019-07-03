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
    public class All:Connection, IAll
    {

        public DataTable Items()
        {
            string query = "SELECT * FROM Items";
            DataTable dataTable = new DataTable();

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
        public DataTable Operations()
        {
            string query = "SELECT * FROM Operations WHERE Operations.DuplicateId IS NULL";
            DataTable dataTable = new DataTable();

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
        public DataTable pauseView()
        {
            DataTable dataTable = new DataTable();
            string query = "SELECT Pause.PauseName, CAST(Pause.PauseStart AS time) AS 'Start', CAST(Pause.PauseEnd AS time) AS 'End' FROM Pause ORDER BY Pause.PauseEnd DESC";
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
        public DataTable DuplicateName()
        {
            DataTable dataTable = new DataTable();
            string query = "SELECT * FROM Duplicate";
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
