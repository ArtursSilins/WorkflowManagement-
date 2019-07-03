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
    public class InLineFirstNrRepository:Connection, IInLineFirstNrRepository
    {
        public DataTable GetTable()
        {
            DataTable table = new DataTable();
            string query = "SELECT I.StartTime, I.EndTime, I.Count, I.ItemId, I.OperationId FROM InLine I WHERE I.Nr = 1";
            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                adapter.Fill(table);

                command.ExecuteNonQuery();
                connection.Close();
            }
            return table;
        }
    }
}
