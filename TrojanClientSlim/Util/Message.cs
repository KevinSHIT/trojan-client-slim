using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrojanClientSlim.Util
{
    class Message
    {
        public enum Mode { Info = 0, Warning = 1, Error = 2};
        public static void Show(string content, Mode messageMode)
        {
            switch (messageMode)
            {
                case Mode.Error:
                    MessageBox.Show(content, "FATAL ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case Mode.Info:
                    MessageBox.Show(content, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case Mode.Warning:
                    MessageBox.Show(content, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                default:
                    throw new IndexOutOfRangeException();
            }

            
        }
    }
}
