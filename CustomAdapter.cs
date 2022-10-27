using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Util.Zip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Food_drink_2._0
{
    internal class CustomAdapter : BaseAdapter
    {


        Context context;
        List<string> title = new List<string>();
        List<Bitmap> images = new List<Bitmap>();
        LayoutInflater LayoutInflater;

        public CustomAdapter(Context context_, List<string> title_, List<Bitmap> images_)
        {

            this.context = context_;
            this.title = title_;
            this.images = images_;
            LayoutInflater = LayoutInflater.From(context);



        }


        public override int Count => title.Count;

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return 0;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            convertView = LayoutInflater.Inflate(Resource.Layout.panel, parent, false);

            TextView text = convertView.FindViewById<TextView>(Resource.Id.txt);
            ImageView img = convertView.FindViewById<ImageView>(Resource.Id.img);

            text.Text = title[position];
            img.SetImageBitmap(images[position]);

            return convertView;

        }
    }
}