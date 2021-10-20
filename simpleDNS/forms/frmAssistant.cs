using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace simpleDNS
{

    public struct Entry
    {

        public Entry(string _subdomain, string _type, string _value)
        {
            subdomain = _subdomain;
            type = _type;
            value = _value;
        }

        public string subdomain { get; }
        public string type { get; }
        public string value { get; }

        public override string ToString() => $"{subdomain}\tIN\t{type}\t{value}\r\n";


    }
    public partial class frmAssistant : Form
    {
        private frmMain _parent;
        private string[] _types = new string[3] { "A", "CNAME", "NS" };
        private List<Entry> _entries = new List<Entry>();

        public frmAssistant(frmMain parent)
        {
            InitializeComponent();
            _parent = parent;

            foreach(string type in _types)
            {
                cbxType.Items.Add(type);
            }
            cbxType.SelectedIndex = 0;
        }

        private void ZoneFormater()
        {
            tbxResult.Text = null;
            if (!string.IsNullOrEmpty(tbxDomain.Text)){
                tbxResult.Text += $"$ORIGIN\t{tbxDomain.Text}\r\n";
            }

            if(nudTTL.Value > 0)
            {
                tbxResult.Text += $"$TTL\t{nudTTL.Value}\r\n";
            }

            foreach (Entry entry in _entries.FindAll(x => x.type.Equals("NS")))
            {
                tbxResult.Text += entry.ToString();
            }

            foreach(Entry entry in _entries.FindAll(x => !x.type.Equals("NS")))
            {
                tbxResult.Text += entry.ToString();
            }
        }

        private void ResetSubDomain()
        {
            tbxSubDomain.Text = null;
            cbxType.SelectedIndex = 0;
            tbxValue.Text = null;
        }

        private void tbxDomain_TextChanged(object sender, EventArgs e)
        {
            ZoneFormater();
        }

        private void nudTTL_ValueChanged(object sender, EventArgs e)
        {
            ZoneFormater();
        }

        private void cbxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxType.GetItemText(cbxType.SelectedItem).Equals("NS"))
            {
                tbxSubDomain.Text = "@";
                tbxSubDomain.Enabled = false;
            }
            else
            {
                tbxSubDomain.Enabled = true;
                if (tbxSubDomain.Text.Equals("@"))
                {
                    tbxSubDomain.Text = null;
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(tbxSubDomain.Text) || string.IsNullOrEmpty(tbxValue.Text))
            {
                return;
            }
            Entry entry = new Entry(tbxSubDomain.Text, cbxType.GetItemText(cbxType.SelectedItem), tbxValue.Text);
            _entries.Add(entry);
            ResetSubDomain();
            ZoneFormater();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            _parent.LoadZone(tbxResult.Text);
        }
    }
}
