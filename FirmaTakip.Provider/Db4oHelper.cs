using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace FirmaTakip.Provider
{
    public sealed class Db4oHelper
    {
        readonly static string YapFileName = Path.Combine(String.Format("{0}\\FirmaTakipDb.stn", Application.StartupPath));

        public static string GetdbFile()
        {
            return YapFileName;
        }
    }
}
