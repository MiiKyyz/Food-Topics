using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using AndroidX.AppCompat.App;
using System.Text;
using static Java.Util.Jar.Attributes;
using Android.Util;
using Android.Graphics;
using System.Net;

namespace Food_drink_2._0
{
    [Activity(Label = "RecommendedPickedActivity")]
    public class RecommendedPickedActivity : AppCompatActivity
    {


        Drink drink = new Drink();
        Button btn;
        TextView title, description;
        ImageView img;
        string title_, description_, price, imageUrl, link;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.recommended_picked);
            // Create your application here

            title = FindViewById<TextView>(Resource.Id.title);
            description = FindViewById<TextView>(Resource.Id.description);
            img = FindViewById<ImageView>(Resource.Id.img);
            btn = FindViewById<Button>(Resource.Id.btn);

            btn.Click += Btn_Click;
            btn.Enabled = false;
            

            title_ = Intent.GetStringExtra("title" ?? "not recv");
            description_ = Intent.GetStringExtra("description" ?? "not recv");
            price = Intent.GetStringExtra("price" ?? "not recv");
            imageUrl = Intent.GetStringExtra("imageUrl" ?? "not recv");
            link = Intent.GetStringExtra("link" ?? "not recv");


            
            WineInfo(title_, description_, price, imageUrl);

            btn.Enabled = true;
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Intent buy = new Intent(this, typeof(WebsiteActivity));
            buy.PutExtra("link", link);
            StartActivity(buy);
        }

        private void WineInfo(string title_, string description_, string price, string imageUrl)
        {
            title.Text = $"{title_}";

            description.Text = $"{description_}";
            Bitmap bitmap = GetImg(imageUrl);
            btn.Text = $"est. {price}";
            img.SetImageBitmap(bitmap);




        }

        private Bitmap GetImg(string url)
        {
            using (WebClient webClient = new WebClient())
            {
                byte[] bytes = webClient.DownloadData(url);
                if (bytes != null && bytes.Length > 0)
                {
                    return BitmapFactory.DecodeByteArray(bytes, 0, bytes.Length);
                }
            }
            return null;
        }

    }
}