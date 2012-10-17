using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;

namespace FirmaTakip.WinUI.StokFormlari.RaporFormlari
{
    public partial class HamMalzemeRaporAna : DevExpress.XtraEditors.XtraForm
    {
        public enum DataGridDurum
        {
            Genel,
            IslenenMalzemeBilgisi,
            HamStokBilgisi,
            SevkAlaniBilgisi,
            HurdaAlaniBilgisi
        }

        private DataGridDurum gridDurum;
        private static StokFormlari.RaporFormlari.HamMalzemeRaporAna _hamMalzemeRaporAna;
        static HamMalzemeRaporAna()
        {
            
        }
        public HamMalzemeRaporAna()
        {
            InitializeComponent();
        }
        int seciliHamMalzemeId = 0;

        private void HamMalzemeRaporAna_Load(object sender, EventArgs e)
        {
            GridGenelDoldur();
        }
        private void GridGenelDoldur()
        {
            gridView1.Columns.Clear();
            gridControl1.DataSource = new Stok.Services.HamStokRapor().Genel();
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[1].Caption = "Malzeme Adı";
            gridView1.Columns[2].Caption = "Malzeme No";
            gridView1.Columns[3].Caption = "Ham Miktarı";
            gridView1.Columns[4].Caption = "Üretimdeki Miktarı";
            gridView1.Columns[5].Caption = "Sevk Alanındaki Miktarı";
            gridView1.Columns[6].Caption = "Hurda Alanındaki Miktar";
            gridView1.Columns[7].Caption = "Birim";
            gridDurum = DataGridDurum.Genel;
        }

        private void GridIslenenMalzemePartiDoldur(int hamMalzemeId)
        {
            gridView1.Columns.Clear();
            gridControl1.DataSource = new Stok.Services.HamStokRapor().IslenenMalzemelerinBilgisi(hamMalzemeId);
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[1].Visible = false;
            gridView1.Columns[3].Caption = "Ambara Giriş Tarihi";
            gridView1.Columns[4].Caption = "Üretime Sevk Tarihi";
            gridView1.Columns[5].Caption = "Üretimdeki Miktar";
            gridView1.Columns[6].Caption = "Birim";
            gridDurum = DataGridDurum.IslenenMalzemeBilgisi;
        }

        private void GridHamStokPartiDoldur(int hamMalzemeId)
        {
            gridView1.Columns.Clear();
            gridControl1.DataSource = new Stok.Services.HamStokRapor().HamStokPartiBilgisi(hamMalzemeId);
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[1].Visible = false;
            gridView1.Columns[3].Caption = "Ambara Giriş Tarihi";
            gridView1.Columns[4].Caption = "Gelen Miktar";
            gridView1.Columns[5].Caption = "Kalan Miktar";
            gridView1.Columns[6].Caption = "Birim";
            gridDurum = DataGridDurum.HamStokBilgisi;
        }

        private void GridSevkAlaniPartiDoldur(int hamMalzemeId)
        {
            gridView1.Columns.Clear();
            gridControl1.DataSource = new Stok.Services.HamStokRapor().SevkAlaniPartiBilgisi(hamMalzemeId);
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[1].Visible = false;
            gridView1.Columns[3].Caption = "Ambara Giriş Tarihi";
            gridView1.Columns[4].Caption = "Sevk Alanına Sevk Tarihi";
            gridView1.Columns[5].Caption = "Sevk Alanındaki Miktar";
            gridView1.Columns[6].Caption = "Birim";
            gridDurum = DataGridDurum.SevkAlaniBilgisi;
        }

        private void GridHurdaAlaniPartiDoldur(int hamMalzemeId)
        {
            gridView1.Columns.Clear();
            gridControl1.DataSource = new Stok.Services.HamStokRapor().HurdaAlaniPartiBilgisi(hamMalzemeId);
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[1].Visible = false;
            gridView1.Columns[3].Caption = "Ambara Giriş Tarihi";
            gridView1.Columns[4].Caption = "Hurda Alanına Sevk Tarihi";
            gridView1.Columns[5].Caption = "Hurda Alanındaki Miktar";
            gridView1.Columns[6].Caption = "Birim";
            gridDurum = DataGridDurum.HurdaAlaniBilgisi;
        }

        private void btnGenel_Click(object sender, EventArgs e)
        {
            GridGenelDoldur();
            lblSeciliMalzeme.Text = "";
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (gridDurum == DataGridDurum.Genel)
            {
                seciliHamMalzemeId = Convert.ToInt32(gridView1.GetFocusedRowCellValue("HamMalzemeId"));
                lblSeciliMalzeme.Text = gridView1.GetFocusedRowCellValue("HamMalzemeAdi") + " (" + gridView1.GetFocusedRowCellValue("HamMalzemeNo") + ")";
            }
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            if (seciliHamMalzemeId != 0)
            {
                switch (radioGroup1.SelectedIndex)
                {
                    case 0:
                        GridIslenenMalzemePartiDoldur(seciliHamMalzemeId);
                        break;
                    case 1:
                        GridHamStokPartiDoldur(seciliHamMalzemeId);
                        break;
                    case 2:
                        GridSevkAlaniPartiDoldur(seciliHamMalzemeId);
                        break;
                    case 3:
                        GridHurdaAlaniPartiDoldur(seciliHamMalzemeId);
                        break;

                    default:
                        break;
                }
            }
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (seciliHamMalzemeId != 0)
            {
                switch (radioGroup1.SelectedIndex)
                {
                    case 0:
                        GridIslenenMalzemePartiDoldur(seciliHamMalzemeId);
                        break;
                    case 1:
                        GridHamStokPartiDoldur(seciliHamMalzemeId);
                        break;
                    case 2:
                        GridSevkAlaniPartiDoldur(seciliHamMalzemeId);
                        break;
                    case 3:
                        GridHurdaAlaniPartiDoldur(seciliHamMalzemeId);
                        break;

                    default:
                        break;
                }
            }


        }
    }
}