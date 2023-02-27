using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MUSTERIAPPS
{
    public partial class MusteriAnaForm : Form
    {
        //1.Aşama
        public MusteriEkleForm yeniMusteri;
        public MusteriIslemForm islemler;
        public RaporForm raporlar;
        public UrunTanimlaForm urunler;
        public YeniKullaniciForm kullanicilar;
        public MusteriGirisForm oturum;
        public MusteriAnaForm()
        {
            InitializeComponent();
        }

        #region Değişkenler ve Tanımlamalar        
        public bool durum = false;
        public string yetkili;
        public bool kapatma = false;
        #endregion

        #region Kullanıcı Tanımlı Olaylar
        private void baslat()
        {
            oturum = new MusteriGirisForm();
            oturum.afrm = this;
        }
        #endregion

        #region Nesne Tanımlı Olaylar

        private void MusteriAnaForm_Load(object sender, EventArgs e)
        {
            //baslat();
            //oturum.ShowDialog();
            //if (durum==false & kapatma==false)
            //    MusteriAnaForm_Load(sender, e);
            //else
            //{
            //    if (kapatma == false)
            //    {
            //        if (yetkili == "Yönetici")
            //            YonetimMenu.Visible = true;
            //        else
            //            YonetimMenu.Visible = false;
            //    }
            //    else
            //        Application.Exit();
            //}
        }

        private void YeniMenuItem_Click(object sender, EventArgs e)
        {
            //2.Aşama
            yeniMusteri = new MusteriEkleForm();
            yeniMusteri.afrm = this;
            yeniMusteri.ShowDialog();
        }

        private void IslemlerMenuItem_Click(object sender, EventArgs e)
        {
            islemler = new MusteriIslemForm();
            islemler.afrm = this;
            islemler.ShowDialog();
        }

        private void UrunlerMenuItem_Click(object sender, EventArgs e)
        {
            urunler = new UrunTanimlaForm();
            urunler.afrm = this;
            urunler.ShowDialog();
        }

        private void RaporMenuItem_Click(object sender, EventArgs e)
        {
            raporlar = new RaporForm();
            raporlar.afrm = this;
            raporlar.RaporTuru = this.Name;
            raporlar.ShowDialog();
        }

        private void yeniKullaniciMenuItem_Click(object sender, EventArgs e)
        {
            kullanicilar = new YeniKullaniciForm();
            kullanicilar.afrm = this;
            kullanicilar.ShowDialog();
        }

        private void kapatMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Uygulamadan Çıkmak \nistiyor musunuz?", "Onay",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                Application.Exit();
        }

        #endregion
    }
}
