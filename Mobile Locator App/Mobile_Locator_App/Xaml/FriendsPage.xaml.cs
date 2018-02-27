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
	public partial class FriendsPage : ContentPage
	{
        public FriendsPage()
        {
            InitializeComponent();
            InitializePageDesign();

            MessagingCenter.Subscribe<DBSupervisor>(this, "noInternet", (sender) =>
            {
                Console.WriteLine("************************************************************MessagingCenter noInternet");
                DisplayAlert("No Internet Connection.", "The application cannot connect to the internet, please ensure that your device is connected to a valid network.", "OK");
            });
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


        private void Button_NavAddFriends_Clicked(object sender, EventArgs e)
        {
            ActorPrimus.stopActors();
            Navigation.PushModalAsync(new AddFriendsPage());
            //NavigationCode.GoAddFriends();
        }

        private void Button_NavLocator_Clicked(object sender, EventArgs e)
        {
            ActorPrimus.stopActors();
            Navigation.PushModalAsync(new LocatorPage());
            //NavigationCode.GoLocator();
        }


        private void Button_Exit_Clicked(object sender, EventArgs e)
        {
            NavigationCode.ExitApp();
        }

    }
}