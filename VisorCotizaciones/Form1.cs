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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            conexion c = new conexion();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
           {
            BO.Metodo mt = new BO.Metodo();
            DataTable dt = (DataTable)mt.Datos();
            DataRow row;

            DataTable dtVer = new DataTable();
            dtVer.Columns.Add("Name", typeof(String));
            dtVer.Columns.Add("ResaleID", typeof(String));
            dtVer.Columns.Add("DateQuote", typeof(DateTime));
            dtVer.Columns.Add("QuoteNum", typeof(int));
            dtVer.Columns.Add("PartNum", typeof(int));
            dtVer.Columns.Add("LineDesc", typeof(String));
            dtVer.Columns.Add("ListPrice", typeof(decimal));
            dtVer.Columns.Add("Euro", typeof(decimal));
            dtVer.Columns.Add("Dolar", typeof(decimal));
            dtVer.Columns.Add("Cop", typeof(decimal));
            DataRow dRow;
            DataTable ds = new DataTable();
            ds = mt.Datos();
            bool Existe = false;
            if (ds.Rows.Count - 1 >= 0)
            {
                for (int i = 0; i <= ds.Rows.Count - 1; i++)
                {
                    Existe = false;
                    if (dtVer.Rows.Count - 1 >= 0)
                    {
                        for (int j = 0; j <= dtVer.Rows.Count - 1; j++)
                        {
                            if (ds.Rows[i]["ResaleID"].ToString() == dtVer.Rows[j]["ResaleID"].ToString())
                            {
                                Existe = true;
                                break;
                            }
                        }
                    }
                    if (!Existe)
                    {
                        decimal Euro = 0;
                        decimal Dolar = 0;
                        decimal Cop = 0;
                        dRow = dtVer.NewRow();
                        dRow["Name"] = ds.Rows[i]["Name"].ToString();
                        dRow["ResaleID"] = ds.Rows[i]["ResaleID"].ToString();
                        dRow["DateQuote"] = Convert.ToDateTime(ds.Rows[i]["DateQuoted"]);
                        dRow["QuoteNum"] = ds.Rows[i]["QuoteNum"].ToString();
                        dRow["PartNum"] = ds.Rows[i]["PartNum"].Equals(System.DBNull.Value);
                        dRow["LineDesc"] = ds.Rows[i]["LineDesc"].ToString();
                        
                        

                        for (int h = 0; h <= ds.Rows.Count - 1; h++)
                        {
                            if (ds.Rows[i]["QuoteNum"].ToString() == ds.Rows[h]["QuoteNum"].ToString())
                            {
                                if (ds.Rows[h]["Moneda"].ToString() == "EUR")
                                {
                                    if (!string.IsNullOrEmpty(ds.Rows[h]["ListPrice"].ToString()))
                                        Euro += Convert.ToDecimal(ds.Rows[h]["ListPrice"]);
                                    
                                }
                                if (ds.Rows[h]["Moneda"].ToString() == "USD")
                                {
                                    if (!string.IsNullOrEmpty(ds.Rows[h]["ListPrice"].ToString()))
                                    Dolar += Convert.ToDecimal(ds.Rows[h]["ListPrice"]);
                                }
                                if (ds.Rows[h]["Moneda"].ToString() == "COP")
                                {
                                    if (!string.IsNullOrEmpty(ds.Rows[h]["ListPrice"].ToString()))
                                    Cop += Convert.ToDecimal(ds.Rows[h]["ListPrice"]);
                                }
                            }
                        }

                        dRow["ListPrice"] = Convert.ToDecimal(ds.Rows[i]["ListPrice"]);
                        dRow["Euro"] = Euro;
                        dRow["Dolar"] = Dolar;
                        dRow["Cop"] = Cop;
                        dtVer.Rows.Add(dRow);
                    }

                } //
                dgVerDatos.DataSource = dtVer;
            }
               
               
              
                
               
           }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
