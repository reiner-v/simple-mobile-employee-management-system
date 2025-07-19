using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_Lehitimo
{
    internal class CreateID
    {
        Random rnd;
        public string CreationID(int minID, int maxID)
        {
            rnd = new Random();
            int id = rnd.Next(minID, maxID);
            return id.ToString();
        }
    }
}