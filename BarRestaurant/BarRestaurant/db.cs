using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarRestaurant
{
    class Db
    {
        public Db()
        {
        }

        public static SqlConnection GetConnection()
        {
            string str = "Data Source=(local);Initial Catalog=Restaurant;Integrated Security=SSPI;MultipleActiveResultSets=true";

            SqlConnection con = new SqlConnection(str);
            con.Open();
            return con;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
