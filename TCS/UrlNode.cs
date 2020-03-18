using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCS.Util;

namespace TCS
{
    public partial class UrlNode : Form
    {

        TreeView tv;
        public UrlNode(TreeView tv)
        {
            InitializeComponent();
            this.tv = tv;
        }

        private string _getContent = null;
        private void OK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_getContent))
            {
                _getContent = ContentBox.Text = Http.GET(UrlBox.Text);
            }
            string[] b = Regex.Split(Encrypt.DeBase64(_getContent), "\n");
            if (MessageBox.Show(
                $"[Summary]\r\n" +
                $"Group: ${Encrypt.SHA1(UrlBox.Text)}\r\n" +
                $"Count: {b.Length}\r\n" +
                $"Your operation may overwrite the original data. Do you want to continue?",
                "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string n = Encrypt.SHA1(UrlBox.Text);
                if (tv.Nodes.ContainsKey(n))
                {
                    for (int i = 0; i < tv.Nodes.Count; i++)
                    {
                        if (tv.Nodes[i].Text == n)
                        {
                            tv.Nodes[i].Remove();
                            i -= 1;
                        }
                    }
                }
                tv.Nodes.Add(new TreeNode()
                {
                    Text = n
                });

                foreach (var v in b)
                {
                    if (!string.IsNullOrWhiteSpace(v))
                    {
                        //Regex.Replace(v, @"\p{Cs}", "")
                        string[] bb = v.Split('#');
                        TreeNode vv = new TreeNode();
                        if (bb.Length == 2)
                            vv.Text = bb[1];
                        else
                            vv.Text = "Untitled";
                        vv.Tag = v;
                        tv.Nodes[tv.Nodes.Count - 1].Nodes.Add(vv);
                    }
                }
            }
        }

        private void Get_Click(object sender, EventArgs e)
        {
            _getContent = ContentBox.Text = Http.GET(UrlBox.Text);
        }
    }
}
