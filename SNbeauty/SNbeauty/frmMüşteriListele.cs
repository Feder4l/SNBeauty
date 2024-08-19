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
    public partial class frmMüşteriListele : Form
    {
        public frmMüşteriListele()
        {
            InitializeComponent(); // Form yüklendiğinde otomatik olarak çağrılan metot. Form bileşenlerini başlatır.
        }

        SqlConnection baglanti = new SqlConnection("Data Source=FEDERAL\\SQLEXPRESS01;Initial Catalog=müsteriListes;Integrated Security=True");
        DataSet daset = new DataSet();

        private void frmMüşteriListele_Load(object sender, EventArgs e)
        {
            Kayıt_Göster(); // Form yüklendiğinde Kayıt_Göster metodu çağrılır ve veriler DataGridView'e yüklenir.
        }

        private void Kayıt_Göster()
        {
            baglanti.Open();
            // SqlDataAdapter nesnesi tanımlanıyor. Veritabanından veri çekmek için kullanılır.
            SqlDataAdapter adtr = new SqlDataAdapter("select *from müşteri", baglanti);   
            adtr.Fill(daset, "müşteri");// SqlDataAdapter ile veritabanından veriler DataSet'e yükleniyor. "müşteri" tablosu DataSet içinde "müşteri" adında bir tabloya aktarılıyor.

            // DataGridView kontrolüne DataSet içindeki "müşteri" tablosu bağlanarak veriler gösteriliyor.
            dataGridView1.DataSource = daset.Tables["müşteri"];

            baglanti.Close(); // Veritabanı bağlantısı kapatılıyor.
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // DataGridView hücre içeriği tıklandığında herhangi bir işlem yapılmasına gerek olmadığı için bu metot boş bırakılmış.
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // DataGridView hücreleri çift tıklandığında bu metot çalışır.

            // Seçilen müşterinin bilgileri ilgili TextBox kontrollerine aktarılır.
            txtTc.Text = dataGridView1.CurrentRow.Cells["tc"].Value.ToString();
            txtAdSoyad.Text = dataGridView1.CurrentRow.Cells["adsoyad"].Value.ToString();
            txtTelefon.Text = dataGridView1.CurrentRow.Cells["telefon"].Value.ToString();
            TxtAdres.Text = dataGridView1.CurrentRow.Cells["adres"].Value.ToString();
            TxtEmail.Text = dataGridView1.CurrentRow.Cells["email"].Value.ToString();
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open(); 
            SqlCommand komut = new SqlCommand("update müşteri set adsoyad=@adsoyad,telefon=@telefon,adres=@adres,email=@email where tc=@tc", baglanti);

            // Parametreler atanır.
            komut.Parameters.AddWithValue("@tc", txtTc.Text);
            komut.Parameters.AddWithValue("@adsoyad", txtAdSoyad.Text);
            komut.Parameters.AddWithValue("@telefon", txtTelefon.Text);
            komut.Parameters.AddWithValue("@adres", TxtAdres.Text);
            komut.Parameters.AddWithValue("@email", TxtEmail.Text);

            // Komut veritabanında çalıştırılır.
            komut.ExecuteNonQuery();
            baglanti.Close(); 

            // DataSet içindeki "müşteri" tablosu temizlenir.
            daset.Tables["müşteri"].Clear();
            // DataGridView'e güncel veriler yeniden yüklenir.
            Kayıt_Göster();
            // Kullanıcıya güncelleme yapıldı mesajı gösterilir.
            MessageBox.Show("Müşteri kaydı güncellendi");
            // Formdaki TextBox kontrolleri temizlenir.
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
        }


        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            // Silme işlemi için SqlCommand nesnesi oluşturuluyor. "delete from müşteri" sorgusu kullanılıyor ve silinecek TC değeri parametre olarak ekleniyor.
            SqlCommand komut = new SqlCommand("delete from müşteri where tc='" + dataGridView1.CurrentRow.Cells["tc"].Value.ToString() + "'", baglanti);
            // SqlCommand ile veritabanında silme işlemi gerçekleştiriliyor.
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt Silindi");// Kullanıcıya kaydın silindiğine dair bilgi veren bir mesaj gösteriliyor.
        }


        private void txtTcAra_TextChanged(object sender, EventArgs e)
        {
            // DataTable nesnesi oluşturuluyor.
            DataTable tablo = new DataTable();
            baglanti.Open();
            // SqlDataAdapter nesnesi oluşturuluyor ve müşteri tablosundan TC alanı içinde arama yapılıyor.
            SqlDataAdapter adtr = new SqlDataAdapter("select *from müşteri where tc like  '%" + txtTcAra.Text + "%'", baglanti);

            // Veriler SqlDataAdapter ile DataTable'a aktarılıyor.
            adtr.Fill(tablo);

            // DataGridView'e DataTable içindeki veriler bağlanıyor.
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }

    }
}
