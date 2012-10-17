using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;
using System.Drawing;

namespace sametson
{
    public class FormAyar
    {
        /// <summary>
        /// Programın açıldığı bilgisayarın ekran çözünürlüğünü alarak
        /// formun ebatını ayarlar
        /// </summary>
        /// <param name="frm">Ebatı ayarlanacak form </param>
        public static void Boyut(Form frm)
        {
            frm.Left = Convert.ToInt32(SystemInformation.PrimaryMonitorSize.Width * 0.05);
            frm.Top = Convert.ToInt32(SystemInformation.PrimaryMonitorSize.Height * 0.05);
            frm.Width = Convert.ToInt32(SystemInformation.PrimaryMonitorSize.Width * 0.9);
            frm.Height = Convert.ToInt32(SystemInformation.PrimaryMonitorSize.Height * 0.9);
        }
    }
}
