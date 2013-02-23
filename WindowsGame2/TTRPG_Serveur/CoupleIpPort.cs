using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace TTRPG_Serveur
{
    public class CoupleIpPort : Object
    {
        IPAddress ip;
        int port;

        public CoupleIpPort(IPAddress ip, int port)
        {
            this.ip = ip;
            this.port = port;
        }


        public override int GetHashCode()
        {
            return this.ip.GetHashCode() * 10000 + this.port;
        }

        public override bool Equals(object obj)
        {
            return this.GetHashCode()==((CoupleIpPort)obj).GetHashCode();
        }
        //public override int CompareTo(object obj)
        //{

        //    try
        //    {
        //        int val = 0;
        //        CoupleIpPort cp = (CoupleIpPort)(obj);
        //        val = this.port - cp.port;
        //        if (val == 0)
        //        {
        //            val = this.ip.GetHashCode() - cp.ip.GetHashCode();
        //        }

        //        return val;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
    }
}
