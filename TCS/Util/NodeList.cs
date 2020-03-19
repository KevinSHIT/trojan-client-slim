using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System.Windows.Forms;

namespace TCS.Util
{
    public static class NodeList
    {
        /* Node.json Example
         * {
         *   "Tree0": [
         *     "Node#0",
         *     "Node#1"
         *   ],
         *   "Tree1": [
         *     "Node#0",
         *     "Node#1"
         *   ]
         * }
         *
         * Tree0 & Tree1 is Group
         * Node* is node(share link)
         *
         * NodeInfo
         * Name -> Node Name
         * Tag -> NodeShareLink
         */

        public static void BindTreeView(TreeView treeView, string json)
        {
            treeView.Nodes.Clear();

            if (IsJOjbect(json))
            {
                JObject jo = (JObject)JsonConvert.DeserializeObject(json);

                foreach (var item in jo)
                {
                    TreeNode tree;
                    if (item.Value.GetType() == typeof(JArray))
                    {
                        tree = new TreeNode(item.Key);
                        AddTreeChildNode(ref tree, item.Value.ToString());
                        treeView.Nodes.Add(tree);
                    }
                    else
                    {
                        MessageBox.Show($"NODE.JSON ERRER {item.GetType()}");
                    }
                }
            }
            treeView.ExpandAll();
        }
        public static void AddTreeChildNode(ref TreeNode parantNode, string value)
        {
            //MessageBox.Show(value + " " + IsJOjbect(value) + " " + IsJArray(value));
            if (IsJArray(value))
            {
                JArray ja = (JArray)JsonConvert.DeserializeObject(value);
                foreach (JValue item in ja)
                {
                    string v = item.ToString();
                    string[] vv = v.Split('#');
                    TreeNode tOb = new TreeNode();
                    if (vv.Length == 2)
                    {
                        tOb.Text = vv[1];
                    }
                    else if (vv.Length == 1)
                    {
                        tOb.Text = "Untitled";
                    }
                    else
                    {
                        vv[0] = string.Empty;
                        tOb.Text = ShareLink.CombineToString(vv);
                    }
                    tOb.Tag = v;
                    parantNode.Nodes.Add(tOb);
                }
            }
        }
        private static bool IsJOjbect(string value)
        {
            try
            {
                JObject.Parse(value);
                return true;
            }
            catch
            {
                return false;
            }
        }
        private static bool IsJArray(string value)
        {
            try
            {
                JArray.Parse(value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static JObject ToJObject(this TreeView tv)
        {
            JObject jo = new JObject();
            JArray ja;
            for (int i = 0; i < tv.Nodes.Count; i++)
            {
                ja = new JArray();
                foreach (TreeNode v in tv.Nodes[i].Nodes)
                {
                    // Tag storages share link
                    if (v.Tag != null)
                        ja.Add(v.Tag);
                }
                // Root node's name is group name
                jo.Add(tv.Nodes[i].Text, ja);
            }

            return jo;
        }


    }
}
