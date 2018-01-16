using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mobile_Locator_App.Code;
using Mobile_Locator_App.Database;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile_Locator_App.Xaml
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
        public HomePage()
        {
            InitializeComponent();
            InitializePageDesign();
        }

        void InitializePageDesign() // to set the elements on the Log in page to the colours set in the Constants Class
        {
            BackgroundColor = Constants.BackgroundColour;
            // call GetFriends to populate the listview
            GetFriends();
            DisplayAlert("Username", "Current user is " + User.Username, "OK");
        }

        private void GetFriends()
        {
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




        private void Button_NavFriends_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Mobile_Locator_App.Xaml.FriendsPage());

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