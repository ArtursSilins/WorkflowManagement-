using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Adding
{
    public class ItemsOperationsConnectionRepository:Repository.ConnectionString.Connection, Domain.Interfaces.IItemsOperationsConnectionRepository
    {
        private Graphics graphic;
      
        public void AddConnectionWithDuplicate(string itemId, string operationId, string time, string setupTime, string duplicateId)
        {

            Bitmap bitmap = new Bitmap(100, 21);
            graphic = Graphics.FromImage(bitmap);
            graphic.FillRectangle(Brushes.White, 0, 0, 100, 21);
            graphic.FillRectangle(Brushes.CornflowerBlue, 0, 0, 0, 21);
            MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);

            byte[] pic = stream.ToArray();

            string query = "INSERT INTO ItemsOperations VALUES ( @itemId, @operationId, @Time, NULL, NULL, @pic, NULL, @SetupTime, @duplicateId  ) ";

            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {


                connection.Open();
                command.Parameters.AddWithValue("@itemId", itemId);
                command.Parameters.AddWithValue("@operationId", operationId);
                command.Parameters.AddWithValue("@pic", pic);
                command.Parameters.AddWithValue("@Time", time);
                command.Parameters.AddWithValue("@SetupTime", setupTime);
                command.Parameters.AddWithValue("@duplicateId", duplicateId);

                command.ExecuteNonQuery();
                connection.Close();

            }

        }
        public void AddConnection(string itemId, string operationId, string time, string setupTime)
        {
            Bitmap bitmap = new Bitmap(100, 21);
            graphic = Graphics.FromImage(bitmap);
            graphic.FillRectangle(Brushes.White, 0, 0, 100, 21);
            graphic.FillRectangle(Brushes.CornflowerBlue, 0, 0, 0, 21);
            MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);

            byte[] pic = stream.ToArray();

            string query = "INSERT INTO ItemsOperations VALUES ( @ItemId, @OperationId, @Time, NULL, NULL, @pic, NULL, @SetupTime, NULL  ) ";

            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {

                connection.Open();
                command.Parameters.AddWithValue("@ItemId", itemId);
                command.Parameters.AddWithValue("@OperationId", operationId);
                command.Parameters.AddWithValue("@pic", pic);
                command.Parameters.AddWithValue("@Time", time);
                command.Parameters.AddWithValue("@SetupTime", setupTime);

                command.ExecuteNonQuery();


            }

        }
    }
}
