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
    public partial class UrunStokHareketForm : Form
    {
        public UrunTanimlaForm urnfrm;
        public UrunStokHareketForm()
        {
            InitializeComponent();
        }

        #region Değişkenler ve Tanımlamalar
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

        private void VeriDoldur()
        {
            string sorgu = "Select IslemTarihi, AlinanMiktar, AlisFiyati, AlisTutari" +
                "  from StokHareket Where UrunKodu='"+tbUrunKod.Text+"'";
            dgvUrunListe.DataSource = veriGetir(sorgu);
        }

        private void BaslikGoster()
        {
            dgvUrunListe.Columns[0].HeaderText = "İşlem Tarihi";
            dgvUrunListe.Columns[0].Width = 120;
            dgvUrunListe.Columns[0].DefaultCellStyle.Alignment = 
                DataGridViewContentAlignment.MiddleCenter;
            dgvUrunListe.Columns[1].HeaderText = "Miktar";
            dgvUrunListe.Columns[1].Width = 60;
            dgvUrunListe.Columns[1].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
            dgvUrunListe.Columns[2].HeaderText = "Alış Fiyatı";
            dgvUrunListe.Columns[2].Width = 120;
            dgvUrunListe.Columns[2].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;
            dgvUrunListe.Columns[2].DefaultCellStyle.Format = "C2";
            dgvUrunListe.Columns[3].HeaderText = "Alış Tutarı";
            dgvUrunListe.Columns[3].Width = 120;
            dgvUrunListe.Columns[3].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;
            dgvUrunListe.Columns[3].DefaultCellStyle.Format = "C2";
        }

        private  void Baslat()
        {
            VeriDoldur();
            BaslikGoster();
        }

        #endregion

        #region Nesne Tanımlı Olaylar
        private void UrunStokHareketForm_Load(object sender, EventArgs e)
        {
            Baslat();
        }

        private void btnAnaForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
