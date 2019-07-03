using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.ConnectionString;

namespace Repository.Delete
{
    public class ElapsedInLineTimesRepository:Connection, IElapsedInLineTimesRepository
    {
        public void DeleteEndTimes()
        {
            DateTime timeNow = DateTime.Now;
            string query = "DELETE FROM Inline WHERE Inline.EndTime < '" + timeNow.ToString("yyyy-MM-dd HH:mm:ss") + "'";

            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void DeleteStarTimes()
        {
            DateTime timeNow = DateTime.Now;
            string query = "DELETE FROM Inline WHERE Inline.StartTime < '" + timeNow.ToString("yyyy-MM-dd HH:mm:ss") + "'";

            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void DeleteRow(DataRow row)
        {
            DateTime timeNow = DateTime.Now;
            string query = "DELETE FROM InLine WHERE InLine.ItemId = "+row["ItemId"]+" AND InLine.OperationId = "+row["OperationId"]+"";

            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
