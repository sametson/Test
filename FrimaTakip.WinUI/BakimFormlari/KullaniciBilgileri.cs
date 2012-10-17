using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;

namespace FirmaTakip.Formlar
{
    public partial class KullaniciBilgileri : DevExpress.XtraEditors.XtraForm
    {
        public KullaniciBilgileri()
        {
            InitializeComponent();
        }

        public void KullaniciBilgileri_Load(object sender, EventArgs e)
        {
            Stream s = null;
            try
            {
                s = File.Open(string.Format("{0}\\logo.jpg", Application.StartupPath), FileMode.Open);
            }
            catch
            {

                MessageBox.Show("Logo seçmeniz gerekmektedir...\rResim penceresine çift tıklayınız.");
            }
            if (s != null)
            {

                Image temp = Image.FromStream(s);
                s.Close();
                pictureEdit1.Image = temp;
                temp = null;
            }
            BilgileriDoldur();
        }

        private void BilgileriDoldur()
        {
            var kullanici = new Services.Kullanici().Get();
            if (kullanici != null)
            {
                txtFirmaAdi.Text = kullanici.Adi;
                txtFirmaAdres.Text = kullanici.Adres;
                txtFirmaEmail.Text = kullanici.Email;
                txtFirmaFaks.Text = kullanici.Faks;
                txtFirmaTel.Text = kullanici.Tel;

            }
        }

        private void pictureEdit1_MouseClick(object sender, MouseEventArgs e)
        {
            pictureEdit1.Image = null;
            pictureEdit1.LoadImage();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {

            pictureEdit1.Image.Save(string.Format("{0}\\logo.jpg", Application.StartupPath), System.Drawing.Imaging.ImageFormat.Jpeg);
            new Services.Kullanici().Kaydet(txtFirmaAdi.Text, txtFirmaTel.Text, txtFirmaFaks.Text, txtFirmaEmail.Text, txtFirmaAdres.Text);
        }
    }
}