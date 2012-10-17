namespace FirmaTakip.WinUI
{
    partial class StokMenu
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
            DevExpress.XtraEditors.TileItemElement tileItemElement1 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement2 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement3 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement4 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement5 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement6 = new DevExpress.XtraEditors.TileItemElement();
            this.tileControl1 = new DevExpress.XtraEditors.TileControl();
            this.tileGroup2 = new DevExpress.XtraEditors.TileGroup();
            this.tileItemHamStok = new DevExpress.XtraEditors.TileItem();
            this.tileItemImalatSevk = new DevExpress.XtraEditors.TileItem();
            this.tileGroup6 = new DevExpress.XtraEditors.TileGroup();
            this.tileItemMalzemeler = new DevExpress.XtraEditors.TileItem();
            this.tileItemIslenmisMalzemeSevk = new DevExpress.XtraEditors.TileItem();
            this.tileGroup1 = new DevExpress.XtraEditors.TileGroup();
            this.tileItemStokRaporlari = new DevExpress.XtraEditors.TileItem();
            this.tileItemMusteriyeSevk = new DevExpress.XtraEditors.TileItem();
            this.tileGroup3 = new DevExpress.XtraEditors.TileGroup();
            this.SuspendLayout();
            // 
            // tileControl1
            // 
            this.tileControl1.AllowDrop = true;
            this.tileControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tileControl1.Groups.Add(this.tileGroup2);
            this.tileControl1.Groups.Add(this.tileGroup6);
            this.tileControl1.Groups.Add(this.tileGroup1);
            this.tileControl1.Location = new System.Drawing.Point(0, 0);
            this.tileControl1.MaxId = 15;
            this.tileControl1.Name = "tileControl1";
            this.tileControl1.Size = new System.Drawing.Size(1057, 641);
            this.tileControl1.TabIndex = 2;
            this.tileControl1.Text = "Giriş Menüsü";
            this.tileControl1.VerticalContentAlignment = DevExpress.Utils.VertAlignment.Center;
            // 
            // tileGroup2
            // 
            this.tileGroup2.Items.Add(this.tileItemHamStok);
            this.tileGroup2.Items.Add(this.tileItemImalatSevk);
            this.tileGroup2.Name = "tileGroup2";
            // 
            // tileItemHamStok
            // 
            this.tileItemHamStok.AppearanceItem.Normal.Font = new System.Drawing.Font("Tahoma", 12F);
            this.tileItemHamStok.AppearanceItem.Normal.Options.UseFont = true;
            this.tileItemHamStok.BackgroundImage = global::FirmaTakip.WinUI.Properties.Resources.kasa;
            this.tileItemHamStok.BackgroundImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleRight;
            this.tileItemHamStok.BackgroundImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.ZoomInside;
            tileItemElement1.Text = "Ambara Malzeme<br>Giriş<br>İrsaliyesi";
            tileItemElement1.TextLocation = new System.Drawing.Point(0, 0);
            this.tileItemHamStok.Elements.Add(tileItemElement1);
            this.tileItemHamStok.Id = 0;
            this.tileItemHamStok.IsLarge = true;
            this.tileItemHamStok.Name = "tileItemHamStok";
            this.tileItemHamStok.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(this.tileItemHamStok_ItemClick);
            // 
            // tileItemImalatSevk
            // 
            this.tileItemImalatSevk.AppearanceItem.Normal.Font = new System.Drawing.Font("Tahoma", 12F);
            this.tileItemImalatSevk.AppearanceItem.Normal.Options.UseFont = true;
            this.tileItemImalatSevk.BackgroundImage = global::FirmaTakip.WinUI.Properties.Resources.islemesevk;
            this.tileItemImalatSevk.BackgroundImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleRight;
            this.tileItemImalatSevk.BackgroundImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.ZoomInside;
            tileItemElement2.Text = "Ham Malzemenin<br>İşlemeye Sevki";
            tileItemElement2.TextLocation = new System.Drawing.Point(0, 0);
            this.tileItemImalatSevk.Elements.Add(tileItemElement2);
            this.tileItemImalatSevk.Id = 10;
            this.tileItemImalatSevk.IsLarge = true;
            this.tileItemImalatSevk.Name = "tileItemImalatSevk";
            this.tileItemImalatSevk.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(this.tileItemImalatSevk_ItemClick);
            // 
            // tileGroup6
            // 
            this.tileGroup6.Items.Add(this.tileItemMalzemeler);
            this.tileGroup6.Items.Add(this.tileItemIslenmisMalzemeSevk);
            this.tileGroup6.Name = "tileGroup6";
            // 
            // tileItemMalzemeler
            // 
            this.tileItemMalzemeler.AppearanceItem.Normal.Font = new System.Drawing.Font("Tahoma", 12F);
            this.tileItemMalzemeler.AppearanceItem.Normal.Options.UseFont = true;
            this.tileItemMalzemeler.BackgroundImage = global::FirmaTakip.WinUI.Properties.Resources.malzemeler;
            this.tileItemMalzemeler.BackgroundImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleRight;
            this.tileItemMalzemeler.BackgroundImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.ZoomInside;
            tileItemElement3.Text = "Malzemeler";
            tileItemElement3.TextLocation = new System.Drawing.Point(0, 0);
            this.tileItemMalzemeler.Elements.Add(tileItemElement3);
            this.tileItemMalzemeler.Id = 6;
            this.tileItemMalzemeler.IsLarge = true;
            this.tileItemMalzemeler.Name = "tileItemMalzemeler";
            this.tileItemMalzemeler.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(this.tileItemMalzemeler_ItemClick);
            // 
            // tileItemIslenmisMalzemeSevk
            // 
            this.tileItemIslenmisMalzemeSevk.AppearanceItem.Normal.Font = new System.Drawing.Font("Tahoma", 12F);
            this.tileItemIslenmisMalzemeSevk.AppearanceItem.Normal.Options.UseFont = true;
            this.tileItemIslenmisMalzemeSevk.BackgroundImage = global::FirmaTakip.WinUI.Properties.Resources.SevkAlani;
            this.tileItemIslenmisMalzemeSevk.BackgroundImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleRight;
            this.tileItemIslenmisMalzemeSevk.BackgroundImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.ZoomInside;
            tileItemElement4.Text = "İşlenmiş Malzemenin<br>Alana Sevki";
            tileItemElement4.TextLocation = new System.Drawing.Point(0, 0);
            this.tileItemIslenmisMalzemeSevk.Elements.Add(tileItemElement4);
            this.tileItemIslenmisMalzemeSevk.Id = 12;
            this.tileItemIslenmisMalzemeSevk.IsLarge = true;
            this.tileItemIslenmisMalzemeSevk.Name = "tileItemIslenmisMalzemeSevk";
            this.tileItemIslenmisMalzemeSevk.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(this.tileItemIslenmisMalzemeSevk_ItemClick);
            // 
            // tileGroup1
            // 
            this.tileGroup1.Items.Add(this.tileItemStokRaporlari);
            this.tileGroup1.Items.Add(this.tileItemMusteriyeSevk);
            this.tileGroup1.Name = "tileGroup1";
            // 
            // tileItemStokRaporlari
            // 
            this.tileItemStokRaporlari.AppearanceItem.Normal.Font = new System.Drawing.Font("Tahoma", 12F);
            this.tileItemStokRaporlari.AppearanceItem.Normal.Options.UseFont = true;
            this.tileItemStokRaporlari.BackgroundImage = global::FirmaTakip.WinUI.Properties.Resources.stokrapor;
            this.tileItemStokRaporlari.BackgroundImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleRight;
            this.tileItemStokRaporlari.BackgroundImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.ZoomInside;
            tileItemElement5.Text = "Stok<br>Raporları";
            tileItemElement5.TextLocation = new System.Drawing.Point(0, 0);
            this.tileItemStokRaporlari.Elements.Add(tileItemElement5);
            this.tileItemStokRaporlari.Id = 14;
            this.tileItemStokRaporlari.IsLarge = true;
            this.tileItemStokRaporlari.Name = "tileItemStokRaporlari";
            this.tileItemStokRaporlari.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(this.tileItemStokRaporlari_ItemClick);
            // 
            // tileItemMusteriyeSevk
            // 
            this.tileItemMusteriyeSevk.AppearanceItem.Normal.Font = new System.Drawing.Font("Tahoma", 12F);
            this.tileItemMusteriyeSevk.AppearanceItem.Normal.Options.UseFont = true;
            this.tileItemMusteriyeSevk.BackgroundImage = global::FirmaTakip.WinUI.Properties.Resources.musterisevk;
            this.tileItemMusteriyeSevk.BackgroundImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleRight;
            this.tileItemMusteriyeSevk.BackgroundImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.ZoomInside;
            tileItemElement6.Text = "Müşteriye<br>Sevk İrsaliyesi";
            tileItemElement6.TextLocation = new System.Drawing.Point(0, 0);
            this.tileItemMusteriyeSevk.Elements.Add(tileItemElement6);
            this.tileItemMusteriyeSevk.Id = 13;
            this.tileItemMusteriyeSevk.IsLarge = true;
            this.tileItemMusteriyeSevk.Name = "tileItemMusteriyeSevk";
            this.tileItemMusteriyeSevk.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(this.tileItemMusteriyeSevk_ItemClick);
            // 
            // tileGroup3
            // 
            this.tileGroup3.Name = "tileGroup3";
            // 
            // StokMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1057, 641);
            this.Controls.Add(this.tileControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "StokMenu";
            this.Text = "Stok Menüsü";
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TileControl tileControl1;
        private DevExpress.XtraEditors.TileGroup tileGroup2;
        private DevExpress.XtraEditors.TileItem tileItemHamStok;
        private DevExpress.XtraEditors.TileGroup tileGroup6;
        private DevExpress.XtraEditors.TileGroup tileGroup1;
        private DevExpress.XtraEditors.TileGroup tileGroup3;
        private DevExpress.XtraEditors.TileItem tileItemImalatSevk;
        private DevExpress.XtraEditors.TileItem tileItemMalzemeler;
        private DevExpress.XtraEditors.TileItem tileItemIslenmisMalzemeSevk;
        private DevExpress.XtraEditors.TileItem tileItemStokRaporlari;
        private DevExpress.XtraEditors.TileItem tileItemMusteriyeSevk;
    }
}