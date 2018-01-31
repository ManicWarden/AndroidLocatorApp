using Mobile_Locator_App.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Mobile_Locator_App.Database;
using Akka.Actor;
using System.Collections.ObjectModel;

namespace Mobile_Locator_App.Xaml
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PendingFriendRequests : ContentPage
	{
        private readonly IActorRef getPendingFriendsActor;
        private readonly IActorRef confirmFriendRequestActor;
        private ObservableCollection<string> PendingFriendCollection = new ObservableCollection<string>();

        public PendingFriendRequests ()
		{
			InitializeComponent ();
            InitializePageDesign();
            PendingFriendCollection.Clear();

            
            ActorPrimus.Initialise();

            Props GetPendingFriendsProps = Props.Create<GetPendingFriends>();
            getPendingFriendsActor = ActorPrimus.MainActorSystem.ActorOf(GetPendingFriendsProps, "getPendingFriendsActor");

            Props ConfirmFriendRequestProps = Props.Create<ConfirmFriendRequest>();
            confirmFriendRequestActor = ActorPrimus.MainActorSystem.ActorOf(ConfirmFriendRequestProps, "confirmFriendRequestActor");

            MessagingCenter.Subscribe<GetPendingFriends, List<string>>(this, "hasFriends", (sender, arg) =>
            {
                Console.WriteLine("**************************************************************MessagingCenter has friends");
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

            MessagingCenter.Subscribe<GetPendingFriends, List<string>>(this, "hasNoFriends", (sender, arg) =>
            {
                Console.WriteLine("*************************************************************MessagingCenter hasNoFriends");
                noFriendList();
            });
            FriendListView.ItemsSource = PendingFriendCollection;
            getFriends();

        }

        void InitializePageDesign() // to set the elements on the Log in page to the colours set in the Constants Class
        {
            BackgroundColor = Constants.BackgroundColour;
        }

        private void getFriends()
        {
            Console.WriteLine("************************************************************getFriends");

            Console.WriteLine("**********************************************************getFriends after");
            ActorPrimus.DBSupervisorActor.Tell(new DBSupervisor.GetPendingFriendsCommand(getPendingFriendsActor));
        }

        public void loadFriends(List<string> Friends)
        {
            // load the list of friends onto the listview
            Console.WriteLine("**********************************************************loadFriends");
            for (int i = 0; i < Friends.Count; i++)
            {
                PendingFriendCollection.Add(Friends[i]);
            }
            FriendListView.ItemsSource = PendingFriendCollection;
        }

        public void noFriends()
        {
            Console.WriteLine("**********************************************************noFriends");
            // display no friends found on the list view
            FriendListView.ItemsSource = new string[] { "No pending requests were found" };
        }

        public void noFriendList()
        {
            FriendListView.ItemsSource = new string[] { "No pending requests found" };
        }

        // place a button or an onclick event on the listview for each username displayed
        // on this click call the function that calls the ConfirmFriendRequest actor passing
        // the username of the chosen member of the listview

        private void Button_NavHome_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Mobile_Locator_App.Xaml.HomePage());

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

        private async void FriendListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            // ask for confirmation onclick on a username in the pending friends listview
            
            var answer = await DisplayAlert("Alert", "Are you sure you want to accept the friend request of " + FriendListView.SelectedItem.ToString(), "Yes", "No");

            // if yes was clicked
            if(answer)
            {
                ActorPrimus.DBSupervisorActor.Tell(new DBSupervisor.ConfirmFriendRequestCommand(confirmFriendRequestActor, FriendListView.SelectedItem.ToString()));
                DisplayAlert("Success", FriendListView.SelectedItem.ToString() + " has been added as a friend", "OK");

                PendingFriendCollection.Remove(FriendListView.SelectedItem.ToString());
                
                

            }
            else
            {
                return;
            }
        }
    }
}