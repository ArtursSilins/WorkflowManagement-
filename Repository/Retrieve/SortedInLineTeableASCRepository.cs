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
    public class SortedInLineTeableASCRepository:Connection, ISortedInLineTableASCRepository
    {
        public DataTable Get()
        {
            DataTable table = new DataTable();
            string query = "SELECT il.ID, il.OperationId FROM InLine il ORDER BY  OperationId asc, StartTime asc ";

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
