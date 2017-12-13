using System;

namespace IRemote
{
    [Serializable]
    public class ServerInfo
    {
        public ServerInfo()
        {
        }

        public ServerInfo(string ip, int port, string serverAlias, string version)
        {
            this._ip = ip;
            this._port = port;
            this._serverAlias = serverAlias;
            this._version = version;
        }

        public string IP
        {
            get
            {
                return this._ip;
            }
            set
            {
                this._ip = value;
            }
        }

        public int Port
        {
            get
            {
                return this._port;
            }
            set
            {
                this._port = value;
            }
        }

        public string ServerAlias
        {
            get
            {
                return this._serverAlias;
            }
            set
            {
                this._serverAlias = value;
            }
        }

        public string Version
        {
            get
            {
                return this._version;
            }
            set
            {
                this._version = value;
            }
        }

        public string MonitorServer_IP
        {
            get
            {
                return this._monitor_IP;
            }
            set
            {
                this._monitor_IP = value;
            }
        }

        public int MonitorServer_Port
        {
            get
            {
                return this._monitor_port;
            }
            set
            {
                this._monitor_port = value;
            }
        }

        private string _ip;

        private int _port;

        private string _serverAlias;

        private string _version;

        private string _monitor_IP;

        private int _monitor_port;
    }
}
