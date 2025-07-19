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
    [Activity(Label = "AdminNote")]
    public class AdminNote : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.adminnote);
            TextView txtpass = FindViewById<TextView>(Resource.Id.xtextPassword);
            Button backbtn = FindViewById<Button>(Resource.Id.xbackbtn);

            txtpass.Text = "\nPassword: slay@lehitimo2023";
            backbtn.Click += Backbtn_Click;
        }
        private void Backbtn_Click(object sender, EventArgs e)
        {
            Intent nextpage = new Intent(this, typeof(AdminMenu));
            StartActivity(nextpage);
        }
    }
}