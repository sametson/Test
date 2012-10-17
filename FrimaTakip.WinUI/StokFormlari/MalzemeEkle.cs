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
    public partial class Malzeme : DevExpress.XtraEditors.XtraForm
    {
        public Malzeme()
        {
            InitializeComponent();
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (txtStokKodu.Text == "" || txtMalzemeAdi.Text == "" || cmbBirim.EditValue==null)
            {
                MessageBox.Show(this, "Eksik Bilgi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                new Stok.Services.HamMalzeme().Kaydet(txtMalzemeAdi.Text, txtStokKodu.Text, Convert.ToInt32(cmbBirim.EditValue));
                
                this.Close();
            }
        }

        private void Malzeme_Load(object sender, EventArgs e)
        {
            cmbBirimDoldur();
        }

        private void cmbBirimDoldur()
        {
            DevExpress.XtraEditors.Controls.LookUpColumnInfo kolon1 = new DevExpress.XtraEditors.Controls.LookUpColumnInfo();
            kolon1.Caption = "Birimi";
            kolon1.FieldName = "Adi";
            cmbBirim.Properties.DataSource = null;
            cmbBirim.Properties.Columns.Clear();
            cmbBirim.Properties.Columns.Add(kolon1);
            cmbBirim.Properties.DataSource = new Stok.Services.HamMalzeme().GetAllbyStatus(true);
            cmbBirim.Properties.DisplayMember = "Adi";
            cmbBirim.Properties.ValueMember = "Id";
            cmbBirim.Properties.DataSource = new Stok.Services.Birim().GetAll();
        }
    }
}