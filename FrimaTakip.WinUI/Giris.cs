using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using sametson;
using FirmaTakip.Services;
using FirmaTakip.Formlar;
using DevExpress.Metro.Navigation;

namespace FirmaTakip.WinUI
{
    public partial class Giris : DevExpress.XtraEditors.XtraForm
    {
        private static Giris _formGiris;

        static Giris()
        {
            AnaForm.formBakimAc += new EventHandler(AnaForm_FormBakimAc);
            AnaForm.formRaporAc += new EventHandler(AnaForm_formRaporAc);
            AnaForm.formFirmaAc += new EventHandler(AnaForm_formFirmaAc);
            AnaForm.formTezgahAc += new EventHandler(AnaForm_formTezgahAc);
            StokMenu.formHamStokAc += new EventHandler(StokMenu_formHamStokAc);
            StokMenu.formMalzemeAc += new EventHandler(StokMenu_formMalzemeAc);
            StokMenu.formImalatAc += new EventHandler(StokMenu_formImalatAc);
            StokMenu.fromIslenmisMalzemeSevkAc += new EventHandler(StokMenu_fromIslenmisMalzemeSevk);
            StokMenu.formMusteriSevkAc += new EventHandler(StokMenu_formMusteriSevkAc);
            StokMenu.formStokRaporAc += new EventHandler(StokMenu_formStokRaporAc);
        }

        static void StokMenu_formStokRaporAc(object sender, EventArgs e)
        {
            _formGiris.GoTo<StokFormlari.RaporFormlari.HamMalzemeRaporAna>();
        }

        static void StokMenu_formMusteriSevkAc(object sender, EventArgs e)
        {
            _formGiris.GoTo<StokFormlari.MusteriyeSevk>();
        }

        static void StokMenu_fromIslenmisMalzemeSevk(object sender, EventArgs e)
        {
            _formGiris.GoTo<StokFormlari.IslenmisMalzemeSevk>();
        }


        static void StokMenu_formImalatAc(object sender, EventArgs e)
        {
            _formGiris.GoTo<StokFormlari.HamMalzemeSevk>();
        }

        static void StokMenu_formMalzemeAc(object sender, EventArgs e)
        {
            _formGiris.GoTo<StokFormlari.Malzemeler>();
        }

        static void StokMenu_formHamStokAc(object sender, EventArgs e)
        {
            _formGiris.GoTo<HamStok>();
        }

        #region Bakim Eventları
        static void AnaForm_formTezgahAc(object sender, EventArgs e)
        {
            _formGiris.GoTo<Tezgah>();
        }
        static void AnaForm_formFirmaAc(object sender, EventArgs e)
        {
            _formGiris.GoTo<Formlar.Firma>();
        }
        static void AnaForm_formRaporAc(object sender, EventArgs e)
        {
            _formGiris.GoTo<Formlar.BakimRaporlari>();
        }
        static void AnaForm_FormBakimAc(object sender, EventArgs e)
        {

            _formGiris.GoTo<Formlar.Bakim>();

        }
        #endregion


        public Giris()
        {
            InitializeComponent();
            _formGiris = this;
        }

        private void Giris_Load(object sender, EventArgs e)
        {
            FormAyar.Boyut(this);
            Services.Admin.OpenConnection();
        }

        private void tileItemKullaniciBilgileri_ItemClick(object sender, TileItemEventArgs e)
        {
            this.GoTo<KullaniciBilgileri>();
        }

        private void tileItemBakimMenusu_ItemClick(object sender, TileItemEventArgs e)
        {
            this.GoTo<AnaForm>();
                        
        }

        private void tileItemStokMenu_ItemClick(object sender, TileItemEventArgs e)
        {
            this.GoTo<StokMenu>();
        }

        private void Giris_FormClosing(object sender, FormClosingEventArgs e)
        {
            new Stok.Services.SevkIrsaliye().GeriAlCookie();
            Services.Admin.CloseConnection();
        }
    }
}