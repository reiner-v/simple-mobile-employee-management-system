using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using static Android.Provider.Contacts.Intents;

namespace Project_Lehitimo
{
    [Activity(Label = "EmployeeInfo")]
    public class EmployeeInfo : Activity
    {
        REST retrieve,getIP;
        Intent nextpage;

        EditText empidtxt, nametxt, branchtxt, positiontxt, joinedtxt;
        Button backbtn;

        string res,ip , url;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.employeeinfo);

            empidtxt = FindViewById<EditText>(Resource.Id.xempidtxt);
            nametxt = FindViewById<EditText>(Resource.Id.xnametxt);
            branchtxt = FindViewById<EditText>(Resource.Id.xbranchtxt);
            positiontxt = FindViewById<EditText>(Resource.Id.xpositiontxt);
            joinedtxt = FindViewById<EditText>(Resource.Id.xjoinedtxt);

            backbtn = FindViewById<Button>(Resource.Id.xbackbtn);

            empidtxt.Text = Intent.GetStringExtra("employeeid");

            getIP = new REST(url = "");
            ip = getIP.RESTip();
            allData();

            backbtn.Click += Backbtn_Click;

        }

        private void Backbtn_Click(object sender, EventArgs e)
        {
            nextpage = new Intent(this, typeof(ClientMenu));
            nextpage.PutExtra("employeeid", empidtxt.Text);
            StartActivity(nextpage);
        }

        private void allData()
        {
            retrieve = new REST("http://" + ip + "/LEHITIMO/REST/search_employee.php?empID=" + empidtxt.Text);
            res = retrieve.RESTdata();
            using JsonDocument doc = JsonDocument.Parse(res);
            JsonElement root = doc.RootElement;

            int rootcount = root.GetArrayLength();
            var ul = root[0];

            empidtxt.Text = ul.GetProperty("employeeid").ToString();
            nametxt.Text = ul.GetProperty("name").ToString();
            joinedtxt.Text = ul.GetProperty("joined").ToString();
            branchtxt.Text = ul.GetProperty("branch").ToString();
            positiontxt.Text = ul.GetProperty("position").ToString();
        }
    }
}