using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static Android.Content.ClipData;
using static Android.Icu.Text.CaseMap;

namespace Food_drink_2._0
{
    public class Recommendation : AndroidX.Fragment.App.Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }
        View view;
        AutoCompleteTextView txt_auto;
        Button btn;
        ListView listWine;
        List<string> list_wine = new List<string>();
        Drink drink = new Drink();
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return 

            view = inflater.Inflate(Resource.Layout.recommendation_layout, container, false);


            txt_auto = view.FindViewById<AutoCompleteTextView>(Resource.Id.txt_auto);
            btn = view.FindViewById<Button>(Resource.Id.btn);
            listWine = view.FindViewById<ListView>(Resource.Id.listWine);
            FileWine();
            ArrayAdapter adapter = new ArrayAdapter(Context, Resource.Layout.panel, Resource.Id.txt, list_wine);

            txt_auto.Adapter = adapter;



            btn.Click += Btn_Click;

            listWine.ItemClick += ListWine_ItemClick;

            return view;
        }

        private async void ListWine_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {

            List<string> data = new List<string>();

            data = await drink.RecommendedPicked(drink.titles[e.Position]);

            


            Intent next = new Intent(Context, typeof(RecommendedPickedActivity));
            next.PutExtra("title", data[0]);
            next.PutExtra("description", data[1]);
            next.PutExtra("price", data[2]);
            next.PutExtra("imageUrl", data[3]);
            next.PutExtra("link", data[4]);
            StartActivity(next);
            
            Toast.MakeText(Context, $"{data.Count}", ToastLength.Short).Show();
        }

        private async void Btn_Click(object sender, EventArgs e)
        {

            if (txt_auto.Text != "")
            {
                await drink.Recommendation(listWine, Context, btn, txt_auto.Text);

            }
            else
            {
                Toast.MakeText(Context, "Empty Box", ToastLength.Short).Show();


            }

            
        }

        private void FileWine()
        {
            Stream data = Activity.Assets.Open(@"wines.txt");

            using (StreamReader reader = new StreamReader(data))
            {

                string line;
                int count = 0;
                while ((line = reader.ReadLine()) != null)
                {

                    list_wine.Add(line);
                    count += 1;

                }

                Log.Info("Lenght: ", count.ToString());

            }



        }

    }
}