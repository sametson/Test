using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using sametson;
using DevExpress.XtraEditors;
using DevExpress.Metro.Navigation;
using FirmaTakip.Formlar;

namespace FirmaTakip
{
    public partial class AnaForm : DevExpress.XtraEditors.XtraForm
    {
        public static event EventHandler formBakimAc;
        public static event EventHandler formTezgahAc;
        public static event EventHandler formRaporAc;
        public static event EventHandler formFirmaAc;

        public AnaForm()
        {
            InitializeComponent();
        }

        private void AnaForm_Load(object sender, EventArgs e)
        {
            FormAyar.Boyut(this);
        }

        private void tileItemTezgahlar_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {            
            if (formTezgahAc != null)
                formTezgahAc(null, null);            
        }

        private void tileItemFirmalar_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            if (formFirmaAc != null)
                formFirmaAc(null, null);
        }

        private void tileItemBakimlar_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            if (formBakimAc != null)
                formBakimAc(null, null);
        }

        private void tileItemExit_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            Application.Exit();
        }

        private void tileItemBakimRapolari_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            if (formRaporAc != null)
                formRaporAc(null, null);
        }

        private void tileItemKullaniciBilgileri_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            this.GoTo<KullaniciBilgileri>();
        }
    }
}