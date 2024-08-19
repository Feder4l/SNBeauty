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
    public partial class satici_anasayfa : Form
    {
        public satici_anasayfa()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=FEDERAL\\SQLEXPRESS01;Initial Catalog=müsteriListes;Integrated Security=True");
        DataSet daset = new DataSet();
        private void sepetlistele()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select *from sepett",baglanti);
            adtr.Fill(daset, "sepett");
            dataGridView1.DataSource = daset.Tables["sepett"];
            baglanti.Close();

        }

        private bool durum;
        private void barkodkontrol()
        {
            durum = true;
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from sepett",baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                if (txtBarkodNo.Text==read["barkodno"].ToString())
                {
                    durum = false;
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            urunEkle Ekle = new urunEkle();
            Ekle.Show();

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Form5_Load(object sender, EventArgs e)
        {
            sepetlistele();
        }
        private void hesapla()
        {
            try
            {
                baglanti.Open();
               
               SqlCommand komut = new SqlCommand("select sum(toplamfiyati) from sepett",baglanti);
                lblGenelToplam.Text = komut.ExecuteScalar() + " TL ";
                baglanti.Close();
            
            }
            catch(Exception)
            {

            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            urunListesi Liste = new urunListesi();
            Liste.Show();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            frmMüşteriListele liste = new frmMüşteriListele();
            liste.ShowDialog();

        }
        private void button6_Click(object sender, EventArgs e)
        {
            earsivfatura Fatura = new earsivfatura();
            Fatura.Show();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            frmMüşteriEkle ekle = new frmMüşteriEkle();
            ekle.ShowDialog();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            kargoDurumlari Kargo = new kargoDurumlari();
            Kargo.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            frmkategori kategori = new frmkategori();
            kategori.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            frmMarka marka = new frmMarka();
            marka.ShowDialog();
        }

        private void txtTc_TextChanged(object sender, EventArgs e)
        {
            if(txtTc.Text=="")
            {
                txtAdSoyad.Text = "";
                txtTelefon.Text = "";
            }
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from müşteri where tc like '" + txtTc.Text + "'",baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                txtAdSoyad.Text = read["adsoyad"].ToString();
                txtTelefon.Text = read["telefon"].ToString();
            }
            baglanti.Close();
        }

        private void txtBarkodNo_TextChanged(object sender, EventArgs e)
        {
            temizle();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from urun where barkodno like '" + txtBarkodNo.Text + "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                txtÜrünAdı.Text = read["urunadi"].ToString();
                txtSatışFiyatı.Text = read["satisfiyati"].ToString();
            }
            baglanti.Close();
        }

        private void temizle()
        {
            if (txtBarkodNo.Text == "")
            {
                foreach (Control item in groupBox2.Controls)
                {
                    if (item is TextBox)
                    {
                        if (item != txtMiktarı)
                        {
                            item.Text = "";
                        }
                    }


                }
            }
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
          

           
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into sepett(tc,adsoyad,telefon,barkodno,urunadi,miktari,satisfiyati,toplamfiyati,tarih) values(@tc,@adsoyad,@telefon,@barkodno,@urunadi,@miktari,@satisfiyati,@toplamfiyati,@tarih)", baglanti);
                komut.Parameters.AddWithValue("@tc", txtTc.Text);
                komut.Parameters.AddWithValue("@adsoyad", txtAdSoyad.Text);
                komut.Parameters.AddWithValue("@telefon", txtTelefon.Text);
                komut.Parameters.AddWithValue("@barkodno", txtBarkodNo.Text);
                komut.Parameters.AddWithValue("@urunadi", txtÜrünAdı.Text);
                komut.Parameters.AddWithValue("@miktari", int.Parse(txtMiktarı.Text));
                komut.Parameters.AddWithValue("@satisfiyati", double.Parse(txtSatışFiyatı.Text));
                komut.Parameters.AddWithValue("@toplamfiyati", double.Parse(txtToplamFiyat.Text));
                komut.Parameters.AddWithValue("@tarih", DateTime.Now.ToString());
                komut.ExecuteNonQuery();
                baglanti.Close();
                daset.Tables["sepett"].Clear();
                sepetlistele();
                temizle();
            hesapla();
            foreach (Control item in groupBox2.Controls)
            {
                if (item is TextBox) {
                    if (item != txtMiktarı) {
                        item.Text = "";
                    }
                }
            }
        }

        private void txtMiktarı_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtToplamFiyat.Text = (double.Parse(txtMiktarı.Text) * double.Parse(txtSatışFiyatı.Text)).ToString() ;
            }
            catch(Exception)
            {
                ;
            }
        }

        private void txtSatışFiyatı_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtToplamFiyat.Text = (double.Parse(txtMiktarı.Text) * double.Parse(txtSatışFiyatı.Text)).ToString();
            }
            catch (Exception)
            {
                ;
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from sepett where barkodno='" + dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString() + "'  ",baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ürün sepetten çıkarıldı");
            daset.Tables["sepett"].Clear();
            sepetlistele();
            hesapla();
        }

        private void btnSatıiListesi_Click(object sender, EventArgs e)
        {
            frmSatişListele satis = new frmSatişListele();
            satis.ShowDialog();
        }

        private void btnSatışYap_Click(object sender, EventArgs e)
        {
            for(int i=0; i< dataGridView1.Rows.Count-1; i++)
            {

                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into satis(adsoyad,telefon,barkodno,urunadi,miktari,satisfiyati,toplamfiyati,tarih,tc) values(@adsoyad,@telefon,@barkodno,@urunadi,@miktari,@satisfiyati,@toplamfiyati,@tarih,@tc)", baglanti);
                komut.Parameters.AddWithValue("@adsoyad", txtAdSoyad.Text);
                komut.Parameters.AddWithValue("@telefon", txtTelefon.Text);
                komut.Parameters.AddWithValue("@barkodno",dataGridView1.Rows[i].Cells["barkodno"].Value.ToString());
                komut.Parameters.AddWithValue("@urunadi", dataGridView1.Rows[i].Cells["urunadi"].Value.ToString());
                komut.Parameters.AddWithValue("@miktari", int.Parse(dataGridView1.Rows[i].Cells["miktari"].Value.ToString()));
                komut.Parameters.AddWithValue("@satisfiyati", double.Parse(dataGridView1.Rows[i].Cells["satisfiyati"].Value.ToString()));
                komut.Parameters.AddWithValue("@toplamfiyati", double.Parse(dataGridView1.Rows[i].Cells["toplamfiyati"].Value.ToString()));
                komut.Parameters.AddWithValue("@tarih", DateTime.Now.ToString());
                komut.Parameters.AddWithValue("@tc", txtTc.Text);
                komut.ExecuteNonQuery();
               
                baglanti.Close();

            }
            baglanti.Open();
            SqlCommand komut3 = new SqlCommand("delete from sepett ", baglanti);
            komut3.ExecuteNonQuery();
            baglanti.Close();
            daset.Tables["sepett"].Clear();
            sepetlistele();
            hesapla();

        }
    }
}
