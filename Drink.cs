using Android.Animation;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Lang.Reflect;
using Java.Util.Concurrent;
using Newtonsoft.Json.Linq;
using Org.Apache.Http.Impl.Conn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static Android.Content.ClipData;
using static Java.Util.Jar.Pack200;

namespace Food_drink_2._0
{
    public class Drink
    {
        string url_buy = "";
        public List<string> Dish_Pairing_for_Wine = new List<string>()
        {
            "assyrtiko", "moschofilero", "muscadet", "viognier", "verdicchio", "white burgundy",
            "chardonnay", "gruener veltliner", "frascati", "gavi", "trebbiano", "sauvignon blanc",
            "albarino", "verdejo", "vermentino", "pinot grigio", "gewurztraminer", "chenin blanc",
            "riesling", "zweigelt", "barbera wine", "primitivo", "pinot noir", "nebbiolo", "red burgundy",
            "rioja", "grenache", "malbec", "zinfandel", "sangiovese", "cabernet sauvignon", "tempranillo",
            "shiraz", "merlot", "nero d avola", "bordeaux", "port", "gamay", "dornfelder", "sparkling red wine",
            "pinotage", "agiorgitiko", "dessert wine", "moscato", "rose wine", "sparkling rose", "sparkling wine",
            "cava", "champagne", "sparkling rose", "sherry", "cream sherry", "dry sherry"
        };
        string wineP = "";


        public async Task Dish_Wine(string wine,TextView description, TextView deserts)
        {


            string URL = $"https://api.spoonacular.com/food/wine/dishes?apiKey=307bbe52d24749a29b3e5f932cf20e9b&wine={wine}";



            var handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            string All_Data = await client.GetStringAsync(URL);
            var drink_data = JObject.Parse(All_Data);


            Log.Info("Wine", $"{drink_data}");


            description.Text = $"\nDescription: {drink_data["text"]}\n  ";
            deserts.Text = "Matching deserts to eat with the Wine are: \n\n";
            int counter = 0;
            foreach (var pair in drink_data["pairings"]) {

                counter++;
                deserts.Text += $"{counter}. {pair}\n";


            }

        }

        public string buy()
        {
            return url_buy;


        }



        ObjectAnimator anim;
    

        private void AnimationDown(LinearLayout layout )
        {
            anim = ObjectAnimator.OfFloat(layout, "alpha", 1.0f, 0);
            anim.SetDuration(1000);
            anim.Start();

        }
        private void AnimationUp(LinearLayout layout)
        {
            anim = ObjectAnimator.OfFloat(layout, "alpha", 0, 1f);
            anim.SetDuration(1000);
            anim.Start();

         
        }

 

        public async Task PairingWine(TextView drink = null, TextView Title = null, TextView Description = null, ImageView img = null, Button btn = null, TextView wines = null, LinearLayout linearLayout = null)
        {

            if(linearLayout.Alpha != 0)
            {

                AnimationDown(linearLayout);

            }

           
            string URL = $"https://api.spoonacular.com/food/wine/pairing?apiKey=307bbe52d24749a29b3e5f932cf20e9b&food={drink.Text}";
            
 
            var handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            string all_data = await client.GetStringAsync(URL);
            var pairing_data = JObject.Parse(all_data);
            

            //Log.Info("Wine", $"{pairing_data}");



           
     
 


            try
            {
                Description.Text = "";
                Title.Text = "";
                wines.Text = "";
                btn.Enabled = true;

                Title.Text = $"{pairing_data["productMatches"][0]["title"]}";
                Bitmap bit = GetImg($"{pairing_data["productMatches"][0]["imageUrl"]}");
                img.SetImageBitmap(bit);


                Description.Text += $"{pairing_data["pairingText"]}\n";
                Description.Text += $"{pairing_data["productMatches"][0]["description"]}\n";

            


                btn.Text = $"est. {pairing_data["productMatches"][0]["price"]}";
                int Count = 0;
                wines.Text += $"Paired Wines:\n";
                foreach (var wine in pairing_data["pairedWines"])
                {

                    Count += 1;
                    wines.Text += $"{ Count}. {wine}\n";


                }
                url_buy = $"{pairing_data["productMatches"][0]["link"]}";


            }
            catch
            {
                Title.Text = "Not Found!";
                Description.Text = $"It was not possible to find any matching wine of the following food or dish {drink.Text}";
                btn.Enabled = false;
                wines.Text = "";
                Log.Info("Error", "Error");

            }

            AnimationUp(linearLayout);

     
            drink.Text = "";
        }

        public List<string> titles = new List<string>();
        List<Bitmap> img = new List<Bitmap> { };
        List<Bitmap> bitmap_img = new List<Bitmap>();
        CustomAdapter adapter_title, adapter_load;
        public List<string> Found = new List<string>() { "Recipe Does not Exists!" };
        public List<string> load = new List<string>() { "Loading..." };

        private Bitmap ImageLoad()
        {

            using (WebClient webClient = new WebClient())
            {
                //byte[] bytes = webClient.DownloadData(url);
                byte[] bytes = BitConverter.GetBytes(Resource.Drawable.load);
                if (bytes != null && bytes.Length > 0)
                {
                    return BitmapFactory.DecodeByteArray(bytes, 0, bytes.Length);
                }
            }
            return null;

        }

        
        public async Task<List<string>> RecommendedPicked(string title)
        {
            string URL = $"https://api.spoonacular.com/food/wine/recommendation?apiKey=307bbe52d24749a29b3e5f932cf20e9b&wine={wineP}&number=10";

            

            var handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            string All_Data = await client.GetStringAsync(URL);
            var data_in_string = JObject.Parse(All_Data);

            List<string> data = new List<string>();



            try
            {

                foreach (var item in data_in_string["recommendedWines"])
                {

                    if (item["title"].ToString() == title)
                    {


                        data.Add(item["title"].ToString());
                        data.Add(item["description"].ToString());
                        data.Add(item["price"].ToString());
                        data.Add(item["imageUrl"].ToString());
                        data.Add(item["link"].ToString());



                        Log.Info("Selection", $"item: {item}");

                    }
                }

            }
            catch (Exception ex)
            {

                Log.Info("Error", $"Error: {ex.Message}");
            }

            return data;

            
        }

        

        public async Task Recommendation(ListView list, Context context, Button btn, string wine)
        {
            wineP = wine;
            Log.Info("wineP", $"wineP: {wineP}");
            btn.Enabled = false;


            titles.Clear();
            img.Clear();
            bitmap_img.Clear();

            bitmap_img.Add(ImageLoad());
            adapter_load = new CustomAdapter(context, load, bitmap_img);

            list.Adapter = adapter_load;

            

            try
            {

                string URL = $"https://api.spoonacular.com/food/wine/recommendation?apiKey=307bbe52d24749a29b3e5f932cf20e9b&wine={wine}&number=10";



                var handler = new HttpClientHandler();
                HttpClient client = new HttpClient(handler);
                string All_Data = await client.GetStringAsync(URL);
                var data_in_string = JObject.Parse(All_Data);

                //Log.Info("Selection", $"item: {data_in_string}");

                foreach (var item in data_in_string["recommendedWines"])
                {

                    //Log.Info("data:", $"titles: {item}");

                    Bitmap bit = GetImg(item["imageUrl"].ToString());

                    img.Add(bit);
                    titles.Add(item["title"].ToString());
                }

                if (titles.Count == 0)
                {
                    bitmap_img.Add(ImageLoad());
                    adapter_title = new CustomAdapter(context, Found, bitmap_img);

                    list.Adapter = adapter_title;

                    Log.Info("Done:", $"Done!");

                }
                else
                {
                    adapter_title = new CustomAdapter(context, titles, img);

                    list.Adapter = adapter_title;

                    Log.Info("Done:", $"Done!");
                }


                btn.Enabled = true;

            }
            catch
            {
                List<string> NO = new List<string>() { "Was not found"};
         
                ArrayAdapter adapter = new ArrayAdapter(context, Resource.Layout.panel, Resource.Id.txt, NO);

             ;

                list.Adapter = adapter;
                btn.Enabled = true;
                Log.Info("Error!!!!!!!!!!!!!!!!!11:", $"Error!!!!!!!!!!!!!!!!!!!!!!!!!!!1");


            }




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