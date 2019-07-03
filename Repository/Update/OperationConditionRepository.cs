using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.ConnectionString;

namespace Repository.Update
{
    public class OperationConditionRepository:Connection, IOperationConditionRepository
    {
      
        public void OperationIsBusy(string comboBoxItems, string comboBoxOperations)
        {
            string query = "UPDATE Operations SET Operations.Busy = 'true', Operations.ItemId = @Item WHERE Operations.Id = @Operation";
            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@Item", comboBoxItems);
                command.Parameters.AddWithValue("@Operation", comboBoxOperations);
                command.ExecuteNonQuery();
                connection.Close();
            }

        }
        public void OperationIsNotBusy(string comboBoxItems, string comboBoxOperations)
        {
            string query = "UPDATE Operations SET Operations.Busy = 'False', Operations.ItemId = NULL WHERE Operations.Id = @Operation AND Operations.ItemId = @Item";
            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {

                connection.Open();
                command.Parameters.AddWithValue("@Item", comboBoxItems);
                command.Parameters.AddWithValue("@Operation", comboBoxOperations);
                command.ExecuteNonQuery();
                connection.Close();
            }

        }
    }
}
