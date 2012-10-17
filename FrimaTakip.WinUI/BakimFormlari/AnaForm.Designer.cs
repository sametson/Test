namespace FirmaTakip
{
    partial class AnaForm
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
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraEditors.TileItemElement tileItemElement4 = new DevExpress.XtraEditors.TileItemElement();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnaForm));
            DevExpress.XtraEditors.TileItemElement tileItemElement1 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement2 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement3 = new DevExpress.XtraEditors.TileItemElement();
            this.tileControl1 = new DevExpress.XtraEditors.TileControl();
            this.tileGroup2 = new DevExpress.XtraEditors.TileGroup();
            this.tileGroup1 = new DevExpress.XtraEditors.TileGroup();
            this.tileItemBakimRapolari = new DevExpress.XtraEditors.TileItem();
            this.tileGroup3 = new DevExpress.XtraEditors.TileGroup();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.tileItemTezgahlar = new DevExpress.XtraEditors.TileItem();
            this.tileItemFirmalar = new DevExpress.XtraEditors.TileItem();
            this.tileItemBakimlar = new DevExpress.XtraEditors.TileItem();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // tileControl1
            // 
            this.tileControl1.AllowDrag = false;
            this.tileControl1.AllowDrop = true;
            this.tileControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tileControl1.Groups.Add(this.tileGroup2);
            this.tileControl1.Groups.Add(this.tileGroup1);
            this.tileControl1.Location = new System.Drawing.Point(0, 0);
            this.tileControl1.MaxId = 6;
            this.tileControl1.Name = "tileControl1";
            this.tileControl1.Size = new System.Drawing.Size(1085, 611);
            this.tileControl1.TabIndex = 0;
            this.tileControl1.Text = "Giriş Menüsü";
            this.tileControl1.VerticalContentAlignment = DevExpress.Utils.VertAlignment.Center;
            // 
            // tileGroup2
            // 
            this.tileGroup2.Items.Add(this.tileItemTezgahlar);
            this.tileGroup2.Items.Add(this.tileItemFirmalar);
            this.tileGroup2.Name = "tileGroup2";
            // 
            // tileGroup1
            // 
            this.tileGroup1.Items.Add(this.tileItemBakimlar);
            this.tileGroup1.Items.Add(this.tileItemBakimRapolari);
            this.tileGroup1.Name = "tileGroup1";
            // 
            // tileItemBakimRapolari
            // 
            this.tileItemBakimRapolari.AppearanceItem.Normal.Font = new System.Drawing.Font("Tahoma", 12F);
            this.tileItemBakimRapolari.AppearanceItem.Normal.Options.UseFont = true;
            this.tileItemBakimRapolari.BackgroundImage = global::FirmaTakip.WinUI.Properties.Resources.report;
            this.tileItemBakimRapolari.BackgroundImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            this.tileItemBakimRapolari.BackgroundImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.ZoomInside;
            tileItemElement4.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Bottom;
            tileItemElement4.Text = "Bakım Raporları";
            tileItemElement4.TextLocation = new System.Drawing.Point(0, 0);
            this.tileItemBakimRapolari.Elements.Add(tileItemElement4);
            this.tileItemBakimRapolari.Id = 4;
            this.tileItemBakimRapolari.IsLarge = true;
            this.tileItemBakimRapolari.Name = "tileItemBakimRapolari";
            this.tileItemBakimRapolari.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(this.tileItemBakimRapolari_ItemClick);
            // 
            // tileGroup3
            // 
            this.tileGroup3.Name = "tileGroup3";
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageSize = new System.Drawing.Size(60, 60);
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            // 
            // tileItemTezgahlar
            // 
            this.tileItemTezgahlar.AppearanceItem.Normal.Font = new System.Drawing.Font("Tahoma", 12F);
            this.tileItemTezgahlar.AppearanceItem.Normal.Options.UseFont = true;
            this.tileItemTezgahlar.BackgroundImage = global::FirmaTakip.WinUI.Properties.Resources.Tezgah;
            this.tileItemTezgahlar.BackgroundImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleRight;
            this.tileItemTezgahlar.BackgroundImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.ZoomInside;
            tileItemElement1.Text = "Tezgahlar";
            tileItemElement1.TextLocation = new System.Drawing.Point(0, 0);
            this.tileItemTezgahlar.Elements.Add(tileItemElement1);
            this.tileItemTezgahlar.Id = 0;
            this.tileItemTezgahlar.IsLarge = true;
            this.tileItemTezgahlar.Name = "tileItemTezgahlar";
            this.tileItemTezgahlar.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(this.tileItemTezgahlar_ItemClick);
            // 
            // tileItemFirmalar
            // 
            this.tileItemFirmalar.AppearanceItem.Normal.Font = new System.Drawing.Font("Tahoma", 12F);
            this.tileItemFirmalar.AppearanceItem.Normal.Options.UseFont = true;
            this.tileItemFirmalar.BackgroundImage = global::FirmaTakip.WinUI.Properties.Resources.company;
            this.tileItemFirmalar.BackgroundImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            this.tileItemFirmalar.BackgroundImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.ZoomInside;
            tileItemElement2.Text = "Firmalar";
            tileItemElement2.TextLocation = new System.Drawing.Point(0, 0);
            this.tileItemFirmalar.Elements.Add(tileItemElement2);
            this.tileItemFirmalar.Id = 1;
            this.tileItemFirmalar.IsLarge = true;
            this.tileItemFirmalar.Name = "tileItemFirmalar";
            this.tileItemFirmalar.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(this.tileItemFirmalar_ItemClick);
            // 
            // tileItemBakimlar
            // 
            this.tileItemBakimlar.AppearanceItem.Normal.Font = new System.Drawing.Font("Tahoma", 12F);
            this.tileItemBakimlar.AppearanceItem.Normal.Options.UseFont = true;
            this.tileItemBakimlar.BackgroundImage = global::FirmaTakip.WinUI.Properties.Resources.bakimlar1;
            this.tileItemBakimlar.BackgroundImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            this.tileItemBakimlar.BackgroundImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.ZoomInside;
            tileItemElement3.Text = "Bakımlar";
            tileItemElement3.TextLocation = new System.Drawing.Point(0, 0);
            this.tileItemBakimlar.Elements.Add(tileItemElement3);
            this.tileItemBakimlar.Id = 2;
            this.tileItemBakimlar.IsLarge = true;
            this.tileItemBakimlar.Name = "tileItemBakimlar";
            this.tileItemBakimlar.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(this.tileItemBakimlar_ItemClick);
            // 
            // AnaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1085, 611);
            this.Controls.Add(this.tileControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AnaForm";
            this.Text = "Menü";
            this.Load += new System.EventHandler(this.AnaForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TileControl tileControl1;
        private DevExpress.XtraEditors.TileGroup tileGroup2;
        private DevExpress.XtraEditors.TileItem tileItemTezgahlar;
        private DevExpress.XtraEditors.TileGroup tileGroup3;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraEditors.TileItem tileItemFirmalar;
        private DevExpress.XtraEditors.TileGroup tileGroup1;
        private DevExpress.XtraEditors.TileItem tileItemBakimlar;
        private DevExpress.XtraEditors.TileItem tileItemBakimRapolari;
    }
}