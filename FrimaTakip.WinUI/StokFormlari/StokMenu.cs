using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace FirmaTakip.WinUI
{
    public partial class StokMenu : DevExpress.XtraEditors.XtraForm
    {

        public static event EventHandler formHamStokAc;
        public static event EventHandler formMalzemeAc;
        public static event EventHandler formImalatAc;
        public static event EventHandler fromIslenmisMalzemeSevkAc;
        public static event EventHandler formMusteriSevkAc;
        public static event EventHandler formStokRaporAc;

        public StokMenu()
        {
            InitializeComponent();
        }

        private void tileItemHamStok_ItemClick(object sender, TileItemEventArgs e)
        {
            if (formHamStokAc != null)
                formHamStokAc(null, null);   
        }

        private void tileItemMalzemeler_ItemClick(object sender, TileItemEventArgs e)
        {
            if (formMalzemeAc!=null)
                formMalzemeAc(null, null);
        }

        private void tileItemImalatSevk_ItemClick(object sender, TileItemEventArgs e)
        {
            if (formImalatAc != null)
                formImalatAc(null, null);
        }

        private void tileItemIslenmisMalzemeSevk_ItemClick(object sender, TileItemEventArgs e)
        {
            if (fromIslenmisMalzemeSevkAc != null)
                fromIslenmisMalzemeSevkAc(null, null);
        }

        private void tileItemMusteriyeSevk_ItemClick(object sender, TileItemEventArgs e)
        {
            if (formMusteriSevkAc!=null)
                formMusteriSevkAc(null, null);
        }

        private void tileItemStokRaporlari_ItemClick(object sender, TileItemEventArgs e)
        {
            if (formStokRaporAc != null)
                formStokRaporAc(null, null);
        }

   

    }
}