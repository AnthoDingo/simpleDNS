using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace simpleDNS
{
    public partial class frmAssistant : Form
    {
        private string[] types = new string[2] { "A", "CNAME" };

        public frmAssistant()
        {
            InitializeComponent();

            foreach(string type in types)
            {
                cbxType.Items.Add(type);
            }
        }
    }
}
