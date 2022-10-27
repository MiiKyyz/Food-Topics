using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Food_drink_2._0
{
    public class DishWine : AndroidX.Fragment.App.Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }
        View view;
        Drink drink = new Drink();
        AutoCompleteTextView txt_auto;
        Button btn_search;
        TextView wine_txt, description, deserts;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment

            view = inflater.Inflate(Resource.Layout.dish_wine, container, false);
            txt_auto = view.FindViewById<AutoCompleteTextView>(Resource.Id.txt_auto);
            btn_search = view.FindViewById<Button>(Resource.Id.btn_search);


            wine_txt = view.FindViewById<TextView>(Resource.Id.wine_txt);
            description = view.FindViewById<TextView>(Resource.Id.description);
            deserts = view.FindViewById<TextView>(Resource.Id.deserts);

            btn_search.Click += Btn_search_Click;



            ArrayAdapter adapter = new ArrayAdapter(Context, Resource.Layout.panel, Resource.Id.txt, drink.Dish_Pairing_for_Wine);

            txt_auto.Adapter = adapter;



            return view;

            
        }

        private void Btn_search_Click(object sender, EventArgs e)
        {
            
            if(txt_auto.Text != "")
            {

                drink.Dish_Wine(txt_auto.Text, description, deserts);

                wine_txt.Text = txt_auto.Text;

                txt_auto.Text = "";
            }



        }
    }
}