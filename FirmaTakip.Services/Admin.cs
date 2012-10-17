using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Db4objects.Db4o;

namespace FirmaTakip.Services
{
    public class Admin
    {
        
        public static IObjectContainer db;
        public static void OpenConnection()
        {           
            db = Db4oEmbedded.OpenFile(Provider.Db4oHelper.GetdbFile());
        }

        public static void CloseConnection()
        {
            db.Close();
        }
    }
}
