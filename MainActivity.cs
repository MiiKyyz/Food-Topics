using System;
using System.Collections.Generic;
using System.IO;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.Widget;
using AndroidX.Core.View;
using AndroidX.DrawerLayout.Widget;
using Google.Android.Material.BottomNavigation;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Navigation;
using Google.Android.Material.Snackbar;

namespace Food_drink_2._0
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true, Icon ="@drawable/Logo")]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener, BottomNavigationView.IOnNavigationItemSelectedListener
    {

        List<AndroidX.Fragment.App.Fragment> fragments = new List<AndroidX.Fragment.App.Fragment>()
        {
     
            new Recipe(),
            new IngredientsInfo(),
            new WineFragment(),
     
        
           



        };
       

      

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

           

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);
            SupportFragmentManager.BeginTransaction().Replace(Resource.Id.frame, fragments[0]).Commit();
        }

        

        
        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;

            switch (id)
            {

                case Resource.Id.Recipes:
                    SupportFragmentManager.BeginTransaction().Replace(Resource.Id.frame, fragments[0]).Commit();
                   
                    break;
                case Resource.Id.Ingredients:
                    SupportFragmentManager.BeginTransaction().Replace(Resource.Id.frame, fragments[1]).Commit();
                
                    break;
                case Resource.Id.Wine:
                    SupportFragmentManager.BeginTransaction().Replace(Resource.Id.frame, fragments[2]).Commit();
                
                    break;
             
          

                

            }

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }


        public override void OnBackPressed()
        {
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            if (drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                base.OnBackPressed();
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

