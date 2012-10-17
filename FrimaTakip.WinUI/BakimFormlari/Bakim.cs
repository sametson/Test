using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Metro.Navigation;

namespace FirmaTakip.Formlar
{
    public partial class Bakim : DevExpress.XtraEditors.XtraForm
    {
        private static Bakim _formBakim;
        static Bakim()
        {
            Services.Tezgah.KayitYapildi += new EventHandler(Tezgah_KayitYapildi);
            Services.Firma.IslemYapildi += new EventHandler(Firma_IslemYapildi);
        }

        static void Firma_IslemYapildi(object sender, EventArgs e)
        {
            _formBakim.cmbFirmaDoldur();
        }

        static  void  Tezgah_KayitYapildi(object sender, EventArgs e)
        {
            _formBakim.cmbTezgahDoldur();            
        }

        
        public Bakim()
        {
            InitializeComponent();
            _formBakim = this;
        }

        private Guid seciliBakimID;

        private void Bakim_Load(object sender, EventArgs e)
        {
            cmbFirmaDoldur();
            cmbTezgahDoldur();
            TabloDoldur();
        }

        private void TabloDoldur()
        {
            gridView1.Columns.Clear();
            gridControl1.DataSource = new Services.Bakim().GetAllforTablo();
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[3].AppearanceCell.ForeColor = Color.Red;
        }

        private void cmbFirma_EditValueChanged(object sender, EventArgs e)
        {
            var firma = new Services.Firma().GetById((Guid)cmbFirma.EditValue);
            txtFirmaAdres.Text = firma.Adres;
            txtFirmaCep.Text = firma.Cep;
            txtFirmaFaks.Text = firma.Faks;
            txtFirmaTel.Text = firma.Tel;
        }

        private void cmbTezgah_EditValueChanged(object sender, EventArgs e)
        {
            var tezgah = new Services.Tezgah().GetById((Guid)cmbTezgah.EditValue);
            txtTezgahModel.Text = tezgah.Model;
            txtTezgahSeriNo.Text = tezgah.SeriNo;
            txtTezgahYil.Text = tezgah.Yil;
        }

        void cmbFirmaDoldur()
        {
            DevExpress.XtraEditors.Controls.LookUpColumnInfo kolon1 = new DevExpress.XtraEditors.Controls.LookUpColumnInfo();
            kolon1.Caption = "Firmalar";
            kolon1.FieldName = "Adi";
            cmbFirma.Properties.DataSource = null;
            cmbFirma.Properties.Columns.Clear();
            cmbFirma.Properties.Columns.Add(kolon1);


            cmbFirma.Properties.DataSource = new Services.Firma().GetAll();
            cmbFirma.Properties.DisplayMember = "Adi";
            cmbFirma.Properties.ValueMember = "Id";
        }

        public void cmbTezgahDoldur()
        {
            DevExpress.XtraEditors.Controls.LookUpColumnInfo kolon1 = new DevExpress.XtraEditors.Controls.LookUpColumnInfo();
            kolon1.Caption = "Marka";
            kolon1.FieldName = "Marka";
            DevExpress.XtraEditors.Controls.LookUpColumnInfo kolon2 = new DevExpress.XtraEditors.Controls.LookUpColumnInfo();
            kolon2.Caption = "Model";
            kolon2.FieldName = "Model";
            DevExpress.XtraEditors.Controls.LookUpColumnInfo kolon3 = new DevExpress.XtraEditors.Controls.LookUpColumnInfo();
            kolon3.Caption = "Seri No";
            kolon3.FieldName = "SeriNo";
            cmbTezgah.Properties.DataSource = null;
            cmbTezgah.Properties.Columns.Clear();
            cmbTezgah.Properties.Columns.Add(kolon1);
            cmbTezgah.Properties.Columns.Add(kolon2);
            cmbTezgah.Properties.Columns.Add(kolon3);


            cmbTezgah.Properties.DataSource = new Services.Tezgah().GetAll();
            cmbTezgah.Properties.DisplayMember = "Marka";
            cmbTezgah.Properties.ValueMember = "Id";
        }

        private void btnBakimEkle_Click(object sender, EventArgs e)
        {

            new Services.Bakim().Kaydet(null, txtServisRaporNo.Text, txtArizaAciklama.Text, Convert.ToDateTime(dtTarih.EditValue), Convert.ToDecimal(txtBakimTutar.Text), (Guid)cmbTezgah.EditValue, (Guid)cmbFirma.EditValue);
            TabloDoldur();

        }

        private void btnBakimDegistir_Click(object sender, EventArgs e)
        {

            new Services.Bakim().Kaydet(seciliBakimID, txtServisRaporNo.Text, txtArizaAciklama.Text, Convert.ToDateTime(dtTarih.EditValue), Convert.ToDecimal(txtBakimTutar.Text), (Guid)cmbTezgah.EditValue, (Guid)cmbFirma.EditValue);
            TabloDoldur();
        }

        private void btnBakimSil_Click(object sender, EventArgs e)
        {

            new Services.Bakim().Sil(seciliBakimID);
            TabloDoldur();
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                seciliBakimID = (Guid)(gridView1.GetFocusedRowCellValue("Id"));
                BilgileriDoldur();
            }
            catch
            {

            }

        }

        private void BilgileriDoldur()
        {
            DTO.BakimEdit bakim = new Services.Bakim().GetById(seciliBakimID);
            cmbFirma.EditValue = bakim.FirmaId;
            cmbTezgah.EditValue = bakim.TezgahId;
            txtBakimTutar.Text = bakim.Tutar.ToString();
            txtArizaAciklama.Text = bakim.Ariza;
            txtServisRaporNo.Text = bakim.ServisRaporNo;
            dtTarih.EditValue = bakim.Tarih;
        }


    }
}