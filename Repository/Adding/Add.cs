using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Repository.ConnectionString;

namespace Repository.Adding
{
    public class Add : Connection, IAdd
    {
        public static Graphics graphic;
       
        public void Item(string id, string name)
        {
            string query = "INSERT INTO Items VALUES ( @Id, @Name ) ";

            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))

            {
                connection.Open();
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@Name", name);
                command.ExecuteNonQuery();
                connection.Close();
            }
           
        }
        public void Operation(string id, string name)
        {          
            string query = "INSERT INTO Operations VALUES (@Id, @Name, 'False', NULL, NULL ) ";

            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {

                connection.Open();
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@Name", name);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void ItemToDisplay(string id, string name)
        {
            Bitmap bitmap = new Bitmap(100, 21);
            graphic = Graphics.FromImage(bitmap);
            graphic.FillRectangle(Brushes.White, 0, 0, 100, 21);
            graphic.FillRectangle(Brushes.CornflowerBlue, 0, 0, 0, 21);
            MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);

            byte[] pic = stream.ToArray();

            string query = "INSERT INTO Display VALUES ( @Id, @Name, @pic, @pic, @pic, @pic, @pic, @pic, @pic, @pic, @pic, @pic, @pic, @pic, @pic, @pic, @pic, @pic, @pic, @pic, @pic, @pic, @pic, @pic, @pic, @pic, @pic, @pic, @pic, @pic, @pic, @pic, @pic   ) ";

            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {


                connection.Open();
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@pic", pic);

                command.ExecuteNonQuery();
                connection.Close();
            }

        }
        public void DuplicateName(string name)
        {
            string query = "INSERT INTO Duplicate VALUES ( @Name ) ";

            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {

                connection.Open();
                command.Parameters.AddWithValue("@Name", name);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void DuplicateOperation(string id, string name, string duplicateId)
        {
            string query = "INSERT INTO Operations VALUES(@id, @Name, 'False', NULL, @duplicateId ) ";

            using (SqlConnection connection = GetSqlConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {

                connection.Open();
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@duplicateId", duplicateId);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
