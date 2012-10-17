using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace FirmaTakip.Formlar
{
    public partial class Firma : DevExpress.XtraEditors.XtraForm
    {
        public Firma()
        {
            InitializeComponent();
        }

        private void Firma_Load(object sender, EventArgs e)
        {
            TabloyuDoldur();
        }

        private void TabloyuDoldur()
        {
            gridView1.Columns.Clear();
            gridControl1.DataSource = new Services.Firma().GetAllforTablo();
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[1].Caption = "Adı";

        }

        private void btnFirmaEkle_Click(object sender, EventArgs e)
        {

            new Services.Firma().Kaydet(null, txtFirmaAdi.Text, txtFirmaTel.Text, txtFirmaFaks.Text, txtFirmaCep.Text, txtFirmaAdres.Text);
            new sametson.ControlEdit().LayoutBilgiTemizle(layoutControl1);
            TabloyuDoldur();

        }

        private void btnFirmaDegistir_Click(object sender, EventArgs e)
        {
            new Services.Firma().Kaydet((Guid)(txtFirmaAdi.Tag), txtFirmaAdi.Text, txtFirmaTel.Text, txtFirmaFaks.Text, txtFirmaCep.Text, txtFirmaAdres.Text);
            new sametson.ControlEdit().LayoutBilgiTemizle(layoutControl1);
            TabloyuDoldur();
        }


        private void btnFirmaSil_Click(object sender, EventArgs e)
        {
            new Services.Firma().Sil((Guid)(txtFirmaAdi.Tag));
            new sametson.ControlEdit().LayoutBilgiTemizle(layoutControl1);
            TabloyuDoldur();
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            Guid firmaID = (Guid)(gridView1.GetFocusedRowCellValue("Id"));
            var firma = new Services.Firma().GetById(firmaID);
            BilgileriDoldur(firma);
        }

        private void BilgileriDoldur(DTO.FirmaEdit firma)
        {
            txtFirmaAdi.Tag = firma.Id;
            txtFirmaAdi.Text = firma.Adi;
            txtFirmaAdres.Text = firma.Adres;
            txtFirmaCep.Text = firma.Cep;
            txtFirmaFaks.Text = firma.Faks;
            txtFirmaTel.Text = firma.Tel;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            TabloyuDoldur();
        }

    }
}