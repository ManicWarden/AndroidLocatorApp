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

namespace Mobile_Locator_App.Xaml
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddFriendsPage : ContentPage
    {
        
        private readonly IActorRef addFriendActor;
        public AddFriendsPage()
        {
            InitializeComponent();
            InitializePageDesign();

            ActorPrimus.Initialise();

            Props addFriendProps = Props.Create<AddFriend>();
            addFriendActor = ActorPrimus.MainActorSystem.ActorOf(addFriendProps, "addFriendActor");

            MessagingCenter.Subscribe<DBSupervisor>(this, "noInternet", (sender) =>
            {
                Console.WriteLine("************************************************************MessagingCenter noInternet");
                DisplayAlert("No Internet Connection.", "The application cannot connect to the internet, please ensure that your device is connected to a valid network.", "OK");
            });
        }

        void InitializePageDesign() // to set the elements on the Log in page to the colours set in the Constants Class
        {
            BackgroundColor = Constants.BackgroundColour;
            Label_Username.TextColor = Constants.MainTextColour;
            Entry_Username.TextColor = Constants.MainTextColour;
            Label_Description.TextColor = Constants.MainTextColour;
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


        private void Button_NavLocator_Clicked(object sender, EventArgs e)
        {
            string username = "";
            ActorPrimus.stopActors();
            Navigation.PushModalAsync(new LocatorPage(username));
            //NavigationCode.GoLocator();
        }


        private void Button_Exit_Clicked(object sender, EventArgs e)
        {
            NavigationCode.ExitApp();
        }

        private void Button_Submit_Clicked(object sender, EventArgs e)
        {
            
            // if there is a value in the username textbox
            if (!string.IsNullOrWhiteSpace(Entry_Username.Text))
            {
                Console.WriteLine("************AddFriendActor Called");
                // if the entered username exists
                if (Entry_Username.Text != User.Username)
                {
                    if (User.CheckInternetConnection())
                    {
                        if (DBSupervisor.RedisDB.KeyExists(Entry_Username.Text))
                        {
                             if (checkFriend())
                             {
                                if (checkPendingList())
                                {
                                    DisplayAlert("Request Already Exists", "You currently have a friend request pending with " + Entry_Username.Text, "OK");
                                    return;
                                }
                                else if(checkUserPendingList())
                                {
                                    DisplayAlert("Check Pending Requests",Entry_Username.Text + " has a pending friend request lodged with you, please check your pending " +
                                        "friends requests.", "OK");
                                    return;
                                }
                                else
                                {
                                    ActorPrimus.DBSupervisorActor.Tell(new DBSupervisor.AddFriendCommand(Entry_Username.Text, addFriendActor));
                                    DisplayAlert("Request Made", "A friend request has been made to " + Entry_Username.Text, "OK");
                                }
                             }


                        }
                        else
                        {
                            DisplayAlert("Invalid Username", "The username you have entered is not recognised, please try another", "OK");
                            return;
                        }

                    }
                    else
                    {
                          DisplayAlert("No Internet Connection.", "The application cannot connect to the internet, please ensure that your device is connected to a valid network.", "OK");
                          return;
                    }
            }
                else
                {
                    DisplayAlert("Alert", "Please do not enter your own username.", "OK");
                    return;
                }

            }
            else
            {
                DisplayAlert("Friends Username", "Please enter a username into the labeled box", "OK");
                return;
            }
        }

        private bool checkFriend()
        {
            var length = DBSupervisor.RedisDB.ListLength(User.Username + "Friends");
            bool check = true;

            // if the user already has a list in which other users usernames can be stored
            if (DBSupervisor.RedisDB.KeyExists(User.Username + "Friends"))
            {
                
                for(int i = 0; i < length; i++)
                {
                    var value = DBSupervisor.RedisDB.ListGetByIndex(User.Username + "Friends", i);
                    // check if the user entered by the current user is already in the current users friend list
                    if (value.ToString().Contains(Entry_Username.Text))
                    {
                        DisplayAlert("Friend Exists", "The username you entered is already associated as a friend to this account", "OK");
                        check = false;
                    }
                }

                // if the entered username already exists in the current users friend list return false so that it is not entered again 
                if (check is false)
                {
                    return false;
                }
                else
                {
                    return true;
                }
                
            }
            else
            {
                // otherwise return true so that a list can be created using the current users username + Friends
                return true;
            }
        }

        bool checkPendingList()
        {
            // will check the specified users pending list to see if the current user exists in it            
            bool requestExists = false;
            // if the username exists

            if (DBSupervisor.RedisDB.KeyExists(Entry_Username.Text+"PendingFriends"))
            {
                var length = DBSupervisor.RedisDB.ListLength(Entry_Username.Text + "PendingFriends");
                for (int i = 0; i < length; i++)
                {
                    var value = DBSupervisor.RedisDB.ListGetByIndex(Entry_Username.Text + "PendingFriends", i);
                    if (User.Username == value.ToString())
                    {
                        // if the current user already has a request pending
                        requestExists = true;
                    }

                }

                return requestExists;
            }

            else
            {
                return requestExists;
            }

        }

        bool checkUserPendingList()
        {
            // will check to see if a request from the specified user is already lodged with the current user 
            bool requestExists = false;
            if (DBSupervisor.RedisDB.KeyExists(User.Username + "PendingFriends"))
            {
                var length = DBSupervisor.RedisDB.ListLength(User.Username + "PendingFriends");
                for (int i = 0; i < length; i++)
                {
                    var value = DBSupervisor.RedisDB.ListGetByIndex(User.Username + "PendingFriends", i);
                    if (Entry_Username.Text == value.ToString());
                    // if the requested user already has a request pending with the current user
                    requestExists = true;
                }
                return requestExists;
            }
            else
            {
                return requestExists;
            }
        }
    }
}