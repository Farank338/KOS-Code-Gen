using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace KOSKodGen_build_4
{
    public delegate void DataTransfer(string data);

    public partial class Form1 : Form
    {
        private secure_cfg a;

        public Form1()
        {
            InitializeComponent();
            a = new secure_cfg();

            transferDelegate += new DataTransfer(ReceiveInput);
        }

        public void ReceiveInput(string data)
        {
            a.general_setting = data;
        }

        public DataTransfer transferDelegate;

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                a = null;
                GC.Collect();
                a = new secure_cfg();
                //Get the path of specified file
                string filePath = openFileDialog1.FileName;
                if (File.Exists(filePath))
                {
                    string readText = File.ReadAllText(filePath);
                    // richTextBox1.Text = "aaaa\nffff";
                    a.load_file(readText);
                }
                treeView1.ContextMenuStrip = contextMenuStrip1;

                a.make_tree(treeView1);
                treeView1.SelectedNode = treeView1.Nodes[0];
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            treeView1.SelectedNode = e.Node;
        }

        private void treeView1_Click(object sender, EventArgs e)
        {
            a.changing_base_setting(treeView1, treeView1.SelectedNode);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool flag = false;
            string path = "";
            this.Text = a.save_to_file();
            File.WriteAllText("secure.cfg", a.save_to_file());
            /*
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                flag = true;
                path = folderBrowserDialog1.SelectedPath;
                File.WriteAllText("secure.cfg", a.save_to_file());
            }*/
        }

        private void addEntityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            a.add_ent(treeView1);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            a = null;
            GC.Collect();
            a = new secure_cfg();
            treeView1.Nodes.Clear();
            treeView1.Nodes.Add("General");
            treeView1.ContextMenuStrip = contextMenuStrip1;
            treeView1.SelectedNode = treeView1.Nodes[0];
        }

        private void mainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            a.add_base(1, treeView1.SelectedNode, treeView1);
        }

        private void coreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            a.add_base(2, treeView1.SelectedNode, treeView1);
        }

        private void changeGeneralToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Changing_General ch = new Changing_General(ref a.general_setting, transferDelegate);
            ch.Show();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView1.Nodes[0] == treeView1.SelectedNode)
            {
                a = null;
                GC.Collect();
                a = new secure_cfg();
                treeView1.Nodes.Clear();
                treeView1.Nodes.Add("General");
                treeView1.ContextMenuStrip = contextMenuStrip1;
                treeView1.SelectedNode = treeView1.Nodes[0];
            }
            if (treeView1.SelectedNode.Parent != null)
            {
                if (treeView1.SelectedNode.Parent == treeView1.Nodes[0])
                {
                    a.ent.RemoveAt(treeView1.Nodes[0].Nodes.IndexOf(treeView1.SelectedNode));
                    treeView1.SelectedNode.Remove();
                }
                if (treeView1.SelectedNode.Parent.Parent != null)
                {
                    if (treeView1.SelectedNode.Parent.Parent == treeView1.Nodes[0])
                    {
                        if (treeView1.SelectedNode.Text == "Base_Main")
                        {
                            a.ent[treeView1.Nodes[0].Nodes.IndexOf(treeView1.SelectedNode.Parent)].base_p.main_p = null;
                            treeView1.SelectedNode.Remove();
                            GC.Collect();
                        }
                        if (treeView1.SelectedNode.Text == "Base_Core")
                        {
                            a.ent[treeView1.Nodes[0].Nodes.IndexOf(treeView1.SelectedNode.Parent)].base_p.core_p = null;
                            treeView1.SelectedNode.Remove();
                            GC.Collect();
                        }
                    }
                }
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (treeView1.Nodes[0] == treeView1.SelectedNode)
            {
                contextMenuStrip1.Items[0].Visible = true;
                contextMenuStrip1.Items[1].Visible = false;
                contextMenuStrip1.Items[2].Visible = false;
                contextMenuStrip1.Items[3].Visible = true;
                contextMenuStrip1.Items[4].Visible = true;
                contextMenuStrip1.Items[5].Visible = true;
            }
            if (treeView1.SelectedNode.Parent != null)
            {
                if (treeView1.SelectedNode.Parent == treeView1.Nodes[0])
                {
                    contextMenuStrip1.Items[0].Visible = false;
                    contextMenuStrip1.Items[1].Visible = true;
                    contextMenuStrip1.Items[2].Visible = true;
                    contextMenuStrip1.Items[3].Visible = true;
                    contextMenuStrip1.Items[4].Visible = true;
                    contextMenuStrip1.Items[5].Visible = false;
                }
                if (treeView1.SelectedNode.Parent.Parent != null)
                {
                    if (treeView1.SelectedNode.Parent.Parent == treeView1.Nodes[0])
                    {
                        contextMenuStrip1.Items[0].Visible = false;
                        contextMenuStrip1.Items[1].Visible = false;
                        contextMenuStrip1.Items[2].Visible = true;
                        contextMenuStrip1.Items[3].Visible = true;
                        contextMenuStrip1.Items[4].Visible = true;
                        contextMenuStrip1.Items[5].Visible = false;
                    }
                    if (treeView1.SelectedNode.Parent.Parent.Parent != null)
                    {
                        if (treeView1.SelectedNode.Parent.Parent.Parent == treeView1.Nodes[0])
                        {
                            contextMenuStrip1.Items[0].Visible = false;
                            contextMenuStrip1.Items[1].Visible = false;
                            contextMenuStrip1.Items[2].Visible = false;
                            contextMenuStrip1.Items[3].Visible = true;
                            contextMenuStrip1.Items[4].Visible = true;
                            contextMenuStrip1.Items[5].Visible = false;
                        }
                    }
                }
            }
        }

        private void collapseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeView1.CollapseAll();
        }

        private void expandAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeView1.ExpandAll();
        }

        private void verifyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Verification ch = new Verification(a);
            ch.Show();
        }

        public int tree_view_node_edite = -1;

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            if (treeView1.Nodes[0] != null)
            {
                if (treeView1.SelectedNode == treeView1.Nodes[0])
                {
                    treeView1.LabelEdit = true;
                    tree_view_node_edite = 0;
                    treeView1.SelectedNode.BeginEdit();
                }
                if (treeView1.SelectedNode.Parent == treeView1.Nodes[0])
                {
                    treeView1.LabelEdit = true;
                    tree_view_node_edite = 1;
                    treeView1.SelectedNode.BeginEdit();
                }
            }
        }

        private void treeView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && tree_view_node_edite == 0)
            {
                tree_view_node_edite = -1;
                treeView1.SelectedNode.EndEdit(true);
            }
            if (e.KeyCode == Keys.Enter && tree_view_node_edite == 1)
            {
                tree_view_node_edite = -1;
                treeView1.SelectedNode.EndEdit(true);

                a.ent[treeView1.Nodes[0].Nodes.IndexOf(treeView1.SelectedNode)].change_name(treeView1.Nodes[0].Nodes[treeView1.Nodes[0].Nodes.IndexOf(treeView1.SelectedNode)].Text);
                //a.ent[treeView1.Nodes[0].Nodes.IndexOf(treeView1.SelectedNode)].name = treeView1.Nodes[0].Nodes[treeView1.Nodes[0].Nodes.IndexOf(treeView1.SelectedNode)].Text;
            }
        }

        private void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (tree_view_node_edite == 0)
            {
                tree_view_node_edite = -1;
                treeView1.SelectedNode.EndEdit(true);
            }
            if (tree_view_node_edite == 1)
            {
                tree_view_node_edite = -1;
                treeView1.SelectedNode.EndEdit(true);
                a.ent[treeView1.Nodes[0].Nodes.IndexOf(treeView1.SelectedNode)].name = treeView1.Nodes[0].Nodes[treeView1.Nodes[0].Nodes.IndexOf(treeView1.SelectedNode)].Text;
                //a.ent[treeView1.Nodes[0].Nodes.IndexOf(treeView1.SelectedNode)].name = treeView1.Nodes[0].Nodes[treeView1.Nodes[0].Nodes.IndexOf(treeView1.SelectedNode)].Text;
            }
        }
    }

    public class secure_cfg
    {
        public string general_setting;

        public string save_to_file()
        {
            string file = "";
            file += general_setting + "\n";
            if (ent != null)
            {
                foreach (entity a in ent)
                {
                    file += a.save_to_file();
                }
            }
            return file;
        }

        public void add_base(int f, TreeNode b, TreeView c)
        {
            if (b.Parent == c.Nodes[0])
            {
                if (f == 1)
                {
                    if (ent[c.Nodes[0].Nodes.IndexOf(b)].base_p == null)
                    {
                        ent[c.Nodes[0].Nodes.IndexOf(b)].base_p_create();
                    }
                    else
                    {
                    }
                    if (ent[c.Nodes[0].Nodes.IndexOf(b)].base_p.main_p == null)
                    {
                        ent[c.Nodes[0].Nodes.IndexOf(b)].base_p.main_create();
                        b.Nodes.Add("Base_Main");
                        ent[c.Nodes[0].Nodes.IndexOf(b)].base_p.main_p.add_prop(b.Nodes[b.Nodes.Count - 1]);
                    }
                }
                if (f == 2)
                {
                    if (ent[c.Nodes[0].Nodes.IndexOf(b)].base_p == null)
                    {
                        ent[c.Nodes[0].Nodes.IndexOf(b)].base_p_create();
                    }
                    else
                    {
                    }
                    if (ent[c.Nodes[0].Nodes.IndexOf(b)].base_p.core_p == null)
                    {
                        ent[c.Nodes[0].Nodes.IndexOf(b)].base_p.core_create();
                        b.Nodes.Add("Base_Core");
                        ent[c.Nodes[0].Nodes.IndexOf(b)].base_p.core_p.add_prop(b.Nodes[b.Nodes.Count - 1]);
                    }
                }
            }
        }

        public void changing_base_setting(TreeView a, TreeNode b)
        {
            if (b.Parent != null)
                if (b.Parent.Parent != null)
                    if (b.Parent.Parent.Parent != null)
                    {
                        int ent_n = b.Parent.Parent.Parent.Nodes.IndexOf(b.Parent.Parent);
                        int core_or_main = b.Parent.Parent.Nodes.IndexOf(b.Parent);
                        if (b.Parent.Text == "Base_Main")
                        {
                            int set_n = b.Parent.Nodes.IndexOf(b);
                            if (ent[ent_n].base_p.main_p.properties_value[set_n] == true)
                            {
                                ent[ent_n].base_p.main_p.properties_value[set_n] = false;
                                b.Text = ent[ent_n].base_p.main_p.properties[set_n] + '=' + (ent[ent_n].base_p.main_p.properties_value[set_n] ? "grant" : "deny");
                            }
                            else
                            {
                                ent[ent_n].base_p.main_p.properties_value[set_n] = true;
                                b.Text = ent[ent_n].base_p.main_p.properties[set_n] + '=' + (ent[ent_n].base_p.main_p.properties_value[set_n] ? "grant" : "deny");
                            }
                        }
                        if (b.Parent.Text == "Base_Core")
                        {
                            int set_n = b.Parent.Nodes.IndexOf(b);
                            if (ent[ent_n].base_p.core_p.properties_value[set_n] == true)
                            {
                                ent[ent_n].base_p.core_p.properties_value[set_n] = false;
                                b.Text = ent[ent_n].base_p.core_p.properties[set_n] + '=' + (ent[ent_n].base_p.core_p.properties_value[set_n] ? "grant" : "deny");
                            }
                            else
                            {
                                ent[ent_n].base_p.core_p.properties_value[set_n] = true;
                                b.Text = ent[ent_n].base_p.core_p.properties[set_n] + '=' + (ent[ent_n].base_p.core_p.properties_value[set_n] ? "grant" : "deny");
                            }
                        }
                    }
        }

        public void load_file(string file)
        {
            file = clear_comentaries(file);
            file = clear_space(file);
            file = clear_enter(file);
            char[] entity_separator = { '}' };
            string[] entity = file.Split(entity_separator);
            entity[0] = find_general_setting(entity[0]);
            foreach (string e in entity)
            {
                if (e != "\n")
                {
                    if (ent == null)
                    {
                        ent = new List<entity>();
                    }
                    entity temp = new entity();
                    temp.creating_entity(e);
                    //f.Text = Convert.ToString(temp.base_p.main_p.properties_value[1]);
                    ent.Add(temp);
                }
            }
            GC.Collect();
        }

        public string clear_comentaries(string file)
        {
            while (file.IndexOf("/*") != -1)
            {
                file = file.Remove(file.IndexOf("/*"), file.IndexOf("*/") - file.IndexOf("/*") + 2);
            }
            return file;
        }

        public string clear_space(string file)
        {
            while (file.IndexOf("  ") != -1)
            {
                file = file.Replace("  ", "");
            }
            return file;
        }

        public string clear_enter(string file)
        {
            while (file.IndexOf("\n\n") != -1)
            {
                file = file.Replace("\n\n", "\n");
            }
            if (file[0] == '\n')
            {
                file = file.Remove(0, 1);
            }
            return file;
        }

        public string find_general_setting(string segment)
        {
            string entity0 = segment.Substring(segment.IndexOf("entity"));
            general_setting = segment.Substring(0, segment.IndexOf("entity") - 1);
            return entity0;
        }

        public void make_tree(TreeView a)
        {
            a.Nodes.Clear();
            a.Nodes.Add("General");

            foreach (entity temp in ent)
            {
                TreeNode b = new TreeNode();
                b.Text = temp.name;
                if (temp.base_p != null)
                {
                    if (temp.base_p.main_p != null)
                    {
                        TreeNode bmain = new TreeNode();
                        bmain.Text = "Base_Main";
                        temp.base_p.main_p.add_prop(ref bmain);
                        b.Nodes.Add(bmain);
                    }
                    if (temp.base_p.core_p != null)
                    {
                        TreeNode bcore = new TreeNode();
                        bcore.Text = "Base_Core";
                        temp.base_p.core_p.add_prop(ref bcore);
                        b.Nodes.Add(bcore);
                    }
                    a.Nodes[0].Nodes.Add(b);
                }
            }
        }

        public void add_ent(TreeView a)
        {
            if (a.Nodes[0] == null)
            {
                a.Nodes.Clear();
                a.Nodes.Add("General");
            }
            a.Nodes[0].Nodes.Add("New entity");
            if (ent == null)
            {
                ent = new List<entity>();
            }
            entity temp = new entity();
            temp.name = "New entity";
            ent.Add(temp);
        }

        public List<entity> ent;

        public class entity
        {
            public string name;
            public polyci_base base_p;

            public void change_name(string a)
            {
                name = null;
                name = a;
            }

            public void base_p_create()
            {
                if (base_p == null)
                {
                    base_p = new polyci_base();
                }
            }

            public string save_to_file()
            {
                string file = "entity " + name + "{\n";
                if (base_p != null)
                {
                    if (base_p.main_p != null)
                    {
                        file += base_p.main_p.save_to_file();
                    }
                    if (base_p.core_p != null)
                    {
                        file += base_p.core_p.save_to_file();
                    }

                    file += "}\n";
                }
                return file;
            }

            public void creating_entity(string file)
            {
                char[] splitter = { '{' };
                string[] temp = file.Split(splitter);
                setting_name(temp[0]);
                finding_policy(temp[1]);
            }

            public void setting_name(string file)
            {
                name = file.Substring(file.IndexOf(" ")).Trim(' ');
            }

            public void finding_policy(string file)
            {
                file = file.Replace("\n", String.Empty);
                char[] splitter = { ';' };
                string[] temp = file.Split(splitter);
                //base
                base_p = new polyci_base();
                base_p.main_create();
                bool main_active = false;
                for (int j = 0; j < temp.Length; j++)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (temp[j].IndexOf(base_p.main_p.properties[i]) != -1)
                        {
                            if (temp[j].IndexOf("grant") != -1)
                                base_p.main_p.properties_value[i] = true;
                            main_active = true;
                        }
                    }
                }

                if (main_active == false)
                {
                    base_p.main_delete();
                    base_p.main_p = null;
                }
                base_p.core_create();
                bool core_active = false;
                for (int j = 0; j < temp.Length; j++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (temp[j].IndexOf(base_p.core_p.properties[i]) != -1)
                        {
                            if (temp[j].IndexOf("grant") != -1)
                                base_p.core_p.properties_value[i] = true;
                            core_active = true;
                        }
                    }
                }

                if (core_active == false)
                {
                    base_p.core_delete();
                    base_p.core_p = null;
                }
                if (core_active == false && main_active == false)
                {
                    base_p = null;
                }
            }

            public class polyci_base
            {
                public policy_base_main main_p;
                public policy_base_core core_p;

                public void main_create()
                {
                    main_p = new policy_base_main();
                }

                public void core_create()
                {
                    core_p = new policy_base_core();
                }

                public void main_delete()
                {
                    main_p.delete();
                }

                public void core_delete()
                {
                    core_p.delete();
                }

                public class policy_base_main
                {
                    public string[] properties = { "execute call main", "security call Register", "receive in", "send out" };
                    public bool[] properties_value = { false, false, false, false };

                    public void add_prop(ref TreeNode a)
                    {
                        for (int i = 0; i < properties.Length; i++)
                        {
                            a.Nodes.Add(properties[i] + '=' + (properties_value[i] ? "grant" : "deny"));
                        }
                    }

                    public void add_prop(TreeNode a)
                    {
                        for (int i = 0; i < properties.Length; i++)
                        {
                            a.Nodes.Add(properties[i] + '=' + (properties_value[i] ? "grant" : "deny"));
                        }
                    }

                    public void delete()
                    {
                        properties = null;
                        properties_value = null;
                    }

                    public string save_to_file()
                    {
                        string file = "";
                        for (int i = 0; i < properties.Length; i++)
                        {
                            if (properties_value[i] == true)
                            {
                                file += "\t" + properties[i] + " = grant;\n";
                            }
                        }
                        return file;
                    }
                }

                public class policy_base_core
                {
                    public string[] properties = { "security call Init", "security call InitEx", "security call Fini" };
                    public bool[] properties_value = { false, false, false };

                    public void add_prop(ref TreeNode a)
                    {
                        for (int i = 0; i < properties.Length; i++)
                        {
                            a.Nodes.Add(properties[i] + '=' + (properties_value[i] ? "grant" : "deny"));
                        }
                    }

                    public void add_prop(TreeNode a)
                    {
                        for (int i = 0; i < properties.Length; i++)
                        {
                            a.Nodes.Add(properties[i] + '=' + (properties_value[i] ? "grant" : "deny"));
                        }
                    }

                    public void delete()
                    {
                        properties = null;
                        properties_value = null;
                    }

                    public string save_to_file()
                    {
                        string file = "";
                        for (int i = 0; i < properties.Length; i++)
                        {
                            if (properties_value[i] == true)
                            {
                                file += "\t" + properties[i] + " = grant;\n";
                            }
                        }
                        return file;
                    }
                }
            }
        }
    }
}