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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Galeri_Otomasyonu
{
    public partial class Form_Araclar : Form
    {
        public Form_Araclar()
        {
            InitializeComponent();
        }
        GaleriDbContext db = new GaleriDbContext();
        private void btn_listele_Click(object sender, EventArgs e)
        {
            try
            {
                var list = db.Table_Araclar
                             .Include(arc => arc.Table_Vites)
                             .Include(arc => arc.Table_Yakit)
                             .Include(arc => arc.Table_Durum)
                             .Select(arc => new
                             {
                                 arc.AracID,
                                 AraçPlakası = arc.Arac_Plaka,
                                 AraçMarkası = arc.Arac_Marka,
                                 AraçModeli = arc.Arac_Model,
                                 ModelYılı = arc.Arac_Model_Yili,
                                 Kilometresi = arc.Arac_Kilometresi,
                                 Fiyatı = arc.Arac_Fiyat,

                                 VitesTürü = arc.Table_Vites.Vites_Turu,
                                 YakıtTürü = arc.Table_Yakit.Yakit_Turu,
                                 AraçDurumu = arc.Table_Durum.Arac_Durumu
                             })
                             .ToList();

                dataGridView1.DataSource = list;

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dataGridView1.Columns["AracID"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata = " + ex.Message);
            }

        }

        private void Form_Araclar_Load(object sender, EventArgs e)
        {
            try
            {
                combo_vites.DataSource = db.Table_Vites 
                .OrderBy(v => v.Vites_Turu) 
                .Select(v => new
                {
                    v.VitesID, 
                    FullName = v.Vites_Turu  
                })
                .ToList();
                combo_vites.DisplayMember = "FullName"; 
                combo_vites.ValueMember = "VitesID"; 
                                                         
                combo_yakit.DataSource = db.Table_Yakit
                    .OrderBy(y => y.Yakit_Turu)
                    .Select(y => new
                    {
                        y.YakitID,
                        FullName = y.Yakit_Turu
                    })
                    .ToList();
                combo_yakit.DisplayMember = "FullName";
                combo_yakit.ValueMember = "YakitID";

                combo_durum.DataSource = db.Table_Durum
               .OrderBy(d => d.Arac_Durumu)
               .Select(d => new
               {
                   d.DurumID,
                   FullName = d.Arac_Durumu
               })
               .ToList();
                combo_durum.DisplayMember = "FullName";
                combo_durum.ValueMember = "DurumID";
                btn_listele.PerformClick();
            }
            catch (Exception ex)
            { MessageBox.Show($"Hata = {ex.Message}"); }
        }

        private void btn_ekle_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedVitesId = (int)combo_vites.SelectedValue;
                int selectedYakitId = (int)combo_yakit.SelectedValue;
                int selectedDurumId = (int)combo_durum.SelectedValue;

                var newKayit= new Araclar
                {
                    
                    Arac_Plaka = txt_plaka.Text,
                    Arac_Marka = txt_marka.Text,
                    Arac_Model = txt_model.Text,
                    Arac_Model_Yili = Convert.ToInt32(txt_model_yili.Text),
                    Arac_Kilometresi = Convert.ToInt32(txt_km.Text),
                    Arac_Fiyat = Convert.ToDecimal(txt_fiyat.Text),

                    VitesID = selectedVitesId,
                    YakitID = selectedYakitId,
                    DurumID = selectedDurumId
                };

                db.Table_Araclar.Add(newKayit);
                db.SaveChanges();

                MessageBox.Show("Araç kaydı başarıyla eklendi.");

                btn_listele.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata = " + ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txt_plaka.Text = dataGridView1.Rows[e.RowIndex].Cells["AraçPlakası"].Value.ToString();
                txt_marka.Text = dataGridView1.Rows[e.RowIndex].Cells["AraçMarkası"].Value.ToString();
                txt_model.Text = dataGridView1.Rows[e.RowIndex].Cells["AraçModeli"].Value.ToString();
                txt_model_yili.Text = dataGridView1.Rows[e.RowIndex].Cells["ModelYılı"].Value.ToString();
                txt_km.Text = dataGridView1.Rows[e.RowIndex].Cells["Kilometresi"].Value.ToString();
                txt_fiyat.Text = dataGridView1.Rows[e.RowIndex].Cells["Fiyatı"].Value.ToString();
                combo_vites.Text = dataGridView1.Rows[e.RowIndex].Cells["VitesTürü"].Value.ToString();
                combo_yakit.Text = dataGridView1.Rows[e.RowIndex].Cells["YakıtTürü"].Value.ToString();
                combo_durum.Text = dataGridView1.Rows[e.RowIndex].Cells["AraçDurumu"].Value.ToString();
            }

        }

        

        private void btn_güncelle_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.Index >= 0)
                {
                    if (txt_model_yili.Text == "" || txt_fiyat.Text == "" || txt_km.Text == "")
                    {
                        MessageBox.Show("Lütfen güncellemek için bir satır seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                int selectedId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["AracID"].Value);
                Araclar araclar = db.Table_Araclar.Find(selectedId);

                if ( araclar!= null)
                {
                    araclar.Arac_Plaka = txt_plaka.Text;
                    araclar.Arac_Marka = txt_marka.Text;
                    araclar.Arac_Model = txt_model.Text;
                    araclar.Arac_Model_Yili = Convert.ToInt32(txt_model_yili.Text);
                    araclar.Arac_Kilometresi = Convert.ToInt32(txt_km.Text);
                    araclar.Arac_Fiyat = Convert.ToDecimal(txt_fiyat.Text);
                    araclar.VitesID = (int)combo_vites.SelectedValue;
                    araclar.YakitID = (int)combo_yakit.SelectedValue;
                    araclar.DurumID = (int)combo_durum.SelectedValue;
                    db.SaveChanges();
                    MessageBox.Show("Araç güncellendi!");
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
                        int selectedId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["AracID"].Value);
                        Araclar araclar = db.Table_Araclar.Find(selectedId);
                        if (araclar != null)
                        {
                            db.Table_Araclar.Remove(araclar);
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
