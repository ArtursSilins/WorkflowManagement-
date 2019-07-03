using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Retrieve
{
    public class ExcelRepository:ConnectionString.Connection, Domain.Interfaces.IExcelRepository
    {
        public DataTable GetData()
        {
            DataTable dataTable = new DataTable();
            DateTime date = DateTime.Now.Date;
            string query = "SELECT Items.Name AS Item, Operations.Name, ItemsOperations.StartTime, ItemsOperations.EndTime, ItemsOperations.Count FROM ItemsOperations, Operations, Items WHERE ItemsOperations.OperationId = Operations.Id AND ItemsOperations.ItemId = Items.Id AND CAST(ItemsOperations.StartTime as date) = @date GROUP BY ItemsOperations.StartTime, Operations.Name, ItemsOperations.EndTime, Items.Name, ItemsOperations.Count " +
                " UNION ALL SELECT Items.Name AS Item, Operations.Name, InLine.StartTime AS StartTime , InLine.EndTime, InLine.Count FROM InLine, Operations, Items WHERE InLine.OperationId = Operations.Id AND InLine.ItemId = Items.Id AND CAST(InLine.StartTime as date) = @date GROUP BY InLine.StartTime, Operations.Name, InLine.EndTime, Items.Name, InLine.Count";

            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@date", date);
                adapter.Fill(dataTable);               
                command.ExecuteNonQuery();
                connection.Close();
            }
            return dataTable;
        }
    }
}
