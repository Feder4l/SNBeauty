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
    public partial class Form6 : Form
    {
        static string constring = "Data Source=FEDERAL\\SQLEXPRESS01;Initial Catalog=müsteriListes;Integrated Security=True";
        SqlConnection baglanti = new SqlConnection(constring);
        public Form6()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox4.Text != "" && textBox3.Text != "")
                {
                    if (baglanti.State == ConnectionState.Open)
                        baglanti.Close();
                    baglanti.Open();
                    SqlCommand kayit = new SqlCommand("insert into satici(kullanici_adi,sifre)values(@kad,@ksifre)", baglanti);
                    kayit.Parameters.AddWithValue("@kad", textBox4.Text);
                    kayit.Parameters.AddWithValue("@ksifre", textBox3.Text);
                    kayit.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Başarıyla eklendi.");
                }
            }
            catch
            {
                MessageBox.Show("Hata!!");
            }
        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }
    }
}
