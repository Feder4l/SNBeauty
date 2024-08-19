using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SNbeauty
{
    public partial class frmSatişListele : Form
    {
        public frmSatişListele()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=FEDERAL\\SQLEXPRESS01;Initial Catalog=müsteriListes;Integrated Security=True");
        DataSet daset = new DataSet();
       private void satislistele()
            {
                baglanti.Open();
                SqlDataAdapter adtr = new SqlDataAdapter("select *from satis", baglanti);
                adtr.Fill(daset, "satis");
                dataGridView1.DataSource = daset.Tables["satis"];
                baglanti.Close();

            }
        private void frmSatişListele_Load(object sender, EventArgs e)
        {
        }
    }
}
