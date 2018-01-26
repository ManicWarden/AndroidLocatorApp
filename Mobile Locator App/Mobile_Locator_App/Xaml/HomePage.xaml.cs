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
        private ObservableCollection<string> FriendCollection = new ObservableCollection<string>();


        public HomePage()
        {
            InitializeComponent();
            InitializePageDesign();
            FriendCollection.Clear();

            ActorPrimus.Initialise();

            Props getFriendProps = Props.Create<GetFriends>();
            getFriendsActor = ActorPrimus.MainActorSystem.ActorOf(getFriendProps, "getFriendsActor");

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
            FriendListView.ItemsSource = FriendCollection;
            getFriends();

        }

        void InitializePageDesign() // to set the elements on the Log in page to the colours set in the Constants Class
        {
            BackgroundColor = Constants.BackgroundColour;
            // call GetFriends to populate the listview
            
            DisplayAlert("Username", "Current user is " + User.Username, "OK");
            //FriendListView.ItemsSource = new string[] { "" };

        }

        private void getFriends()
        {
            Console.WriteLine("************************************************************getFriends");
            //RetrieveFriends retrieveFriends = new RetrieveFriends();
            

            

            Console.WriteLine("**********************************************************getFriends after");
            ActorPrimus.DBSupervisorActor.Tell(new DBSupervisor.GetFriendsCommand(getFriendsActor));
            



            // use an actor to get the users friends in a list
            // which will then be used to populate the listview

            //Find way to make username a public constant when the user has signed in or registered
            //DatabaseActions.GetFriends();
            // a list will be returned containing the usernames of the friends, and maybe the first and second names in different lists
            // assign said list/s to the listview FriendListView
            /*
            items = new string[] { "Vegetables", "Fruits", "Flower Buds", "Legumes", "Bulbs", "Tubers" };
            ListAdapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, items);
            */
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
            Navigation.PushModalAsync(new Mobile_Locator_App.Xaml.PendingFriendRequests());

        }

        private void Button_NavAddFriends_Clicked(object sender, EventArgs e)
        {
            
            Navigation.PushModalAsync(new Mobile_Locator_App.Xaml.AddFriendsPage());
        }

        private void Button_NavLocator_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Mobile_Locator_App.Xaml.LocatorPage());
        }


        private void Button_Exit_Clicked(object sender, EventArgs e)
        {
            NavigationCode.ExitApp();
        }


    }

}