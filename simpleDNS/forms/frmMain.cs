using System;
using System.Windows.Forms;
using System.IO;

namespace simpleDNS
{
    public partial class mainForm : Form
    {
        private ZoneManager _zm;

        public mainForm()
        {
            InitializeComponent();
            _zm = new ZoneManager();
            //assistantToolStripMenuItem.Visible = false;

        }
        private void mainForm_Shown(object sender, EventArgs e)
        {
            Console.SetOut(new ControlWriter(tbxLogs));
        }

        private void preStart()
        {
            tbxMasterFile.Enabled = true;
            tbxLogs.Text = null;
            sauvegarderToolStripMenuItem.Enabled = true;
            btnAction.Enabled = true;
        }
        
        private void btnAction_Click(object sender, EventArgs e)
        {
            switch (_zm.IsRunning)
            {
                case false:
                    tbxLogs.Text = null;
                    _zm.LoadFromText(tbxMasterFile.Text);
                    _zm.Start();
                    btnAction.Text = "Arrêter";
                    break;
                case true:
                    _zm.Stop();
                    btnAction.Text = "Démarrer";
                    break;
            }
            btnReload.Enabled = !btnReload.Enabled;
        }
        private void btnReload_Click(object sender, EventArgs e)
        {
            _zm.Stop();
            tbxLogs.Text = null;
            _zm.LoadFromText(tbxMasterFile.Text);
            _zm.Start();
        }
        private void tbxLogs_TextChanged(object sender, EventArgs e)
        {
            tbxLogs.SelectionStart = tbxLogs.Text.Length;
            tbxLogs.ScrollToCaret();
        }
        private void nouveauToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbxMasterFile.Text = null;
            preStart();
        }

        private void importerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            fileDialog.Filter = "Fichier texte (*.txt)|*.txt";
            fileDialog.RestoreDirectory = true;
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                //Get the path of specified file
                string filePath = fileDialog.FileName;

                //Read the contents of the file into a stream
                var fileStream = fileDialog.OpenFile();
                StreamReader reader = new StreamReader(fileStream);
                tbxMasterFile.Text = reader.ReadToEnd();
            }
            preStart();
        }

        private void sauvegarderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            saveDialog.Filter = "Fichier texte (*.txt)|*.txt";
            saveDialog.RestoreDirectory = true;
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveDialog.FileName, tbxMasterFile.Text);
            }
        }
        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(_zm.IsRunning) { _zm.Stop(); }
            Environment.Exit(0);
        }

        private void assistantToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAssistant assistant = new frmAssistant();
            assistant.Show();
        }

        private void aProposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAboutBox box = new frmAboutBox();
            box.ShowDialog();
        }
    }
}
