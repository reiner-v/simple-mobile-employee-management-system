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
using static Android.Provider.Contacts.Intents;

namespace Project_Lehitimo
{
    [Activity(Label = "ChangePassword")]
    public class ChangePassword : Activity
    {
        Intent nextpage;
        REST insert, getIP;

        EditText currpasstxt, newpasstxt;
        Button changepassbtn, backbtn;

        string url, res, ip,empid;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.changepassword);

            currpasstxt = FindViewById<EditText>(Resource.Id.xcurrpasstxt);
            newpasstxt = FindViewById<EditText>(Resource.Id.xnewpasstxt);

            changepassbtn = FindViewById<Button>(Resource.Id.xchangepassbtn);
            backbtn = FindViewById<Button>(Resource.Id.xbackbtn);

            empid = Intent.GetStringExtra("employeeid");

            getIP = new REST(url = "");
            ip = getIP.RESTip();

            changepassbtn.Click += Changepassbtn_Click;
            backbtn.Click += Backbtn_Click;

        }

        private void Backbtn_Click(object sender, EventArgs e)
        {
            nextpage = new Intent(this, typeof(ClientMenu));
            nextpage.PutExtra("employeeid", empid);
            StartActivity(nextpage);
        }

        private void Changepassbtn_Click(object sender, EventArgs e)
        {
            url = "http://" + ip + "/LEHITIMO/REST/change_password.php?empID=" + empid + " &newpword=" + newpasstxt.Text + " &currpword=" + currpasstxt.Text;
            insert = new REST(url);
            res = insert.RESTdata();
            Toast.MakeText(this, res, ToastLength.Long).Show();
        }
    }
}