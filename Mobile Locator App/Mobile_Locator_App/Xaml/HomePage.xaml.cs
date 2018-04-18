using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mobile_Locator_App.Code;
using Mobile_Locator_App.Database;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Akka.Actor;
using System.Collections.ObjectModel;

namespace Mobile_Locator_App.Xaml
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
        private readonly IActorRef getFriendsActor;
        private readonly IActorRef getLocationActor;
        private ObservableCollection<string> FriendCollection = new ObservableCollection<string>();
         

        public HomePage()
        {
            InitializeComponent();
            InitializePageDesign();
            FriendCollection.Clear();

            ActorPrimus.Initialise();

            Props getFriendProps = Props.Create<GetFriends>(); 
            getFriendsActor = ActorPrimus.MainActorSystem.ActorOf(getFriendProps, "getFriendsActor");

            Props getLocationProps = Props.Create<GetLocationActor>();
            getLocationActor = ActorPrimus.MainActorSystem.ActorOf(getLocationProps, "getLocationActor");
            
            if(!User.CheckInternetConnection())
            {
                DisplayAlert("No Internet Connection", "Please connect to the internet, this application will not function properly" +
                    " without it", "OK");
                return;
            }

            // sends the context of the application to the GetLocationActor which will in turn 
            // call the getlocation functionality of the getLocation class


            FriendListView.ItemsSource = FriendCollection;
            InitialiseMessagingCenters();
            getFriends();
            Console.WriteLine("************************************************** Calling GetLocationActor");
            
            ActorPrimus.MainActorSystem.ActorOf(Props.Create(
                () => new GetLocationActor(getLocationActor, Droid.MainActivity.activity)));


            //ActorPrimus.GetLocationActor.Tell(new GetLocationActor(getLocationActor, Droid.MainActivity.activity)); 
            Console.WriteLine("**************************************************After Calling GetLocationActor");


        }

        void InitializePageDesign() // to set the elements on the page to the colours set in the Constants Class
        {
            BackgroundColor = Constants.BackgroundColour;
            if(User.CheckInternetConnection())
            {
                if (DBSupervisor.RedisDB.KeyExists(User.Username + "LocationDisabled"))
                {
                    Button_LocationEnabled.Text = "Location Disabled";
                }
                else
                {
                    Button_LocationEnabled.Text = "Location Enabled";
                }
            }
            else
            {
                Button_LocationEnabled.Text = "No Internet";
            }
            
           
            // call GetFriends to populate the listview
            
            //DisplayAlert("Username", "Current user is " + User.Username, "OK"); 
            //FriendListView.ItemsSource = new string[] { "" };

        }
        #region MessagingCenters
        private void InitialiseMessagingCenters()
        {

            MessagingCenter.Subscribe<DBSupervisor, string>(this, "noInternet", (sender, arg) =>
            {
                Console.WriteLine("************************************************************ DBSupervisor noInternet exception");
                Device.BeginInvokeOnMainThread(() => {
                    DisplayAlert("No Internet Connection ", "The application cannot connect to the internet, please ensure that your device is connected to a valid network.", "OK");
                });
                Console.WriteLine("************************************************************MessagingCenter noInternet");

            });


            /******************** Retrieving the users friends *************************/
            MessagingCenter.Subscribe<GetFriends, List<string>>(this, "hasFriends", (sender, arg) =>
            {
                Console.WriteLine("*************************************************************MessagingCenter has friends");
                // if the list has at least one value
                if (arg.Count > 0)
                {
                    loadFriends(arg);
                }
                else
                {
                    noFriends(arg);
                }
            });

            MessagingCenter.Subscribe<GetFriends, List<string>>(this, "hasNoFriends", (sender, arg) =>
            {
                Console.WriteLine("************************************************************MessagingCenter hasNoFriends"); 
                noFriendList(arg);
            });

            /******************** Retrieving users current location***********************/

            MessagingCenter.Subscribe<getLocation, string[]>(this, "gotLocation", (sender, arg) =>
            {
                Console.WriteLine("************************************************************Users Location has been retrieved.");
                gotLocation(arg);

      
            });

            MessagingCenter.Subscribe<getLocation, string[]>(this, "Mobius", (sender, arg) =>
            {
                 Console.WriteLine("*************************************************************Location has not been found.");
                DisplayAlert("Alert", "No location has been found, and so the slog begins.", "OK");
            
            });


        }
        #endregion

        private void gotLocation(string[] location)
        {
            //DisplayAlert("Alert", "Location has been found. Longitude: " + location[0] + " Latitude: " + location[1], "OK");

        }


        private void getFriends()
        {

            Console.WriteLine("**********************************************************getFriends after");
            ActorPrimus.DBSupervisorActor.Tell(new DBSupervisor.GetFriendsCommand(getFriendsActor));
            getLocationActor.Tell(new GetLocationActor.Initialise(Droid.MainActivity.activity));
            Console.WriteLine("************2 Actors running"); 
            //ActorPrimus.GetLocationActor.Tell(new GetLocationActor.)

        }

        private void noInteretConnection()
        {
            Console.WriteLine("************************************************************ DBSupervisor noInternet exception");
            Device.BeginInvokeOnMainThread(() => {
                DisplayAlert("No Internet Connection ", "The application cannot connect to the internet, please ensure that your device is connected to a valid network.", "OK");
            });
            Console.WriteLine("************************************************************MessagingCenter noInternet");
        }

        public void loadFriends(List<string> Friends)
        {
            // load the list of friends onto the listview
            Console.WriteLine("**********************************************************loadFriends");
            //DisplayAlert("Test", "The value Friends is: " + Friends.ToString(), "OK");

            for(int i = 0; i<Friends.Count; i++)
            {
                FriendCollection.Add(Friends[i]);
            }
            

            FriendListView.ItemsSource = FriendCollection;
            Console.WriteLine("**********************************************************loadFriends After");
        }

        public void noFriends(List<string> Friends)
        {
            if(Friends.Count == 0)
            {
                Friends.Add("No friends were found");
            }
            Console.WriteLine("**********************************************************noFriends");
            // display no friends found on the list view
            //FriendCollection.Add("No friends were found");
            //FriendListView.ItemsSource = new string[] { "No friends were found" }; 
            
        }

        public void noFriendList(List<string> Friends)
        {
            if (Friends.Count == 0)
            {
                Friends.Add("No friends were found");
            }
            //FriendListView.ItemsSource = new string[] { "No friends were found please add some" };
        }


        private void Button_NavPending_Clicked(object sender, EventArgs e)
        {
            ActorPrimus.stopActors();
            Navigation.PushModalAsync(new Mobile_Locator_App.Xaml.PendingFriendRequests());

        }

        private void Button_NavAddFriends_Clicked(object sender, EventArgs e)
        {
            ActorPrimus.stopActors();
            Navigation.PushModalAsync(new Mobile_Locator_App.Xaml.AddFriendsPage());
        }

        private void Button_NavLocator_Clicked(object sender, EventArgs e)
        {
            string username = "";
            ActorPrimus.stopActors();
            Navigation.PushModalAsync(new LocatorPage(username));
        }


        private void Button_Exit_Clicked(object sender, EventArgs e)
        {
            NavigationCode.ExitApp();
        }

        private void Button_LocationEnabled_Clicked(object sender, EventArgs e)
        {
            // if the users location is enabled
            if (!DBSupervisor.RedisDB.KeyExists(User.Username + "LocationDisabled"))
            {
                // disable the location
                User.locationEnabled = false;
                DBSupervisor.RedisDB.StringSet(User.Username + "LocationDisabled", "true");
                Button_LocationEnabled.Text = "Location Disabled";
            }
            // if the users location is enabled
            else
            {
                User.locationEnabled = true;
                if(DBSupervisor.RedisDB.KeyExists(User.Username + "LocationDisabled"))
                {
                    DBSupervisor.RedisDB.KeyDelete(User.Username + "LocationDisabled");
                }
                // get users location and store it in the server
                getLocationActor.Tell(new GetLocationActor.Initialise(Droid.MainActivity.activity));
                Button_LocationEnabled.Text = "Location Enabled";
            }
        }

        private async Task FriendListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if(FriendListView.SelectedItem == null)
            {
                return;
            }
            string selectedUser = FriendListView.SelectedItem.ToString();
            // if the user specified has their location enabled 
            if (!DBSupervisor.RedisDB.KeyExists(selectedUser + "LocationDisabled"))
            {
                // if the selected user has a location saved
                if (Database.DBSupervisor.RedisDB.KeyExists(selectedUser + "Longtitude") &&
                        Database.DBSupervisor.RedisDB.KeyExists(selectedUser + "Latitude"))
                {
                    var answer = await DisplayAlert("Alert", "Do you want to see the location of " + selectedUser + "?", "Yes", "No");
                    if (answer)
                    {
                        locateFriend(selectedUser);
                    }
                    else
                    {
                        FriendListView.SelectedItem = null;
                        return;
                    }
                }

            }
            else
            {
                await DisplayAlert("No Location", "The user you have selected does not have their location services enabled please try another", "OK");
                FriendListView.SelectedItem = null;
            }
        }

        private void locateFriend(string username)
        {

            User.addToFriendsToLocate(username);
            Navigation.PushModalAsync(new Mobile_Locator_App.Xaml.LocatorPage(username));
        }

    }

    

}