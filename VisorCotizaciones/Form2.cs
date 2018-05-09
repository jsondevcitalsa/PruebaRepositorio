using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace VisorCotizaciones
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try 
            {
            
                DataTable dtVFact = new DataTable();
                dtVFact.Columns.Add("Name", typeof(String));
                dtVFact.Columns.Add("CustNum", typeof(String));
                dtVFact.Columns.Add("State", typeof(String));
                dtVFact.Columns.Add("CantFact", typeof(Decimal));
                dtVFact.Columns.Add("Total", typeof(Decimal));

                BO.Metodo mt = new BO.Metodo();
                DataTable dtFac = new DataTable();
                dtFac = mt.factur();
                
                DataRow dRow;
                DataTable ds = new DataTable();
                ds = mt.factur();
                bool Existe = false;


                if (ds.Rows.Count - 1 >= 0)
                {
                    for (int i = 0; i <= ds.Rows.Count - 1; i++)
                    {
                        Existe = false;
                        if (dtVFact.Rows.Count - 1 >= 0)
                        {
                            for (int j = 0; j <= dtVFact.Rows.Count - 1; j++)
                            {
                                if (ds.Rows[i]["CustNum"].ToString() == dtVFact.Rows[j]["CustNum"].ToString())
                                {
                                    Existe = true;
                                    break;
                                }
                            }
                        }
                        if (!Existe)
                        {
                            decimal suma = 0;
                            decimal Contador = 0;

                            dRow = dtVFact.NewRow();
                            dRow["Name"] = ds.Rows[i]["Name"].ToString();
                            dRow["CustNum"] = ds.Rows[i]["CustNum"].ToString();
                            dRow["State"] = ds.Rows[i]["State"].ToString();
                            dRow["CustNum"] = Convert.ToInt32(ds.Rows[i]["CustNum"]);

                            for (int h = 0; h <= ds.Rows.Count - 1; h++)
                            {
                                if (ds.Rows[i]["CustNum"].ToString() == ds.Rows[h]["CustNum"].ToString())
                                {
                                    Contador++;
                                    suma = suma + Convert.ToDecimal (ds.Rows[i]["DocInvoiceAmt"]);
                                }
                            }
                            dRow["CantFact"] = Contador;
                            dRow["Total"] = suma;
                            dtVFact.Rows.Add(dRow);
                        }
                    } 
                    dtVerDatos.DataSource = dtVFact;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
