using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Retrieve
{
    public class DisplayRepository:Repository.ConnectionString.Connection, Domain.Interfaces.IDisplayRepository
    {
        public DataTable ItemForDisplay()
        {
            DataTable dataTable = new DataTable();

            string query = "SELECT DISTINCT d.Id, d.Item, s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12, s13, s14, s15, s16," +
                " s17, s18, s19, s20, s21, s22, s23, s24, s25, s26, s27, s28, s29, s30, s31 " +
                "FROM Display d INNER JOIN ItemsOperations io ON io.ItemId = d.Id LEFT JOIN InLine il ON il.ItemId = d.Id " +
                "WHERE io.EndTime <> '"+null+ "' OR il.EndTime <> '" + null + "' OR io.EndTime <> '" + null + "' AND il.EndTime <> '" + null + "'";

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
