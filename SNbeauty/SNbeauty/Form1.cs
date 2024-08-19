using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace SNbeauty
{
    public partial class kullanici_giris : Form
    {
        // Static değişkenler kullanıcı bilgilerini saklamak için kullanılır
        public static string sid, ssifre;
        public static string mid, msifre;
        public static string zkid, zsifre;

        public kullanici_giris()
        {
            InitializeComponent();  // Form bileşenlerini başlatır
        }

        // Veritabanı bağlantı dizesi
        static string constring = "Data Source=FEDERAL\\SQLEXPRESS01;Initial Catalog=müsteriListes;Integrated Security=True";
        SqlConnection baglanti = new SqlConnection(constring);  // Veritabanı bağlantısını oluşturur

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Kullanıcı adı ve şifre alanları boş değilse
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                try
                {
                    // Eğer bağlantı açıksa kapatır
                    if (baglanti.State == ConnectionState.Open)
                        baglanti.Close();
                    // Bağlantıyı açar
                    baglanti.Open();

                    // Yönetici tablosundan kullanıcı adı ve şifre kontrolü yapar
                    SqlCommand kullanici = new SqlCommand("Select yönetici.kullanici__adi, yönetici.sifre FROM yönetici WHERE kullanici__adi = @kadi and sifre = @ksifre", baglanti);
                    kullanici.Parameters.AddWithValue("@kadi", SqlDbType.NVarChar).Value = textBox1.Text;
                    kullanici.Parameters.AddWithValue("@ksifre", SqlDbType.Int).Value = textBox2.Text;
                    SqlDataReader oku = kullanici.ExecuteReader();

                    // Eğer kullanıcı bilgileri doğruysa
                    if (oku.Read())
                    {
                        zkid = textBox1.Text;  // Kullanıcı adını saklar
                        MessageBox.Show("Giriş Başarılı, HOŞGELDİN GÜZELLİK");
                        kullanici_anasayfa yeni = new kullanici_anasayfa();  // Ana sayfa formunu açar
                        this.Hide();  // Mevcut formu gizler
                        yeni.Show();  // Yeni formu gösterir
                    }
                    else
                    {
                        // Eğer kullanıcı bilgileri yanlışsa
                        MessageBox.Show("Böyle bir kayıt yok.");
                    }
                    baglanti.Close();  // Bağlantıyı kapatır
                }
                catch
                {
                    // Bir hata oluşursa mesaj gösterir
                    MessageBox.Show("Hata!!");
                }
            }
            else
            {
                // Kullanıcı adı veya şifre alanı boşsa mesaj gösterir
                MessageBox.Show("Boş alanları doldurun.");
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {
          
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Şifre yenileme formunu açar
            sifre_yenile Sifre_Yenile = new sifre_yenile();
            Sifre_Yenile.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
           
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Form6'yı açar
            Form6 form6 = new Form6();
            form6.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Kullanıcı kayıt formunu açar
            kullanici_kayit kullanici_Kayit = new kullanici_kayit();
            kullanici_Kayit.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Satıcı girişi: kullanıcı adı ve şifre alanları boş değilse
            if (textBox4.Text != "" && textBox3.Text != "")
            {
                try
                {
                    // Eğer bağlantı açıksa kapatır
                    if (baglanti.State == ConnectionState.Open)
                        baglanti.Close();
                    // Bağlantıyı açar
                    baglanti.Open();

                    // Satıcı tablosundan kullanıcı adı ve şifre kontrolü yapar
                    SqlCommand kullanici = new SqlCommand("Select satici.kullanici_adi, satici.sifre FROM satici WHERE kullanici_adi = @kadi and sifre = @ksifre", baglanti);
                    kullanici.Parameters.AddWithValue("@kadi", SqlDbType.NVarChar).Value = textBox4.Text;
                    kullanici.Parameters.AddWithValue("@ksifre", SqlDbType.Int).Value = textBox3.Text;
                    SqlDataReader oku = kullanici.ExecuteReader();

                    // Eğer kullanıcı bilgileri doğruysa
                    if (oku.Read())
                    {
                        zkid = textBox4.Text;  // Kullanıcı adını saklar
                        MessageBox.Show("Giriş Başarılı, HOŞGELDİN GÜZELLİK");
                        satici_anasayfa yeni = new satici_anasayfa();  // Ana sayfa formunu açar
                        this.Hide();  // Mevcut formu gizler
                        yeni.Show();  // Yeni formu gösterir
                    }
                    else
                    {
                        // Eğer kullanıcı bilgileri yanlışsa
                        MessageBox.Show("Böyle bir kayıt yok.");
                    }
                    baglanti.Close();  // Bağlantıyı kapatır
                }
                catch
                {
                    // Bir hata oluşursa mesaj gösterir
                    MessageBox.Show("Hata!!");
                }
            }
            else
            {
                // Kullanıcı adı veya şifre alanı boşsa mesaj gösterir
                MessageBox.Show("Boş alanları doldurun.");
            }
        }

        private void kullanici_giris_Load(object sender, EventArgs e)
        {
        }
    }
}
