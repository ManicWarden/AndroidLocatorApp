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
	public partial class SettingsPage : ContentPage
	{
        public SettingsPage()
        {
            InitializeComponent();
        }

        private void Button_NavHome_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new HomePage());
            //NavigationCode.GoHome();
        }

        private void Button_NavFriends_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new FriendsPage());
            //NavigationCode.GoFriends();
        }

        private void Button_NavAddFriends_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new AddFriendsPage());
            //NavigationCode.GoAddFriends();
        }

        private void Button_NavLocator_Clicked(object sender, EventArgs e)
        {
            string username = "";
            Navigation.PushModalAsync(new LocatorPage(username));
            //NavigationCode.GoLocator();
        }

        private void Button_NavSettings_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new SettingsPage());
            //NavigationCode.GoSettings();
        }

        private void Button_Exit_Clicked(object sender, EventArgs e)
        {
            NavigationCode.ExitApp();
        }
    }
}