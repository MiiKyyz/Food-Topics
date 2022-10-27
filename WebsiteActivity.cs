using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Webkit;
using static Java.Util.Jar.Attributes;

namespace Food_drink_2._0
{
    [Activity(Label = "WebsiteActivity", Theme = "@style/Theme.AppCompat.NoActionBar")]
    public class WebsiteActivity : AppCompatActivity
    {

        WebView webView;
        Button back_btn;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Website);



            back_btn = FindViewById<Button>(Resource.Id.back_btn);

            back_btn.Click += Back_btn_Click;

            string link="";

            link = Intent.GetStringExtra("link" ?? "not recv");




            webView = FindViewById<WebView>(Resource.Id.webView);
            webView.Settings.JavaScriptEnabled = true;
            webView.LoadUrl(link);



            // Create your application here
        }


    

        private void Back_btn_Click(object sender, EventArgs e)
        {

            base.OnBackPressed();

         
        }
    }
}