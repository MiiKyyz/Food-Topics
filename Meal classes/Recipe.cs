using Android.Animation;
using Android.App;
using Android.Content;
using Android.InputMethodServices;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Java.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Java.Util.Jar.Attributes;


namespace Food_drink_2._0
{

    public class Recipe : AndroidX.Fragment.App.Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }
    
        
        

        List<string> cuisine = new List<string>()
        {

            "Select Cuisine",

            "African", "American", "British", "Cajun", "Caribbean", "Chinese", "Eastern European", "European",
            "French", "German", "Greek", "Indian", "Irish", "Italian", "Japanese", "Jewish", "Korean", "Latin American",
            "Mediterranean", "Mexican", "Middle Eastern", "Nordic", "Southern", "Spanish", "Thai", "Vietnamese"




        };
 
        List<string> Diet = new List<string>()
        {
            "Select Diet",

            "Gluten Free", "Ketogenic", "Vegetarian", "Lacto-Vegetarian", "Ovo-Vegetarian", "Vegan", "Pescetarian", "Paleo",
            "Primal", "Low FODMAP", "Whole30"
        };
        List<string> intolerances = new List<string>()
        {

            "Select Intolerances",
            "Dairy", "Egg", "Gluten", "Grain", "Peanut", "Seafood", "Sesame", "Shellfish", "Soy", "Sulfite", "Tree Nut", "Wheat"
        };

        List<string> type = new List<string>()
        {
            "Select Type",
            "main course", "side dish", "dessert", "appetizer", "salad", "bread", "breakfast", "soup", "beverage", "sauce",
            "marinade", "fingerfood", "snack", "drink"

        };

        View view;
        Button btn;
       
        List<string> ingredients_txt = new List<string>();
        List<string> extras = new List<string>();
        ListView list;
        Meal meal = new Meal();
        AutoCompleteTextView txt_auto;
        Spinner Cuisine_spinner, Diet_spinner, Intolerances_spinner, Type_spinner;


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment



            view = inflater.Inflate(Resource.Layout.recipe_layout, container, false);

            Cuisine_spinner = view.FindViewById<Spinner>(Resource.Id.Cuisine_spinner);
            Diet_spinner = view.FindViewById<Spinner>(Resource.Id.Diet_spinner);
            Intolerances_spinner = view.FindViewById<Spinner>(Resource.Id.Intolerances_spinner);
            Type_spinner = view.FindViewById<Spinner>(Resource.Id.Type_spinner);



            
            ArrayAdapter adapter_Cuisine = new ArrayAdapter(Context, Android.Resource.Layout.SimpleSpinnerItem, cuisine);
            Cuisine_spinner.Adapter = adapter_Cuisine;
            Cuisine_spinner.ItemSelected += ItemSelected;




            ArrayAdapter adapter_Diet = new ArrayAdapter(Context, Android.Resource.Layout.SimpleSpinnerItem, Diet);
            Diet_spinner.Adapter = adapter_Diet;
            Diet_spinner.ItemSelected += ItemSelected;

            ArrayAdapter adapter_Intolerances = new ArrayAdapter(Context, Android.Resource.Layout.SimpleSpinnerItem, intolerances);
            Intolerances_spinner.Adapter = adapter_Intolerances;
            Intolerances_spinner.ItemSelected += ItemSelected;

            ArrayAdapter adapter_Type = new ArrayAdapter(Context, Android.Resource.Layout.SimpleSpinnerItem, type);
            Type_spinner.Adapter = adapter_Type;
            Type_spinner.ItemSelected += ItemSelected;



            list = view.FindViewById<ListView>(Resource.Id.list);
            txt_auto = view.FindViewById<AutoCompleteTextView>(Resource.Id.txt_auto);
            btn = view.FindViewById<Button>(Resource.Id.btn);
            btn.Click += Btn_Click;
            IngridientList();


            ArrayAdapter adapter = new ArrayAdapter(Context, Android.Resource.Layout.SimpleExpandableListItem1, ingredients_txt);
            
            txt_auto.Adapter = adapter;


            list.ItemClick += List_ItemClick;



            return view;

        }



        private void ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {

            extras.Clear();
            switch (e.Parent.Id)
            {


                case Resource.Id.Cuisine_spinner:

                    string Cuisine = $"cuisine={cuisine[e.Position]}";


                    if(Cuisine != "cuisine=Select Cuisine") {

                        extras.Add(Cuisine);
                    }


                    break;
                case Resource.Id.Diet_spinner:
                    string Diet_txt = $"diet={Diet[e.Position]}";


                    if (Diet_txt != "diet=Select Diet")
                    {

                        extras.Add(Diet_txt);
                    }

                    break;
                case Resource.Id.Intolerances_spinner:
                    string Intolerances_txt = $"intolerances={intolerances[e.Position]}";


                    if (Intolerances_txt != "intolerances=Select Intolerances")
                    {

                        extras.Add(Intolerances_txt);
                    }

                    break;
                case Resource.Id.Type_spinner:
                    string Type_txt = $"type={type[e.Position]}";


                    if (Type_txt != "type=Select Type")
                    {

                        extras.Add(Type_txt);
                    }

                    break;

            }




        }



        private void List_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {



            Intent nextActivity = new Intent(Context, typeof(InformationRecipe));
            nextActivity.PutExtra("ingredient", meal.titles[e.Position]);
        
            StartActivity(nextActivity);


            Toast.MakeText(Context, $"Name: {meal.titles[e.Position]}", ToastLength.Short).Show();
        }

      
        private void Btn_Click(object sender, EventArgs e)
        {

            foreach (var item in extras)
            {

           

                Log.Info("Selection", $"item: {item}");


            }

            try
            {
                if (txt_auto.Text == "")
                {


                    Toast.MakeText(Context, "Empty", ToastLength.Short).Show();

                }
                else
                {

                    Toast.MakeText(Context, "Loading..", ToastLength.Short).Show();
                  
                    _ = meal.mealsAsync(list, Context, btn, txt_auto.Text, extras);
                    
                    txt_auto.Text = "";
                }

            }
            catch
            {

                Toast.MakeText(Context, "Not Found", ToastLength.Short).Show();

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
                        //Log.Info("data:", $"{line.Split(';')[0]}, {line.Split(';')[1]}");
                        count += 1;


                    }
                    catch
                    {

                    }
                }

                //Log.Info("Lenght: ", count.ToString());

            }

        }
    }
}