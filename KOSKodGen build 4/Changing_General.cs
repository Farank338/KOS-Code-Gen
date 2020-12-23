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
    public partial class Changing_General : Form
    {
        private string a;
        private DataTransfer transferDel;

        public Changing_General(ref string set, DataTransfer del)
        {
            InitializeComponent();
            a = set;
            richTextBox1.Text = a;
            transferDel = del;
        }

        private void Changing_General_FormClosed(object sender, FormClosedEventArgs e)
        {
            a = richTextBox1.Text;
            string data = richTextBox1.Text;
            transferDel.Invoke(data);
        }
    }
}