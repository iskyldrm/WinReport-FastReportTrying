using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinReport
{
    public partial class DataTableOrnek : Form
    {
        public DataTableOrnek()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SatisRaporu satisRaporu = new SatisRaporu();

            dataGridView1.DataSource = satisRaporu.VerileriDoldur();
        }
    }
}
