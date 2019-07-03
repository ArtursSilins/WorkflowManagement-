using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Update
{
    public class UpdateDisplayRepository:Repository.ConnectionString.Connection, Domain.Interfaces.IUpdateDisplayRepository
    {
        public void AddPictureToDisplay(DataRow row, DataColumn column, byte[] picture, int i, int columnCount)
        {
            string query = "UPDATE Display SET Display.s" + columnCount.ToString() + " = @picture WHERE Display.Id = @id  ";
            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.Parameters.AddWithValue("@picture", picture);
                command.Parameters.AddWithValue("@id", int.Parse(row[i].ToString()));
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
