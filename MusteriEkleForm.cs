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
    public partial class MusteriEkleForm : Form
    {
        //3.Aşama
        public MusteriAnaForm afrm;
        public MusteriEkleForm()
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
        private void KomutCalistir (string komut)
        {
            try
            {
                if (musConn.State == ConnectionState.Closed)
                    musConn.Open();
                musCmd.Connection = musConn;
                musCmd.CommandText = komut;
                IslemDurumu=musCmd.ExecuteNonQuery();
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

        private void MusteriEkleForm_Load(object sender, EventArgs e)
        {
            tbMusteriNo.Select();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (tbMusteriNo.Text.Trim() !="" & tbSirketBilgi.Text.Trim() !="" &
                tbYetkiliAd.Text.Trim() !="" & tbYetkiliSoyad.Text.Trim() !="" &
                tbAdres.Text.Trim() !="" & tbTelefonNo.Text.Trim() !="")
            {
                string komut = "Insert Into Kimlik (MusteriNo, FirmaBilgisi, " +
                    "YetkiliAdi, YetkiliSoyadi, FirmaAdresi, TelefonNo) Values ('"
                    +tbMusteriNo.Text+"','"+tbSirketBilgi.Text+"','"
                    +tbYetkiliAd.Text+"','"+tbYetkiliSoyad.Text+"','"
                    +tbAdres.Text+"','"+tbTelefonNo.Text+"')";
                KomutCalistir(komut);
                if (IslemDurumu==1)
                {
                    MessageBox.Show("Müşteri Kayıt \nİşlemi Başarılı", "Bilgi",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    foreach (Control tbox in this.Controls)
                    {
                        if (tbox is TextBox)
                            tbox.Text = "";
                    }
                    tbMusteriNo.Select();
                }
                else
                {
                    MessageBox.Show("Müşteri Kayıt \nİşlemi Başarısız", "Bilgi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tbMusteriNo.Select();
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
