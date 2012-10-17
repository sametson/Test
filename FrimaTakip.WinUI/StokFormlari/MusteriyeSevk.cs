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
    public partial class MusteriyeSevk : DevExpress.XtraEditors.XtraForm
    {
        private static MusteriyeSevk _formMusteriyeSevk;
        private Stok.HurdaAlani seciliHurdaAlani;
        private Stok.SevkAlani seciliSevkAlani;
        private Stok.SevkIrsaliye seciliIrsaliye;
        private Stok.SevkIrsaliyeDetay seciliSevkIrsaliyeDetay;

        DataTable table = new DataTable();
        DataTable tableCookie;

        static MusteriyeSevk()
        {
            Stok.Services.Musteri.IslemYapildi += new EventHandler(Musteri_IslemYapildi);
        }

        static void Musteri_IslemYapildi(object sender, EventArgs e)
        {
            _formMusteriyeSevk.cmbMusteriDoldur();
        }
        public MusteriyeSevk()
        {
            InitializeComponent();
            _formMusteriyeSevk = this;
        }

        private void MusteriyeSevk_Load(object sender, EventArgs e)
        {
            cmbMusteriDoldur();
            GridIrsaliyeDoldur();
            TablolariOlustur();
        }

        private void GridHurdaDoldur()
        {
            gridViewSevkAlani.Columns.Clear();
            gridControlSevkAlani.DataSource = new Stok.Services.Hurda().GridHurdaAlaniMevcutDurumTablo();
            gridViewSevkAlani.Columns[0].Visible = false;
            gridViewSevkAlani.Columns[7].Caption = "Açıklama";
        }
        private void GridSevkAlaniDoldur()
        {
            gridViewSevkAlani.Columns.Clear();
            gridControlSevkAlani.DataSource = new Stok.Services.SevkAlani().GridSevkAlaniMevcutDurumTablo();
            gridViewSevkAlani.Columns[0].Visible = false;
            gridViewSevkAlani.Columns[6].Caption = "Açıklama";
        }
        private void GridIrsaliyeDoldur()
        {
            gridControlIrsaliye.DataSource = new Stok.Services.SevkIrsaliye().GetAllforGrid();
            gridViewIrsaliye.Columns[0].Visible = false;
            gridViewIrsaliye.Columns[2].Caption = "Firma Adı";
            gridViewIrsaliye.Columns[4].Caption = "Açıklama";
        }

        private void GridSevkYeriDoldur()
        {
            switch (cmbSevkAlani.SelectedIndex)
            {
                case 0:
                    GridHurdaDoldur();
                    break;
                case 1:
                    GridSevkAlaniDoldur();
                    break;
                default:
                    break;
            }
        }

        private void TablolariOlustur()
        {
            table.Columns.Add("Id");
            table.Columns.Add("SevkIrsaliyeId");
            table.Columns.Add("SevkAlaniId");
            table.Columns.Add("HurdaAlaniId");
            table.Columns.Add("MalzemeBilgi");
            table.Columns.Add("SarjNo");
            table.Columns.Add("Miktar");
            table.Columns.Add("Birim");
            table.Columns.Add("MalzemeDurumu");

            gridControlIrsaliyeDetay.DataSource = table;
            gridViewIrsaliyeDetay.Columns[0].Visible = false;
            gridViewIrsaliyeDetay.Columns[1].Visible = false;
            gridViewIrsaliyeDetay.Columns[2].Visible = false;
            gridViewIrsaliyeDetay.Columns[3].Visible = false;

            tableCookie = new DataTable();
            tableCookie.Columns.Add("Id");
            tableCookie.Columns.Add("SevkIrsaliyeId");
            tableCookie.Columns.Add("SevkAlaniId");
            tableCookie.Columns.Add("HurdaAlaniId");
            tableCookie.Columns.Add("MalzemeBilgi");
            tableCookie.Columns.Add("SarjNo");
            tableCookie.Columns.Add("Miktar");
            tableCookie.Columns.Add("Birim");
            tableCookie.Columns.Add("MalzemeDurumu");
        }

        private void cmbMusteriDoldur()
        {
            DevExpress.XtraEditors.Controls.LookUpColumnInfo kolon1 = new DevExpress.XtraEditors.Controls.LookUpColumnInfo();
            kolon1.Caption = "Firmalar";
            kolon1.FieldName = "Adi";
            cmbMusteri.Properties.DataSource = null;
            cmbMusteri.Properties.Columns.Clear();
            cmbMusteri.Properties.Columns.Add(kolon1);
            cmbMusteri.Properties.DataSource = new Stok.Services.Musteri().GetAllbyStatus(true);
            cmbMusteri.Properties.DisplayMember = "Adi";
            cmbMusteri.Properties.ValueMember = "Id";
        }

        private void cmbSevkAlani_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbSevkAlani.SelectedIndex)
            {
                case 0:
                    txtMiktar.Properties.ReadOnly = false;
                    GridHurdaDoldur();
                    break;

                case 1:
                    txtMiktar.Properties.ReadOnly = true;
                    GridSevkAlaniDoldur();
                    break;

                default:
                    break;
            }
        }

        private void cmbMusteri_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                StokFormlari.Musteri frmMusteri = new StokFormlari.Musteri();
                frmMusteri.ShowDialog();
            }
        }


        private void gridViewSevkAlani_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            switch (cmbSevkAlani.SelectedIndex)
            {
                case 0:
                    seciliHurdaAlani = new Stok.Services.Hurda().GetbyId(Convert.ToInt32(gridViewSevkAlani.GetFocusedRowCellValue("Id")));
                    break;

                case 1:
                    seciliSevkAlani = new Stok.Services.SevkAlani().GetbyId(Convert.ToInt32(gridViewSevkAlani.GetFocusedRowCellValue("Id")));
                    break;

                default:
                    seciliSevkAlani = null;
                    seciliHurdaAlani = null;
                    break;
            }
        }

        private void btnIrsaliyeKaydet_Click(object sender, EventArgs e)
        {
            if (dtTarih.EditValue!=null && txtIrsaliyeNo.Text!="")
            {
                int irsaliyeId = new Stok.Services.SevkIrsaliye().Kaydet(txtIrsaliyeNo.Text, Convert.ToInt32(cmbMusteri.EditValue), Convert.ToDateTime(dtTarih.EditValue), txtAciklama.Text);
                if (irsaliyeId == 0)
                {
                    MessageBox.Show(this, "Bu irsaliye numarasına ait sevk mevcut", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    SevkIrsaliyeDetayKaydet(irsaliyeId);
                }

                GridIrsaliyeDoldur(); 
            }
            else
            {
                MessageBox.Show(this, "Gerekli bilgileri girmediniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void SevkIrsaliyeDetayKaydet(int irsaliyeId)
        {
            string[] a;
            for (int i = 0; i < gridViewIrsaliyeDetay.RowCount; i++)
            {
                a = new string[9];
                for (int ii = 0; ii < 9; ii++)
                {
                    a[ii] = gridViewIrsaliyeDetay.GetRowCellValue(i, gridViewIrsaliyeDetay.Columns[ii]).ToString();
                }

                if (a[0] == "0")
                {
                    if (a[8] == "Hurda")
                    {
                        new Stok.Services.SevkIrsaliye().DetayKaydet(irsaliyeId, true, Convert.ToDecimal(a[6]), null, Convert.ToInt32(a[3]));
                    }
                    else
                    {
                        new Stok.Services.SevkIrsaliye().DetayKaydet(irsaliyeId, false, Convert.ToDecimal(a[6]), Convert.ToInt32(a[2]), null);
                    }

                }
                else
                {
                    if (a[8] == "Hurda")
                    {
                        new Stok.Services.SevkIrsaliye().DetayDegistir(Convert.ToInt64(a[0]), irsaliyeId, true, Convert.ToDecimal(a[6]), null, Convert.ToInt32(a[3]));
                    }
                    else
                    {
                        new Stok.Services.SevkIrsaliye().DetayDegistir(Convert.ToInt64(a[0]), irsaliyeId, false, Convert.ToDecimal(a[6]), Convert.ToInt32(a[2]), null);
                    }
                }
            }
            table.Clear();
            new Stok.Services.SevkIrsaliye().DeleteAllCookie();
        }

        private void btnIrsaliyeIcerigeEkle_Click(object sender, EventArgs e)
        {
            switch (cmbSevkAlani.SelectedIndex)
            {
                case 0:
                    if (seciliHurdaAlani != null)
                    {
                        if (Convert.ToInt32(txtMiktar.Text)>=0)
                        {
                            string mesaj = new Stok.Services.SevkIrsaliye().CookieKaydet(true, Convert.ToDecimal(txtMiktar.Text), null, Convert.ToInt32(seciliHurdaAlani.Id));
                            if (mesaj == null)
                            {
                                table.Rows.Add("0", "0", "0", seciliHurdaAlani.Id, seciliHurdaAlani.IslenmisMalzemeDetay.IslenmisMalzeme.No + "/" + seciliHurdaAlani.IslenmisMalzemeDetay.IslenmisMalzeme.Adi, seciliHurdaAlani.IslenmisMalzemeDetay.HamStokDetay.Sarj.No, txtMiktar.Text, seciliHurdaAlani.IslenmisMalzemeDetay.IslenmisMalzeme.Birim.Adi, "Hurda");
                            }
                            else
                            {
                                MessageBox.Show(this, mesaj, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            GridHurdaDoldur(); 
                        }
                        else
                        {
                            MessageBox.Show(this, "Miktarı kontrol ediniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    break;

                case 1:
                    if (seciliSevkAlani != null)
                    {
                        table.Rows.Add("0", "0", seciliSevkAlani.Id, "0", seciliSevkAlani.IslenmisMalzemeDetay.IslenmisMalzeme.No + "/" + seciliSevkAlani.IslenmisMalzemeDetay.IslenmisMalzeme.Adi, seciliSevkAlani.IslenmisMalzemeDetay.HamStokDetay.Sarj.No, seciliSevkAlani.Miktar, seciliSevkAlani.IslenmisMalzemeDetay.IslenmisMalzeme.Birim.Adi, "Mamül");
                        new Stok.Services.SevkIrsaliye().CookieKaydet(false, seciliSevkAlani.Miktar, seciliSevkAlani.Id, null);
                        GridSevkAlaniDoldur();
                    }
                    break;

                default:
                    break;
            }

        }

        private void MusteriyeSevk_FormClosing(object sender, FormClosingEventArgs e)
        {
            new Stok.Services.SevkIrsaliye().GeriAlCookie();
        }

        private void gridViewIrsaliyeDetay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (seciliSevkIrsaliyeDetay != null)
                {
                    new Stok.Services.SevkIrsaliye().DeleteDetayById(seciliSevkIrsaliyeDetay.Id);
                }
                else
                {
                    if (gridViewIrsaliyeDetay.GetFocusedRowCellValue("MalzemeDurumu") == "Hurda")
                    {
                        new Stok.Services.SevkIrsaliye().DeleteCookie(true, Convert.ToDecimal(gridViewIrsaliyeDetay.GetFocusedRowCellValue("Miktar")), null, Convert.ToInt32(gridViewIrsaliyeDetay.GetFocusedRowCellValue("HurdaAlaniId")));
                    }
                    else
                    {
                        new Stok.Services.SevkIrsaliye().DeleteCookie(false, Convert.ToDecimal(gridViewIrsaliyeDetay.GetFocusedRowCellValue("Miktar")), Convert.ToInt32(gridViewIrsaliyeDetay.GetFocusedRowCellValue("SevkAlaniId")), null);
                    }
                }
                gridViewIrsaliyeDetay.DeleteRow(gridViewIrsaliyeDetay.GetSelectedRows()[0]);
                gridControlIrsaliyeDetay.DataSource = table;
                GridSevkYeriDoldur();
            }
        }

        private void gridViewIrsaliyeDetay_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            seciliSevkIrsaliyeDetay = new Stok.Services.SevkIrsaliye().GetDetaybyId(Convert.ToInt64(gridViewIrsaliyeDetay.GetFocusedRowCellValue("Id")));
        }

        private void gridViewIrsaliye_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            seciliIrsaliye = new Stok.Services.SevkIrsaliye().GetbyId(Convert.ToInt32(gridViewIrsaliye.GetFocusedRowCellValue("Id")));
            GridIrsaliyeDetayDoldur();
            if (seciliIrsaliye!=null)
            {

                txtAciklama.Text = seciliIrsaliye.Aciklama;
                txtIrsaliyeNo.Text = seciliIrsaliye.IrsaliyeNo;
                dtTarih.EditValue = seciliIrsaliye.Tarih;
                cmbMusteri.EditValue = seciliIrsaliye.MusteriId; 
            }
        }

        private void GridIrsaliyeDetayDoldur()
        {
            try
            {
                new Stok.Services.SevkIrsaliye().GeriAlCookie();
                var result = new Stok.Services.SevkIrsaliye().GetDetayByIrsaliyeId(seciliIrsaliye.Id);
                table.Clear();
                foreach (var item in result)
                {
                    table.Rows.Add(item.Id, item.SevkIrsaliyeId, item.SevkAlaniId, item.HurdaAlaniId, item.MalzemeBilgi, item.SarjNo, item.Miktar, item.Birim, item.MalzemeDurumu);
                }
            }
            catch 
            {
                
            }
        }

        private void btnIrsaliyeGuncelle_Click(object sender, EventArgs e)
        {
            if (seciliIrsaliye != null)
            {
                int irsaliyeId = new Stok.Services.SevkIrsaliye().Degistir(seciliIrsaliye.Id, txtIrsaliyeNo.Text, Convert.ToInt32(cmbMusteri.EditValue), Convert.ToDateTime(dtTarih.EditValue), txtAciklama.Text);
                if (irsaliyeId == 0)
                {
                    MessageBox.Show(this, "Bu irsaliye numarasına ait sevk mevcut", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    SevkIrsaliyeDetayKaydet(irsaliyeId);
                  
                }

                GridIrsaliyeDoldur();
            }
            else
            {
                MessageBox.Show(this, "Güncellemek istediğiniz irsaliyeyi seçmediniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridControlIrsaliye_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (seciliIrsaliye!=null)
                {
                    DialogResult silinsinMi = MessageBox.Show(seciliIrsaliye.IrsaliyeNo + " numaralı irsaliyeyi silmek istediğinizden eminmisiniz ?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (silinsinMi == DialogResult.Yes)
                    {
                        if (new Stok.Services.SevkIrsaliye().Sil(seciliIrsaliye.Id) == 0)
                        {
                            MessageBox.Show(this, " Önce silmek istediğiniz irsaliyenin içeriğini siliniz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    GridIrsaliyeDoldur(); 
                }
                else
                {
                    MessageBox.Show(this,"İrsaliye seçmediniz","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            new Stok.Services.SevkIrsaliye().GeriAlCookie();
            table.Clear();
            txtAciklama.Text = "";
            txtIrsaliyeNo.Text = "";
            dtTarih.Text = "";
        }

    }
}