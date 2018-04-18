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
        private string friendUsername;
        public LocatorPage( string friend)
        {
            
            InitializeComponent();
            InitializePageDesign();
            friendUsername = friend;

            MessagingCenter.Subscribe<Database.DBSupervisor>(this, "noInternet", (sender) =>
            {
                Console.WriteLine("************************************************************MessagingCenter noInternet");
                DisplayAlert("No Internet Connection.", "The application cannot connect to the internet, please ensure that your device is connected to a valid network.", "OK");

            });
            checkNet();
            InitMap();

            // add the users current location to the map
            addPin(User.Username, userLatitude, userLongitude);
            
            //if the user has selected a friend to find 
            if (friendUsername.Length > 0)
            {
                // find all friendsToLocate locations and add them as pins to the map
                //for (int i = 0; i < User.friendsToLocate.Count; i++)
                //{
                    if (Database.DBSupervisor.RedisDB.KeyExists(friendUsername + "Longtitude") &&
                        Database.DBSupervisor.RedisDB.KeyExists(friendUsername + "Latitude"))
                    {

                        double latitude = Convert.ToDouble(Database.DBSupervisor.RedisDB.StringGet(friendUsername + "Latitude"));
                        double longitude = Convert.ToDouble(Database.DBSupervisor.RedisDB.StringGet(friendUsername + "Latitude"));
                        addPin(friendUsername, latitude, longitude);
                    }
                    else
                    {
                        DisplayAlert("Alert", "The user you are trying to find does not have any location data, please try another.", "OK");
                        return;
                    }
                    // after the friends have been put on the map clear the list
                    
                    
                //} // end for loop



            } // end if

            
        }
            
        /// <summary> 
        /// Check internet connectivity
        /// if not connected then wait 5 seconds for the  user to enable the internet
        /// before reloading the page and trying again
        /// </summary>
        private void checkNet()
        {

            if (!User.CheckInternetConnection())
            {

                DisplayAlert("No Internet Connection.", "The application cannot connect to the internet, please ensure that your device is connected to a valid network.", "OK");
                System.Threading.Tasks.Task.Delay(5000);
                Navigation.PushModalAsync(new LocatorPage(friendUsername));
            }
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
            User.friendsToLocate.Clear();
            map.Pins.Add(pin);
        }


        void InitializePageDesign() // to set the elements on the Log in page to the colours set in the Constants Class
        {
            BackgroundColor = Constants.BackgroundColour;

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


}