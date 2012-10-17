using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Xpo.DB.Helpers;
using DevExpress.XtraCharts;
using DevExpress.Metro.Navigation;

namespace FirmaTakip.Formlar
{
    public partial class BakimRaporlari : DevExpress.XtraEditors.XtraForm
    {
        private static BakimRaporlari _formBakimRaporlari;
        static BakimRaporlari()
        {
            Services.Bakim.IslemYapildi += new EventHandler(Bakim_IslemYapildi);
        }

        static void Bakim_IslemYapildi(object sender, EventArgs e)
        {
            _formBakimRaporlari.cmbTezgahDoldur();
            _formBakimRaporlari.cmbYillarDoldur();
        }

        public BakimRaporlari()
        {
            InitializeComponent();
            _formBakimRaporlari = this;
        }

        private void BakimRaporlari_Load(object sender, EventArgs e)
        {
            cmbYillarDoldur();
            cmbTezgahDoldur();
            radioGroup1.EditValue = "Yıl";
        }

        private void cmbYillarDoldur()
        {
            var kolon1 = new DevExpress.XtraEditors.Controls.LookUpColumnInfo();
            kolon1.Caption = "Yıllar";
            kolon1.FieldName = "Yil";
            cmbBakimYili.Properties.DataSource = null;
            cmbBakimYili.Properties.Columns.Clear();
            cmbBakimYili.Properties.Columns.Add(kolon1);
            cmbBakimYili.Properties.DataSource = new Services.Raporlama().GetBakimYillari();
            cmbBakimYili.Properties.DisplayMember = "Yil";
            cmbBakimYili.Properties.ValueMember = "Yil";
        }

        private void cmbBakimYili_EditValueChanged(object sender, EventArgs e)
        {
            chartControl1.Series[0].ArgumentDataMember = "TezgahBilgisi";
            chartControl1.Series[0].ArgumentScaleType = ScaleType.Qualitative;
            chartControl1.Series[0].ValueDataMembers[0] = "Tutar";
            chartControl1.Series[0].ValueScaleType = ScaleType.Numerical;
            chartControl1.Titles[0].Text = cmbBakimYili.EditValue.ToString() + "  yılına ait bakımın tezgahlara dağılımı";
            chartControl1.Series[0].DataSource = new Services.Raporlama().YillikTezgahBakimPastasi((int)cmbBakimYili.EditValue);
            gridView1.Columns.Clear();
            gridControl1.DataSource = new Services.Raporlama().YillikTezgahBakimPastasi((int)cmbBakimYili.EditValue);
        }

        private void cmbTezgah_EditValueChanged(object sender, EventArgs e)
        {
            chartControl2.Series[0].ArgumentDataMember = "Yil";
            chartControl2.Series[0].ArgumentScaleType = ScaleType.Qualitative;
            chartControl2.Series[0].ValueDataMembers[0] = "Tutar";
            chartControl2.Series[0].ValueScaleType = ScaleType.Numerical;
            chartControl2.Series[0].DataSource = new Services.Raporlama().RaporTezgahGenel((Guid)cmbTezgah.EditValue);
            chartControl2.Series[0].Name = new Services.Tezgah().GetByIdBilgi((Guid)cmbTezgah.EditValue).TezgahBilgisi;
            chartControl2.Titles[0].Text = new Services.Tezgah().GetByIdBilgi((Guid)cmbTezgah.EditValue).TezgahBilgisi + "  tezgahına ait bakımın yıllara göre dağılımı";
            gridView1.Columns.Clear();
            gridControl1.DataSource = new Services.Raporlama().RaporTezgahGenel((Guid)cmbTezgah.EditValue);
        }

        private void chartControl1_MouseClick(object sender, MouseEventArgs e)
        {
            if (radioGroup1.EditValue == "Yıl")
            {
                ChartHitInfo hi = chartControl1.CalcHitInfo(e.X, e.Y);
                SeriesPoint pnt = hi.SeriesPoint;

                if (pnt != null)
                {
                    sametson.Degiskenler.TezgahId = Convert.ToInt64(pnt.Argument.Split('-')[0]);
                    sametson.Degiskenler.BakimYili = Convert.ToInt32(cmbBakimYili.EditValue);
                }
            }
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioGroup1.EditValue == "Yıl")
            {
                cmbBakimYili.Enabled = true;
                cmbTezgah.Enabled = false;
                chartControl1.Visible = true;
                chartControl2.Visible = false;
            }
            else
            {
                cmbBakimYili.Enabled = false;
                cmbTezgah.Enabled = true;
                chartControl1.Visible = false;
                chartControl2.Visible = true;
            }
        }

        void cmbTezgahDoldur()
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
            cmbTezgah.Properties.DisplayMember = "Model";
            cmbTezgah.Properties.ValueMember = "Id";
        }


    }
}