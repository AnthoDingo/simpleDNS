using System;
using System.Collections.Generic;
using System.Text;
using DNS.Server;
using DNS.Client;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

namespace simpleDNS
{
    class ZoneManager
    {
        private string _ZoneName;
        private TimeSpan _TTL = new TimeSpan(3600);
        private MasterFile _MasterFile;
        private DnsServer _Server;
        private bool _isZoneFile = false;
        private bool _isRunning = false;

        public ZoneManager(){}

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

        public bool IsRunning
        {
            get { return _isRunning;}
        }

        public void LoadFromFile(string Path)
        {
            Console.WriteLine("{0} : Loading zone from file : {1}", "INFO", Path);
            StreamReader reader = new StreamReader(Path);
            LoadFromText(reader.ReadToEnd());

        }

        public void Save(string ZoneFile, string Path)
        {
            File.WriteAllText(Path, ZoneFile);
        }

        public void LoadFromText(string text)
        {
            Console.WriteLine("{0} : Loading zone ...", "INFO");
            string[] lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            string[] origin = LineFormater(lines[0]);
            if (origin[0].Equals("$ORIGIN"))
            {
                //Console.WriteLine("{0} : Domain {1}", "INFO", origin[1].ToLower());
                _isZoneFile = true;
                _ZoneName = origin[1].ToLower();
                if(!_ZoneName.Substring(_ZoneName.Length - 1).Equals('.'))
                {
                    //_ZoneName += ".";
                }
                Console.WriteLine("{0} : Domain {1}", "INFO", _ZoneName);
            }
            string[] ttl = LineFormater(lines[1]);
            if (ttl[0].Equals("$TTL"))
            {
                TTL = new TimeSpan(Int32.Parse(ttl[1]));
                Console.WriteLine("{0} : TTL {1}", "INFO", ttl[1]);
            }

            _MasterFile = new MasterFile(TTL);

            foreach (string line in lines)
            {
                if(line.Length == 0)
                {
                    continue;
                }

                string[] values = LineFormater(line);
                if (values[0].ToUpper().Equals("$ORIGIN") || values[0].ToUpper().Equals("$TTL"))
                {
                    continue;
                }

                if (values[1].ToUpper().Equals("IN"))
                {
                    switch (values[2].ToUpper())
                    {
                        case "A":
                            AddIPAddressResourceRecord(values[0], values[3]);
                            break;
                        case "CNAME":
                            AddCanonicalNameResourceRecord(values[0], values[3]);
                            break;
                    }
                }
                
            }
        }

        public void Start()
        {
            _Server = new DnsServer(_MasterFile);
            MainAsync(_Server);
            _isRunning = true;
        }

        public void Stop()
        {
            Console.WriteLine("{0} : Stopping server ...", "INFO");
            _Server.Dispose();
            _isRunning = false;
            Console.WriteLine("{0} : Server stopped.", "INFO");

        }

        public void Restart()
        {
            Stop();
            Start();
        }

        private async static Task MainAsync(DnsServer server)
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

            Console.WriteLine("{0} : Server DNS started", "INFO");
            await server.Listen().ConfigureAwait(false);
        }

        private string[] LineFormater(string line)
        {
            string _line = line.Replace("\t", " ");
            string[] values = _line.Split(" ");
            values = values.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            return values;
        }

        private string getDomain(string domain)
        {
            string _domain = domain;
            if (_isZoneFile && domain != _ZoneName)
            {
                _domain = String.Format("{0}.{1}", domain, _ZoneName);
            }

            return _domain;
        }

        private void AddIPAddressResourceRecord(string domain, string ip)
        {
            try
            {
                _MasterFile.AddIPAddressResourceRecord(getDomain(domain), ip);
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
                _MasterFile.AddCanonicalNameResourceRecord(getDomain(domain), cname);
                Console.WriteLine("{0} : Adding CNAME Record : {1} => {2}", "INFO", getDomain(domain), cname);
            }
            catch (InvalidCastException e)
            {
                Console.WriteLine("{0} : {1}", "ERROR", e);
            }
        }
    }
}
