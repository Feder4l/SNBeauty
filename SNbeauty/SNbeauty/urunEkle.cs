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
    public partial class urunEkle : Form
    {
        public urunEkle()
        {
            InitializeComponent();  // Form bileşenlerini başlatır
        }

        // SQL bağlantısı oluşturma
        SqlConnection baglanti = new SqlConnection("Data Source=FEDERAL\\SQLEXPRESS01;Initial Catalog=müsteriListes;Integrated Security=True");

        // Kategori verilerini ComboBox'a yükleme
        private void kategorigetir()
        {
            baglanti.Open();  // Bağlantıyı aç
            SqlCommand komut = new SqlCommand("select *from kategori", baglanti);  // SQL sorgusu oluştur
            SqlDataReader read = komut.ExecuteReader();  // Verileri oku
            while (read.Read())  // Tüm verileri okuyana kadar
            {
                comboKategori.Items.Add(read["kategori"].ToString());  // ComboBox'a kategorileri ekle
            }
            baglanti.Close();  // Bağlantıyı kapat
        }

        // Kategori ComboBox'ı değiştirildiğinde çalışan olay
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboMarka.Items.Clear();  // Marka ComboBox'ını temizle
            comboMarka.Text = "";  // Marka ComboBox'ını sıfırla
            baglanti.Open();  // Bağlantıyı aç
            SqlCommand komut = new SqlCommand("select kategori,marka from markabilgileri where kategori='" + comboKategori.SelectedItem + "' ", baglanti);  // SQL sorgusu
            SqlDataReader read = komut.ExecuteReader();  // Verileri oku
            while (read.Read())  // Tüm verileri okuyana kadar
            {
                comboMarka.Items.Add(read["marka"].ToString());  // ComboBox'a markaları ekle
            }
            baglanti.Close();  // Bağlantıyı kapat
        }

        private void btnyeniekle_Click(object sender, EventArgs e)
        {
            baglanti.Open();  // Bağlantıyı aç
            SqlCommand komut = new SqlCommand("insert into urun(barkodno,kategori,marka,alisfiyati,satisfiyati,urunadi) values(@barkodno,@kategori,@marka,@alisfiyati,@satisfiyati,@urunadi)", baglanti);  // SQL sorgusu
            komut.Parameters.AddWithValue("@barkodno", txtBarkodNo.Text);  // Parametrelerle değerleri ekle
            komut.Parameters.AddWithValue("@kategori", comboKategori.Text);
            komut.Parameters.AddWithValue("@marka", comboMarka.Text);
            komut.Parameters.AddWithValue("@alisfiyati", double.Parse(txtAlışfiyatı.Text));
            komut.Parameters.AddWithValue("@satisfiyati", double.Parse(txtSatışFiyatı.Text));
            komut.Parameters.AddWithValue("@urunadi", txtÜrünAdı.Text);
            komut.ExecuteNonQuery();  // Sorguyu çalıştır
            baglanti.Close();  // Bağlantıyı kapat
            MessageBox.Show("Ürün Eklendi!");  // Başarı mesajı
            foreach (Control item in groupBox1.Controls)  // Grup kutusu içindeki tüm kontrol elemanlarını döngüye al
            {
                if (item is TextBox)  // Eğer kontrol bir TextBox ise
                {
                    item.Text = "";  // TextBox içeriğini temizle
                }
                if (item is ComboBox)  // Eğer kontrol bir ComboBox ise
                {
                    item.Text = "";  // ComboBox seçimini temizle
                }
            }
        }

        // Form yüklendiğinde çalışan olay
        private void urunEkle_Load(object sender, EventArgs e)
        {
            kategorigetir();  // Kategorileri yükleme metodu çağrılır
        }

        // Barkod numarası TextBox'ı değiştiğinde çalışan olay (boş, kullanılmıyor)
        private void BarkodNotxt_TextChanged(object sender, EventArgs e)
        {
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void label13_Click(object sender, EventArgs e)
        {
        }
    }
}
