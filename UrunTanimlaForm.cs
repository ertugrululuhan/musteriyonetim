using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MUSTERIAPPS
{
    public partial class UrunTanimlaForm : Form
    {
        public MusteriAnaForm afrm;
        public UrunStokHareketForm StokGoster;
        public RaporForm Raporlar;
        public UrunTanimlaForm()
        {
            InitializeComponent();
        }

        #region Değişkenler ve Tanımlamalar
        static string vtBaglantiAdres = "Data Source=localhost;Initial Catalog=Musteri;" +
            "User Id=sa;Password=mmyo;";
        SqlConnection musConn = new SqlConnection(vtBaglantiAdres);
        SqlDataAdapter musAdp;
        SqlCommand musCmd = new SqlCommand();

        int islemDurumu;
        bool dolumu = true;
        //string UrunKodu;
        #endregion

        #region Kullanıcı Tanımlı Olaylar
        private DataTable veriGetir(string sorgu)
        {
            DataTable UrunTablo = new DataTable();
            UrunTablo.Clear();
            musAdp = new SqlDataAdapter(sorgu, musConn);
            musAdp.Fill(UrunTablo);
            return UrunTablo;
        }

        private void veriDoldur()
        {
            string sorgu = "Select UrunKodu, UrunTanimi, UrunMiktari, " +
                "AlisFiyati, SatisFiyati from Urunler";
            dgvUrunListe.DataSource = veriGetir(sorgu);
        }

        private void BaslikGoster()
        {
            dgvUrunListe.Columns[0].HeaderText = "Ürün Kodu";
            dgvUrunListe.Columns[0].Width = 80;
            dgvUrunListe.Columns[0].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
            dgvUrunListe.Columns[1].HeaderText = "Ürün Tanımı";
            dgvUrunListe.Columns[1].Width = 300;
            dgvUrunListe.Columns[1].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;
            dgvUrunListe.Columns[2].HeaderText = "Miktar";
            dgvUrunListe.Columns[2].Width = 60;
            dgvUrunListe.Columns[2].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
            dgvUrunListe.Columns[3].HeaderText = "Alış Fiyati";
            dgvUrunListe.Columns[3].Width = 120;
            dgvUrunListe.Columns[3].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;
            dgvUrunListe.Columns[3].DefaultCellStyle.Format = "C2";
            dgvUrunListe.Columns[4].HeaderText = "Satış Fiyati";
            dgvUrunListe.Columns[4].Width = 120;
            dgvUrunListe.Columns[4].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;
            dgvUrunListe.Columns[4].DefaultCellStyle.Format = "C2";
        }

        private void Baslat()
        {
            veriDoldur();
            BaslikGoster();
        }

        private void SayisalControl(object sender, KeyPressEventArgs e)
        {
            e.Handled = (!char.IsDigit(e.KeyChar) & !char.IsControl(e.KeyChar)
                             & e.KeyChar != 44);
            //MessageBox.Show("Veri Alanlarına \nSayısal Değer Giriniz", "Dikkat!",
            //        MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void KomutCalistir(string komut)
        {
            try
            {
                if (musConn.State == ConnectionState.Closed)
                    musConn.Open();
                musCmd.Connection = musConn;
                musCmd.CommandText = komut;
                islemDurumu = musCmd.ExecuteNonQuery();
            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.Message, "Bilgi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                if (musConn.State == ConnectionState.Open)
                    musConn.Close();
            }
        }

        private void Temizle()
        {
            foreach (Control tbox in this.Controls)
                if (tbox is TextBox)
                    tbox.Text = "";
            tbUrunKodu.Select();           
        }

        private void BoslukKontrol()
        {
            foreach (Control tbox in this.Controls)
                if (tbox is TextBox)
                    if (tbox.Text.Trim() =="")
                    {
                        dolumu = false;
                        break;
                    }   
        }

        private void StokHareketYedekle(string UrunKodu)
        {
            DataTable StokHareket = new DataTable();
            string sorgu = "Select UrunKodu, IslemTarihi, AlinanMiktar, AlisFiyati, " +
                "AlisTutari  from StokHareket Where UrunKodu='"+UrunKodu+"'";
            StokHareket = veriGetir(sorgu);
            string komut;
            for (int k=0;k<StokHareket.Rows.Count;k++)
            {
                komut = "Insert Into StokHareketSil " +
                    "(UrunKodu,IslemTarihi,AlinanMiktar,AlisFiyati,AlisTutari) " +
                    "Values ('"+StokHareket.Rows[k].ItemArray[0].ToString ()+
                    "','"+StokHareket.Rows[k].ItemArray[1].ToString()+
                    "','"+StokHareket.Rows[k].ItemArray[2].ToString()+
                    "','"+StokHareket.Rows[k].ItemArray[3].ToString()+
                    "','"+StokHareket.Rows[k].ItemArray[4].ToString ()+"')";
                KomutCalistir(komut);
            }
        }

        #endregion

        #region Nesne Tanımlı Olaylar

        private void UrunTanimlaForm_Load(object sender, EventArgs e)
        {
            Baslat();
        }

        private void tbAlisFiyat_Validating(object sender, CancelEventArgs e)
        {
            if (tbAlisFiyat.Text.Trim() != "")
            {
                tbSatisFiyat.Text =
                    ((Convert.ToDouble(tbAlisFiyat.Text) * 1.20) * 1.18).ToString();
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            BoslukKontrol();
            if (dolumu)
            {
                string komut = "Insert into Urunler(UrunKodu,UrunTanimi," +
                    "UrunMiktari,AlisFiyati,SatisFiyati) " +
                    "Values('" + tbUrunKodu.Text + "','" + tbUrunTanimi.Text + "','"
                    + tbMiktar.Text + "','" + tbAlisFiyat.Text + "','" + tbSatisFiyat.Text + "')";
                KomutCalistir(komut);
                if (islemDurumu == 1)
                {
                    MessageBox.Show("Ürün Ekleme \nİşlemi Başarılı", "Bilgi",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    double AlisTutari =
                        Convert.ToDouble(tbMiktar.Text) * Convert.ToDouble(tbAlisFiyat.Text);
                    komut = "Insert into StokHareket(UrunKodu, " +
                        "IslemTarihi, AlinanMiktar, AlisFiyati, AlisTutari) " +
                        "Values('" + tbUrunKodu.Text + "','"
                        + DateTime.Now.ToString("MM.dd.yyyy") + "','" + tbMiktar.Text +
                        "','" + tbAlisFiyat.Text + "','" + AlisTutari + "')";
                    KomutCalistir(komut);
                    veriDoldur();
                    Temizle();
                }
                else
                {
                    MessageBox.Show("Ürün Ekleme \nİşlemi Başarısız", "Bilgi",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Veri Alanlarını \nkontrol ediniz", "Bilgi",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                tbUrunKodu.Select();
            }
        }

        private void urunGuncelleMenuItem_Click(object sender, EventArgs e)
        {
            btnKaydet.Enabled = false;
            tbUrunKodu.Enabled = false;
            //tbUrunTanimi.Enabled = false;
            tbSatisFiyat.Enabled = false;
            tbUrunKodu.Text = dgvUrunListe.CurrentRow.Cells[0].Value.ToString();
            tbUrunTanimi.Text = dgvUrunListe.CurrentRow.Cells[1].Value.ToString();
            tbMiktar.Text = dgvUrunListe.CurrentRow.Cells[2].Value.ToString();
            tbAlisFiyat.Text = dgvUrunListe.CurrentRow.Cells[3].Value.ToString();
            tbSatisFiyat.Text = dgvUrunListe.CurrentRow.Cells[4].Value.ToString();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            BoslukKontrol();
            if (dolumu)
            {
                string komut = "Update Urunler set UrunTanimi='" + tbUrunTanimi.Text +
                    "', UrunMiktari=UrunMiktari+'" + Convert.ToInt16(tbMiktar.Text) + 
                    "', AlisFiyati='" + tbAlisFiyat.Text +"', SatisFiyati = '" + tbSatisFiyat.Text +
                    "' Where UrunKodu='"+ tbUrunKodu.Text + "'";
                KomutCalistir(komut);
                if (islemDurumu == 1)
                {
                    MessageBox.Show("Ürün Güncelleme \nİşlemi Başarılı", "Bilgi",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //double AlisTutari = Convert.ToDouble(tbMiktar.Text) * 
                    //    Convert.ToDouble(tbAlisFiyat.Text);
                    komut = "Insert into " +
                        "StokHareket(UrunKodu,IslemTarihi,AlinanMiktar,AlisFiyati,AlisTutari) " +
                        "Values ('" + tbUrunKodu.Text +
                        "','" + DateTime.Now.ToString("MM.dd.yyyy") + "','" + tbMiktar.Text + "','"
                        + tbAlisFiyat.Text + "','" + (Convert.ToDouble(tbMiktar.Text) *
                        Convert.ToDouble(tbAlisFiyat.Text)).ToString() + "')";
                    KomutCalistir(komut);
                    Temizle();
                    veriDoldur();
                    tbUrunKodu.Enabled = true;
                    tbSatisFiyat.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Ürün Güncelleme \nİşlemi Başarısız", "Bilgi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Veri Alanlarını \nkontrol ediniz", "Bilgi",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void urunSilMenuItem_Click(object sender, EventArgs e)
        {
            string UrunKodu = dgvUrunListe.CurrentRow.Cells[0].Value.ToString();
            if (MessageBox.Show("Seçili Ürün Bilgisini \nSilmek istiyor musunuz?", "Bilgi",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                string komut = "Insert Into UrunlerSil(UrunKodu,UrunTanimi," +
                    "UrunMiktari,AlisFiyati,SatisFiyati) " +
                    "Values('" + dgvUrunListe.CurrentRow.Cells[0].Value.ToString() +
                    "','" + dgvUrunListe.CurrentRow.Cells[1].Value.ToString() +
                    "','" + dgvUrunListe.CurrentRow.Cells[2].Value.ToString() +
                    "','" + dgvUrunListe.CurrentRow.Cells[3].Value.ToString() +
                    "','" + dgvUrunListe.CurrentRow.Cells[4].Value.ToString() + "')";
                KomutCalistir(komut);
                if (islemDurumu == 1)
                {
                    StokHareketYedekle(UrunKodu);
                    komut = "Delete from Urunler Where UrunKodu='" + UrunKodu + "'";
                    KomutCalistir(komut);
                    komut = "Delete from StokHareket Where UrunKodu='" + UrunKodu + "'";
                    KomutCalistir(komut);
                    veriDoldur();
                }
            }
        }

        private void urunHareketMenuItem_Click(object sender, EventArgs e)
        {
            StokGoster = new UrunStokHareketForm();
            StokGoster.urnfrm = this;
            StokGoster.tbUrunKod.Text =
                dgvUrunListe.CurrentRow.Cells[0].Value.ToString();
            StokGoster.tbUrunBilgisi.Text =
                dgvUrunListe.CurrentRow.Cells[1].Value.ToString();
            StokGoster.ShowDialog();
        }

        private void btnRapor_Click(object sender, EventArgs e)
        {
            Raporlar = new RaporForm();
            Raporlar.urnfrm = this;
            Raporlar.RaporTuru = this.Name;            
            Raporlar.ShowDialog();
        }

        private void urunRaporMenuItem_Click(object sender, EventArgs e)
        {
            Raporlar = new RaporForm();
            Raporlar.urnfrm = this;
            Raporlar.RaporTuru = "Stok";
            Raporlar.UrunKodu =
                dgvUrunListe.CurrentRow.Cells[0].Value.ToString();
            Raporlar.ShowDialog();
        }

        private void btnAnaForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
