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
    public partial class MusteriIslemForm : Form
    {
        public MusteriAnaForm afrm;
        public MusteriGuncelleForm MusteriGuncel;
        public MusteriHareketForm SatisHareket;
        public MusteriSatisForm SatisYap;
        public RaporForm raporlar;

        public MusteriIslemForm()
        {
            InitializeComponent();
        }

        #region Değişkenler ve Tanımlamalar
        static string vtBaglantiadres = "Data Source=localhost;" +
            "Initial Catalog=Musteri;User Id=sa;Password=mmyo;";
        SqlConnection musConn = new SqlConnection(vtBaglantiadres);
        SqlDataAdapter musAdp;
        SqlCommand musCmd = new SqlCommand();
        int islemDurumu;
        //string MusteriNo;
        #endregion

        #region Kullanıcı Tanımlı Olaylar
        private DataTable veriGetir(string sorgu)
        {
            DataTable MusteriTablo = new DataTable();
            musAdp = new SqlDataAdapter(sorgu, musConn);
            musAdp.Fill(MusteriTablo);
            return MusteriTablo;
        }
        private void veriDoldur()
        {
            string sorgu = "Select MusteriNo, FirmaBilgisi, YetkiliAdi, " +
                "YetkiliSoyadi, FirmaAdresi, TelefonNo  From Kimlik";
            dgvMusteriListe.DataSource = veriGetir(sorgu);
        }

        private void BaslikGoster()
        {
            dgvMusteriListe.Columns[0].HeaderText = "Müşteri No";
            dgvMusteriListe.Columns[0].Width = 100;
            dgvMusteriListe.Columns[0].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
            dgvMusteriListe.Columns[1].HeaderText = "Firma Bilgisi";
            dgvMusteriListe.Columns[1].Width = 320;
            dgvMusteriListe.Columns[1].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;
            dgvMusteriListe.Columns[2].HeaderText = "Yetkili Adı";
            dgvMusteriListe.Columns[2].Width = 120;
            dgvMusteriListe.Columns[2].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;
            dgvMusteriListe.Columns[3].HeaderText = "Yetkili Soyadı";
            dgvMusteriListe.Columns[3].Width = 120;
            dgvMusteriListe.Columns[3].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;
            dgvMusteriListe.Columns[4].HeaderText = "Firma Adresi";
            dgvMusteriListe.Columns[4].Width = 320;
            dgvMusteriListe.Columns[4].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;
            dgvMusteriListe.Columns[5].HeaderText = "Telefon No";
            dgvMusteriListe.Columns[5].Width = 100;
            dgvMusteriListe.Columns[5].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
        }
        
        private void KomutCalistir(string komut)
        {
            try
            {
                if (musConn.State == ConnectionState.Closed)
                    musConn.Open();
                musCmd.CommandText = komut;
                musCmd.Connection = musConn;
                islemDurumu = musCmd.ExecuteNonQuery();
            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.Message, "Dikkat", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                if (musConn.State == ConnectionState.Open)
                    musConn.Close();
            }
        }

        private void baslat()
        {
            veriDoldur();
            BaslikGoster();
        }

        private void SatislariSil(string MusteriNo)
        {
            string sorgu = "Select MusteriNo, FirmaBilgisi, UrunKodu, UrunTanimi, " +
                "SatisMiktari, SatisTutari, OdemeTuru from Satislar " +
                "Where MusteriNo='"+MusteriNo+"'";
            DataTable SatislarTablo = veriGetir(sorgu);
            string komut;
            for (int k=0;k<SatislarTablo.Rows.Count;k++)
            {
                komut = "Insert Into SatislarSil (MusteriNo, FirmaBilgisi, UrunKodu, " +
                    "UrunTanimi, SatisMiktari, SatisTutari, OdemeTuru) " +
                    "Values ('"+SatislarTablo.Rows[k].ItemArray[0].ToString()+
                    "','"+SatislarTablo.Rows[k].ItemArray[1].ToString ()+
                    "','"+SatislarTablo.Rows[k].ItemArray[2].ToString()+
                    "','"+SatislarTablo.Rows[k].ItemArray[3].ToString()+
                    "','"+SatislarTablo.Rows[k].ItemArray[4].ToString ()+
                    "','"+SatislarTablo.Rows[k].ItemArray[5].ToString()+
                    "','"+SatislarTablo.Rows[k].ItemArray[6].ToString ()+"')";
                KomutCalistir(komut);
            }

        }
        #endregion

        #region Nesne Tanımlı Olaylar

        private void MusteriIslemForm_Load(object sender, EventArgs e)
        {
            baslat();
        }

        private void tbSirketBilgi_TextChanged(object sender, EventArgs e)
        {
            if (tbSirketBilgi.Text.Trim() == "")
            {
                veriDoldur();
            }
            else
            {
                string sorgu = "Select MusteriNo, FirmaBilgisi, YetkiliAdi, " +
                    "YetkiliSoyadi, FirmaAdresi, TelefonNo From Kimlik " +
                    "Where FirmaBilgisi LIKE '%" + tbSirketBilgi.Text + "%'";
                dgvMusteriListe.DataSource = veriGetir(sorgu);
            }
        }

        private void tbYetkiliAd_TextChanged(object sender, EventArgs e)
        {
            if (tbYetkiliAd.Text.Trim() == "")
            {
                veriDoldur();
            }
            else
            {
                string sorgu = "Select MusteriNo, FirmaBilgisi, YetkiliAdi, " +
                    "YetkiliSoyadi, FirmaAdresi, TelefonNo From Kimlik " +
                    "Where YetkiliAdi LIKE '%" + tbYetkiliAd.Text + "%'";
                dgvMusteriListe.DataSource = veriGetir(sorgu);
            }
        }

        private void guncelleMenuItem_Click(object sender, EventArgs e)
        {
            MusteriGuncel = new MusteriGuncelleForm();
            MusteriGuncel.islmfrm = this;
            MusteriGuncel.tbMusteriNo.Enabled = false;
            MusteriGuncel.tbSirketBilgi.Enabled = false;

            MusteriGuncel.tbMusteriNo.Text =
                dgvMusteriListe.CurrentRow.Cells[0].Value.ToString();
            MusteriGuncel.tbSirketBilgi.Text =
                dgvMusteriListe.CurrentRow.Cells[1].Value.ToString();
            MusteriGuncel.tbYetkiliAd.Text =
                dgvMusteriListe.CurrentRow.Cells[2].Value.ToString();
            MusteriGuncel.tbYetkiliSoyad.Text =
                dgvMusteriListe.CurrentRow.Cells[3].Value.ToString();
            MusteriGuncel.tbAdres.Text =
                dgvMusteriListe.CurrentRow.Cells[4].Value.ToString();
            MusteriGuncel.tbTelefonNo.Text =
                dgvMusteriListe.CurrentRow.Cells[5].Value.ToString();
            MusteriGuncel.tbYetkiliAd.Select();
            MusteriGuncel.ShowDialog();
            MusteriGuncel.tbMusteriNo.Enabled = true;
            MusteriGuncel.tbSirketBilgi.Enabled = true;
            veriDoldur();
        }

        private void hareketlerMenuItem_Click(object sender, EventArgs e)
        {
            SatisHareket = new MusteriHareketForm();
            SatisHareket.islmfrm = this;
            SatisHareket.tbMusteriNo.Text =
                dgvMusteriListe.CurrentRow.Cells[0].Value.ToString();
            SatisHareket.tbSirketBilgi.Text =
                dgvMusteriListe.CurrentRow.Cells[1].Value.ToString();
            SatisHareket.tbMusteriNo.Enabled = false;
            SatisHareket.tbSirketBilgi.Enabled = false;
            SatisHareket.ShowDialog();
        }

        private void satisYapMenuItem_Click(object sender, EventArgs e)
        {
            SatisYap = new MusteriSatisForm();
            SatisYap.islmfrm = this;
            SatisYap.tbMusteriNo.Text =
                dgvMusteriListe.CurrentRow.Cells[0].Value.ToString();
            SatisYap.tbSirketBilgi.Text =
                dgvMusteriListe.CurrentRow.Cells[1].Value.ToString();
            SatisYap.ShowDialog();
        }

        private void silMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Seçili Olan Müşteriyi \n " +
                "Silmek İstiyor musunuz?", "Bilgi", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) ==
                DialogResult.Yes)
            {
                string MusteriNo =
                    dgvMusteriListe.CurrentRow.Cells[0].Value.ToString();
                string komut = "Insert into KimlikSil (MusteriNo, FirmaBilgisi, " +
                    "YetkiliAdi, YetkiliSoyadi,FirmaAdresi, TelefonNo) " +
                    "Values ('"
                    + dgvMusteriListe.CurrentRow.Cells[0].Value.ToString() + "','"
                    + dgvMusteriListe.CurrentRow.Cells[1].Value.ToString() + "','"
                    + dgvMusteriListe.CurrentRow.Cells[2].Value.ToString() + "','"
                    + dgvMusteriListe.CurrentRow.Cells[3].Value.ToString() + "','"
                    + dgvMusteriListe.CurrentRow.Cells[4].Value.ToString() + "','"
                    + dgvMusteriListe.CurrentRow.Cells[5].Value.ToString() + "')";
                KomutCalistir(komut);
                if (islemDurumu == 1)
                {
                    SatislariSil(MusteriNo);
                    komut = "Delete from Kimlik Where MusteriNo='" + MusteriNo + "'";
                    KomutCalistir(komut);
                    komut = "Delete from Satislar Where MusteriNo='" + MusteriNo + "'";
                    KomutCalistir(komut);
                    MessageBox.Show("Müşteri Silme \nİşlemi Başarılı", "Bilgi",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    veriDoldur();
                }
            }
        }

        private void raporMenuItem_Click(object sender, EventArgs e)
        {
            raporlar = new RaporForm();
            raporlar.islmfrm = this;
            raporlar.MusteriNo =
                dgvMusteriListe.CurrentRow.Cells[0].Value.ToString();
            raporlar.RaporTuru = "Satis";
            raporlar.ShowDialog();
        }

        private void btnAnaForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
