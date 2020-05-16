using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using TCS.Util;

using Message = TCS.Util.Message;

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
        private string _decryptContent = null;
        private void OK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ContentBox.Text))
            {
                _getContent = ContentBox.Text = Http.GET(UrlBox.Text);
            }
            else
            {
                _getContent = ContentBox.Text;
            }
            try
            {
                string[] b = Regex.Split(Encrypt.DeBase64(_getContent, true), "\n");
                if (MessageBox.Show(
                    $"[Summary]\r\n" +
                    $"Group: ${Encrypt.SHA1(ContentBox.Text)}\r\n" +
                    $"Count: {b.Length}\r\n" +
                    $"Your operation may overwrite the original data. Do you want to continue?",
                    "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string n = Encrypt.SHA1(UrlBox.Text);
                    for (int i = 0; i < tv.Nodes.Count; i++)
                    {
                        if (tv.Nodes[i].Text == n)
                        {
                            tv.Nodes[i].Remove();
                            break;
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
                File.WriteAllText(TCSPath.NodeList, Encrypt.Base64(tv.ToJObject().ToString()));
            }
            catch (IOException)
            {
                Message.Show("File written failed!", Message.Mode.Error);
            }
            catch (FormatException)
            {
                Message.Show("Input is invalid", Message.Mode.Error);
            }
            catch (Exception)
            {

            }
        }

        private void Get_Click(object sender, EventArgs e)
        {
            _getContent = ContentBox.Text = Http.GET(UrlBox.Text);
        }
    }
}
