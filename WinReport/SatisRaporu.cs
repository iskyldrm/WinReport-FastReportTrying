using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinReport
{
    public class SatisRaporu
    {
        /*ALFKI Alfreds Futterkiste	0,00	0,00	0,00	0,00	0,00	0,00	0,00	1086,00	0,00	1208,00	0,00	0,00
            ANATR Ana Trujillo Emparedados y helados	0,00	0,00	0,00	0,00	0,00	0,00	0,00	480,00	0,00	0,00	320,00	0,00
            ANTON Antonio Moreno Taquería	0,00	0,00	0,00	881,00	2157,00	2082,00	0,00	0,00	1332,00	0,00	0,00	0,00
            AROUT Around the Horn	0,00	453,00	0,00	0,00	0,00	2143,00	0,00	0,00	0,00	1704,00	621,00	1668,00
            BERGS Berglunds snabbköp	0,00	1207,00	0,00	0,00	3193,00	1566,00	0,00	1504,00	4879,00	630,00	1459,00	97,00
            BLAUS Blauer See Delikatessen	0,00	0,00	0,00	286,00	0,00	330,00	464,00	0,00	0,00	0,00	0,00	0,00
            BLONP Blondesddsl père et fils	0,00	4049,00	0,00	0,00	0,00	3213,00	0,00	450,00	660,00	0,00	0,00	0,00 */
        string[] aylar = new string[] { "Ocak","Subat","Mart","Nisan","Mayıs","Haziran","Temmuz","Agustos","Eylul","Ekim","Kasım","Aralık" };
        DataTable ResultTable = new DataTable();
        string constr = @"server=(localdb)\mssqllocaldb;Database=Northwind;Trusted_Connection=true;";


        public SatisRaporu()
        {
            ResultTable = new DataTable();
             ResultTable = DataTableOlustur();
        }
        public DataTable DataTableOlustur()
        {
            
            //CusomerId için kolon tanımlaması
            DataColumn col = new DataColumn("CustomerId", typeof(string));
            col.Caption = "Customer ID";
            col.AllowDBNull = false;
            ResultTable.Columns.Add(col);
            

            col = new DataColumn("CompanyName", typeof(string));
            col.Caption = "Company Name";
            ResultTable.Columns.Add(col);
           

            for (int i = 1; i <= 12; i++)
            {
                //DataTable table = new DataTable();

                //col = new DataColumn(aylar[i - 1].ToString(), typeof(string));
                col = new DataColumn(aylar[i - 1].ToString(), typeof(decimal));
                col.DefaultValue = 0;
                ResultTable.Columns.Add(col);
                
                
            }

            return ResultTable;
        }


        public DataTable VerileriDoldur()
        {
            DataTable customerTbl;

            string sql = "Select CustomerId,CompanyName from Customers";
            customerTbl = getData(sql);

            foreach (DataRow item in customerTbl.Rows)
            {
                DataRow NewRowM = ResultTable.NewRow();
                NewRowM["CustomerID"] = item["CustomerId"];
                NewRowM["CompanyName"] = item["CompanyName"];

                //ResultTable.Rows.Add(NewRowM);

                sql = "Select month(o.orderdate) ay ,sum(od.UnitPrice*od.Quantity) ciro from orders o inner join [Order Details] od";
                sql += " on o.OrderId = od.OrderId where o.CustomerId = '"+item["CustomerId"].ToString()+"' and year(o.OrderDate) = 1997 ";
                sql += "Group by month(o.OrderDate)";

                DataTable cirolar = getData(sql);
                foreach (DataRow aylarRow in cirolar.Rows)
                {
                    // aylar string dizisinde datarowdan gelen ay ismi bulunur
                    int gelenAy = int.Parse(aylarRow["ay"].ToString()) - 1;

                    string bulunanAyIsmi = aylar[gelenAy];

                    NewRowM[bulunanAyIsmi] = aylarRow["Ciro"];
                }
                ResultTable.Rows.Add(NewRowM);

                
            }
            return ResultTable;
        }

        public DataTable getData(string sql)
        {
            DataTable table = new DataTable();

            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                table.Load(cmd.ExecuteReader());
                conn.Close();
            }

            return table;
        }
    }
}
