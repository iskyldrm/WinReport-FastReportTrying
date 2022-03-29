using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FastReport;
using FastReport.Export.PdfSimple;

namespace WinReport
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }
          private void Form1_Load(object sender, EventArgs e)
                {

                }
        private DataTable getTable(string sql)
        {
            DataTable tbl = new DataTable();

            using (SqlConnection conn = new SqlConnection(@"Server=(localdb)\mssqllocaldb;Database=Northwind;Trusted_Connection=true"))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                tbl.Load(cmd.ExecuteReader());
                conn.Close();
            }
            return tbl;

        }


        private void button1_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            DataTable tbl = getTable("Select * from Employees");
            ds.Tables.Add(tbl);

            Report report = new Report();
            report.Load("Employee.frx");
            report.RegisterData(tbl,"Employees");
            

            if (checkBox1.Checked)
            {
                report.Design();
            }
            else
            {
                report.Show();
            }

        }

      
    }
}
