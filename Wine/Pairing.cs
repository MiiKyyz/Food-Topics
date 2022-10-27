using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Food_drink_2._0
{
    public class Pairing : AndroidX.Fragment.App.Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }
        View view;
        Button btn, buy;
        AutoCompleteTextView txt_auto;
        Drink Drink = new Drink();
        TextView Title, Description, wines;
        ImageView img;
        LinearLayout linearLayout;
        List<string> ingredients_txt = new List<string>();
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
       
         

            view = inflater.Inflate(Resource.Layout.pairing_layout, container, false);
            IngridientList();

            btn = view.FindViewById<Button>(Resource.Id.btn);
            buy = view.FindViewById<Button>(Resource.Id.buy);
            txt_auto = view.FindViewById<AutoCompleteTextView>(Resource.Id.txt_auto);
            Title = view.FindViewById<TextView>(Resource.Id.Title);
            wines = view.FindViewById<TextView>(Resource.Id.wines);
            Description = view.FindViewById<TextView>(Resource.Id.Description);


            linearLayout = view.FindViewById<LinearLayout>(Resource.Id.linearLayout);

            linearLayout.Alpha = 0;

            img = view.FindViewById<ImageView>(Resource.Id.img);



            ArrayAdapter adapter = new ArrayAdapter(Context, Resource.Layout.panel, Resource.Id.txt, ingredients_txt);
            txt_auto.Adapter = adapter;


            btn.Click += Btn_Click;


            buy.Click += Buy_Click;

            return view;
        }

        private async void Buy_Click(object sender, EventArgs e)
        {

            string url = Drink.buy();
            Intent Buy = new Intent(Context, typeof(WebsiteActivity));
            Buy.PutExtra("link", url);
            StartActivity(Buy);


        }

        private async void Btn_Click(object sender, EventArgs e)
        {


            if(txt_auto.Text != "")
            {

                await Drink.PairingWine(txt_auto, Title, Description, img, buy, wines, linearLayout);

            }
        }


        public void IngridientList()
        {


            Stream data = Activity.Assets.Open(@"lista.txt");

            using (StreamReader reader = new StreamReader(data))
            {

                string line;
                int count = 0;

                while ((line = reader.ReadLine()) != null)
                {

                    try
                    {

                        ingredients_txt.Add(line.Split(';')[0]);
                        count += 1;

                    }
                    catch
                    {

                    }
                }

            

            }

        }
    }
}