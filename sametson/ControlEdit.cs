using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using System.Drawing;
using DevExpress.Utils;
using DevExpress.XtraLayout;

namespace sametson
{
    public class ControlEdit
    {
        /// <summary>
        /// LayoutControl içindeki textbox ve Memoedit lerin içeriğini temizler
        /// </summary>
        /// <param name="layoutControl">Temizlenecek layoutControl</param>
        public void LayoutBilgiTemizle(LayoutControl layoutControl)
        {

            foreach (var item in layoutControl.Items)
            {
                if (item is LayoutControlItem)
                {
                    if (((LayoutControlItem)item).Control is TextEdit || ((LayoutControlItem)item).Control is MemoEdit)
                    {
                        ((LayoutControlItem)item).Control.Text = "";
                    }
                }  

            }
        }

    }
}
