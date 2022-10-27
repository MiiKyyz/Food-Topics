using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Google.Android.Material.BottomNavigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Android.Content.ClipData;

namespace Food_drink_2._0
{
    [Obsolete]
    public class WineFragment : AndroidX.Fragment.App.Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }


        Dictionary<int, AndroidX.Fragment.App.Fragment> frag = new Dictionary<int, AndroidX.Fragment.App.Fragment>()
        {
            


            {Resource.Id.Dish_Wine,  new DishWine() },
            {Resource.Id.Pairing,  new Pairing() },
          
            {Resource.Id.Recommendation,  new Recommendation() }



        };

        View view;
       
        BottomNavigationView bottomNavigationView;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment

            view  = inflater.Inflate(Resource.Layout.wine_layout, container, false);

            bottomNavigationView = view.FindViewById<BottomNavigationView>(Resource.Id.navigation);


            bottomNavigationView.ItemSelected += BottomNavigationView_ItemSelected;




            



            Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.FrameWine, new DishWine()).Commit();

    

            return view;

        }

     

      
        private void BottomNavigationView_ItemSelected(object sender, Google.Android.Material.Navigation.NavigationBarView.ItemSelectedEventArgs e)
        {


            switch (e.Item.ItemId)
            {


                case Resource.Id.Dish_Wine:
                    Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.FrameWine, frag[e.Item.ItemId]).Commit();
            
                    //2131230726
                    break;
                case Resource.Id.Pairing:
             
                    Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.FrameWine, frag[e.Item.ItemId]).Commit();
                    break;
      
                case Resource.Id.Recommendation:
               
                    Activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.FrameWine, frag[e.Item.ItemId]).Commit();
                    break;
       


            }

        }

   
    }
}