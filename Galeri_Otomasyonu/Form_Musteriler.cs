using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Galeri_Otomasyonu
{
    public partial class Form_Musteriler : Form
    {
        public Form_Musteriler()
        {
            InitializeComponent();
        }
        GaleriDbContext db = new GaleriDbContext();
        private void btn_listele_Click(object sender, EventArgs e)
        {
            try
            {
                var musteri = db.Table_Musteri.ToList();
                dataGridView1.DataSource = musteri;

                dataGridView1.Columns["Musteri_Id"].HeaderText = "Müşteri ID";
                dataGridView1.Columns["Musteri_Ad"].HeaderText = "Müşteri Adı";
                dataGridView1.Columns["Musteri_Soyad"].HeaderText = "Müşteri Soyadı";
                dataGridView1.Columns["Musteri_TcNo"].HeaderText = "Müşteri TcNo";
                dataGridView1.Columns["Musteri_Telefon"].HeaderText = "Telefon Numarası";
                dataGridView1.Columns["Musteri_Email"].HeaderText = "E-Posta Adresi";



                dataGridView1.Columns["Musteri_Id"].Visible = false;

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


            }
            catch (Exception ex)
            {

                MessageBox.Show($"Hata = {ex.Message}");
            }

        }

        private void btn_ekle_Click(object sender, EventArgs e)
        {
            try
            {
                Musteri newmusteri = new Musteri()
                {
                    Musteri_Ad = txt_isim.Text,
                    Musteri_Soyad = txt_soyisim.Text,
                    Musteri_Telefon = maskedtxt_telno.Text,
                    Musteri_TcNo = maskedtxt_tcno.Text,
                    Musteri_Email = txt_email.Text,
                    

                };
                db.Table_Musteri.Add(newmusteri);
                db.SaveChanges();

                MessageBox.Show("Yeni Müşteri Eklendi!");
                btn_listele.PerformClick();
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Hata = {ex.Message}");
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txt_isim.Text = dataGridView1.Rows[e.RowIndex].Cells["Musteri_Ad"].Value.ToString();
                txt_soyisim.Text = dataGridView1.Rows[e.RowIndex].Cells["Musteri_Soyad"].Value.ToString();
                maskedtxt_tcno.Text = dataGridView1.Rows[e.RowIndex].Cells["Musteri_TcNo"].Value.ToString();
                txt_email.Text = dataGridView1.Rows[e.RowIndex].Cells["Musteri_Email"].Value.ToString();
                maskedtxt_telno.Text = dataGridView1.Rows[e.RowIndex].Cells["Musteri_Telefon"].Value.ToString();
            }

        }

        private void btn_güncelle_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.Index < 0)
                {
                    MessageBox.Show("Lütfen güncellemek için bir satır seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int selectedId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Musteri_Id"].Value);
                Musteri musteri = db.Table_Musteri.Find(selectedId);

                if (musteri != null)
                {
                    musteri.Musteri_Ad = txt_isim.Text;
                    musteri.Musteri_Soyad = txt_soyisim.Text;
                    musteri.Musteri_TcNo = maskedtxt_tcno.Text;
                    musteri.Musteri_Telefon = maskedtxt_telno.Text;
                    musteri.Musteri_Email = txt_email.Text;
                    db.SaveChanges();
                    MessageBox.Show("Müşteri güncellendi!");
                    btn_listele.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata = {ex.Message}");
            }

        }

        private void btn_sil_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult sonuc = MessageBox.Show(
                            "Silmek istediğinize emin misiniz?",
                            "Silme Onayı",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

                if (sonuc == DialogResult.Yes)
                {
                    if (dataGridView1.CurrentRow != null)
                    {
                        int selectedId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Musteri_Id"].Value);
                        Musteri musteri = db.Table_Musteri.Find(selectedId);
                        if (musteri != null)
                        {
                            db.Table_Musteri.Remove(musteri);
                            db.SaveChanges();
                            MessageBox.Show("Müşteri Silindi!");
                            btn_listele.PerformClick();

                        }
                    }
                
                }
            }


            catch (Exception ex)
            {
                MessageBox.Show($"Hata = {ex.Message}");
            }

        }
    }
}
