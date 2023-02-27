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
    public partial class RaporForm : Form
    {
        public MusteriAnaForm afrm;
        public MusteriIslemForm islmfrm;
        public UrunTanimlaForm urnfrm;
        public RaporForm()
        {
            InitializeComponent();
        }

        #region Değişkenler ve Tanımlamalar
        public string MusteriNo;
        public string UrunKodu;
        public string RaporTuru;

        static string vtBaglantiAdres = "Data Source=localhost;Initial Catalog=Musteri;" +
            "User Id=sa;Password=mmyo;";
        SqlConnection musConn = new SqlConnection(vtBaglantiAdres);
        SqlDataAdapter musAdp;

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

        private void MusteriDoldur()
        {
            string sorgu = "Select MusteriNo, FirmaBilgisi, FirmaAdresi, TelefonNo from Kimlik";
            RaporMusteriListe rapor = new RaporMusteriListe();
            rapor.SetDataSource(veriGetir(sorgu));
            RaporGoster.ReportSource = rapor;
        }

        private void MusteriSatisDoldur()
        {
            string sorgu = "Select UrunKodu, UrunTanimi, SatisMiktari, SatisTutari, " +
                "OdemeTuru from Satislar Where MusteriNo='"+MusteriNo+"'";
            RaporMusteriSatis rapor = new RaporMusteriSatis();
            rapor.SetDataSource(veriGetir(sorgu));
            RaporGoster.ReportSource = rapor;
        }

        private void UrunDoldur()
        {
            string sorgu = "Select UrunKodu, UrunTanimi, UrunMiktari, SatisFiyati from Urunler";
            RaporUrunlerListe rapor = new RaporUrunlerListe();
            rapor.SetDataSource(veriGetir(sorgu));
            RaporGoster.ReportSource = rapor;
        }

        private void UrunStokDoldur()
        {
            string sorgu = "Select UrunKodu, IslemTarihi, AlinanMiktar, " +
                "AlisFiyati, AlisTutari from StokHareket Where UrunKodu='"+UrunKodu+"'";
            RaporUrunStok rapor = new RaporUrunStok();
            rapor.SetDataSource(veriGetir(sorgu));
            RaporGoster.ReportSource = rapor;
        }
        #endregion

        #region Nesne Tanımlı Olaylar



        #endregion

        private void RaporForm_Load(object sender, EventArgs e)
        {
            if (RaporTuru=="MusteriAnaForm")
            {
                //RaporMusteriListe rapor = new RaporMusteriListe();
                //RaporGoster.ReportSource = rapor;
                MusteriDoldur();
                RaporTuru = "";
            }
            else if (RaporTuru=="UrunTanimlaForm")
            {
                UrunDoldur();
                RaporTuru = "";
            }
            else if (RaporTuru=="Satis")
            {
                MusteriSatisDoldur();
                RaporTuru = "";
            }
            else if (RaporTuru=="Stok")
            {
                UrunStokDoldur();
                RaporTuru = "";
            }
        }

        private void cmbRaporTuru_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRaporTuru.SelectedIndex==0)
            {
                MusteriDoldur();
            }
            if (cmbRaporTuru.SelectedIndex==1)
            {
                UrunDoldur();
            }
        }
    }
}
