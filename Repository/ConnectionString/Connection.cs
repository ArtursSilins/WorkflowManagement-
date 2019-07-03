using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Repository.ConnectionString
{
    public class Connection
    {
        public SqlConnection GetSqlConnection()
        {
            return new SqlConnection(@" Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\X\Documents\Visual Studio 2017\Projects\DatuBazeTest1\Repository\KvistData.mdf; Integrated Security = True");
        }
    }
}
