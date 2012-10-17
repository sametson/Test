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
    public partial class Musteri : DevExpress.XtraEditors.XtraForm
    {
        private Stok.Musteri seciliMusteri;
        public Musteri()
        {
            InitializeComponent();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            new Stok.Services.Musteri().Kaydet(txtFirmaAdi.Text);
            GridDoldur();
        }

        private void btnDegistir_Click(object sender, EventArgs e)
        {
            if (seciliMusteri != null) 
            {
                new Stok.Services.Musteri().Degistir(seciliMusteri.Id,txtFirmaAdi.Text);
            }
            GridDoldur();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (seciliMusteri != null)
            {
                new Stok.Services.Musteri().Sil(seciliMusteri.Id);
            }
            GridDoldur();
        }

        private void Musteri_Load(object sender, EventArgs e)
        {
            GridDoldur();
        }
        private void GridDoldur()
        {
            gridControl1.DataSource = new Stok.Services.Musteri().GetAllbyStatus(true);
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[1].Caption = "Firma Adı";
            gridView1.Columns[2].Visible = false;
            gridView1.Columns[3].Visible = false;
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            seciliMusteri=new Stok.Services.Musteri().GetbyId(Convert.ToInt32(gridView1.GetFocusedRowCellValue("Id")));
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}