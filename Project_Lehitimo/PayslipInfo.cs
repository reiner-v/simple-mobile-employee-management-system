using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace Project_Lehitimo
{
    [Activity(Label = "PayslipInfo")]
    public class PayslipInfo : Activity
    {
        Intent nextpage;
        REST retrieve, getIP;

        EditText paysliptxt, empidtxt, wdaystxt, tearningtxt, tdeductiontxt, npaytxt, salarytxt;
        Button backbtn, confirmbtn;
        Spinner monspin;
        string ip, url, res, empid,payperiod;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.addpayslip);

            TextView title = FindViewById<TextView>(Resource.Id.textView1);
            paysliptxt = FindViewById<EditText>(Resource.Id.xpayidtxt);
            empidtxt = FindViewById<EditText>(Resource.Id.xempidtxt);
            monspin = FindViewById<Spinner>(Resource.Id.xmonthspin);
            wdaystxt = FindViewById<EditText>(Resource.Id.xwdaystx);
            tearningtxt = FindViewById<EditText>(Resource.Id.xtearningtxt);
            tdeductiontxt = FindViewById<EditText>(Resource.Id.txtdeductiontxt);
            npaytxt = FindViewById<EditText>(Resource.Id.xnpaytxt);
            salarytxt = FindViewById<EditText>(Resource.Id.xsalarytxt);
            confirmbtn = FindViewById<Button>(Resource.Id.xconfirmbtn);
            backbtn = FindViewById<Button>(Resource.Id.xbackbtn);
            TextView montxt = FindViewById<TextView>(Resource.Id.textView4);

            empid = Intent.GetStringExtra("employeeid");
            payperiod = Intent.GetStringExtra("payperiod");
            empid = empid.Trim();
            payperiod = payperiod.Trim();

            getIP = new REST(url = "");
            ip = getIP.RESTip();

            title.Text = "PAYSLIP: " + payperiod.ToUpper();

            confirmbtn.Visibility = ViewStates.Gone;
            monspin.Visibility = ViewStates.Gone;
            montxt.Visibility = ViewStates.Gone;

            DisplayPayslip();
            backbtn.Click += Backbtn_Click;
        }

        private void Backbtn_Click(object sender, EventArgs e)
        {
            nextpage = new Intent(this, typeof(SelectedPayslip));
            nextpage.PutExtra("employeeid", empid);
            StartActivity(nextpage);
        }
        
        private void DisplayPayslip()
        {
            url = "http://"+ip+"/LEHITIMO/REST/search_payslip.php?empID=" + empid + " &empPeriod="+ payperiod;
            retrieve = new REST(url);
            res = retrieve.RESTdata();
            using JsonDocument doc = JsonDocument.Parse(res);
            JsonElement root = doc.RootElement;

            int rootcount = root.GetArrayLength();
            var ul = root[0];

            paysliptxt.Text = ul.GetProperty("payslipid").ToString();
            empidtxt.Text = empid;
            //empidtxt.Text = ul.GetProperty("employeeid").ToString();
            //montxt.Text = ul.GetProperty("payperiod").ToString();
            wdaystxt.Text = ul.GetProperty("workdays").ToString();
            salarytxt.Text = ul.GetProperty("empsalary").ToString();
            tearningtxt.Text = ul.GetProperty("tearning").ToString();
            tdeductiontxt.Text = ul.GetProperty("tdeduction").ToString();
            npaytxt.Text = ul.GetProperty("netpay").ToString();
        }
    }
    
}