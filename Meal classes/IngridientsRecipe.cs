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

    public class IngridientsRecipe : AndroidX.Fragment.App.Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        View view;
        Spinner spinner;
        SpinnerAdapter adapter;


        List<string> name = new List<string>() { "miky", "ortiz", "deleon"};


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment

            view = inflater.Inflate(Resource.Layout.ingridient_recipe_layout, container, false);

            spinner = view.FindViewById<Spinner>(Resource.Id.spinner);


            adapter = new SpinnerAdapter(Context, name);
            spinner.Adapter = adapter;




            return view;



        }

    

      
    }
}