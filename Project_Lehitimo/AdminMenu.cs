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
    [Activity(Label = "AdminMenu")]
    public class AdminMenu : Activity
    {
        Intent nextpage;
        Button sebtn,addbtn,logoutbtn,notebtn, cpassbtn;

        string empid;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.admin_menu);

            sebtn = FindViewById<Button>(Resource.Id.xsebtn);
            addbtn = FindViewById<Button>(Resource.Id.xaddbtn);
            notebtn = FindViewById<Button>(Resource.Id.xnotebtn);
            cpassbtn = FindViewById<Button>(Resource.Id.xcpasswordbtn);
            logoutbtn = FindViewById<Button>(Resource.Id.xlogoutbtn);

            empid = Intent.GetStringExtra("employeeid");

            sebtn.Click += Sebtn_Click;
            addbtn.Click += Addbtn_Click;
            notebtn.Click += Notebtn_Click;
            cpassbtn.Click += Cpassbtn_Click;
            logoutbtn.Click += Logoutbtn_Click;
        }

        private void Cpassbtn_Click(object sender, EventArgs e)
        {
            nextpage = new Intent(this, typeof(ChangePassword));
            nextpage.PutExtra("employeeid", empid);
            StartActivity(nextpage);
        }

        private void Notebtn_Click(object sender, EventArgs e)
        {
            nextpage = new Intent(this, typeof(AdminNote)); //go to login
            StartActivity(nextpage);
        }

        private void Logoutbtn_Click(object sender, EventArgs e)
        {
            nextpage = new Intent(this, typeof(MainActivity)); //go to login
            Toast.MakeText(this, "Goodbye", ToastLength.Long).Show();
            StartActivity(nextpage);
        }

        private void Addbtn_Click(object sender, EventArgs e)
        {
            nextpage = new Intent(this, typeof(AddEmployee)); //go to add employee
            //nextpage.PutExtra("employeeid", empid);
            StartActivity(nextpage);
            
        }

        private void Sebtn_Click(object sender, EventArgs e)
        {
            nextpage = new Intent(this, typeof(SeEmployee)); //go to search and edit
            //nextpage.PutExtra("employeeid", empid);
            StartActivity(nextpage);
        }
    }
}