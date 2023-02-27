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
    public partial class YeniKullaniciForm : Form
    {
        public MusteriAnaForm afrm;
        public MusteriGirisForm grsform;
        public YeniKullaniciForm()
        {
            InitializeComponent();
        }

        #region Değişkenler ve Tanımlamalar
        static string vtBaglantiAdres = "Data Source=localhost;Initial Catalog=Musteri;" +
            "User Id=sa;Password=mmyo;";
        //static string vtBaglantiAdres="Data Source=localhost;Initial Catalog=Musteri;Integrated Security=true;"
        SqlConnection musConn = new SqlConnection(vtBaglantiAdres);
        SqlDataAdapter musAdp;
        SqlCommand musCmd = new SqlCommand();

        int islemDurumu;
        #endregion

        #region Kullanıcı Tanımlı Olaylar
        private DataTable veriGetir(string sorgu)
        {
            DataTable KullaniciTablo = new DataTable();
            KullaniciTablo.Clear();
            musAdp = new SqlDataAdapter(sorgu, musConn);
            musAdp.Fill(KullaniciTablo);
            return KullaniciTablo;
        }
        private void veriDoldur()
        {
            string sorgu = "Select KullaniciAdi, KullaniciSifresi, KullaniciYetkisi" +
                                  " from Kullanici";
            dgvKullanListe.DataSource = veriGetir(sorgu);
        }
        private void BaslikGoster()
        {
            dgvKullanListe.Columns[0].HeaderText = "Ad";
            dgvKullanListe.Columns[0].Width = 180;
            dgvKullanListe.Columns[0].DefaultCellStyle.Alignment = 
                DataGridViewContentAlignment.MiddleCenter;
            dgvKullanListe.Columns[1].HeaderText = "Şifre";
            dgvKullanListe.Columns[1].Width = 170;
            dgvKullanListe.Columns[1].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
            dgvKullanListe.Columns[2].HeaderText = "Yetki";
            dgvKullanListe.Columns[2].Width = 110;
            dgvKullanListe.Columns[2].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
        }
        private void KomutCalistir(string komut)
        {
            try
            {
                if (musConn.State == ConnectionState.Closed)
                    musConn.Open();
                musCmd.Connection = musConn;
                musCmd.CommandText = komut;
                islemDurumu=musCmd.ExecuteNonQuery();
            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.Message, "Bilgi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                if (musConn.State==ConnectionState.Open)
                    musConn.Close();
            }
        }
        private void baslat()
        {
            veriDoldur();
            BaslikGoster();
            tbAd.Select();
        }
        #endregion

        #region Nesne Tanımlı Olaylar

        private void YeniKullaniciForm_Load(object sender, EventArgs e)
        {
            baslat();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (tbAd.Text.Trim() !="" & tbSifre.Text.Trim() !="" & 
                cmbYetki.Text.Trim() !="")
            {
                string komut = "Insert into Kullanici " +
                    "(KullaniciAdi, KullaniciSifresi,KullaniciYetkisi) " +
                    "Values ('"+tbAd.Text+"','"+tbSifre.Text+"','"
                    +cmbYetki.Text+"')";
                KomutCalistir(komut);
                if (islemDurumu==1)
                {
                    MessageBox.Show("Kullanıcı Ekleme \n" +
                        "İşlemi başarılı", "Bilgi", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    veriDoldur();
                    tbAd.Clear();
                    tbSifre.Clear();
                    cmbYetki.Text = "";
                    tbAd.Select();
                }
                else
                {
                    MessageBox.Show("Kullanıcı Ekleme \n" +
                        "İşlemi başarısız", "Bilgi", MessageBoxButtons.OK, 
                        MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Veri Alanlarını \nkontrol " +
                    "ediniz","Bilgi", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                tbAd.Select();
            }
        }

        private void guncelleMenuItem_Click(object sender, EventArgs e)
        {
            tbAd.Text = 
                dgvKullanListe.CurrentRow.Cells[0].Value.ToString();
            tbSifre.Text =
                dgvKullanListe.CurrentRow.Cells[1].Value.ToString();
            cmbYetki.Text =
                dgvKullanListe.CurrentRow.Cells[2].Value.ToString();
            btnKaydet.Enabled = false;
            tbAd.Enabled = false;
            tbSifre.Select();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (tbAd.Text.Trim() !="" & tbSifre.Text.Trim() !="" &
                cmbYetki.Text.Trim() !="")
            {
                string komut = "Update Kullanici set " +
                    "KullaniciSifresi='"+tbSifre.Text+"', " +
                    "KullaniciYetkisi='"+cmbYetki.Text+
                    "'  where KullaniciAdi='"+tbAd.Text+"'";
                KomutCalistir(komut);
                if (islemDurumu==1)
                {
                    MessageBox.Show("Kullanıcı Bilgilerindeki " +
                        "\nGüncelleme Başarılı", "Bilgi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    veriDoldur();
                    btnKaydet.Enabled = true;
                    tbAd.Enabled = true;
                    tbAd.Clear();
                    tbSifre.Clear();
                    cmbYetki.Text = "";
                    tbAd.Select();
                }
                else
                {
                    MessageBox.Show("Kullanıcı Bilgilerindeki " +
                        "\nDeğişim yapılamadı", "Bilgi", 
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Veri Alanlarını " +
                        "\nKontrol Ediniz", "Bilgi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
            }
        }

        private void silMenuItem_Click(object sender, EventArgs e)
        {
            string KullanAd =
                dgvKullanListe.CurrentRow.Cells[0].Value.ToString();
            string komut="Delete from Kullanici where " +
                "KullaniciAdi='"+KullanAd+"'";
            KomutCalistir(komut);
            if (islemDurumu==1)
            {
                MessageBox.Show("Kullanıcı Bilgilerini " +
                        "\nSilme İşlemi Başarılı", "Bilgi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                veriDoldur();
                tbAd.Select();
            }
            else
            {
                MessageBox.Show("Kullanıcı Bilgilerini " +
                        "\nSilme Başarısız", "Bilgi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
            }
        }

        private void btnAnaForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
