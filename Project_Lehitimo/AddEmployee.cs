using Android.App;
using Android.Content;
using Android.Media.TV;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using Java.Util.Functions;
using Org.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using static Android.Provider.UserDictionary;

namespace Project_Lehitimo
{
    [Activity(Label = "AddEmployee")]
    public class AddEmployee : Activity
    {
        REST retrieve,insert,getIP;
        Intent nextpage;

        EditText empidtxt, nametxt, joinedtxt;
        Spinner branchspin, positionspin;
        RadioButton adminrbtn, clientrbtn;
        Button addbtn, backbtn;

        string url, res,ip,role;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.addemployee);

            empidtxt = FindViewById<EditText>(Resource.Id.xempidtxt);
            nametxt = FindViewById<EditText>(Resource.Id.xnametxt);
            branchspin = FindViewById<Spinner>(Resource.Id.xbranchspin);
            positionspin = FindViewById<Spinner>(Resource.Id.xpostionspin);
            joinedtxt = FindViewById<EditText>(Resource.Id.xjoinedtxt);
            adminrbtn = FindViewById<RadioButton>(Resource.Id.xadminrbtn);
            clientrbtn = FindViewById<RadioButton>(Resource.Id.xclientrbtn);
            addbtn = FindViewById<Button>(Resource.Id.xaddbtn);
            backbtn = FindViewById<Button>(Resource.Id.xbackbtn);

            CreateID createid = new CreateID();
            empidtxt.Text = createid.CreationID(000000, 999999);

            getIP = new REST(url = "");
            ip = getIP.RESTip();
            PopulateSpinner("http://"+ ip +"/LEHITIMO/REST/branches.php/", "place", branchspin);
            PopulateSpinner("http://" + ip+ "/LEHITIMO/REST/positions.php/", "sposition", positionspin);

            adminrbtn.Click += RbGroup_Click;
            clientrbtn.Click += RbGroup_Click;

            addbtn.Click += Addbtn_Click; //haven't done yet
            backbtn.Click += Backbtn_Click;
            joinedtxt.Text = DateTime.Now.ToString("yyyy MMMM dd");

        }

        private void RbGroup_Click(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            role = rb.Text;
        }


        private void Backbtn_Click(object sender, EventArgs e)
        {
            nextpage = new Intent(this, typeof(AdminMenu));
            StartActivity(nextpage);
        }

        private void Addbtn_Click(object sender, EventArgs e)
        {

            url = "http://"+ip+ "/LEHITIMO/REST/add_employee.php?EmpID=" + empidtxt.Text + " &Emp_Name=" + nametxt.Text + " &Emp_Branch=" + branchspin.SelectedItem.ToString() + " &Emp_Pos=" + positionspin.SelectedItem.ToString() + " &Emp_Joined=" + joinedtxt.Text;
            insert = new REST(url);
            insert.RESTdata();

            url ="http://"+ip+"/LEHITIMO/REST/add_account.php?EmpID=" + empidtxt.Text + " &rs=" + role;
            insert = new REST(url);
            res = insert.RESTdata(); ;
            Toast.MakeText(this, res, ToastLength.Long).Show();

            nametxt.Text = "";
            branchspin.SetSelection(0); // go to the first index or value
            positionspin.SetSelection(0);
            clientrbtn.Selected = false;//doesn't work
            adminrbtn.Selected = false;

            return;
        }

        private void PopulateSpinner(string urlstr, string rootvalue, Spinner spin)
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
                all[c] = ul.GetProperty(rootvalue).ToString();
            }
            //---------------------------------spinner--------add a value or item
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, all);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spin.Adapter = adapter;
        }

    }
}