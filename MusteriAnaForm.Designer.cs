namespace MUSTERIAPPS
{
    partial class MusteriAnaForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.AnaMenu = new System.Windows.Forms.MenuStrip();
            this.MusteriMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.YeniMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.IslemlerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UrunlerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RaporMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.YonetimMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.yeniKullaniciMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kapatMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AnaMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // AnaMenu
            // 
            this.AnaMenu.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.AnaMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MusteriMenu,
            this.YonetimMenu,
            this.kapatMenuItem});
            this.AnaMenu.Location = new System.Drawing.Point(0, 0);
            this.AnaMenu.Name = "AnaMenu";
            this.AnaMenu.Size = new System.Drawing.Size(446, 25);
            this.AnaMenu.TabIndex = 0;
            this.AnaMenu.Text = "Ana Menü";
            // 
            // MusteriMenu
            // 
            this.MusteriMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.YeniMenuItem,
            this.IslemlerMenuItem,
            this.UrunlerMenuItem,
            this.RaporMenuItem});
            this.MusteriMenu.Name = "MusteriMenu";
            this.MusteriMenu.Size = new System.Drawing.Size(71, 21);
            this.MusteriMenu.Text = "&Müşteri";
            // 
            // YeniMenuItem
            // 
            this.YeniMenuItem.Name = "YeniMenuItem";
            this.YeniMenuItem.Size = new System.Drawing.Size(180, 22);
            this.YeniMenuItem.Text = "Yeni";
            this.YeniMenuItem.Click += new System.EventHandler(this.YeniMenuItem_Click);
            // 
            // IslemlerMenuItem
            // 
            this.IslemlerMenuItem.Name = "IslemlerMenuItem";
            this.IslemlerMenuItem.Size = new System.Drawing.Size(180, 22);
            this.IslemlerMenuItem.Text = "İşlemler";
            this.IslemlerMenuItem.Click += new System.EventHandler(this.IslemlerMenuItem_Click);
            // 
            // UrunlerMenuItem
            // 
            this.UrunlerMenuItem.Name = "UrunlerMenuItem";
            this.UrunlerMenuItem.Size = new System.Drawing.Size(180, 22);
            this.UrunlerMenuItem.Text = "Ürünler";
            this.UrunlerMenuItem.Click += new System.EventHandler(this.UrunlerMenuItem_Click);
            // 
            // RaporMenuItem
            // 
            this.RaporMenuItem.Name = "RaporMenuItem";
            this.RaporMenuItem.Size = new System.Drawing.Size(180, 22);
            this.RaporMenuItem.Text = "Rapor";
            this.RaporMenuItem.Click += new System.EventHandler(this.RaporMenuItem_Click);
            // 
            // YonetimMenu
            // 
            this.YonetimMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.yeniKullaniciMenuItem});
            this.YonetimMenu.Name = "YonetimMenu";
            this.YonetimMenu.Size = new System.Drawing.Size(77, 21);
            this.YonetimMenu.Text = "Yönetim";
            // 
            // yeniKullaniciMenuItem
            // 
            this.yeniKullaniciMenuItem.Name = "yeniKullaniciMenuItem";
            this.yeniKullaniciMenuItem.Size = new System.Drawing.Size(180, 22);
            this.yeniKullaniciMenuItem.Text = "Yeni Kullanıcı";
            this.yeniKullaniciMenuItem.Click += new System.EventHandler(this.yeniKullaniciMenuItem_Click);
            // 
            // kapatMenuItem
            // 
            this.kapatMenuItem.Name = "kapatMenuItem";
            this.kapatMenuItem.Size = new System.Drawing.Size(52, 21);
            this.kapatMenuItem.Text = "Çıkış";
            this.kapatMenuItem.Click += new System.EventHandler(this.kapatMenuItem_Click);
            // 
            // MusteriAnaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 344);
            this.ControlBox = false;
            this.Controls.Add(this.AnaMenu);
            this.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.AnaMenu;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MusteriAnaForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MÜŞTERİ BİLGİ SİSTEMİ";
            this.Load += new System.EventHandler(this.MusteriAnaForm_Load);
            this.AnaMenu.ResumeLayout(false);
            this.AnaMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip AnaMenu;
        private System.Windows.Forms.ToolStripMenuItem MusteriMenu;
        private System.Windows.Forms.ToolStripMenuItem YeniMenuItem;
        private System.Windows.Forms.ToolStripMenuItem IslemlerMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kapatMenuItem;
        private System.Windows.Forms.ToolStripMenuItem YonetimMenu;
        private System.Windows.Forms.ToolStripMenuItem yeniKullaniciMenuItem;
        private System.Windows.Forms.ToolStripMenuItem UrunlerMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RaporMenuItem;
    }
}

