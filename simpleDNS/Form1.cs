using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DNS.Server;
using DNS.Client;
using System.IO;

namespace simpleDNS
{
    public partial class mainForm : Form
    {
        private bool isRunning = false;
        private MasterFile _masterFile;
        private DnsServer _server;
        private TimeSpan TTL = new TimeSpan(3600);
        private bool isZoneFile = false;
        private string ZoneName;

        public mainForm()
        {
            InitializeComponent();

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

        private void Start()
        {
            _masterFile = new MasterFile(TTL);
            _server = new DnsServer(_masterFile);
            ZoneParser();
            MainAsync(_server);
            FileParser();
            btnReload.Enabled = true;
            isRunning = true;
        }

        private void Stop()
        {
            _server.Dispose();
            btnReload.Enabled = false;
            isRunning = false;
        }

        public async static Task MainAsync(DnsServer server)
        {

            Console.WriteLine("{0} : Starting DNS ...", "INFO");

            server.Responded += (sender, e) =>
            {
                Console.WriteLine("{0} : {1}", "REQUEST", e.Request);
                Console.WriteLine("{0} : {1}", "ANSWER", e.Response);
            };
            server.Listening += (sender, e) => Console.WriteLine("{0} : Listening ...", "INFO");
            server.Errored += (sender, e) =>
            {
                Console.WriteLine("{0} : {1}", "INFO", e.Exception);
                ResponseException responseError = e.Exception as ResponseException;
                if (responseError != null) Console.WriteLine(responseError.Response);
            };

            await server.Listen().ConfigureAwait(false);
        }

        private void nouveauToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbxMasterFile.Text = null;
            preStart();
        }

        private void btnAction_Click(object sender, EventArgs e)
        {
            switch (isRunning)
            {
                case false:
                    tbxLogs.Text = null;
                    Start();
                    btnAction.Text = "Arrêter";
                    break;
                case true:
                    Stop();
                    btnAction.Text = "Démarrer";
                    break;
            }
        }
        private void btnReload_Click(object sender, EventArgs e)
        {
            Stop();
            tbxLogs.Text = null;
            Start();
        }

        private string[] LineFormater(string line)
        {
            string _line = line.Replace("\t", " ");
            string[] values = _line.Split(" ");
            values = values.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            return values;
        }

        private void ZoneParser()
        {
            string[] origin = LineFormater(tbxMasterFile.Lines[0]);
            if (origin[0].Equals("$ORIGIN"))
            {
                Console.WriteLine("{0} : Domain {1}", "INFO", origin[1].ToLower());
                isZoneFile = true;
                ZoneName = origin[1].ToLower();
            }
            string[] ttl = LineFormater(tbxMasterFile.Lines[1]);
            if (ttl[0].Equals("$TTL"))
            {
                Console.WriteLine("{0} : TTL {1}", "INFO", ttl[1]);
                TTL = new TimeSpan(Int32.Parse(ttl[1]));
            }
        }

        private void FileParser()
        {
            foreach (string line in tbxMasterFile.Lines)
            {
                if (line.Length == 0)
                {
                    continue;
                }
                string[] values = LineFormater(line);

                if (values[0].Equals("$ORIGIN") || values[0].Equals("$TTL"))
                {
                    continue;
                }

                switch (values[1].ToUpper())
                {
                    case "A":
                        AddIPAddressResourceRecord(values[0], values[2]);
                        break;
                    case "CNAME":
                        AddCanonicalNameResourceRecord(values[0], values[2]);
                        break;
                }
            }
        }

        private string getDomain(string domain)
        {
            string _domain = domain;
            if (isZoneFile && domain != ZoneName)
            {
                _domain = String.Format("{0}.{1}", domain, ZoneName);
            }

            return _domain;
        }

        private void AddIPAddressResourceRecord(string domain, string ip)
        {
            try
            {
                _masterFile.AddIPAddressResourceRecord(getDomain(domain), ip);
                Console.WriteLine("{0} : Adding A Record : {1} => {2}", "INFO", getDomain(domain), ip);
            }
            catch (InvalidCastException e)
            {
                Console.WriteLine("{0} : {1}", "ERROR", e);
            }

        }

        public void AddCanonicalNameResourceRecord(string domain, string cname)
        {
            try
            {
                _masterFile.AddCanonicalNameResourceRecord(getDomain(domain), cname);
                Console.WriteLine("{0} : Adding CNAME Record : {1} => {2}", "INFO", getDomain(domain), cname);
            }
            catch (InvalidCastException e)
            {
                Console.WriteLine("{0} : {1}", "ERROR", e);
            }

        }

        private void tbxLogs_TextChanged(object sender, EventArgs e)
        {
            tbxLogs.SelectionStart = tbxLogs.Text.Length;
            tbxLogs.ScrollToCaret();
        }

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
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

        private void aProposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox box = new AboutBox();
            box.ShowDialog();
        }
    }
}
