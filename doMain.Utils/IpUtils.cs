using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace doMain.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public class IpUtils
    {
        /// <summary>
        /// Gets the local ip address.
        /// </summary>
        /// <returns></returns>
        public static string GetLocalIPAddress()
        {
            try
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return ip.ToString();
                    }
                }
            }
            catch
            {
                return "";
            }

            return "";
        }

        /// <summary>
        /// Gets the internet ip address.
        /// </summary>
        /// <returns></returns>
        public static string GetInternetIPAddress()
        {
            try
            {
                // check IP using DynDNS's service
                WebRequest request = WebRequest.Create("http://checkip.dyndns.org");
                WebResponse response = request.GetResponse();
                StreamReader stream = new StreamReader(response.GetResponseStream());

                // IMPORTANT: set Proxy to null, to drastically INCREASE the speed of request
                //request.Proxy = null;

                // read complete response
                string ipAddress = stream.ReadToEnd();

                // replace everything and keep only IP
                return ipAddress.
                    Replace("<html><head><title>Current IP Check</title></head><body>Current IP Address: ", string.Empty).
                    Replace("</body></html>", string.Empty).Replace(CharacterUtils.SaltoLinea,"");
            }
            catch
            {
                return "";
            }
        }
    }
}
