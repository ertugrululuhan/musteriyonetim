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
    public partial class MusteriHareketForm : Form
    {
        public MusteriIslemForm islmfrm;
        public MusteriHareketForm()
        {
            InitializeComponent();
        }

        #region Değişkenler ve Tanımlamalar

        static string vtBaglantiAdres = "Data Source=localhost;" +
            "Initial Catalog=Musteri;User Id=sa;Password=mmyo;";
        SqlConnection musConn = new SqlConnection(vtBaglantiAdres);
        SqlDataAdapter musAdp;
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
            string sorgu="Select UrunKodu, UrunTanimi, SatisMiktari, " +
                "SatisTutari, OdemeTuru From Satislar Where MusteriNo='"
                +tbMusteriNo.Text+"'";
            dgvSatisListe.DataSource = veriGetir(sorgu);
        }

       private void BaslikGoster()
        {
            dgvSatisListe.Columns[0].HeaderText = "Ürün Kodu";
            dgvSatisListe.Columns[0].Width = 100;
            dgvSatisListe.Columns[0].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
            dgvSatisListe.Columns[1].HeaderText = "Ürün Bilgisi";
            dgvSatisListe.Columns[1].Width = 300;
            dgvSatisListe.Columns[1].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;
            dgvSatisListe.Columns[2].HeaderText = "Miktar";
            dgvSatisListe.Columns[2].Width = 60;
            dgvSatisListe.Columns[2].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
            dgvSatisListe.Columns[3].HeaderText = "Satış Tutarı";
            dgvSatisListe.Columns[3].Width = 120;
            dgvSatisListe.Columns[3].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;
            dgvSatisListe.Columns[3].DefaultCellStyle.Format = "C2";
            dgvSatisListe.Columns[4].HeaderText = "Ödeme Türü";
            dgvSatisListe.Columns[4].Width = 120;
            dgvSatisListe.Columns[4].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
        }

        private void baslat()
        {
            veriDoldur();
            BaslikGoster();
        }

        #endregion

        #region Nesne Tanımlı Olaylar

        #endregion

        private void MusteriHareketForm_Load(object sender, EventArgs e)
        {
            baslat();
        }

        private void btnAnaForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
