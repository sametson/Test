namespace FirmaTakip.WinUI
{
    partial class Giris
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
            this.tileControl1 = new DevExpress.XtraEditors.TileControl();
            this.tileGroup2 = new DevExpress.XtraEditors.TileGroup();
            this.tileItemBakimMenusu = new DevExpress.XtraEditors.TileItem();
            this.tileItemStokMenu = new DevExpress.XtraEditors.TileItem();
            this.tileGroup5 = new DevExpress.XtraEditors.TileGroup();
            this.tileItemKullaniciBilgileri = new DevExpress.XtraEditors.TileItem();
            this.SuspendLayout();
            // 
            // tileControl1
            // 
            this.tileControl1.AllowDrag = false;
            this.tileControl1.AllowDrop = true;
            this.tileControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tileControl1.Groups.Add(this.tileGroup2);
            this.tileControl1.Groups.Add(this.tileGroup5);
            this.tileControl1.Location = new System.Drawing.Point(0, 0);
            this.tileControl1.MaxId = 7;
            this.tileControl1.Name = "tileControl1";
            this.tileControl1.Size = new System.Drawing.Size(1230, 642);
            this.tileControl1.TabIndex = 1;
            this.tileControl1.Text = "Giriş Menüsü";
            this.tileControl1.VerticalContentAlignment = DevExpress.Utils.VertAlignment.Center;
            // 
            // tileGroup2
            // 
            this.tileGroup2.Items.Add(this.tileItemBakimMenusu);
            this.tileGroup2.Items.Add(this.tileItemStokMenu);
            this.tileGroup2.Name = "tileGroup2";
            // 
            // tileItemBakimMenusu
            // 
            this.tileItemBakimMenusu.AppearanceItem.Normal.Font = new System.Drawing.Font("Tahoma", 12F);
            this.tileItemBakimMenusu.AppearanceItem.Normal.Options.UseFont = true;
            this.tileItemBakimMenusu.BackgroundImage = global::FirmaTakip.WinUI.Properties.Resources.bakimlar;
            this.tileItemBakimMenusu.BackgroundImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleRight;
            this.tileItemBakimMenusu.BackgroundImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.ZoomInside;
            tileItemElement1.Text = "Bakım Menüsü";
            tileItemElement1.TextLocation = new System.Drawing.Point(0, 0);
            this.tileItemBakimMenusu.Elements.Add(tileItemElement1);
            this.tileItemBakimMenusu.Id = 0;
            this.tileItemBakimMenusu.IsLarge = true;
            this.tileItemBakimMenusu.Name = "tileItemBakimMenusu";
            this.tileItemBakimMenusu.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(this.tileItemBakimMenusu_ItemClick);
            // 
            // tileItemStokMenu
            // 
            this.tileItemStokMenu.AppearanceItem.Normal.Font = new System.Drawing.Font("Tahoma", 12F);
            this.tileItemStokMenu.AppearanceItem.Normal.Options.UseFont = true;
            this.tileItemStokMenu.BackgroundImage = global::FirmaTakip.WinUI.Properties.Resources.stok;
            this.tileItemStokMenu.BackgroundImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleRight;
            this.tileItemStokMenu.BackgroundImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.ZoomInside;
            tileItemElement2.Text = "Stok Menüsü";
            tileItemElement2.TextLocation = new System.Drawing.Point(0, 0);
            this.tileItemStokMenu.Elements.Add(tileItemElement2);
            this.tileItemStokMenu.Id = 6;
            this.tileItemStokMenu.IsLarge = true;
            this.tileItemStokMenu.Name = "tileItemStokMenu";
            this.tileItemStokMenu.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(this.tileItemStokMenu_ItemClick);
            // 
            // tileGroup5
            // 
            this.tileGroup5.Items.Add(this.tileItemKullaniciBilgileri);
            this.tileGroup5.Name = "tileGroup5";
            // 
            // tileItemKullaniciBilgileri
            // 
            this.tileItemKullaniciBilgileri.AppearanceItem.Normal.Font = new System.Drawing.Font("Tahoma", 12F);
            this.tileItemKullaniciBilgileri.AppearanceItem.Normal.Options.UseFont = true;
            this.tileItemKullaniciBilgileri.BackgroundImage = global::FirmaTakip.WinUI.Properties.Resources.User_Folder;
            this.tileItemKullaniciBilgileri.BackgroundImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleRight;
            this.tileItemKullaniciBilgileri.BackgroundImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.ZoomInside;
            tileItemElement3.Text = "Kullanıcı Bilgileri";
            tileItemElement3.TextLocation = new System.Drawing.Point(0, 0);
            this.tileItemKullaniciBilgileri.Elements.Add(tileItemElement3);
            this.tileItemKullaniciBilgileri.Id = 5;
            this.tileItemKullaniciBilgileri.IsLarge = true;
            this.tileItemKullaniciBilgileri.Name = "tileItemKullaniciBilgileri";
            this.tileItemKullaniciBilgileri.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(this.tileItemKullaniciBilgileri_ItemClick);
            // 
            // Giris
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1230, 642);
            this.Controls.Add(this.tileControl1);
            this.Name = "Giris";
            this.Text = "Giris";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Giris_FormClosing);
            this.Load += new System.EventHandler(this.Giris_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TileControl tileControl1;
        private DevExpress.XtraEditors.TileGroup tileGroup2;
        private DevExpress.XtraEditors.TileItem tileItemBakimMenusu;
        private DevExpress.XtraEditors.TileGroup tileGroup5;
        private DevExpress.XtraEditors.TileItem tileItemKullaniciBilgileri;
        private DevExpress.XtraEditors.TileItem tileItemStokMenu;
    }
}