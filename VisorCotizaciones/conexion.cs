using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace VisorCotizaciones
{
    class conexion
    {
      public  SqlConnection cn;
        SqlCommand cmd;
        SqlDataReader dr;
        public conexion()
        {
            try
            {
                cn = new SqlConnection("Data Source=galgadot;Initial Catalog=Epicor10Test;Persist Security Info=True;User ID=sa;Password=Susana7*");
                cn.Open();
                MessageBox.Show("Conectado");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Conectado"+ex.ToString());
            }

        }
    }
}
