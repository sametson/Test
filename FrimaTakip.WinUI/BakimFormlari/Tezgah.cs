using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Utils;

namespace FirmaTakip
{
    public partial class Tezgah : DevExpress.XtraEditors.XtraForm
    {
        public Tezgah()
        {
            InitializeComponent();
        }

        private void Tezgah_Load(object sender, EventArgs e)
        {
            TezgahlariOlustur();
        }

        private void btnTezgahEkle_Click(object sender, EventArgs e)
        {

           
                    new Services.Tezgah().Kaydet(null, txtTezgahMarka.Text, txtTezgahModel.Text, txtTezgahSeriNo.Text, txtTezgahYil.Text);
                    new sametson.ControlEdit().LayoutBilgiTemizle(layoutControl1);
                    TezgahlariOlustur();
            
        }

        private void btnTezgahDegistir_Click(object sender, EventArgs e)
        {
           
                    new Services.Tezgah().Kaydet((Guid)(txtTezgahMarka.Tag), txtTezgahMarka.Text, txtTezgahModel.Text, txtTezgahSeriNo.Text, txtTezgahYil.Text);
                    new sametson.ControlEdit().LayoutBilgiTemizle(layoutControl1);
                    TezgahlariOlustur();

               
        }

        private void btnTezgahSil_Click(object sender, EventArgs e)
        {
                    new Services.Tezgah().Sil((Guid)(txtTezgahMarka.Tag));
                    new sametson.ControlEdit().LayoutBilgiTemizle(layoutControl1);
                    TezgahlariOlustur();
        }

        private void TezgahlariOlustur()
        {
            for (int i = 0; i < 30; i++)
            {
                foreach (Control lbl in xtraScrollableControlTezgahlar.Controls)
                {
                    if (lbl is LabelControl)
                    {
                        lbl.Dispose();
                    }
                }
            }
            int x = 20, y = 20;

            foreach (var item in (new FirmaTakip.Services.Tezgah().GetAll()))
            {

                LabelControl lbl = new LabelControl();
                lbl.AllowHtmlString = true;
                lbl.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
                lbl.Appearance.ImageIndex = 0;
                lbl.Appearance.ImageList = imageCollection1;
                lbl.Cursor = Cursors.Hand;
                lbl.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                lbl.Appearance.TextOptions.Trimming = Trimming.None;
                lbl.Appearance.TextOptions.VAlignment = VertAlignment.Bottom;
                lbl.AutoEllipsis = true;
                lbl.AutoSizeMode = LabelAutoSizeMode.None;
                //lbl.HtmlImages = this.imageCollection1;
                lbl.ImageAlignToText = ImageAlignToText.TopCenter;
                lbl.Location = new Point(x, y);
                lbl.Tag = item.Id;
                lbl.Size = new Size(100, 125);
                lbl.TabIndex = 0;
                lbl.Text = item.Marka + "</br>" + item.Model;
                x += lbl.Width + 10;
                xtraScrollableControlTezgahlar.Controls.Add(lbl);
                if (x + 100 > xtraScrollableControlTezgahlar.Width)
                {
                    y += lbl.Height + 10;
                    x = 20;
                }
                lbl.Click += new EventHandler(lbl_Click);


            }
        }

        private void lbl_Click(object sender, EventArgs e)
        {
            LabelControl lbl = (LabelControl)sender;
            var tezgah = new Services.Tezgah().GetById((Guid)(lbl.Tag));
            BilgileriDoldur(tezgah);
        }
        /// <summary>
        /// Tezgah Bilgilerini textboxlara doldurur
        /// </summary>
        /// <param name="tezgah">Bilgileri doldurulacak tezgah</param>
        private void BilgileriDoldur(DTO.TezgahEdit tezgah)
        {
            txtTezgahMarka.Tag = tezgah.Id;
            txtTezgahMarka.Text = tezgah.Marka;
            txtTezgahModel.Text = tezgah.Model;
            txtTezgahSeriNo.Text = tezgah.SeriNo;
            txtTezgahYil.Text = tezgah.Yil;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            TezgahlariOlustur();
        }
    }
}