using System;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using DNS.Server;
using DNS.Client;
using System.Text.Json;
using System.Net;
using System.Net.NetworkInformation;

namespace simpleDNS
{
    class ZoneManager
    {
        private string _ZoneName;
        private TimeSpan _TTL = new TimeSpan(0, 0, 0,3600);
        private MasterFile _MasterFile;
        private DnsServer _Server;
        private bool _isZoneFile = false;
        private bool _isRunning = false;

        public ZoneManager() { }

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
            get { return _isRunning; }
        }

        private static void Info(string message)
        {
            Console.WriteLine("[INF] | {0}", message);
        }
        private static void Error(object message)
        {
            Console.WriteLine("[ERR] | {0}", (string)message);
        }
        private static string BeautifyJson(object _object)
        {
            string _json = JsonSerializer.Serialize(_object);
            return BeautifyJson(_json);
        }
        private static string BeautifyJson(string _json)
        {
            using JsonDocument document = JsonDocument.Parse(_json);
            using var stream = new MemoryStream();
            using var writer = new Utf8JsonWriter(stream, new JsonWriterOptions() { Indented = true });
            document.WriteTo(writer);
            writer.Flush();
            return Encoding.UTF8.GetString(stream.ToArray());
        }

        private static IPAddress GetDnsAdress()
        {
            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface networkInterface in networkInterfaces)
            {
                if (networkInterface.OperationalStatus == OperationalStatus.Up)
                {
                    IPInterfaceProperties ipProperties = networkInterface.GetIPProperties();
                    IPAddressCollection dnsAddresses = ipProperties.DnsAddresses;

                    foreach (IPAddress dnsAdress in dnsAddresses)
                    {
                        return dnsAdress;
                    }
                }
            }

            throw new InvalidOperationException("Unable to find DNS Address");
        }

        public void LoadFromFile(string Path)
        {
            Info($"Loading zone from file : {Path}");
            StreamReader reader = new StreamReader(Path);
            LoadFromText(reader.ReadToEnd());
        }

        public void Save(string ZoneFile, string Path)
        {
            File.WriteAllText(Path, ZoneFile);
        }

        public void LoadFromText(string text)
        {
            Info("Loading zone ...");
            string[] lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            string[] origin = LineFormater(lines[0]);
            if (origin[0].Equals("$ORIGIN"))
            {
                _isZoneFile = true;
                _ZoneName = origin[1].ToLower();
                if (!_ZoneName.Substring(_ZoneName.Length - 1).Equals('.'))
                {
                    // temporary disabled
                    //_ZoneName += ".";
                }
                Info($"Domain {_ZoneName}");
            }
            if(lines.Length > 1)
            {
                string[] ttl = LineFormater(lines[1]);
                if (ttl[0].Equals("$TTL"))
                {
                    TTL = new TimeSpan(0, 0, 0, Int32.Parse(ttl[1]));
                    Info($"TTL {ttl[1]}");
                }
            }
            

            _MasterFile = new MasterFile(TTL);

            foreach (string line in lines)
            {
                if (line.Length == 0)
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
                        case "NS":
                            AddNameServerResourceRecord(values[0], values[3]);
                            break;
                    }
                }

            }
        }

        public void Start()
        {
            //_Server = new DnsServer(_MasterFile, GetDnsAdress());
            _Server = new DnsServer(_MasterFile);
            MainAsync(_Server);
            _isRunning = true;
        }

        public void Stop()
        {
            Info("Stopping server ...");
            try
            {
                _Server.Dispose();
                _isRunning = false;
                Info("Server stopped.");
            } catch(Exception e)
            {
                Error(e.Message);
            }

        }

        public void Restart()
        {
            Stop();
            Start();
        }

        private async static Task MainAsync(DnsServer server)
        {

            Info("Starting DNS ...");

            server.Responded += (sender, e) =>
            {
                Console.WriteLine($"[REM] | {e.Remote.Address}:{e.Remote.Port}");
                Console.WriteLine($"[REQ] | {BeautifyJson(e.Request)}");
                Console.WriteLine($"[ANS] | {BeautifyJson(e.Response)}");
            };
            server.Listening += (sender, e) => Info("Listening ...");
            server.Errored += (sender, e) =>
            {
                Error(e.Exception.Message);
                ResponseException responseError = e.Exception as ResponseException;
                if (responseError != null) Error(BeautifyJson(responseError.Response));
            };

            Info("Server DNS started.");
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
                Info($"Adding A Record : {getDomain(domain)} => {ip}");
                _MasterFile.AddIPAddressResourceRecord(getDomain(domain), ip);
            }
            catch (InvalidCastException e)
            {
                Error(e);
            }

        }

        public void AddCanonicalNameResourceRecord(string domain, string cname)
        {
            try
            {
                Info($"Adding CNAME Record : {getDomain(domain)} => {cname}");
                _MasterFile.AddCanonicalNameResourceRecord(getDomain(domain), cname);

            }
            catch (InvalidCastException e)
            {
                Error(e);
            }
        }

        public void AddNameServerResourceRecord(string domain, string nsDomain)
        {
            try
            {
                _MasterFile.AddNameServerResourceRecord(getDomain(domain), nsDomain);
                Info($"Adding NS Record : {getDomain(domain)} => {nsDomain}");
            }
            catch (InvalidCastException e)
            {
                Error(e);
            }
        }
    }
}
