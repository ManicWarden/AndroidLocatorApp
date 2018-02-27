using System;
using System.Linq;
using Android.App;
using Android.Gms.Maps;

using Mobile_Locator_App.Code;
using Mobile_Locator_App.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;


namespace Mobile_Locator_App.Xaml
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LocatorPage : ContentPage
	{

        private Map map;
        private double userLatitude = Convert.ToDouble(User.Latitude);
        private double userLongitude = Convert.ToDouble(User.Longitude);
        private double latitude;
        private double longitude;
        public LocatorPage()
        {
            
            InitializeComponent();
            InitializePageDesign();

            MessagingCenter.Subscribe<Database.DBSupervisor>(this, "noInternet", (sender) =>
            {
                Console.WriteLine("************************************************************MessagingCenter noInternet");
                DisplayAlert("No Internet Connection.", "The application cannot connect to the internet, please ensure that your device is connected to a valid network.", "OK");

            });

            InitMap();

            // add the users current location to the map
            addPin(User.Username, userLatitude, userLongitude);
            //if the user has selected a friend to find 
            if(User.friendsToLocate.Count > 0)
            {
                // find all friendsToLocate locations and add them as pins to the map
                for (int i = 0; i < User.friendsToLocate.Count; i++)
                {
                    if (Database.DBSupervisor.RedisDB.KeyExists(User.friendsToLocate[i] + "Longtitude") &&
                        Database.DBSupervisor.RedisDB.KeyExists(User.friendsToLocate[i] + "Latitude"))
                    {

                        double latitude = Convert.ToDouble(Database.DBSupervisor.RedisDB.StringGet(User.friendsToLocate[i] + "Latitude"));
                        double longitude = Convert.ToDouble(Database.DBSupervisor.RedisDB.StringGet(User.friendsToLocate[i] + "Latitude"));
                    }
                    else
                    {
                        return;
                    }
                    // after the friends have been put on the map clear the list
                    User.friendsToLocate.Clear();
                    addPin(User.friendsToLocate[i], latitude, longitude);
                } // end for loop



            } // end if
                
        }
            



        

        public void InitMap()
        {
            //FromCenterAndRadius is used to center the map on the given position
            // in this case the users last known location
            map = new Map(
            MapSpan.FromCenterAndRadius(
            new Position(userLatitude, userLongitude), Distance.FromMiles(0.3)))
            {
                IsShowingUser = true,
                HeightRequest = 100,
                WidthRequest = 960,
                MapType = MapType.Street,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(userLatitude, userLongitude),
                                             Distance.FromMiles(1)));
            // creating the area where the map will be placed on the page
            //var stack = new Grid { };
            var stack = MapSection;
            stack.Children.Add(map);
            
            //Content = stack;




        }

        public void addPin(string Username, double Latitude, double Longtitude)
        {
            // creating the position from the specified latitude and longitude
            var position = new Position(Latitude, Longtitude);
            // creating the pin that will show a users location on the map
            var pin = new Pin
            {
                Type = PinType.Generic,
                Position = position,
                Label = Username
            };
            map.Pins.Add(pin);
        }


        void InitializePageDesign() // to set the elements on the Log in page to the colours set in the Constants Class
        {
            BackgroundColor = Constants.BackgroundColour;
            /*************************** ******************/
            //SHA1 Key:  38:F4:D1:37:DD:AC:5F:9A:50:52:11:8B:F4:1F:47:77:C8:46:A4:D2
            // Google Maps API Key: AIzaSyA0NaUhV_6er4i0t0nz_XPf0dwAkrAXlg4
            //                      AIzaSyA3e08LCuZ3Rb7_5DWIklwxN87jyu_4lxY
            /**************** USE NEW PACKAGE NAME TO CREATE A NEW GOOGLE MAPS API KEY 
             * HAD TO CHANGE THE OLD NAME BECAUSE THE PROGRAM CANT HANDLE CAPITAL LETTERS************************/
        }



        private void Button_NavHome_Clicked(object sender, EventArgs e)
        {
            ActorPrimus.stopActors();
            Navigation.PushModalAsync(new HomePage());
            //NavigationCode.GoHome();
        }

        private void Button_NavPending_Clicked(object sender, EventArgs e)
        {
            ActorPrimus.stopActors();
            Navigation.PushModalAsync(new Mobile_Locator_App.Xaml.PendingFriendRequests());

        }

        private void Button_NavAddFriends_Clicked(object sender, EventArgs e)
        {
            ActorPrimus.stopActors();
            Navigation.PushModalAsync(new AddFriendsPage());
            //NavigationCode.GoAddFriends();
        }


        private void Button_Exit_Clicked(object sender, EventArgs e)
        {
            NavigationCode.ExitApp();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }

    public partial class oldLocation
    {
        public oldLocation()
        {
            InitiliaseMapFragment initMap = new InitiliaseMapFragment();
            initMap.initiliaseMapFragment();
        }

        private class InitiliaseMapFragment : Java.Lang.Object, IOnMapReadyCallback
        {
            GoogleMap _map;
            public void initiliaseMapFragment()
            {
                // the FragmentManager belongs to the activity class of the project, as activity and contentpage cannot be inherited at
                // the same time a reference is needed to the MainActivity activity for the FragmentManager to work
                MapFragment _mapFragment = MainActivity.activity.FragmentManager.FindFragmentByTag("map") as MapFragment;

                _mapFragment.GetMapAsync(this);
                // after the above call is returned the settings are configured
                if (_map != null)
                {
                    _map.UiSettings.ZoomControlsEnabled = true;
                    _map.UiSettings.CompassEnabled = true;
                    _map.MapType = GoogleMap.MapTypeHybrid; // should take longer to load than normal or terrain but not as long as satellite
                }
            }
            public void OnMapReady(GoogleMap map)
            {
                _map = map;
            }

        }
    }

}