using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static Android.Provider.Contacts.Intents;
using static Java.Util.Jar.Attributes;

namespace Food_drink_2._0
{
    public class IngredientsInfo : AndroidX.Fragment.App.Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }
        View view;
        TextView Name, nutritions, property, flavonoid;
        Button btn;
        AutoCompleteTextView text;
        List<string> edit_txt = new List<string>();
        Dictionary<string, int> ingredients = new Dictionary<string, int>();
     
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment

            view = inflater.Inflate(Resource.Layout.ingredient_layout, container, false);

            Name = view.FindViewById<TextView>(Resource.Id.Name);
            nutritions = view.FindViewById<TextView>(Resource.Id.nutritions);
            property = view.FindViewById<TextView>(Resource.Id.property);
            flavonoid = view.FindViewById<TextView>(Resource.Id.flavonoid);

            btn = view.FindViewById<Button>(Resource.Id.btn);
            text = view.FindViewById<AutoCompleteTextView>(Resource.Id.txt);

            IngridientList();

            


          

            btn.Click += Btn_Click;

            return view;

  
        }

        private void Btn_Click(object sender, EventArgs e)
        {

            


            


            if(text.Text != "")
            {


                try
                {
                    int ingredient = ingredients[text.Text];
                    GetInformation(ingredient);
                }
                catch
                {

                    Toast.MakeText(Context, "It was not found", ToastLength.Short).Show();

                }


                

            }
            else
            {


                Toast.MakeText(Context, "Empty", ToastLength.Short).Show();
            }

            

        }


        private async Task GetInformation(int ID)
        {



            try
            {

                string URL = $"https://api.spoonacular.com/food/ingredients/{ID}/information?apiKey=307bbe52d24749a29b3e5f932cf20e9b&amount=1";


                var handler = new HttpClientHandler();
                HttpClient client = new HttpClient(handler);
                string All_Data = await client.GetStringAsync(URL);
                var data_in_string = JObject.Parse(All_Data);


                //Log.Info("data:", $"{data_in_string}");


                Name.Text = $"{text.Text}";


                nutritions.Text = $"Estimated Cost of The Desert: {data_in_string["estimatedCost"]["value"]}$ Dollars. \n \n \n";


                foreach (var nutrition in data_in_string["nutrition"]["nutrients"])
                {

                    Log.Info("nutrition:", $"{nutrition}");

                    float amount = (float)nutrition["amount"];

                    if (amount != 0.0)
                    {

                        nutritions.Text += $"\n \nName: {nutrition["name"]} \nAmount: {nutrition["amount"]}{nutrition["unit"]} \nPercent Of Daily Needs: {nutrition["percentOfDailyNeeds"]}%";

                    }




                }
                foreach (var properties in data_in_string["nutrition"]["properties"])
                {

                    Log.Info("properties:", $"{properties}");

                    property.Text += $"\n \nName: {properties["name"]} \nAmount: {properties["amount"]} grams.";

                }
                foreach (var flavonoids in data_in_string["nutrition"]["flavonoids"])
                {

                    Log.Info("flavonoids:", $"{flavonoids}");

                    flavonoid.Text += $"\n \nName: {flavonoids["name"]} \nAmount: {flavonoids["amount"]} grams.";

                }

                text.Text = "";
            }
            catch
            {

                Toast.MakeText(Context, "Error", ToastLength.Short).Show();

            }

  

            

        }
   


        private void IngridientList()
        {


            Stream data = Activity.Assets.Open(@"lista.txt");

            using (StreamReader reader = new StreamReader(data))
            {

                string line;
             

                while ((line = reader.ReadLine()) != null)
                {

                    try
                    {

                        ingredients.Add(line.Split(';')[0], int.Parse(line.Split(';')[1]));
                        edit_txt.Add(line.Split(';')[0]);
                        

              
                        //Log.Info("data:", $"{line.Split(';')[0]}, {line.Split(';')[1]}");



                    }
                    catch
                    {

                    }
                }


            }


            ArrayAdapter adapter = new ArrayAdapter(Context, Resource.Layout.panel, Resource.Id.txt, edit_txt);
            text.Adapter = adapter;




        }

    }
}