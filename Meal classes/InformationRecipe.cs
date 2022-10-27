using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static Android.Content.ClipData;

namespace Food_drink_2._0
{
    [Activity(Label = "Information")]
    public class InformationRecipe : AppCompatActivity
    {

     
        TextView name, desc, Prep, nutrients, Properties;
        ImageView img;
        string url_img;
        int ID;
        public Dictionary<string, int> ingridients_list = new Dictionary<string, int>();
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.info_layout);
            // Create your application here


            name = FindViewById<TextView>(Resource.Id.name);
            desc = FindViewById<TextView>(Resource.Id.desc);
            Prep = FindViewById<TextView>(Resource.Id.Prep);
            nutrients = FindViewById<TextView>(Resource.Id.nutrients);
   


            img = FindViewById<ImageView>(Resource.Id.img);


            string ingredient;

            ingredient = Intent.GetStringExtra("ingredient" ?? "not recv");

            name.Text = ingredient;

            ID = await Ingredients_ID(ingredient);

   
            Log.Info("UP:", $"from: {url_img}");


            Bitmap bit = ImgWeb(url_img);


            img.SetImageBitmap(bit);


            _ = Get_Info(ID, desc);


            _ = Get_Nutrients(ID, nutrients);


        }


        private Bitmap ImgWeb(string url)
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



        public async Task Get_Nutrients(int ID, TextView nutrients)
        {

            string URL = $"https://api.spoonacular.com/recipes/{ID}/nutritionWidget.json?apiKey=307bbe52d24749a29b3e5f932cf20e9b";
            Log.Info("URL Info:", $"{URL}");

            var handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            string All_Data = await client.GetStringAsync(URL);
            var nutrients_data = JObject.Parse(All_Data);


            //Log.Info("data:", $"Nutrients: {nutrients_data} ");

            nutrients.Text = $"Bad Nutrition \n \n";

            foreach (var bad in nutrients_data["bad"])
            {

                nutrients.Text += $"{bad["title"]}: {bad["amount"]}\n";


            }


            nutrients.Text += $"\n \nGood Nutrition \n \n";

            foreach (var good in nutrients_data["good"])
            {

                nutrients.Text += $"{good["title"]}: {good["amount"]}\n";


            }

        }
        public async Task Get_Info(int ID, TextView desc)
        {




            string URL = $"https://api.spoonacular.com/recipes/{ID}/information?apiKey=307bbe52d24749a29b3e5f932cf20e9b";
            Log.Info("URL Info:", $"{URL}");

            var handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            string All_Data = await client.GetStringAsync(URL);
            var data_in_string = JObject.Parse(All_Data);
            try
            {
                
                //Log.Info("data:", $"titles: {data_in_string} ");
                int counter = 0;











                //Instructions
                //Prep.Text = $"\n \ninstructions To Cook \n";

                foreach(var steps in data_in_string["analyzedInstructions"][0]["steps"])
                {


                    //Log.Info("data:", $"titles: {steps} ");

                    foreach(var step in steps["ingredients"])
                    {


                        Prep.Text += $"\n \nStep number: {steps["number"]}\nStep instruction: {steps["step"]}\nIngredient name: {step["name"]} \n";
                          
                        break;

                    }

                    foreach(var equipment in steps["equipment"])
                    {

                        Prep.Text += $"Equipment needed: {equipment["name"]}\n \n";
                        


                        
                        break;

                    }


                }



                //Ingredients
                foreach (var item in data_in_string["extendedIngredients"])
                {


                  
                    //Log.Info("data:", $"titles: {item}, ");
                    counter += 1;


                    if (item["consistency"].ToString() != "LIQUID")
                    {
                        desc.Text += $"\n \nIngredient {counter}: {item["name"]} \n" +
                        $"Amount: {item["original"]} \n" +
                        $"Units: {item["amount"]} {item["unit"]}\n \n";

                    }
                    else
                    {

                        

                        desc.Text += $"\n \nIngredient {counter}: {item["name"]} \n" +
                        $"Amount: {item["original"]} \n" +
                        $"Units: {item["amount"]} {item["unit"]}\n" +
                        $"Metrics: {item["measures"]["metric"]["amount"]} {item["measures"]["metric"]["unitLong"]}\n \n";

                    }

                }


            }
            catch
            {



                Log.Info("Error!!!!!!!!!!!!!!!!!11:", $"Error!!!!!!!!!!!!!!!!!!!!!!!!!!!1");


            }

            
        }

        public async Task<int> Ingredients_ID(string ingret)
        {




            string URL = $"https://api.spoonacular.com/recipes/complexSearch?apiKey=307bbe52d24749a29b3e5f932cf20e9b&query={ingret}";


            var handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            string All_Data = await client.GetStringAsync(URL);
            var data_in_string = JObject.Parse(All_Data);

            try
            {

                //Log.Info("data:", $"titles: {data_in_string}");
                foreach (var item in data_in_string["results"])
                {

                    if (ingret == item["title"].ToString())
                    {
                        
                        ID = (int)item["id"];
                        //Log.Info("data:", $"titles: {item["image"]}");
                        url_img = item["image"].ToString();
                    }

                    


                }


            }
            catch
            {



                Log.Info("Error!!!!!!!!!!!!!!!!!11:", $"Error!!!!!!!!!!!!!!!!!!!!!!!!!!!1");


            }

            return ID;
        }


        public override bool OnCreateOptionsMenu(IMenu menu)
        {

            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;

        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {




                return true;
            }

            return base.OnOptionsItemSelected(item);
        }


        public void IngridientList()
        {


            Stream data = Assets.Open(@"lista.txt");

            using (StreamReader reader = new StreamReader(data))
            {

                string line;
                int count = 0;

                while ((line = reader.ReadLine()) != null)
                {

                    try
                    {
                        ingridients_list.Add(line.Split(';')[0], int.Parse(line.Split(';')[1]));
                   
                        //Log.Info("data:", $"{line.Split(';')[0]}, {line.Split(';')[1]}");
                        count += 1;


                    }
                    catch
                    {

                    }
                }

                Log.Info("Lenght: ", count.ToString());

            }

        }

    }
}