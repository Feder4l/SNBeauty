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
    public partial class urunListesi : Form
    {
        public urunListesi()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=FEDERAL\\SQLEXPRESS01;Initial Catalog=müsteriListes;Integrated Security=True");
        DataSet daset = new DataSet();
        private void kategorigetir()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from kategori", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                combokategori.Items.Add(read["kategori"].ToString());
            }
            baglanti.Close();
        }
        private void Markatxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnvarolanaekle_Click(object sender, EventArgs e)
        {
            baglanti.Open(); // Veritabanı bağlantısını aç

            // UPDATE SQL komutu ile urun tablosundaki belirli bir ürünün bilgilerini güncelliyoruz.
            SqlCommand komut = new SqlCommand("update urun set alisfiyati=@alisfiyati, satisfiyati=@satisfiyati, urunadi=@urunadi where barkodno=@barkodno", baglanti);

            // Parametrelerimize değerleri atıyoruz
            komut.Parameters.AddWithValue("@barkodno", BarkodNotxt.Text); // Barkod numarası
            komut.Parameters.AddWithValue("@alisfiyati", double.Parse(AlışFiyatıtxt.Text)); // Alış fiyatı
            komut.Parameters.AddWithValue("@satisfiyati", double.Parse(Satışfiyatıtxt.Text)); // Satış fiyatı
            komut.Parameters.AddWithValue("@urunadi", ÜrünAdıtxt.Text); // Ürün adı

            // SQL komutunu veritabanında çalıştırıyoruz
            komut.ExecuteNonQuery();

            baglanti.Close(); // Veritabanı bağlantısını kapat

            daset.Tables["urun"].Clear(); // DataSet içindeki "urun" tablosunu temizliyoruz
            ÜrünListele(); // Ürünleri tekrar listeliyoruz

            MessageBox.Show("Güncelleme yapıldı"); // Kullanıcıya bilgilendirme mesajı gösteriyoruz

            // Formdaki tüm TextBox bileşenlerini temizliyoruz
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
        }


        private void ÜrünAdıtxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void Satışfiyatıtxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void BarkodNotxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void AlışFiyatıtxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void kategoritxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void urunListesi_Load(object sender, EventArgs e)
        {
            ÜrünListele();
            kategorigetir();
        }

       private void ÜrünListele()
{
    baglanti.Open(); // Veritabanı bağlantısını aç

    // SqlDataAdapter ile urun tablosundaki tüm verileri çekiyoruz
    SqlDataAdapter adtr = new SqlDataAdapter("select * from urun", baglanti);
    adtr.Fill(daset, "urun"); // DataTable içine verileri dolduruyoruz
    dataGridView2.DataSource = daset.Tables["urun"]; // DataGridView'e verileri bağlıyoruz

    baglanti.Close(); // Veritabanı bağlantısını kapat
}

private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
{
    // DataGridView'de bir hücreye çift tıklandığında yapılacak işlemler
    // Seçilen ürünün bilgilerini ilgili TextBox'lara dolduruyoruz
    BarkodNotxt.Text = dataGridView2.CurrentRow.Cells["barkodno"].Value.ToString();
    kategoritxt.Text = dataGridView2.CurrentRow.Cells["kategori"].Value.ToString();
    Markatxt.Text = dataGridView2.CurrentRow.Cells["marka"].Value.ToString();
    AlışFiyatıtxt.Text = dataGridView2.CurrentRow.Cells["alisfiyati"].Value.ToString();
    Satışfiyatıtxt.Text = dataGridView2.CurrentRow.Cells["satisfiyati"].Value.ToString();
    ÜrünAdıtxt.Text = dataGridView2.CurrentRow.Cells["urunadi"].Value.ToString();
}

private void button1_Click(object sender, EventArgs e)
{
    if (BarkodNotxt.Text != "")
    {
        baglanti.Open(); // Veritabanı bağlantısını aç

        // Seçilen ürünün kategori ve markasını güncellemek için SQL UPDATE komutu kullanıyoruz
        SqlCommand komut = new SqlCommand("update urun set kategori=@kategori,marka=@marka where barkodno=@barkodno ", baglanti);
        komut.Parameters.AddWithValue("@barkodno", BarkodNotxt.Text); // Barkod numarası
        komut.Parameters.AddWithValue("@kategori", combokategori.Text); // Yeni kategori
        komut.Parameters.AddWithValue("@marka", combomarka.Text); // Yeni marka

        komut.ExecuteNonQuery(); // SQL komutunu çalıştır

        baglanti.Close(); // Veritabanı bağlantısını kapat

        MessageBox.Show("Güncelleme yapıldı"); // Kullanıcıya güncelleme yapıldı mesajı göster

        daset.Tables["urun"].Clear(); // DataSet içindeki "urun" tablosunu temizle
        ÜrünListele(); // Güncellenmiş ürün listesini tekrar yükle
    }
    else
    {
        MessageBox.Show("Barkod no yazılı değil!"); // Hata mesajı
    }

    // Formdaki tüm ComboBox bileşenlerini temizle
    foreach (Control item in this.Controls)
    {
        if (item is ComboBox)
        {
            item.Text = "";
        }
    }
}


        private void combokategori_SelectedIndexChanged(object sender, EventArgs e)
        {


            combomarka.Items.Clear();
            combomarka.Text = "";
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select kategori,marka from markabilgileri where kategori='" + combokategori.SelectedItem + "' ", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                combomarka.Items.Add(read["marka"].ToString());
            }
            baglanti.Close();
        }

        private void sil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from urun where barkodno='" + dataGridView2.CurrentRow.Cells["barkodno"].Value.ToString()+"'",baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            daset.Tables["urun"].Clear();
            ÜrünListele();
            MessageBox.Show("Kayıt Silindi");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DataTable tablo = new DataTable();
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select *from urun where barkodno like '%"+ txtBarkodNogoreAra.Text + "%'" ,baglanti);
            adtr.Fill(tablo);
            dataGridView2.DataSource = tablo;
            baglanti.Close();

        
        }
    }
}
