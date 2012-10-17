using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FirmaTakip;

namespace FirmaTakip.WinUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Giris());
            //Application.Run(new StokFormlari.RaporFormlari.HamMalzemeRaporAna());
        }
    }
}
