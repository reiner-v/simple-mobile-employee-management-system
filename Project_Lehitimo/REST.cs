using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Project_Lehitimo
{
    internal class REST
    {
        HttpWebResponse response;
        HttpWebRequest request;
        string url,res,ipaddress;

        public REST(string urlstr)
        {
            url = urlstr;
        }

        public string RESTdata()
        {
            request = (HttpWebRequest)WebRequest.Create(url);
            response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            res = reader.ReadToEnd();
            return res;
        }
        public string RESTip()
        {
            ipaddress = "192.168.18.107";
            return ipaddress;
        }
    }
}