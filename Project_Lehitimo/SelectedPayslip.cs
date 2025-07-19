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
using System.Text.Json;

namespace Project_Lehitimo
{
    [Activity(Label = "SelectedPayslip")]
    public class SelectedPayslip : Activity
    {
        Intent nextpage;
        REST getIP, retrieve;

        Spinner payperiodspin;
        Button vpayslipbtn, backbtn;

        string res,url,empid,ip;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.selectpayslip);

            payperiodspin = FindViewById<Spinner>(Resource.Id.xperiodspin);
            vpayslipbtn = FindViewById<Button>(Resource.Id.xvpayslipbtn);
            backbtn = FindViewById<Button>(Resource.Id.xbackbtn);

            empid = Intent.GetStringExtra("employeeid");

            getIP = new REST(url = "");
            ip = getIP.RESTip();

            PopulateSpinner();
            backbtn.Click += Backbtn_Click;
            vpayslipbtn.Click += Vpayslipbtn_Click;
        }

        private void Vpayslipbtn_Click(object sender, EventArgs e)
        {
            nextpage = new Intent(this, typeof(PayslipInfo));
            nextpage.PutExtra("employeeid", empid);
            nextpage.PutExtra("payperiod", payperiodspin.SelectedItem.ToString());
            StartActivity(nextpage);
        }

        private void Backbtn_Click(object sender, EventArgs e)
        {
            nextpage = new Intent(this, typeof(ClientMenu));
            StartActivity(nextpage);
        }
        private void PopulateSpinner()
        {
            url = "http://"+ ip +"/LEHITIMO/REST/payperiod_history.php?empID=" + empid;
            retrieve = new REST(url);
            res = retrieve.RESTdata();
            using JsonDocument doc = JsonDocument.Parse(res);
            JsonElement root = doc.RootElement;

            int rootcount = root.GetArrayLength();
            string[] all = new string[rootcount];
            for (int c = 0; c < rootcount; c++)
            {
                var ul = root[c];
                all[c] = ul.GetProperty("payperiod").ToString();
            }
            //---------------------------------spinner--------add a value or item
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, all);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            payperiodspin.Adapter = adapter;
        }
    }
}