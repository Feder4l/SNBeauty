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
    public partial class frmMarka : Form
    {
        public frmMarka()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=FEDERAL\\SQLEXPRESS01;Initial Catalog=müsteriListes;Integrated Security=True");
        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            // SQL sorgusu ile markabilgileri tablosuna yeni bir marka eklenir.
            SqlCommand komut = new SqlCommand("insert into markabilgileri(kategori,marka) values ('" + comboBox1.Text + "', '" + textBox1.Text + "')", baglanti);
            // Komut veritabanında çalıştırılır.
            komut.ExecuteNonQuery();
            baglanti.Close();
            // TextBox ve ComboBox içerikleri temizlenir.
            textBox1.Text = "";
            comboBox1.Text = "";

            // Kullanıcıya bilgi mesajı gösterilir.
            MessageBox.Show("Marka eklendi");
        }
        // Kategorileri ComboBox'a getiren metot.
        private void kategorigetir()
        {
            // Veritabanı bağlantısı açılır.
            baglanti.Open();
            // SQL sorgusu ile kategori tablosundan kategoriler alınır.
            SqlCommand komut = new SqlCommand("select *from kategori", baglanti);
            // Sorgu sonucu okuyucu oluşturulur.
            SqlDataReader read = komut.ExecuteReader();
            // Okuyucu üzerinde dönerek ComboBox'a kategoriler eklenir.
            while (read.Read())
            {
                comboBox1.Items.Add(read["kategori"].ToString());
            }
            // Veritabanı bağlantısı kapatılır.
            baglanti.Close();
        }
        // Form yüklendiğinde çalışacak metot.
        private void frmMarka_Load(object sender, EventArgs e)
        {
            // Kategorileri ComboBox'a getir.
            kategorigetir();
        }
    }
}
