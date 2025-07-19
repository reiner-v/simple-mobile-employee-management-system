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
using static Android.Provider.Contacts.Intents;

namespace Project_Lehitimo
{
    [Activity(Label = "SeEmployee")]
    public class SeEmployee : Activity
    {
        Intent nextpage;
        REST retrieve,getIP,insert;

        EditText searchtxt, nametxt, empidtxt, joinedtxt;
        Spinner branchspin, positionspin;
        Button searchbtn, updatebtn, payslipbtn, backbtn;

        string url, res,ip;
        string[] all , allbranches, allposition;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.seemployee);

            searchtxt = FindViewById<EditText>(Resource.Id.xsearchtxt);
            nametxt = FindViewById<EditText>(Resource.Id.xnametxt);
            empidtxt = FindViewById<EditText>(Resource.Id.xempidtxt);
            branchspin = FindViewById<Spinner>(Resource.Id.xbranchspin);
            positionspin = FindViewById<Spinner>(Resource.Id.xpostionspin);
            joinedtxt = FindViewById<EditText>(Resource.Id.xjoinedtxt);
            searchbtn = FindViewById<Button>(Resource.Id.xsearchbtn);
            payslipbtn = FindViewById<Button>(Resource.Id.xpayslipbtn);
            updatebtn = FindViewById<Button>(Resource.Id.xupdatebtn);
            backbtn = FindViewById<Button>(Resource.Id.xbackbtn);

            getIP = new REST(url = "");
            ip = getIP.RESTip();
            allbranches = PopulateSpinner("http://" + ip + "/LEHITIMO/REST/branches.php/", "place", branchspin);
            allposition = PopulateSpinner("http://" + ip + "/LEHITIMO/REST/positions.php/", "sposition", positionspin);

            searchbtn.Click += Searchbtn_Click;
            updatebtn.Click += Updatebtn_Click;
            payslipbtn.Click += Payslipbtn_Click;
            backbtn.Click += Backbtn_Click;
        }

        private void Payslipbtn_Click(object sender, EventArgs e)
        {
            nextpage = new Intent(this, typeof(AddPayslip));
            nextpage.PutExtra("employeeid", empidtxt.Text);
            nextpage.PutExtra("position", positionspin.SelectedItem.ToString());
            StartActivity(nextpage);
        }

        private void Updatebtn_Click(object sender, EventArgs e)
        {
            url = "http://" + ip + "/LEHITIMO/REST/update_employee.php?empID=" + empidtxt.Text + " &Emp_Pos=" + positionspin.SelectedItem + " &Emp_Branch=" + branchspin.SelectedItem;
            insert = new REST(url);
            res = insert.RESTdata();
            Toast.MakeText(this, res, ToastLength.Long).Show();
        }

        private void Searchbtn_Click(object sender, EventArgs e) // note: validate when empid doesn't exist
        {
            retrieve = new REST("http://" + ip + "/LEHITIMO/REST/search_employee.php?empID="+ searchtxt.Text);
            res = retrieve.RESTdata();
            using JsonDocument doc = JsonDocument.Parse(res);
            JsonElement root = doc.RootElement;

            int rootcount = root.GetArrayLength();
            var ul = root[0];

            empidtxt.Text = ul.GetProperty("employeeid").ToString();
            nametxt.Text = ul.GetProperty("name").ToString();   
            joinedtxt.Text = ul.GetProperty("joined").ToString();
            for(int i = 0; i < allbranches.Length; i++)
            {
                
                if (allbranches[i] == ul.GetProperty("branch").ToString().Trim())
                {
                    branchspin.SetSelection(i);
                }
            }
            for (int i = 0; i < allposition.Length; i++)
            {
                if (allposition[i] == ul.GetProperty("position").ToString().Trim())
                {
                    positionspin.SetSelection(i);
                }
            }
        }

        private void Backbtn_Click(object sender, EventArgs e)
        {
            nextpage = new Intent(this, typeof(AdminMenu));
            StartActivity(nextpage);
        }

        private string[] PopulateSpinner(string urlstr, string rootvalue, Spinner spin)
        {
            url = urlstr;
            retrieve = new REST(url);
            res = retrieve.RESTdata();
            using JsonDocument doc = JsonDocument.Parse(res);
            JsonElement root = doc.RootElement;

            int rootcount = root.GetArrayLength();
            all = new string[rootcount];
            for (int c = 0; c < rootcount; c++)
            {
                var ul = root[c];
                all[c] = ul.GetProperty(rootvalue).ToString();
            }
            //---------------------------------spinner--------add a value or item
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, all);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spin.Adapter = adapter;
            return all;
        }
    }
}