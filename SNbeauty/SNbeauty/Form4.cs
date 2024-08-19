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
    public partial class kullanici_kayit : Form
    {
        // Veritabanı bağlantı dizesi
        static string constring = "Data Source=FEDERAL\\SQLEXPRESS01;Initial Catalog=müsteriListes;Integrated Security=True";
        SqlConnection baglanti = new SqlConnection(constring);  // SQL bağlantı nesnesi oluşturulur

        // Form yapıcı metodu
        public kullanici_kayit()
        {
            InitializeComponent();  // Form bileşenlerini başlatır
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Kullanıcı adı ve şifre alanlarının boş olmadığını kontrol etme
                if (string.IsNullOrWhiteSpace(textBox4.Text) || string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    MessageBox.Show("Kullanıcı adı ve şifre alanları boş olamaz.");
                    return;  // İşlemi sonlandırır
                }

                if (baglanti.State == ConnectionState.Open)
                    baglanti.Close();  // Bağlantı açıksa kapatır

                baglanti.Open();  // Veritabanı bağlantısını açar

                // SQL sorgusu
                string insertQuery = "INSERT INTO yönetici (kullanici__adi, sifre) VALUES (@kad, @ksifre)";
                SqlCommand kayit = new SqlCommand(insertQuery, baglanti);  // Komut oluşturur

                // Parametre ekleme
                kayit.Parameters.AddWithValue("@kad", textBox4.Text);  // Kullanıcı adı parametresi
                kayit.Parameters.AddWithValue("@ksifre", textBox3.Text);  // Şifre parametresi

                // Sorguyu çalıştırma
                int rowsAffected = kayit.ExecuteNonQuery();  // Sorguyu veritabanında çalıştırır

                // Bağlantıyı kapatma
                baglanti.Close();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Başarıyla eklendi.");  // Başarılı ekleme mesajı
                }
                else
                {
                    MessageBox.Show("Kayıt eklenemedi.");  // Hata mesajı
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("SQL hatası: " + sqlEx.Message);  // SQL hatası mesajı
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message);  // Genel hata mesajı
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
