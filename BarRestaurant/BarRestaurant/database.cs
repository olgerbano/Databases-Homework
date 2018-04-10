using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarRestaurant
{
    class database
    {
        private void connect()
        {
            SqlConnection conn = new SqlConnection("Data Source=(local);Initial Catalog=Restaurant;Integrated Security=SSPI");
        }
    }
}
