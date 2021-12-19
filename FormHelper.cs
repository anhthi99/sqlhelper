using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLTesting
{
    class FormHelper
    {
        
        public static void fn_display_message(string message, string code)
        {
            MessageBoxButtons _buttons = MessageBoxButtons.OK;
            MessageBoxIcon _icon = MessageBoxIcon.None;
            //string last_show_string = "";
            string _title = "";
            switch (code)
            {
                case "OCI":
                    _buttons = MessageBoxButtons.OKCancel;
                    _icon = MessageBoxIcon.Information;
                    _title = "問題";
                    break;
                case "OCQ":
                    _buttons = MessageBoxButtons.OKCancel;
                    _icon = MessageBoxIcon.Question;
                    _title = "問題";
                    break;
                case "OE":
                    _buttons = MessageBoxButtons.OK;
                    _icon = MessageBoxIcon.Error;
                    _title = "エラー";
                    break;
                case "OI":
                    _buttons = MessageBoxButtons.OK;
                    _icon = MessageBoxIcon.Information;
                    _title = "通知";
                    break;
                default:
                    break;
            }
            MessageBox.Show(message, _title, _buttons, _icon);
        }
    }

    
}
