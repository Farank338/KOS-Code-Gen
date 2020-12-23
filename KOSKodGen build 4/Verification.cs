using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KOSKodGen_build_4
{
    public partial class Verification : Form
    {
        public Verification(secure_cfg a)
        {
            InitializeComponent();
            cheking_entity(a);
        }

        private void cheking_entity(secure_cfg a)
        {
            int core_count = 0;
            string error = "";
            foreach (secure_cfg.entity temp in a.ent)
            {
                if (temp.base_p != null)
                {
                    if (temp.base_p.core_p != null)
                    {
                        int core_count_var = 0;
                        for (int i = 0; i < temp.base_p.core_p.properties.Length; i++)
                        {
                            if (temp.base_p.core_p.properties_value[i] == true)
                            {
                                core_count_var++;
                            }
                        }
                        if (core_count_var > 0)
                        {
                            core_count++;
                        }
                        if (core_count_var > 0 && core_count_var < 3)
                        {
                            error += "Core entity" + temp.name + "dont have permission to all (security call)\n";
                        }
                    }
                    if (temp.base_p.main_p != null)
                    {
                        if (temp.base_p.main_p.properties_value[0] != true)
                        {
                            error += "Entity " + temp.name + " dont have permission to " + temp.base_p.main_p.properties[0] + "\n";
                        }
                    }
                }
            }
            if (core_count > 1)
            {
                error += "More than one core entity\n";
            }
            if (error == "")
            {
                richTextBox1.Text = "Verufy successfully";
            }
            else
            {
                richTextBox1.Text = error;
            }
        }
    }
}