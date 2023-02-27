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
    public partial class MusteriGirisForm : Form
    {
        public MusteriAnaForm afrm;
        public YeniKullaniciForm kullanicilar;
        public MusteriGirisForm()
        {
            InitializeComponent();
        }

        #region Değişkenler ve Tanımlamalar
        //static string VtBaglantiAdres = "Data Source=localhost;" +
        //    "Initial Catalog=Musteri;Integrated Security=true;";
        static string VtBaglantiAdres = "Data Source=localhost;" +
            "Initial Catalog=Musteri;User Id=sa;Password=mmyo;";
        SqlConnection musConn = new SqlConnection(VtBaglantiAdres);
        SqlDataAdapter musAdp;

        DataTable kullaniciTablo;
        bool yokmu = false;
        Random rSayi = new Random();
        int islemSonucu = 0;
        string isaret = "+-";
        #endregion

        #region Kullanıcı Tanımlı Olaylar
        private DataTable veriGetir(string sorgu)
        {
            DataTable musteriTablo = new DataTable();
            musAdp = new SqlDataAdapter(sorgu, musConn);
            musAdp.Fill(musteriTablo);
            return musteriTablo;
        }
        private void veriDoldur()
        {
            kullaniciTablo = new DataTable();
            string sorgu = "Select KullaniciAdi, KullaniciSifresi, " +
                " KullaniciYetkisi From Kullanici";
            kullaniciTablo = veriGetir(sorgu);
            if (kullaniciTablo.Rows.Count <= 0)
                yokmu = true;
            else
                yokmu = false;
        }

        private void GuvenlikKoduUret()
        {
            int sayi1 = rSayi.Next(30, 71);
            int sayi2 = rSayi.Next(1, 31);
            int islemtipi = rSayi.Next(0, 2);
            lblSayi1.Text = sayi1.ToString();
            lblSayi2.Text = sayi2.ToString();
            lblIslemTuru.Text = isaret[islemtipi].ToString();
            if (lblIslemTuru.Text == "+")
                islemSonucu = sayi1 + sayi2;
            else
                islemSonucu = sayi1 - sayi2;
        }
        private void formIslem()
        {
            GuvenlikKoduUret();
            tbAd.Select();
        }
        #endregion

        #region Nesne Tanımlı Olaylar

        private void MusteriGirisForm_Load(object sender, EventArgs e)
        {
            tbAd.Select();
            veriDoldur();
            if (yokmu)
            {
                MessageBox.Show("Sistemde Oturum Açmak için \n" +
                    "Kullanıcı Tanımlamalısınız", "Bilgi", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                kullanicilar = new YeniKullaniciForm();
                kullanicilar.grsform = this;
                kullanicilar.ShowDialog();
                veriDoldur();
                if (yokmu)
                {
                    MusteriGirisForm_Load(sender, e);
                }
                else
                {
                    formIslem();
                }
            }
            else
            {
                formIslem();
            }
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            string ad, sifre, yetki;
            for (int k=0;k<kullaniciTablo.Rows.Count;k++)
            {
                ad = kullaniciTablo.Rows[k].ItemArray[0].ToString();
                sifre = kullaniciTablo.Rows[k].ItemArray[1].ToString();
                yetki = kullaniciTablo.Rows[k].ItemArray[2].ToString();
                if (tbAd.Text==ad & tbSifre.Text==sifre & 
                    islemSonucu==Convert.ToInt32(tbSonuc.Text))
                {
                    afrm.durum = true;
                    afrm.yetkili = yetki;
                    break;
                }
            }
            if (afrm.durum)
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("Girilen Verileri \nKontrol ediniz",
                    "Uyarı", MessageBoxButtons.OK, 
                    MessageBoxIcon.Warning);
                tbAd.Clear();
                tbSifre.Clear();
                tbSonuc.Clear();
                formIslem();
                tbAd.Select();
            }
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Uygulamadan Çıkmak \nistiyor musunuz?", "Onay",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                afrm.durum = true;
                afrm.kapatma = true;
                this.Close();
            }
        }

        #endregion
    }
}
