using System;
using System.Collections.Generic;
using System.Text;
using DNS.Server;
using DNS.Client;
using System.IO;

namespace simpleDNS
{
    class ZoneManager
    {
        private string _ZoneName;
        private TimeSpan _TTL = new TimeSpan(3600);
        private MasterFile _MasterFile;
        private DnsServer _Server;

        public ZoneManager()
        {
            _MasterFile = new MasterFile(TTL);
            _Server = new DnsServer(_MasterFile);
        }

        public ZoneManager(MasterFile masterFile)
        {
            _MasterFile = masterFile;
            _Server = new DnsServer(_MasterFile);
        }

        public ZoneManager(MasterFile masterFile, DnsServer server)
        {
            _MasterFile = masterFile;
            _Server = server;
        }

        public string ZoneName
        {
            get { return _ZoneName; }
            set { _ZoneName = value; }
        }

        public TimeSpan TTL
        {
            get { return _TTL; }
            set { _TTL = value; }
        }

        public void Load(string Path)
        {
            StreamReader reader = new StreamReader(Path);
            string config = reader.ReadToEnd();
        }

        public void Save(string ZoneFile, string Path)
        {
            File.WriteAllText(Path, ZoneFile);
        }

        public void ParseText(string text)
        {

        }

    }
}
