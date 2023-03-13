using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerNetApp
{
    class SQLConn
    {
        public static SqlConnection myConn;

        public static SqlConnection newConn(string server, string login, string password)
        {
            myConn = new SqlConnection(String.Format("Server={0}; User = {1}; Password = {2};", server, login, password));
            return myConn;
        }

        public static void closeConn(SqlConnection connection)
        {
            connection.Close();
        }

    }
}
