using System.Net;
using System.Net.Sockets;
using Utilitaires.Random;

namespace LibrairieUtil
{
    public static class GestionReseau
    {


        public static IPAddress GetMyLocalIp()
        {
            IPHostEntry host;
            IPAddress localIp = null;
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIp = ip;
                }
            }
            return localIp;

        }

        //penser gestion erreur
        public static int TrouverPort(int nmbreTentatives)
        {
            int port = -1;
            int i =0;
            IPAddress ip = GetMyLocalIp();
            RandomManager rm = new RandomManager();
            while(port==-1 &&i<nmbreTentatives)
            {
                int t = rm.GetInt(65000);
                try
                {
                    Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    IPEndPoint tempo = new IPEndPoint(ip, t);
                    s.Bind(tempo);
                    s.Dispose();

                    port = t;
                }
                catch
                {
                   
                    i++;
                }
            }
            return port;

        }
    }
}
