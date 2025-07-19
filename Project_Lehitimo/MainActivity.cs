using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using static Android.Provider.UserDictionary;

namespace Project_Lehitimo
{
    [Activity(Label = "LEHITIMO", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Intent nextpage;
        REST retrieve, getIP;

        ImageView lehitimologo;
        EditText usernametxt, passwordtxt;
        Button loginbtn;

        string uname,pword,url,res,ip;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            lehitimologo = FindViewById<ImageView>(Resource.Id.xlogo);
            usernametxt = FindViewById<EditText>(Resource.Id.xusernametxt);
            passwordtxt = FindViewById<EditText>(Resource.Id.xpasswordtxt); 
            loginbtn = FindViewById<Button>(Resource.Id.xloginbtn);

            lehitimologo.SetImageResource(Resource.Drawable.logo);

            getIP = new REST(url = "");
            ip = getIP.RESTip();

            loginbtn.Click += Loginbtn_Click;
        }

        private void Loginbtn_Click(object sender, System.EventArgs e)
        {
            /*nextpage = new Intent(this, typeof(AdminMenu)); //go to admin
            nextpage.PutExtra("employeeid", usernametxt.Text);
            Toast.MakeText(this, "Welcome", ToastLength.Long).Show();
            StartActivity(nextpage);*/
            uname = usernametxt.Text; //employeeid
            pword = passwordtxt.Text;

            url = "http://"+ ip +"/LEHITIMO/REST/account_login.php?uname=" + uname + " &pword=" + pword;
            retrieve = new REST(url);
            res = retrieve.RESTdata();

            if (res.Contains("Admin"))
            {
                nextpage = new Intent(this, typeof(AdminMenu)); //go to admin
                nextpage.PutExtra("employeeid", uname);
                Toast.MakeText(this, "Welcome", ToastLength.Long).Show();
                StartActivity(nextpage);
            }
            else if (res.Contains("Client"))
            {
                nextpage = new Intent(this, typeof(ClientMenu));//go to client
                nextpage.PutExtra("employeeid", uname);
                Toast.MakeText(this, "Welcome", ToastLength.Long).Show();
                StartActivity(nextpage);

            }
            else
            {
                Toast.MakeText(this, res, ToastLength.Long).Show();
            }
            
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}