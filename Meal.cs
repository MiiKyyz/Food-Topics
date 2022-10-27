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
using static Android.Content.ClipData;
using static Android.Icu.Text.CaseMap;

namespace Food_drink_2._0
{
    public class Meal
    {



        public List<string> titles = new List<string>();
        List<Bitmap> img = new List<Bitmap> { };

        public List<string> Found = new List<string>() { "Recipe Does not Exists!"};
        public List<string> load = new List<string>() { "Loading..." };
        List<Bitmap> bitmap_img = new List<Bitmap>();
        CustomAdapter adapter_title, adapter_load;




        private Bitmap ImageConverter(string url)
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


        private Bitmap ImageLoad()
        {

            using (WebClient webClient = new WebClient())
            {
                //byte[] bytes = webClient.DownloadData(url);
                byte[] bytes = BitConverter.GetBytes( Resource.Drawable.load);
                if (bytes != null && bytes.Length > 0)
                {
                    return BitmapFactory.DecodeByteArray(bytes, 0, bytes.Length);
                }
            }
            return null;

        }



        public async Task Ingredients(string ingret)
        {




            string URL = $"https://api.spoonacular.com/recipes/complexSearch?apiKey=307bbe52d24749a29b3e5f932cf20e9b&query={ingret}";

          

        


            var handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            string All_Data = await client.GetStringAsync(URL);
            var data_in_string = JObject.Parse(All_Data);

            try
            {
                foreach (var item in data_in_string["results"])
                {

                    Log.Info("data:", $"titles: {item}");

                  
                }


            }
            catch
            {



                Log.Info("Error!!!!!!!!!!!!!!!!!11:", $"Error!!!!!!!!!!!!!!!!!!!!!!!!!!!1");


            }





        }
     

        public async Task mealsAsync(ListView list, Context context, Button btn, string ingredient, List<string> extras )
        {
            btn.Enabled = false;
            

            titles.Clear();
            img.Clear();
            bitmap_img.Clear();

            bitmap_img.Add(ImageLoad());
            adapter_load = new CustomAdapter(context, load, bitmap_img);

            list.Adapter = adapter_load;

            string URL = $"https://api.spoonacular.com/recipes/complexSearch?apiKey=307bbe52d24749a29b3e5f932cf20e9b&query={ingredient}";

            foreach (var item in extras)
            {

                URL += $"&{item}";

                //Log.Info("Selection", $"item: {item}");


            }

            //Log.Info("URL", $"URL: {URL}");



            var handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            string All_Data = await client.GetStringAsync(URL);
            var data_in_string = JObject.Parse(All_Data);

            try
            {
                foreach (var item in data_in_string["results"])
                {

                    //Log.Info("data:", $"titles: {item}");

                    Bitmap bit = ImageConverter(item["image"].ToString());

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

                

                Log.Info("Error!!!!!!!!!!!!!!!!!11:", $"Error!!!!!!!!!!!!!!!!!!!!!!!!!!!1");


            }



            
        }
    }
}