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
    public partial class MusteriSatisForm : Form
    {
        public MusteriIslemForm islmfrm;
        public UrunStokGuncelleForm StokGuncelle;
        public MusteriSatisForm()
        {
            InitializeComponent();
        }

        #region Değişkenler ve Tanımlamalar
        static string vtBaglantiAdres = "Data Source=localhost;" +
            "Initial Catalog=Musteri;User Id=sa;Password=mmyo;";
        SqlConnection musConn = new SqlConnection(vtBaglantiAdres);
        SqlDataAdapter musAdp;
        SqlCommand musCmd = new SqlCommand();

        int islemDurumu;
        int StokMiktari;
        bool dolumu = true;
        #endregion

        #region Kullanıcı Tanımlı Olaylar
        private DataTable veriGetir(string sorgu)
        {
            DataTable UrunlerTablo = new DataTable();
            musAdp = new SqlDataAdapter(sorgu, musConn);
            musAdp.Fill(UrunlerTablo);
            return UrunlerTablo;
        }

        private void veriDoldur()
        {
            string sorgu = "Select UrunKodu, UrunTanimi, UrunMiktari, " +
                "SatisFiyati From Urunler";
            dgvUrunListe.DataSource = veriGetir(sorgu);
        }

        private void BaslikGoster ()
        {
            dgvUrunListe.Columns[0].HeaderText = "Ürün Kodu";
            dgvUrunListe.Columns[0].Width = 80;
            dgvUrunListe.Columns[0].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
            dgvUrunListe.Columns[1].HeaderText = "Ürün Bilgisi";
            dgvUrunListe.Columns[1].Width = 300;
            dgvUrunListe.Columns[1].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;
            dgvUrunListe.Columns[2].HeaderText = "Miktar";
            dgvUrunListe.Columns[2].Width = 60;
            dgvUrunListe.Columns[2].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
            dgvUrunListe.Columns[3].HeaderText = "Satış Fiyatı";
            dgvUrunListe.Columns[3].Width = 120;
            dgvUrunListe.Columns[3].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;
            dgvUrunListe.Columns[3].DefaultCellStyle.Format = "C2";
        }

        private void OdemeDoldur()
        {
            DataTable OdemeTablo = new DataTable();
            string sorgu = "Select OdemeTuru From Odeme";
            //OdemeTablo = veriGetir(sorgu);
            musAdp = new SqlDataAdapter(sorgu, musConn);
            musAdp.Fill(OdemeTablo);
            int k = 0;
            while (k<OdemeTablo.Rows.Count)
            {
                cmbOdeme.Items.Add
                    (OdemeTablo.Rows[k].ItemArray[0].ToString());
                k++;
            }
        }

        private void KomutCalistir(string komut)
        {
            try
            {
                if (musConn.State == ConnectionState.Closed)
                    musConn.Open();
                musCmd.CommandText = komut;
                musCmd.Connection = musConn;
                islemDurumu=musCmd.ExecuteNonQuery();
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
            cmbOdeme.Text = "Seçiniz";
            tbUrunKodu.Clear();
            tbUrunTanimi.Clear();
            tbSatisFiyat.Clear();
            tbMiktar.Clear();
            tbSatisTutari.Clear();
            tbMiktar.Select();
        }

        private void BoslukKontrol()
        {
            dolumu = true;
            foreach (Control nesne in this.Controls)
            {
                if (nesne is TextBox)
                {
                    if (nesne.Text.Trim() =="")
                    {
                        dolumu = false;
                        break;
                    }
                }
            }
            if (cmbOdeme.Text == "Seçiniz")
                dolumu = false;
        }

        private void Baslat()
        {
            OdemeDoldur();
            veriDoldur();
            BaslikGoster();
        }
        #endregion

        #region Nesne Tanımlı Olaylar


        private void MusteriSatisForm_Load(object sender, EventArgs e)
        {
            Baslat();
        }

        private void satisYapMenuItem_Click(object sender, EventArgs e)
        {
            tbUrunKodu.Text =
                dgvUrunListe.CurrentRow.Cells[0].Value.ToString();
            tbUrunTanimi.Text =
                dgvUrunListe.CurrentRow.Cells[1].Value.ToString();
            tbSatisFiyat.Text =
                dgvUrunListe.CurrentRow.Cells[3].Value.ToString();
            StokMiktari = Convert.ToInt32
                (dgvUrunListe.CurrentRow.Cells[2].Value.ToString());
            tbMiktar.Select();
        }

        private void tbMiktar_Validating(object sender, CancelEventArgs e)
        {
            if (tbMiktar.Text.Trim() !="")
            {
                int SatisMiktari = Int32.Parse(tbMiktar.Text);
                if (SatisMiktari<=StokMiktari)
                {
                    tbSatisTutari.Text =
                        (Convert.ToDouble(tbSatisFiyat.Text) * SatisMiktari).ToString();
                    cmbOdeme.Select();
                }
                else
                {
                    MessageBox.Show("Satış İşlemi için \nYeterli Ürün Stoğu Yok!",
                        "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Temizle();
                }
            }
        }

        private void stokEkleMenuItem_Click(object sender, EventArgs e)
        {
            StokGuncelle = new UrunStokGuncelleForm();
            StokGuncelle.stsfrm = this;
            StokGuncelle.tbUrunKodu.Text =
                dgvUrunListe.CurrentRow.Cells[0].Value.ToString();
            StokGuncelle.tbUrunTanimi.Text =
                dgvUrunListe.CurrentRow.Cells[1].Value.ToString();
            StokGuncelle.ShowDialog();
            veriDoldur();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            BoslukKontrol();
            if (dolumu)
            {
                string komut = "Insert into Satislar(MusteriNo,FirmaBilgisi," +
                    "UrunKodu,UrunTanimi,SatisMiktari,SatisTutari,OdemeTuru) " +
                    "Values ('"+tbMusteriNo.Text+"','"+tbSirketBilgi.Text+"'," +
                    "'"+tbUrunKodu.Text+"','"+tbUrunTanimi.Text+"','"
                    +tbMiktar.Text+"','"+tbSatisTutari.Text+"','"
                    +cmbOdeme.Text+"')";
                KomutCalistir(komut);
                if (islemDurumu==1)
                {
                    MessageBox.Show("Satış İşlemi \nBaşarılı", "Bilgi",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    komut = "Update Urunler Set UrunMiktari=UrunMiktari - '"
                        +Int32.Parse(tbMiktar.Text)+"' Where UrunKodu='"
                        +tbUrunKodu.Text+"'";
                    KomutCalistir(komut);
                    veriDoldur();
                    Temizle();
                }
                else
                {
                    MessageBox.Show("Satış İşlemi \nBaşarısız Oldu", "Bilgi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tbMiktar.Select();
                }
            }
            else
            {
                MessageBox.Show("Veri Alanlarını \nKontrol Ediniz", "Bilgi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnAnaForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
