using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using Android.Views.InputMethods;
using static Android.Provider.Contacts.Intents;
using Android.Media.TV;

namespace Project_Lehitimo
{
    [Activity(Label = "AddPayslip")]
    public class AddPayslip : Activity
    {
        Intent nextpage;
        REST retrieve, getIP, insert;

        EditText paysliptxt, empidtxt, wdaystxt, tearningtxt, tdeductiontxt, npaytxt, salarytxt;
        Spinner monspin;
        Button backbtn, confirmbtn;

        string url, res, ip, pos;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.addpayslip);

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

            empidtxt.Text = Intent.GetStringExtra("employeeid");
            pos = Intent.GetStringExtra("position");

            CreateID createid = new CreateID();
            paysliptxt.Text = createid.CreationID(00000000, 99999999);

            getIP = new REST(url = "");
            ip = getIP.RESTip();
            PopulateSpinner("http://" + ip + "/LEHITIMO/REST/months.php/");
            GetSalary();
            GetTotalDeduction();
            backbtn.Click += Backbtn_Click;

            wdaystxt.TextChanged += GetNetPay;
            confirmbtn.Click += Confirmbtn_Click;

        }

        private void Confirmbtn_Click(object sender, EventArgs e)
        {

            string monthperiod = monspin.SelectedItem + " " + DateTime.Now.ToString("yyyy"); 
            url = "http://192.168.137.1:8080/LEHITIMO/REST/add_payslip.php?payslipID=" + paysliptxt.Text + " &EmpID=" + empidtxt.Text + " &mon=" +
                monthperiod + " &workdays=" + wdaystxt.Text + " &Emp_Salary=" + salarytxt.Text + " &totalE=" + tearningtxt.Text + 
                " &totalD=" + tdeductiontxt.Text + " &emp_npay=" + npaytxt.Text;

            insert = new REST(url);
            res = insert.RESTdata(); ;
            Toast.MakeText(this, res, ToastLength.Long).Show();

            nextpage = new Intent(this, typeof(SeEmployee)); //go to search and edit
            StartActivity(nextpage);
        }

        private void PopulateSpinner(string urlstr)
        {
            url = urlstr;
            retrieve = new REST(url);
            res = retrieve.RESTdata();
            using JsonDocument doc = JsonDocument.Parse(res);
            JsonElement root = doc.RootElement;

            int rootcount = root.GetArrayLength();
            string[] all = new string[rootcount];
            for (int c = 0; c < rootcount; c++)
            {
                var ul = root[c];
                all[c] = ul.GetProperty("month").ToString();
            }
            //---------------------------------spinner--------add a value or item
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, all);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            monspin.Adapter = adapter;
        }

        private void Backbtn_Click(object sender, EventArgs e)
        {
            nextpage = new Intent(this, typeof(SeEmployee));
            StartActivity(nextpage);
        }



        private void GetSalary()
        {
            url = "http://" + ip + "/LEHITIMO/REST/salary.php?emppos=" + pos;
            retrieve = new REST(url);
            res = retrieve.RESTdata();
            salarytxt.Text = res.ToString();
        }

        private void GetTotalDeduction()
        {
            url = "http://" + ip + "/LEHITIMO/REST/total_deduction.php/";
            retrieve = new REST(url);
            res = retrieve.RESTdata();
            tdeductiontxt.Text = res.ToString();
        }

        private void GetTotalEarnings()
        {
            double tearnings = double.Parse(salarytxt.Text) * int.Parse(wdaystxt.Text);
            tearningtxt.Text = tearnings.ToString();
        }

        private void GetNetPay(object sender, EventArgs e)
        {
            wdaystxt.EditorAction += (sender, e) =>
            {
                if (e.ActionId == ImeAction.Done) //validation for 30 or 31 days
                {
                    GetTotalEarnings();
                    npaytxt.Text = (double.Parse(tearningtxt.Text) - double.Parse(tdeductiontxt.Text)).ToString();
                }
                else { e.Handled = false; }
            };
        }
    }
}