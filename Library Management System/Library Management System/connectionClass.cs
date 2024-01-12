using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System
{
    internal class connectionClass
    {
        public static SqlConnection connect()
        {
            SqlConnection conn = new SqlConnection("Data Source=ZEKERIA\\SQLEXPRESS; Initial Catalog=Library; Integrated Security=true;");
            conn.Open();

            return conn;
        }

    }
}
