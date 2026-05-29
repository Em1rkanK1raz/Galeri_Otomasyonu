using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Galeri_Otomasyonu
{
    public partial class AnaMenu : Form
    {
        public AnaMenu()
        {
            InitializeComponent();
        }

        private void btn_arabalar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Sayfaya yönlendiriliyorsunuz, lütfen bekleyin...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Form_Araclar Form_Araclar = new Form_Araclar();
            this.Hide();
            Thread.Sleep(1000);
            Form_Araclar.ShowDialog();
            this.Close();
        }

        private void btn_musteriler_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Sayfaya yönlendiriliyorsunuz, lütfen bekleyin...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Form_Musteriler Form_Musteriler = new Form_Musteriler();
            this.Hide();
            Thread.Sleep(1000);
            Form_Musteriler.ShowDialog();
            this.Close();
           
        }

        private void btn_satislar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Sayfaya yönlendiriliyorsunuz, lütfen bekleyin...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Form_Satislar Form_Satislar = new Form_Satislar();
            this.Hide();
            Thread.Sleep(1000);
            Form_Satislar.ShowDialog();
            this.Close();
        }
    }
}
