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
    [Activity(Label = "ClientMenu")]
    public class ClientMenu : Activity
    {
        Intent nextpage;

        Button profbtn, vpslipbtn, cpassbtn,logoutbtn;

        string empid;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.client_menu);

            profbtn = FindViewById<Button>(Resource.Id.xprofilebtn);
            vpslipbtn = FindViewById<Button>(Resource.Id.xvpayslipbtn);
            cpassbtn = FindViewById<Button>(Resource.Id.xcpasswordbtn);
            logoutbtn = FindViewById<Button>(Resource.Id.xlogoutbtn);

            empid = Intent.GetStringExtra("employeeid");

            profbtn.Click += Profbtn_Click;
            vpslipbtn.Click += Vpslipbtn_Click;
            cpassbtn.Click += Cpassbtn_Click;
            logoutbtn.Click += Logoutbtn_Click;
        }

        private void Logoutbtn_Click(object sender, EventArgs e)
        {
            nextpage = new Intent(this, typeof(MainActivity)); //go to login
            Toast.MakeText(this, "Goodbye", ToastLength.Long).Show();
            StartActivity(nextpage);
        }

        private void Cpassbtn_Click(object sender, EventArgs e)
        {
            nextpage = new Intent(this, typeof(ChangePassword));
            nextpage.PutExtra("employeeid", empid);
            StartActivity(nextpage);
        }

        private void Vpslipbtn_Click(object sender, EventArgs e)
        {
            nextpage = new Intent(this, typeof(SelectedPayslip));
            nextpage.PutExtra("employeeid", empid);
            StartActivity(nextpage);
        }

        private void Profbtn_Click(object sender, EventArgs e)
        {
            nextpage = new Intent(this, typeof(EmployeeInfo));
            nextpage.PutExtra("employeeid", empid);
            StartActivity(nextpage);
        }
    }
}