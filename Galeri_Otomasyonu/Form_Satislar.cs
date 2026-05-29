using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Data.Entity;



namespace Galeri_Otomasyonu
{
    public partial class Form_Satislar : Form
    {
        public Form_Satislar()
        {
            InitializeComponent();
        }
        GaleriDbContext db = new GaleriDbContext();

        private void btn_listele_Click(object sender, EventArgs e)
        {
            try
            {
                var satisListesi = db.Table_Satislar
        .Include(s => s.Table_Musteri)
        .Include(s => s.Table_Araclar)
        .Select(s => new
        {

            s.SatisID,

            Müşteri = s.Table_Musteri.Musteri_Ad + " " + s.Table_Musteri.Musteri_Soyad,

            Araç_Plaka = s.Table_Araclar.Arac_Plaka,
            Araç_Marka = s.Table_Araclar.Arac_Marka,
            Araç_Model = s.Table_Araclar.Arac_Model,

            Satış_Tarihi = s.Satis_Tarihi,
            Satış_Fiyatı = s.Satis_Fiyati
        })
        .ToList();

                dataGridView1.DataSource = satisListesi;

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dataGridView1.Columns["SatisID"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata = " + ex.Message);
            }

        }

        private void Form_Satislar_Load(object sender, EventArgs e)
        {
            {
                try
                {
                    combo_musteri.DataSource = db.Table_Musteri
                    .OrderBy(m => m.Musteri_Ad)
                    .Select(m => new
                    {
                        m.Musteri_Id,
                        FullName = m.Musteri_Ad + " " + m.Musteri_Soyad
                    })
                    .ToList();
                    combo_musteri.DisplayMember = "FullName";
                    combo_musteri.ValueMember = "Musteri_Id";

                    combo_arac.DataSource = db.Table_Araclar
                        .OrderBy(a => a.Arac_Plaka)
                        .Select(a => new
                        {
                            a.AracID,
                            FullName = a.Arac_Plaka + " " + a.Arac_Marka + " " + a.Arac_Model
                        })
                        .ToList();
                    combo_arac.DisplayMember = "FullName";
                    combo_arac.ValueMember = "AracID";



                    btn_listele.PerformClick();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata = {ex.Message}");
                }
            }
        }

        private void btn_ekle_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedMusteriId = (int)combo_musteri.SelectedValue;
                int selectedAractId = (int)combo_arac.SelectedValue;

                var newKayit = new Satis
                {
                    Musteri_Id = selectedMusteriId,
                    AracID = selectedAractId,
                    Satis_Tarihi = Convert.ToDateTime(dtp_satis_tarihi.Value),
                    Satis_Fiyati = Convert.ToDecimal(txt_satis_fiyati.Text)
                };
                db.Table_Satislar.Add(newKayit);
                db.SaveChanges();

                MessageBox.Show("Kayıt başarıyla eklendi.");

                btn_listele.PerformClick();
            }
            catch (Exception ex)
            { MessageBox.Show($"Hata = {ex.Message}"); }


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
                        int selectedId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["SatisID"].Value);

                        var recordToDelete = db.Table_Satislar.Find(selectedId);

                        if (recordToDelete != null)
                        {
                            db.Table_Satislar.Remove(recordToDelete);
                            db.SaveChanges();

                            MessageBox.Show("Kayıt başarıyla silindi.");

                            btn_listele.PerformClick();
                        }
                        else
                        {
                            MessageBox.Show("Kayıt bulunamadı.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata = {ex.Message}");
            }

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                var sonuc_1 = db.Table_Satislar
                    .GroupBy(s => new
                    {
                        s.Table_Musteri.Musteri_Id,
                        s.Table_Musteri.Musteri_Ad,
                        s.Table_Musteri.Musteri_Soyad
                    })
                    .Select(g => new
                    {
                        FullName = g.Key.Musteri_Ad + " " + g.Key.Musteri_Soyad,
                        IslemSayisi = g.Count()
                    })
                    .OrderByDescending(x => x.IslemSayisi)
                    .FirstOrDefault();

                if (sonuc_1 != null)
                {
                    label5.Text = $"En Çok Araç Satın Alan Müşteri: {sonuc_1.FullName}\nToplam Satış Sayısı: {sonuc_1.IslemSayisi}";
                }
                else
                {
                    label5.Text = "Henüz hiç kayıt bulunmuyor.";
                }
            }

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

            var sonuc_2 = db.Table_Satislar
            .OrderByDescending(s => s.Table_Araclar.Arac_Fiyat) 
            .Select(s => new
        {
            MusteriAdSoyad = s.Table_Musteri.Musteri_Ad + " " + s.Table_Musteri.Musteri_Soyad,
            AracBilgisi = s.Table_Araclar.Arac_Marka + " " + s.Table_Araclar.Arac_Model,
            Fiyat = s.Table_Araclar.Arac_Fiyat
        })
        .FirstOrDefault();

            if (sonuc_2 != null)
            {
                label6.Text = $"Satılan En Pahalı Araç: {sonuc_2.AracBilgisi}\nAlan Müşteri: {sonuc_2.MusteriAdSoyad}\nFiyat: {sonuc_2.Fiyat} TL";
            }
            else
            {
               label6.Text = "Henüz hiç kayıt bulunmuyor.";
            }
        }
    }
}
