using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace VisorCotizaciones.BO
{   
    class Metodo
    {
        public DataTable Datos() 
        {
            conexion c = new conexion();
            string sql = "Select q.DateQuoted ,q.QuoteNum  as 'QuoteNum'  " +
                ",c.Name as 'Name',c.ResaleID,ISNULL(p.PartNum,0) as PartNum,CONCAT(c.Address1,c.Address2,c.Address3,c.City,c.State,c.Country) as 'Direccion',q.CurrencyCode as 'Tipo de Moneda', q.ExchangeRate as 'Tipo de Cambio', qd.LineDesc, qdu.ShortChar02 as 'Moneda',COALESCE(qd.ListPrice,0) as ListPrice , '' as 'Peso', '' as 'Euro', '' as 'Dolar'  " +
                " From Erp.QuoteHed q left outer join Erp.QSalesRP qs on q.QuoteNum=qs.QuoteNum left outer join Erp.Customer c on q.CustNum=c.CustNum  left outer join Erp.SalesTer st on q.TerritoryID=st.TerritoryID left outer join Erp.QuoteDtl qd on q.QuoteNum=qd.QuoteNum left outer join Erp.Part p on qd.PartNum=p.PartNum  left outer join Erp.QuoteDtl_UD qdu on qd.SysRowID =qdu.ForeignSysRowID     where   q.DateQuoted >= '2017-01-01' and q.DateQuoted <= '2017-01-30'  group by q.QuoteNum ,q.DateQuoted  , q.ExchangeRate ,q.CurrentStage,c.Name, q.HDCaseNum,qs.Name,st.TerritoryDesc,c.Address1,c.Address2,c.Address3,c.City,c.State,c.Country ,q.CurrencyCode,qd.LineDesc, qdu.ShortChar02,qd.ListPrice,c.ResaleID,p.PartNum ";
            DataSet dtver = new DataSet();
            SqlDataAdapter sqd = new SqlDataAdapter(sql,c.cn);
            sqd.Fill(dtver,"Fila");
            return dtver.Tables["Fila"];

        }
        public DataTable factur() 
        {
            conexion c = new conexion();
            string sql = "select  c.Name,c.CustID,c.State,c.CustNum,id.InvoiceNum,ih.DocInvoiceAmt "+
"from  Erp.Customer c inner join Erp.InvcDtl id on c.CustNum=id.CustNum inner join Erp.InvcHead ih on id.CustNum=ih.CustNum and c.CustNum>=10000 and c.CustNum<=10050";
            DataSet dtFact = new DataSet();
            SqlDataAdapter sqd = new SqlDataAdapter(sql, c.cn);
            sqd.Fill(dtFact, "Datos");
            return dtFact.Tables["Datos"];
         
        }
    }
}
