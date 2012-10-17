using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace FirmaTakip.WinUI.StokFormlari
{
    public partial class HamMalzemeSevk : DevExpress.XtraEditors.XtraForm
    {
        private static StokFormlari.HamMalzemeSevk _formImalatSevk;
        private Stok.IslenmisMalzeme seciliIslenmisMalzeme = null;
        private Stok.HamStokDetay seciliHamStokDetay = null;
        private Stok.IslenmisMalzemeDetay seciliIslenmisMalzemeDetay = null;

        static HamMalzemeSevk()
        {
            Stok.Services.IslenmisMalzeme.IslemYapildi += new EventHandler(IslenmisMalzeme_IslemYapildi);
            Stok.Services.Hurda.IslemYapildi += new EventHandler(Hurda_IslemYapildi);
            Stok.Services.SevkAlani.IslemYapildi += new EventHandler(SevkAlani_IslemYapildi);
        }

        static void SevkAlani_IslemYapildi(object sender, EventArgs e)
        {
            _formImalatSevk.GridIslenmisMalzemeDetayDoldur();
        }

        static void Hurda_IslemYapildi(object sender, EventArgs e)
        {
            _formImalatSevk.GridIslenmisMalzemeDetayDoldur();
        }

        static void IslenmisMalzeme_IslemYapildi(object sender, EventArgs e)
        {
            _formImalatSevk.GridIslenmisMalzemelerDoldur();
        }

        public HamMalzemeSevk()
        {
            InitializeComponent();
            _formImalatSevk = this;
        }

        private void ImalatSevk_Load(object sender, EventArgs e)
        {
            GridIslenmisMalzemelerDoldur();
        }

        private void GridIslenmisMalzemelerDoldur()
        {
            gridControlIslenmisMalzemeler.DataSource = new Stok.Services.IslenmisMalzeme().GetAllbyStatusforImalatGrid(true);
            gridViewIslenmisMalzemeler.Columns[0].Visible = false;
            gridViewIslenmisMalzemeler.Columns[2].Caption = "Referans Ham Malzeme";
        }

        private void GridIslenmisMalzemeDetayDoldur()
        {
            if (seciliIslenmisMalzeme != null)
            {
                gridControlIslenmisMalzemeDetay.DataSource = new Stok.Services.IslenmisMalzeme().GetAllDetayforImalatDetayGrid();
                gridViewIslenmisMalzemeDetay.Columns[0].Visible = false;
                gridViewIslenmisMalzemeDetay.Columns[1].Caption = "İşlenmiş Malzeme";
            }
        }

        private void GridHamStokDetayDoldur()
        {
            if (seciliIslenmisMalzeme != null)
            {
                gridControlHamStokDetay.DataSource = new Stok.Services.Sarj().GetAllforImalatSarjGrid(seciliIslenmisMalzeme.HamMalzemeId, seciliIslenmisMalzeme.Id);
                gridViewHamStokDetay.Columns[8].Visible = false;
                gridViewHamStokDetay.Columns[4].Caption = "Birim";
                gridViewHamStokDetay.Columns[5].Caption = "Çıkan Adet";
                gridViewHamStokDetay.Columns[6].Caption = "Birim";
            }
        }

        private void gridViewIslenmisMalzemeler_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            seciliIslenmisMalzeme = new Stok.Services.IslenmisMalzeme().GetbyId(Convert.ToInt32(gridViewIslenmisMalzemeler.GetFocusedRowCellValue("Id")));
            GridHamStokDetayDoldur();
            GridIslenmisMalzemeDetayDoldur();
        }

        private void gridViewHamStokDetay_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            seciliHamStokDetay = new Stok.Services.HamStok().GetHamStokDetaybyHamStokDetayId(Convert.ToInt32(gridViewHamStokDetay.GetFocusedRowCellValue("Id")));
        }

        private void gridViewIslenmisMalzemeDetay_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            seciliIslenmisMalzemeDetay = new Stok.Services.IslenmisMalzeme().GetDetaybyIslenmisMalzemeDetayId(Convert.ToInt32(gridViewIslenmisMalzemeDetay.GetFocusedRowCellValue("Id")));

        }

        private void btnSevkEt_Click(object sender, EventArgs e)
        {
            string mesaj = null;
            if (seciliHamStokDetay != null && dtTarih.EditValue!=null && txtMiktar.Text!="")
            {
                if (Convert.ToInt32(txtMiktar.Text)>0)
                {
                    mesaj = new Stok.Services.HamStok().MiktarAyarla(seciliHamStokDetay.Id, null, seciliIslenmisMalzeme.KatSayi, Convert.ToDecimal(txtMiktar.Text), true);
                    if (mesaj == null)
                    {
                        new Stok.Services.IslenmisMalzeme().DetayKaydet(seciliIslenmisMalzeme.Id, seciliHamStokDetay.Id, Convert.ToDecimal(txtMiktar.Text), Convert.ToDateTime(dtTarih.EditValue));
                        GridHamStokDetayDoldur();
                        GridIslenmisMalzemeDetayDoldur();
                    }
                    else
                    {
                        MessageBox.Show(this, mesaj, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    } 
                }
                else
                {
                    MessageBox.Show(this, "Miktarı kontrol ediniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(this, "Önce stoktan malzeme seçiniz, miktar ve tarihi giriniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            dtTarih.EditValue = null;
            dtTarih.Text = "";
            txtMiktar.Text = null;

        }

        private void gridControlIslenmisMalzemeDetay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (seciliIslenmisMalzeme != null && seciliIslenmisMalzemeDetay != null)
                {
                    string mesaj = new Stok.Services.HamStok().MiktarAyarla(seciliIslenmisMalzemeDetay.HamStokDetayId,seciliIslenmisMalzemeDetay.SevkMiktar,seciliIslenmisMalzeme.KatSayi,seciliIslenmisMalzemeDetay.Miktar, false);
                    if (mesaj == null)
                    {
                        new Stok.Services.IslenmisMalzeme().DeleteDetay(seciliIslenmisMalzemeDetay.Id);
                    }
                    else
                    {
                        MessageBox.Show(this,mesaj,"Hata",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }

                    seciliIslenmisMalzemeDetay = null;
                    GridHamStokDetayDoldur();
                    GridIslenmisMalzemeDetayDoldur();
                }
            }
        }

        private void dtTarih_EditValueChanged(object sender, EventArgs e)
        {
            if (seciliIslenmisMalzemeDetay != null)
            {
                new Stok.Services.IslenmisMalzeme().DetayTarihDegistir(seciliIslenmisMalzemeDetay.Id, Convert.ToDateTime(dtTarih.EditValue));
                GridIslenmisMalzemeDetayDoldur();
            }
        }


    }
}