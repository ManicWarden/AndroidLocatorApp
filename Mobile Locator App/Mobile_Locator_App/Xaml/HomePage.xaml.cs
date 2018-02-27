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
            // call GetFriends to populate the listview
            
            DisplayAlert("Username", "Current user is " + User.Username, "OK");
            //FriendListView.ItemsSource = new string[] { "" };

        }
        #region MessagingCenters
        private void InitialiseMessagingCenters()
        {
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
                    noFriends();
                }
            });

            MessagingCenter.Subscribe<GetFriends, List<string>>(this, "hasNoFriends", (sender, arg) =>
            {
                Console.WriteLine("************************************************************MessagingCenter hasNoFriends");
                noFriendList();
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
                noLocation();
            });

            MessagingCenter.Subscribe<DBSupervisor>(this, "noInternet", (sender) =>
            {
                Console.WriteLine("************************************************************MessagingCenter noInternet");
                DisplayAlert("No Internet Connection.", "The application cannot connect to the internet, please ensure that your device is connected to a valid network.", "OK");
                
            });
        }
        #endregion

        private void gotLocation(string[] location)
        {
            DisplayAlert("Alert", "Location has been found. Longitude: " + location[0] + " Latitude: " + location[1], "OK");

        }

        private void noLocation()
        {
            DisplayAlert("Alert", "No location has been found, and so the slog begins.", "OK");
        }
        private void getFriends()
        {

            Console.WriteLine("**********************************************************getFriends after");
            ActorPrimus.DBSupervisorActor.Tell(new DBSupervisor.GetFriendsCommand(getFriendsActor));
            getLocationActor.Tell(new GetLocationActor.Initialise(Droid.MainActivity.activity));

            //ActorPrimus.GetLocationActor.Tell(new GetLocationActor.)

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

        public void noFriends()
        {
            Console.WriteLine("**********************************************************noFriends");
            // display no friends found on the list view
            FriendCollection.Add("No friends were found");
            //FriendListView.ItemsSource = new string[] { "No friends were found" };
            
        }

        public void noFriendList()
        {
            FriendCollection.Add("No friends were found");
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
            ActorPrimus.stopActors();
            Navigation.PushModalAsync(new Mobile_Locator_App.Xaml.LocatorPage());
        }


        private void Button_Exit_Clicked(object sender, EventArgs e)
        {
            NavigationCode.ExitApp();
        }

        private async void FriendListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var answer = await DisplayAlert("Alert", "Do you want to see the location of " + FriendListView.SelectedItem.ToString() + "?", "Yes", "No");
            if (answer)
            {
                //User.addToFriendsToLocate(FriendListView.SelectedItem.ToString());
                User.friendsToLocate.Add(FriendListView.SelectedItem.ToString());
                await Navigation.PushModalAsync(new Mobile_Locator_App.Xaml.LocatorPage());
            }
            else
            {
                return;
            }
        }
    }

}