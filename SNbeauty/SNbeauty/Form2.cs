using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SNbeauty
{
    public partial class kullanici_anasayfa : Form
    {
        // kullanici_anasayfa formunun yapıcı metodu
        public kullanici_anasayfa()
        {
            InitializeComponent();  // Form bileşenlerini başlatır
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Akneli ciltler için yeni formu açar
            akneliCiltler Cilt = new akneliCiltler();
            Cilt.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Karma ciltler için yeni formu açar
            karmaCiltler Cilt = new karmaCiltler();
            Cilt.Show();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
        }

        private void label6_Click(object sender, EventArgs e)
        {
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // Kuru ciltler için yeni formu açar
            kuruCiltler Cilt = new kuruCiltler();
            Cilt.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Yağlı ciltler için yeni formu açar
            yaglıCiltler Cilt = new yaglıCiltler();
            Cilt.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Normal ciltler için yeni formu açar
            normalCiltler Cilt = new normalCiltler();
            Cilt.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Lekeli ciltler için yeni formu açar
            lekeliCiltler Cilt = new lekeliCiltler();
            Cilt.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Hassas ciltler için yeni formu açar
            hassasCiltler Cilt = new hassasCiltler();
            Cilt.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
        }
    }
}
