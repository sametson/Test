using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using DevExpress.XtraGrid.Views.Grid;

namespace FirmaTakip.WinUI
{
    public partial class HamStok : DevExpress.XtraEditors.XtraForm
    {
        private static HamStok _formHamStok;
        Stok.HamStok seciliHamStok = null;
        Stok.HamStokDetay seciliHamStokDetay = null;

        static HamStok()
        {
            Stok.Services.HamMalzeme.IslemYapildi += new EventHandler(HamMalzeme_IslemYapildi);
            Stok.Services.Sarj.IslemYapildi += new EventHandler(Sarj_IslemYapildi);
        }

        static void Sarj_IslemYapildi(object sender, EventArgs e)
        {
            if (_formHamStok.cmbMalzeme.EditValue != null)
            {
                _formHamStok.cmbSarjDoldur((int)_formHamStok.cmbMalzeme.EditValue);

            }
        }

        static void HamMalzeme_IslemYapildi(object sender, EventArgs e)
        {
            _formHamStok.cmbMalzemeDoldur();
        }

        public HamStok()
        {
            InitializeComponent();
            _formHamStok = this;
        }

        private void HamStok_Load(object sender, EventArgs e)
        {
            GridHamStokDoldur();
            cmbMalzemeDoldur();
            GridDoldur();
        }

        #region Combobox İşlemleri
        void cmbMalzemeDoldur()
        {
            DevExpress.XtraEditors.Controls.LookUpColumnInfo kolon1 = new DevExpress.XtraEditors.Controls.LookUpColumnInfo();
            DevExpress.XtraEditors.Controls.LookUpColumnInfo kolon2 = new DevExpress.XtraEditors.Controls.LookUpColumnInfo();
            kolon2.Caption = "Malzeme Adı";
            kolon2.FieldName = "Adi";
            kolon1.Caption = "Stok Kodu";
            kolon1.FieldName = "No";
            cmbMalzeme.Properties.DataSource = null;
            cmbMalzeme.Properties.Columns.Clear();
            cmbMalzeme.Properties.Columns.Add(kolon1);
            cmbMalzeme.Properties.Columns.Add(kolon2);
            cmbMalzeme.Properties.DataSource = new Stok.Services.HamMalzeme().GetAllbyStatus(true);
            cmbMalzeme.Properties.DisplayMember = "No";
            cmbMalzeme.Properties.ValueMember = "Id";
        }

        void cmbSarjDoldur(int malzemeId)
        {
            DevExpress.XtraEditors.Controls.LookUpColumnInfo kolon1 = new DevExpress.XtraEditors.Controls.LookUpColumnInfo();
            kolon1.Caption = "Sarj Kodu";
            kolon1.FieldName = "No";
            cmbSarj.Properties.DataSource = null;
            cmbSarj.Properties.Columns.Clear();
            cmbSarj.Properties.Columns.Add(kolon1);
            cmbSarj.Properties.DataSource = new Stok.Services.Sarj().GetAllbyMalzemeId(malzemeId);
            cmbSarj.Properties.DisplayMember = "No";
            cmbSarj.Properties.ValueMember = "Id";
        }

        private void cmbMalzeme_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Malzeme frm = new Malzeme();
                frm.ShowDialog();
            }
        }

        private void cmbSarj_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F2)
            {
                if (cmbMalzeme.EditValue != null)
                {
                    Stok.HamMalzeme hamMalzeme = new Stok.Services.HamMalzeme().GetbyId((int)cmbMalzeme.EditValue);
                    StokFormlari.SarjEkle frm = new StokFormlari.SarjEkle(hamMalzeme);
                    frm.ShowDialog();
                }
            }
        }

        private void cmbMalzeme_EditValueChanged(object sender, EventArgs e)
        {
            Stok.HamMalzeme hamMalzeme = new Stok.Services.HamMalzeme().GetbyId((int)cmbMalzeme.EditValue);
            txtMalzemeAdi.Text = hamMalzeme.Adi;
            txtMalzemeBirim.Text = hamMalzeme.Birim.Adi;
            cmbSarjDoldur(hamMalzeme.Id);
        }
        #endregion

        DataTable table = new DataTable();

        DataTable tableCookie;

        #region Buton İşlemleri
        private void btnStokKaydet_Click(object sender, EventArgs e)
        {
            int hamStokId = new Stok.Services.HamStok().HamStokKaydet(Convert.ToDateTime(dtTarih.EditValue), txtIrsaliyeNo.Text, txtAciklama.Text);

            if (hamStokId == 0)
            {
                MessageBox.Show(this, "Bu irsaliye numarası daha önce kaydedilmiş", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                HamStokDetayKaydet(hamStokId);
            }
            GridHamStokDoldur();
        }

        private void HamStokDetayKaydet(int hamStokId)
        {
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                string[] a = new string[6];
                for (int ii = 0; ii < 6; ii++)
                {
                    a[ii] = gridView1.GetRowCellValue(i, gridView1.Columns[ii]).ToString();
                }
                new Stok.Services.HamStok().HamStokDetayKaydet(Convert.ToInt32(a[5]), Convert.ToInt32(a[1]), hamStokId, Convert.ToDecimal(a[4]));
            }

            new sametson.ControlEdit().LayoutBilgiTemizle(layoutControl2);
            table.Clear();
            gridControl2.DataSource = new Stok.Services.HamStok().GetAllbyStatus(true);
        }

        private void btnStokGuncelle_Click(object sender, EventArgs e)
        {
            if (seciliHamStok != null)
            {
                int hamStokId = new Stok.Services.HamStok().HamStokGuncelle(Convert.ToDateTime(dtTarih.EditValue), txtIrsaliyeNo.Text, txtAciklama.Text, seciliHamStok.Id);

                if (hamStokId == 0)
                {
                    MessageBox.Show(this, "Bu irsaliye numarası daha önce kaydedilmiş", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    HamStokDetayKaydet(seciliHamStok.Id);
                }

                GridHamStokDoldur();
                new sametson.ControlEdit().LayoutBilgiTemizle(layoutControl2);
            }
        }

        private void btnStokSil_Click(object sender, EventArgs e)
        {
            if (seciliHamStok != null)
            {
                DialogResult result = MessageBox.Show(this, "Silmek istediğinizden emin misiniz ?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    string mesaj = new Stok.Services.HamStok().DeleteHamStok(seciliHamStok.Id);

                    if (mesaj != null)
                    {
                        MessageBox.Show(this, mesaj, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        GridHamStokDoldur();
                        new sametson.ControlEdit().LayoutBilgiTemizle(layoutControl2);
                    }

                }
            }
            else
            {
                MessageBox.Show(this, "Seçim yapmadınız !", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void btnListeEkle_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtBirimMiktar.Text) >= 0)
            {
                table.Rows.Add(cmbMalzeme.EditValue, cmbSarj.EditValue, txtMalzemeAdi.Text + "/" + cmbMalzeme.Text, cmbSarj.Text, txtBirimMiktar.Text, "0");
            }
            else
            {
                MessageBox.Show(this, "Birim miktarı kontrol ediniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHamStokDetayGuncelle_Click(object sender, EventArgs e)
        {
            if (seciliHamStokDetay != null)
            {
                new Stok.Services.HamStok().HamStokDetayKaydet(seciliHamStokDetay.Id, Convert.ToInt32(cmbSarj.EditValue), seciliHamStok.Id, Convert.ToDecimal(txtBirimMiktar.Text));
                GridHamStokDetayDoldur();
                seciliHamStokDetay = null;
            }
            else
            {
                MessageBox.Show("Kaydedilmemiş veriyi değiştiremezsiniz");
            }
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            new sametson.ControlEdit().LayoutBilgiTemizle(layoutControl1);
            new sametson.ControlEdit().LayoutBilgiTemizle(layoutControl2);
            table.Clear();
        }
        #endregion

        private void GridHamStokDoldur()
        {
            gridControl2.DataSource = new Stok.Services.HamStok().GetAllbyStatus(true);
            gridView2.Columns[3].Caption = "Açıklama";
            gridView2.Columns[0].Visible = false;
            gridView2.Columns[4].Visible = false;
            gridView2.Columns[5].Visible = false;
        }

        private void GridDoldur()
        {
            table.Columns.Add("MalzemeId");
            table.Columns.Add("SarjId");
            table.Columns.Add("Malzeme");
            table.Columns.Add("Şarj No");
            table.Columns.Add("Birim Miktar");
            table.Columns.Add("Id");

            gridControl1.DataSource = table;
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[1].Visible = false;
            gridView1.Columns[5].Visible = false;

            tableCookie = new DataTable();
            tableCookie.Columns.Add("MalzemeId");
            tableCookie.Columns.Add("SarjId");
            tableCookie.Columns.Add("Malzeme");
            tableCookie.Columns.Add("Şarj No");
            tableCookie.Columns.Add("Birim Miktar");
            tableCookie.Columns.Add("Id");
        }

        private void gridControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {

                if (seciliHamStokDetay != null)
                {
                    if (new Stok.Services.IslenmisMalzeme().GetDetaybyHamStokDetayId(seciliHamStokDetay.Id) != null)
                    {
                        MessageBox.Show(this, "Silmek istediğiniz malzemeden imalat yapılmış", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show(this, "Silmek istediğinizden emin misiniz ?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            new Stok.Services.HamStok().DeleteHamStokDetaybyId(seciliHamStokDetay.Id);
                            seciliHamStokDetay = null;

                        }
                    }
                }

                gridView1.DeleteRow(gridView1.GetSelectedRows()[0]);
                tableCookie.Clear();



                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    string[] a = new string[6];
                    for (int ii = 0; ii < 6; ii++)
                    {
                        a[ii] = gridView1.GetRowCellValue(i, gridView1.Columns[ii]).ToString();
                    }
                    tableCookie.Rows.Add(a[0], a[1], a[2], a[3], a[4], a[5]);
                }

                table.Clear();

                for (int i = 0; i < tableCookie.Rows.Count; i++)
                {
                    table.Rows.Add(tableCookie.Rows[i][0].ToString(), tableCookie.Rows[i][1].ToString(), tableCookie.Rows[i][2].ToString(), tableCookie.Rows[i][3].ToString(), tableCookie.Rows[i][4].ToString(), tableCookie.Rows[i][5].ToString());
                }
                gridControl1.DataSource = table;

            }
        }

        private void gridView2_RowClick(object sender, RowClickEventArgs e)
        {
            int hamStokId = Convert.ToInt32(gridView2.GetFocusedRowCellValue("Id"));

            seciliHamStok = new Stok.Services.HamStok().GetbyId(hamStokId);
            txtAciklama.Text = seciliHamStok.Aciklama;
            txtIrsaliyeNo.Text = seciliHamStok.IrsaliyeNo;
            dtTarih.EditValue = seciliHamStok.Tarih;
            GridHamStokDetayDoldur();

        }

        private void GridHamStokDetayDoldur()
        {
            var hamStokDetayList = new Stok.Services.HamStok().GetHamStokDetayByHamStokId(seciliHamStok.Id);
            table.Clear();
            foreach (Stok.HamStokDetay item in hamStokDetayList)
            {
                table.Rows.Add(item.Sarj.HamMalzemeId, item.SarjId, item.Sarj.HamMalzeme.Adi + "/" + item.Sarj.HamMalzeme.No, item.Sarj.No, item.Miktar, item.Id);
            }
            gridControl1.DataSource = table;
        }

        private void gridView1_RowClick(object sender, RowClickEventArgs e)
        {
            int hamStokDetayId = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Id"));
            seciliHamStokDetay = new Stok.Services.HamStok().GetHamStokDetaybyHamStokDetayId(hamStokDetayId);
            if (seciliHamStokDetay != null)
            {
                cmbMalzeme.EditValue = seciliHamStokDetay.Sarj.HamMalzemeId;
                cmbSarj.EditValue = seciliHamStokDetay.SarjId;
                txtBirimMiktar.Text = seciliHamStokDetay.Miktar.ToString();
            }
            else
            {
                cmbMalzeme.EditValue = new Stok.Services.Sarj().GetbyId(Convert.ToInt32(gridView1.GetFocusedRowCellValue("SarjId"))).HamMalzemeId;
                cmbSarj.EditValue = Convert.ToInt32(gridView1.GetFocusedRowCellValue("SarjId"));
                txtBirimMiktar.Text = gridView1.GetFocusedRowCellValue("Birim Miktar").ToString();
            }
        }

        private void gridView2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                StokFormlari.RaporFormlari.HamMalzemeRaporAna frm = new StokFormlari.RaporFormlari.HamMalzemeRaporAna();
                frm.Show();
            }
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}