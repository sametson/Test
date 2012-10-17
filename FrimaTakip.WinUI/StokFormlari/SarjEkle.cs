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
    public partial class SarjEkle : DevExpress.XtraEditors.XtraForm
    {
       new Stok.HamMalzeme hamMalzeme=null;
        public SarjEkle()
        {
            InitializeComponent();
        }
        public SarjEkle(Stok.HamMalzeme _hamMalzeme)
        {
            InitializeComponent();
            hamMalzeme = _hamMalzeme;
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (txtSarjKodu.Text == "" || hamMalzeme==null)
            {
                MessageBox.Show(this, "Eksik Bilgi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                string sonuc=new Stok.Services.Sarj().Kaydet(txtSarjKodu.Text,hamMalzeme.Id);
                if (sonuc!= null)
                {
                    MessageBox.Show(this, sonuc, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    this.Close();
                }
            }
        }

        private void SarjEkle_Load(object sender, EventArgs e)
        {
            txtMalzemeAdi.Text = hamMalzeme.Adi;
        }

      
    }
}