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
    public partial class sifre_yenile : Form
    {
        // Form yapıcısı
        public sifre_yenile()
        {
            InitializeComponent();  // Form bileşenlerini başlatır
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void sifre_yenile_Load(object sender, EventArgs e)
        {

        }

        // Şifre yenileme butonu tıklama olayı
        private void button1_Click(object sender, EventArgs e)
        {
            // Kullanıcı adı ve yeni şifreyi alır
            string kullaniciAdi = textBox1.Text;
            string yeniSifre = textBox2.Text;

            // Veritabanı bağlantı dizesi
            string connectionString = "Data Source = FEDERAL\\SQLEXPRESS01; Initial Catalog = müsteriListes; Integrated Security = True";

            try
            {
                // Veritabanı bağlantısını açar ve kullanır
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();  // Veritabanı bağlantısını açar

                    // Kullanıcı adıyla eşleşen kaydın şifresini güncelleyen SQL sorgusu
                    string updateQuery = "UPDATE MusteriKaydı SET Sifre = @YeniSifre WHERE KullaniciAdi = @KullaniciAdi";

                    // SQL komutu oluşturur ve parametreleri ekler
                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        command.Parameters.Add("@YeniSifre", SqlDbType.VarChar).Value = yeniSifre;  // Yeni şifre parametresi
                        command.Parameters.Add("@KullaniciAdi", SqlDbType.VarChar).Value = kullaniciAdi;  // Kullanıcı adı parametresi

                        // Komutu çalıştırır ve etkilenen satır sayısını alır
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            // Güncelleme başarılıysa mesaj gösterir
                            MessageBox.Show("Şifre başarıyla güncellendi.");
                        }
                        else
                        {
                            // Kullanıcı bulunamazsa veya başka bir hata oluşursa mesaj gösterir
                            MessageBox.Show("Kullanıcı bulunamadı veya bir hata oluştu.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Hata oluşursa mesaj gösterir
                MessageBox.Show("Bir hata oluştu: " + ex.Message);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }
    }
}
