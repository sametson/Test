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
    public partial class Malzemeler : DevExpress.XtraEditors.XtraForm
    {
        Stok.HamMalzeme seciliHamMalzeme = null;

        Stok.IslenmisMalzeme seciliIslenmisMalzeme = null;

        Stok.Sarj seciliSarjNo = null;

        private static Malzemeler _malzemeler;

        static Malzemeler()
        {
            Stok.Services.HamMalzeme.IslemYapildi += new EventHandler(HamMalzeme_IslemYapildi);
        }

        static void HamMalzeme_IslemYapildi(object sender, EventArgs e)
        {
            _malzemeler.cmbHamMalzemeDoldur();
        }

        public Malzemeler()
        {
            InitializeComponent();
            _malzemeler = this;
        }

        #region Diğer Eventlar
        private void Malzemeler_Load(object sender, EventArgs e)
        {
            cmbHamMalzemeBirimDoldur();
            cmbIslenmisMalzemeBirimDoldur();
            cmbHamMalzemeDoldur();
        }

        private void layoutControlGroupHamMalzemeler_Shown(object sender, EventArgs e)
        {
            Stok.MalzemeTabSecimi.seciliTab = Stok.MalzemeTabSecimi.Tab.HamMalzeme;
            GridDoldur();
        }

        private void layoutControlGroupIslenmisMalzemeler_Shown(object sender, EventArgs e)
        {
            Stok.MalzemeTabSecimi.seciliTab = Stok.MalzemeTabSecimi.Tab.IslenmisMalzeme;
            GridDoldur();
        }

        private void layoutControlGroupSarjNumaralari_Shown(object sender, EventArgs e)
        {
            Stok.MalzemeTabSecimi.seciliTab = Stok.MalzemeTabSecimi.Tab.SarjNumaralari;
            GridDoldur();
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            switch (Stok.MalzemeTabSecimi.seciliTab)
            {
                case FirmaTakip.WinUI.Stok.MalzemeTabSecimi.Tab.HamMalzeme:
                    BilgiDoldurHamMalzeme(Convert.ToInt32(gridView1.GetFocusedRowCellValue("Id")));
                    break;
                case FirmaTakip.WinUI.Stok.MalzemeTabSecimi.Tab.SarjNumaralari:
                    BilgiDoldurSarjNo(Convert.ToInt32(gridView1.GetFocusedRowCellValue("Id")));
                    break;
                case FirmaTakip.WinUI.Stok.MalzemeTabSecimi.Tab.IslenmisMalzeme:
                    BilgiDoldurIslenmisMalzeme(Convert.ToInt32(gridView1.GetFocusedRowCellValue("Id")));
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Grid Doldurma Metotları
        private void GridDoldur()
        {
            switch (Stok.MalzemeTabSecimi.seciliTab)
            {
                case FirmaTakip.WinUI.Stok.MalzemeTabSecimi.Tab.HamMalzeme:
                    GridHamMalzeme();
                    break;

                case FirmaTakip.WinUI.Stok.MalzemeTabSecimi.Tab.SarjNumaralari:
                    GridSarjNumaralari();
                    break;

                case FirmaTakip.WinUI.Stok.MalzemeTabSecimi.Tab.IslenmisMalzeme:
                    GridIslenmisMalzeme();
                    break;

                default:
                    break;
            }
        }

        private void GridHamMalzeme()
        {
            gridView1.Columns.Clear();
            gridControl1.DataSource = new Stok.Services.HamMalzeme().GetAllbyStatusforGrid(true);
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[1].Caption = "Malzeme Adı";
            gridView1.Columns[2].Caption = "Malzeme No";

        }

        private void GridIslenmisMalzeme()
        {
            gridView1.Columns.Clear();
            gridControl1.DataSource = new Stok.Services.IslenmisMalzeme().GetAllbyStatusforGrid(true);
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[1].Caption = "İşlenmiş Malzeme Adı";
            gridView1.Columns[2].Caption = "İşlenmiş Malzeme No";
            gridView1.Columns[3].Caption = "Ham Malzeme Adı";
            gridView1.Columns[4].Caption = "Ham Malzeme No";
            gridView1.Columns[6].Caption = "Kat Sayı";

        }

        private void GridSarjNumaralari()
        {
            gridView1.Columns.Clear();
            gridControl1.DataSource = new Stok.Services.Sarj().GetAllforGrid();
            gridView1.Columns[0].Visible = false;
          

        }
        #endregion

        #region ComboBox Doldur
        private void cmbHamMalzemeBirimDoldur()
        {
            DevExpress.XtraEditors.Controls.LookUpColumnInfo kolon1 = new DevExpress.XtraEditors.Controls.LookUpColumnInfo();
            kolon1.Caption = "Birimi";
            kolon1.FieldName = "Adi";
            cmbHamMalzemeBirim.Properties.DataSource = null;
            cmbHamMalzemeBirim.Properties.Columns.Clear();
            cmbHamMalzemeBirim.Properties.Columns.Add(kolon1);
            cmbHamMalzemeBirim.Properties.DataSource = new Stok.Services.HamMalzeme().GetAllbyStatus(true);
            cmbHamMalzemeBirim.Properties.DisplayMember = "Adi";
            cmbHamMalzemeBirim.Properties.ValueMember = "Id";
            cmbHamMalzemeBirim.Properties.DataSource = new Stok.Services.Birim().GetAll();
        }

        private void cmbIslenmisMalzemeBirimDoldur()
        {
            DevExpress.XtraEditors.Controls.LookUpColumnInfo kolon1 = new DevExpress.XtraEditors.Controls.LookUpColumnInfo();
            kolon1.Caption = "Birimi";
            kolon1.FieldName = "Adi";
            cmbIslenmisMalzemeBirim.Properties.DataSource = null;
            cmbIslenmisMalzemeBirim.Properties.Columns.Clear();
            cmbIslenmisMalzemeBirim.Properties.Columns.Add(kolon1);
            cmbIslenmisMalzemeBirim.Properties.DataSource = new Stok.Services.HamMalzeme().GetAllbyStatus(true);
            cmbIslenmisMalzemeBirim.Properties.DisplayMember = "Adi";
            cmbIslenmisMalzemeBirim.Properties.ValueMember = "Id";
            cmbIslenmisMalzemeBirim.Properties.DataSource = new Stok.Services.Birim().GetAll();
        }

        private void cmbHamMalzemeDoldur()
        {
            DevExpress.XtraEditors.Controls.LookUpColumnInfo kolon1 = new DevExpress.XtraEditors.Controls.LookUpColumnInfo();
            DevExpress.XtraEditors.Controls.LookUpColumnInfo kolon2 = new DevExpress.XtraEditors.Controls.LookUpColumnInfo();
            kolon2.Caption = "Malzeme Adı";
            kolon2.FieldName = "Adi";
            kolon1.Caption = "Stok Kodu";
            kolon1.FieldName = "No";
            cmbSarjHamMalzeme.Properties.DataSource = null;
            cmbSarjHamMalzeme.Properties.Columns.Clear();
            cmbSarjHamMalzeme.Properties.Columns.Add(kolon1);
            cmbSarjHamMalzeme.Properties.Columns.Add(kolon2);
            cmbSarjHamMalzeme.Properties.DataSource = new Stok.Services.HamMalzeme().GetAllbyStatus(true);
            cmbSarjHamMalzeme.Properties.DisplayMember = "No";
            cmbSarjHamMalzeme.Properties.ValueMember = "Id";



            cmbHamMalzeme.Properties.DataSource = null;
            cmbHamMalzeme.Properties.Columns.Clear();
            cmbHamMalzeme.Properties.Columns.Add(kolon1);
            cmbHamMalzeme.Properties.Columns.Add(kolon2);
            cmbHamMalzeme.Properties.DataSource = new Stok.Services.HamMalzeme().GetAllbyStatus(true);
            cmbHamMalzeme.Properties.DisplayMember = "No";
            cmbHamMalzeme.Properties.ValueMember = "Id";
        }
        #endregion

        #region Bilgileri Doldur
        private void BilgiDoldurIslenmisMalzeme(int islenmisMalzemeId)
        {
            try
            {
                seciliIslenmisMalzeme = new Stok.Services.IslenmisMalzeme().GetbyId(islenmisMalzemeId);
                txtIslenmisMalzemeAdi.Text = seciliIslenmisMalzeme.Adi;
                txtIslenmisMalzemeNo.Text = seciliIslenmisMalzeme.No;
                txtKatSayi.Text = seciliIslenmisMalzeme.KatSayi.ToString();
                cmbIslenmisMalzemeBirim.EditValue = seciliIslenmisMalzeme.BirimId;
                cmbHamMalzeme.EditValue = seciliIslenmisMalzeme.HamMalzemeId;
            }
            catch 
            {
            }
        }

        private void BilgiDoldurHamMalzeme(int hamMalzemeId)
        {
            try
            {
                seciliHamMalzeme = new Stok.Services.HamMalzeme().GetbyId(hamMalzemeId);
                txtHamMalzemeAdi.Text = seciliHamMalzeme.Adi;
                txtHamMalzemeNo.Text = seciliHamMalzeme.No;
                cmbHamMalzemeBirim.EditValue = seciliHamMalzeme.BirimId;
            }
            catch
            {
            }
        }

        private void BilgiDoldurSarjNo(int sarjId)
        {
            try
            {
                seciliSarjNo = new Stok.Services.Sarj().GetbyId(sarjId);
                txtSarjNo.Text = seciliSarjNo.No;
                cmbSarjHamMalzeme.EditValue = seciliSarjNo.HamMalzemeId;
            }
            catch 
            {
            }
        }
        #endregion

        #region İşlem Eventları
        private void btnEkle_Click(object sender, EventArgs e)
        {
            string sonuc = null;
            switch (Stok.MalzemeTabSecimi.seciliTab)
            {
                case FirmaTakip.WinUI.Stok.MalzemeTabSecimi.Tab.HamMalzeme:
                  sonuc= new Stok.Services.HamMalzeme().Kaydet(txtHamMalzemeAdi.Text, txtHamMalzemeNo.Text, Convert.ToInt32(cmbHamMalzemeBirim.EditValue));
                    if (sonuc != null)
                    {
                        MessageBox.Show(this, sonuc, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;

                case FirmaTakip.WinUI.Stok.MalzemeTabSecimi.Tab.SarjNumaralari:
                     sonuc= new Stok.Services.Sarj().Kaydet(txtSarjNo.Text,Convert.ToInt32(cmbSarjHamMalzeme.EditValue));
                    if (sonuc != null)
                    {
                        MessageBox.Show(this, sonuc, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;

                case FirmaTakip.WinUI.Stok.MalzemeTabSecimi.Tab.IslenmisMalzeme:
                    sonuc=new Stok.Services.IslenmisMalzeme().Kaydet(txtIslenmisMalzemeAdi.Text, txtIslenmisMalzemeNo.Text, Convert.ToInt32(cmbHamMalzeme.EditValue), Convert.ToInt32(cmbIslenmisMalzemeBirim.EditValue), Convert.ToDecimal(txtKatSayi.Text));
                    if (sonuc!=null)
                    {
                        MessageBox.Show(this,sonuc,"Hata",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                    break;
                default:
                    break;
            }
            GridDoldur();
        }

        private void btnDegistir_Click(object sender, EventArgs e)
        {
            string sonuc = null;
            switch (Stok.MalzemeTabSecimi.seciliTab)
            {
                case FirmaTakip.WinUI.Stok.MalzemeTabSecimi.Tab.HamMalzeme:
                    if (seciliHamMalzeme != null)
                    {
                        sonuc = new Stok.Services.HamMalzeme().Degistir(seciliHamMalzeme.Id,txtHamMalzemeAdi.Text, txtHamMalzemeNo.Text, Convert.ToInt32(cmbHamMalzemeBirim.EditValue));
                        if (sonuc != null)
                        {
                            MessageBox.Show(this, sonuc, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    break;

                case FirmaTakip.WinUI.Stok.MalzemeTabSecimi.Tab.SarjNumaralari:
                    if (seciliSarjNo != null)
                    {
                        sonuc = new Stok.Services.Sarj().Degistir(seciliSarjNo.Id,txtSarjNo.Text, Convert.ToInt32(cmbSarjHamMalzeme.EditValue));
                        if (sonuc != null)
                        {
                            MessageBox.Show(this, sonuc, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    break;

                case FirmaTakip.WinUI.Stok.MalzemeTabSecimi.Tab.IslenmisMalzeme:
                    if (seciliIslenmisMalzeme!=null)
                    {
                        sonuc = new Stok.Services.IslenmisMalzeme().Degistir(seciliIslenmisMalzeme.Id,txtIslenmisMalzemeAdi.Text, txtIslenmisMalzemeNo.Text, Convert.ToInt32(cmbHamMalzeme.EditValue), Convert.ToInt32(cmbIslenmisMalzemeBirim.EditValue), Convert.ToDecimal(txtKatSayi.Text));
                        if (sonuc != null)
                        {
                            MessageBox.Show(this, sonuc, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    break;
                default:
                    break;
            }
            GridDoldur();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            switch (Stok.MalzemeTabSecimi.seciliTab)
            {
                case FirmaTakip.WinUI.Stok.MalzemeTabSecimi.Tab.HamMalzeme:
                    if (seciliHamMalzeme != null)
                    {
                        new Stok.Services.HamMalzeme().AktifDurumDegistir(false, seciliHamMalzeme.Id);
                    }                    
                    break;

                case FirmaTakip.WinUI.Stok.MalzemeTabSecimi.Tab.IslenmisMalzeme:
                    if (seciliIslenmisMalzeme!=null)
                    {
                        new Stok.Services.IslenmisMalzeme().AktifDurumDegistir(seciliIslenmisMalzeme.Id, false);
                    }
                    break;

                default:
                    break;
            }

            GridDoldur();
        }
        #endregion

    }
}