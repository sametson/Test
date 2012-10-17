using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Xml;

namespace FirmaTakip.WinUI.StokFormlari
{
    public partial class IslenmisMalzemeSevk : DevExpress.XtraEditors.XtraForm
    {

        private static IslenmisMalzemeSevk _fromIslenmisMalzemeSevk;
        private Stok.IslenmisMalzemeDetay seciliIslenmisMalzemeDetay;
        private Stok.SevkAlani seciliSevkAlani;
        private Stok.HurdaAlani seciliHurdaAlani;
        static IslenmisMalzemeSevk()
        {
            Stok.Services.IslenmisMalzeme.IslemYapildi += new EventHandler(IslenmisMalzeme_IslemYapildi);
        }

        static void IslenmisMalzeme_IslemYapildi(object sender, EventArgs e)
        {
            _fromIslenmisMalzemeSevk.GridIslenmisMalzemeDoldur();
        }

        public IslenmisMalzemeSevk()
        {
            InitializeComponent();
            _fromIslenmisMalzemeSevk = this;
        }

        private void IslenmisMalzemeSevk_Load(object sender, EventArgs e)
        {
            GridIslenmisMalzemeDoldur();
        }

        private void GridIslenmisMalzemeDoldur()
        {
            gridControlIslenmisMalzeme.DataSource = new Stok.Services.IslenmisMalzeme().GetAllDetayforSevkAlaniGrid();
            gridViewIslenmisMalzeme.Columns[0].Visible = false;
            gridViewIslenmisMalzeme.Columns[1].Caption = "İşlenmiş Malzeme";
        }
        private void GridHurdaDoldur()
        {
            gridViewSevkYeri.Columns.Clear();
            gridControlSevkYeri.DataSource = new Stok.Services.Hurda().GridHurdaAlaniMevcutDurumTablo();
            gridViewSevkYeri.Columns[0].Visible = false;
            gridViewSevkYeri.Columns[7].Caption = "Açıklama";
        }
        private void GridSevkAlaniDoldur()
        {
            gridViewSevkYeri.Columns.Clear();
            gridControlSevkYeri.DataSource = new Stok.Services.SevkAlani().GridSevkAlaniMevcutDurumTablo();
            gridViewSevkYeri.Columns[0].Visible = false;
            gridViewSevkYeri.Columns[7].Caption = "Açıklama";
        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbSevkYeri.SelectedIndex)
            {
                case 0:
                    groupControlHurdaAlani.Visible = true;
                    groupControlSevkAlani.Visible = false;
                    groupControlGridSevkYeri.Text = "Sevk Yeri : Hurda Alanı";
                    GridHurdaDoldur();
                    break;
                case 1:
                    groupControlSevkAlani.Visible = true;
                    groupControlHurdaAlani.Visible = false;
                    groupControlGridSevkYeri.Text = "Sevk Yeri : Sevk Alanı";
                    GridSevkAlaniDoldur();

                    break;
                default:
                    groupControlHurdaAlani.Visible = false;
                    groupControlSevkAlani.Visible = false;
                    break;
            }
        }

        private void btnSevkEt_Click(object sender, EventArgs e)
        {
            if (seciliIslenmisMalzemeDetay != null && dtTarih.EditValue!=null)
            {
                switch (cmbSevkYeri.SelectedIndex)
                {
                    case 0:
                        if (Convert.ToInt32(txtHurdaMiktar.Text)>=0)
                        {
                            if (txtHurdaMiktar.Text != "" && new Stok.Services.Hurda().Kaydet(seciliIslenmisMalzemeDetay.Id, Convert.ToDecimal(txtHurdaMiktar.Text), txtHurdaAciklama.Text, Convert.ToDateTime(dtTarih.EditValue)) != null)
                            {
                                MessageBox.Show(this, "Miktarı tekrar gözden geçiriniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            GridHurdaDoldur(); 
                        }
                        else
                        {
                            MessageBox.Show(this, "Miktarı tekrar gözden geçiriniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;

                    case 1:
                        if (Convert.ToInt32(txtSevkAlaniMiktar.Text)>=0)
                        {
                            if (txtSevkAlaniMiktar.Text != "" && new Stok.Services.SevkAlani().Kaydet(seciliIslenmisMalzemeDetay.Id, Convert.ToDecimal(txtSevkAlaniMiktar.Text), txtSevkAlanAciklama.Text, txtKasaNo.Text, Convert.ToDateTime(dtTarih.EditValue)) != null)
                            {
                                MessageBox.Show(this, "Miktarı tekrar gözden geçiriniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            GridSevkAlaniDoldur(); 
                        }
                        else
                        {
                            MessageBox.Show(this, "Miktarı tekrar gözden geçiriniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;
                    default:
                        break;
                }
                GridIslenmisMalzemeDoldur();
            }
            else
            {
                MessageBox.Show(this, "Gerekli bilgileri giriniz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void gridViewIslenmisMalzeme_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            seciliIslenmisMalzemeDetay = new Stok.Services.IslenmisMalzeme().GetDetaybyIslenmisMalzemeDetayId(Convert.ToInt32(gridViewIslenmisMalzeme.GetFocusedRowCellValue("Id")));
        }

        private void gridViewSevkYeri_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            switch (cmbSevkYeri.SelectedIndex)
            {
                case 0:
                    seciliHurdaAlani = new Stok.Services.Hurda().GetbyId(Convert.ToInt32(gridViewSevkYeri.GetFocusedRowCellValue("Id")));
                    break;

                case 1:
                    seciliSevkAlani = new Stok.Services.SevkAlani().GetbyId(Convert.ToInt32(gridViewSevkYeri.GetFocusedRowCellValue("Id")));
                    break;
                default:
                    break;
            }
        }

        private void gridControlSevkYeri_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (seciliHurdaAlani != null || seciliSevkAlani != null)
                {
                    switch (cmbSevkYeri.SelectedIndex)
                    {
                        case 0: new Stok.Services.Hurda().Delete(seciliHurdaAlani.Id);
                            GridHurdaDoldur();
                            GridIslenmisMalzemeDoldur();
                            seciliHurdaAlani = null;
                            break;

                        case 1: new Stok.Services.SevkAlani().Delete(seciliSevkAlani.Id);
                            GridSevkAlaniDoldur();
                            GridIslenmisMalzemeDoldur();
                            seciliSevkAlani = null;
                            break;

                        default:
                            MessageBox.Show(this, "Seçiminizi yapmadınız", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                    }
                }
            }
        }

        private void dtTarih_EditValueChanged(object sender, EventArgs e)
        {
            //if (seciliHurdaAlani != null || seciliSevkAlani != null)
            //{
            //    switch (cmbSevkYeri.SelectedIndex)
            //    {
            //        case 0:
            //            if (seciliHurdaAlani!=null)
            //            {
            //                new Stok.Services.Hurda().TarihDegistir(seciliHurdaAlani.Id, Convert.ToDateTime(dtTarih.EditValue));
            //                GridHurdaDoldur();
            //                GridIslenmisMalzemeDoldur(); 
            //            }
            //            break;

            //        case 1: 
            //            if (seciliSevkAlani!=null)
            //            {
            //                new Stok.Services.SevkAlani().TarihDegistir(seciliSevkAlani.Id, Convert.ToDateTime(dtTarih.EditValue));
            //                GridSevkAlaniDoldur();
            //                GridIslenmisMalzemeDoldur(); 
            //            }
            //            break;

            //        default:
            //            MessageBox.Show(this, "Seçiminizi yapmadınız", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //            break;
            //    }
            //}
        }

    }
}