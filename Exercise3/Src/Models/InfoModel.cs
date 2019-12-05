using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web;

namespace Excercise3.Models
{
    public class InfoModel
    {
        //position/latitude-deg = '21.32257526' (double)
        private static string GetValue(string response)
        {
            int start = response.IndexOf('\'');
            int end = response.LastIndexOf('\'');
            if (start == end) return "-1";
            ++start;
            return response.Substring(start, end - start);
        }

        public static Location GetLocation(string ip, int port)
        {
            Location loc = new Location();
            TcpClient client = new TcpClient();
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);

            // try to connect 3 times
            for(int i = 0; i < 3; ++i)
            {
                client.Connect(ep);
                if (client.Connected) break;
            }
            if (!client.Connected)
            {
                throw new Exception("Could not connect");
            }
            NetworkStream ns = client.GetStream();
            StreamReader rd = new StreamReader(ns, Encoding.ASCII);
            string res;
            byte[] buf = Encoding.ASCII.GetBytes("get /position/latitude-deg\r\n");
            ns.Write(buf, 0, buf.Length);
            res = rd.ReadLine();
            loc.Lat = GetValue(res);
            buf = Encoding.ASCII.GetBytes("get /position/longitude-deg\r\n");
            ns.Write(buf, 0, buf.Length);
            res = rd.ReadLine();
            loc.Lon = GetValue(res);
            client.Close();

            return loc;
            /*
            Random r = new Random();
            Location l = new Location();
            l.Lon = (r.Next(360) - 180).ToString();
            l.Lat = (r.Next(180) - 90).ToString();
            return l;*/
        }
    }
}