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
    public partial class UrunStokGuncelleForm : Form
    {
        public MusteriSatisForm stsfrm;
        public UrunStokGuncelleForm()
        {
            InitializeComponent();
        }

        #region Değişkenler ve Tanımlamalar
        static string vtBaglantiAdres = "Data Source=localhost;Initial Catalog=Musteri;" +
            "User Id=sa;Password=mmyo;";
        SqlConnection musConn = new SqlConnection(vtBaglantiAdres);
        SqlCommand musCmd = new SqlCommand();
        int islemDurumu;
        bool dolumu=true;
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
        }
        private void BoslukKontrol()
        {
            foreach(Control tbox in this.Controls)
                if (tbox is TextBox)
                    if (tbox.Text.Trim() =="")
                    {
                        dolumu = false;
                        break;
                    }
        }

        private void SayisalControl(object sender, KeyPressEventArgs e)
        {
            e.Handled = 
                (!char.IsDigit(e.KeyChar) & !char.IsControl(e.KeyChar) & e.KeyChar != 44);
        }
        #endregion

        #region Nesne Tanımlı Olaylar

        #endregion

        private void UrunStokGuncelleForm_Load(object sender, EventArgs e)
        {
            tbMiktar.Select();
        }

        private void tbAlisFiyat_Validating(object sender, CancelEventArgs e)
        {
            if (tbAlisFiyat.Text.Trim() != "")
            {
                tbSatisFiyat.Text =
                    ((Convert.ToDouble(tbAlisFiyat.Text) * 1.20) * 1.18).ToString();
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            BoslukKontrol();
            if (dolumu)
            {
                string komut = "Update Urunler Set UrunTanimi='"+tbUrunTanimi.Text+
                    "',UrunMiktari=UrunMiktari+'"+Int16.Parse(tbMiktar.Text)+
                    "', AlisFiyati='"+tbAlisFiyat.Text+"', SatisFiyati='"+tbSatisFiyat.Text+
                    "' Where UrunKodu='"+tbUrunKodu.Text+"'"; 
                KomutCalistir(komut);
                if (islemDurumu==1)
                {
                    komut = "Insert into StokHareket(UrunKodu, IslemTarihi, AlinanMiktar, AlisFiyati, AlisTutari) " +
                        "Values('"+tbUrunKodu.Text+"','"+DateTime.Now.ToString ("MM.dd.yyyy")+"','"
                        +tbMiktar.Text+"','"+tbAlisFiyat.Text+"','"
                        +(double.Parse(tbAlisFiyat.Text)*Int16.Parse(tbMiktar.Text)).ToString ()+"')"; 
                    KomutCalistir(komut);
                    MessageBox.Show("Ürün Güncelleme \nİşlemi Başarılı", "Bilgi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Temizle();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Veri Alanlarını \nkontrol ediniz", "Bilgi",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                tbMiktar.Select();
            }
        }
    }
}
