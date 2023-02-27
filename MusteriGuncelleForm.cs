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


namespace MUSTERIAPPS
{
    public partial class MusteriGuncelleForm : Form
    {
        public MusteriIslemForm islmfrm;
        public MusteriGuncelleForm()
        {
            InitializeComponent();
        }

        #region Değişkenler ve Tanımlamalar

        static string vtBaglantiAdres = "Data Source=localhost;" +
            "Initial Catalog=Musteri;User Id=sa;Password=mmyo;";
        SqlConnection musConn = new SqlConnection(vtBaglantiAdres);
        SqlCommand musCmd = new SqlCommand();
        int IslemDurumu;
        #endregion

        #region Kullanıcı Tanımlı Olaylar
        private void KomutCalistir(string komut)
        {
            try
            {
                if (musConn.State == ConnectionState.Closed)
                    musConn.Open();
                musCmd.Connection = musConn;
                musCmd.CommandText = komut;
                IslemDurumu = musCmd.ExecuteNonQuery();
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
        #endregion

        #region Nesne Tanımlı Olaylar


        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (tbMusteriNo.Text.Trim() != "" & tbSirketBilgi.Text.Trim() != "" &
                tbYetkiliAd.Text.Trim() != "" & tbYetkiliSoyad.Text.Trim() != "" &
                tbAdres.Text.Trim() != "" & tbTelefonNo.Text.Trim() != "")
            {
                string komut = "Update Kimlik Set FirmaBilgisi='"
                    + tbSirketBilgi.Text + "', YetkiliAdi='" + tbYetkiliAd.Text +
                    "', YetkiliSoyadi='" + tbYetkiliSoyad.Text + "', FirmaAdresi='"
                    + tbAdres.Text + "', TelefonNo='" + tbTelefonNo.Text +
                    "' Where MusteriNo='" + tbMusteriNo.Text + "'";
                KomutCalistir(komut);
                if (IslemDurumu == 1)
                {
                    MessageBox.Show(tbMusteriNo.Text + " Müşteri \nBilgileri " +
                        "Güncellendi", "Bilgi", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    foreach (Control tbox in this.Controls)
                    {
                        if (tbox is TextBox)
                            tbox.Text = "";
                    }
                    this.Close();
                }
                else
                {
                    MessageBox.Show(tbMusteriNo.Text + " Müşteri \nBilgileri " +
                        "Güncellenemedi", "Bilgi", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show(" Veri Alanlarını \nKontrol Ediniz", "Bilgi",
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
