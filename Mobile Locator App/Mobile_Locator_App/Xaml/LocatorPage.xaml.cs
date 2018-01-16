﻿using System;
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
	public partial class LocatorPage : ContentPage
	{
        public LocatorPage()
        {
            InitializeComponent();
            InitializePageDesign();
        }

        void InitializePageDesign() // to set the elements on the Log in page to the colours set in the Constants Class
        {
            BackgroundColor = Constants.BackgroundColour;
            //Label_Username.TextColor = Constants.MainTextColour;
            //Label_Password.TextColor = Constants.MainTextColour;
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


        private void Button_Exit_Clicked(object sender, EventArgs e)
        {
            NavigationCode.ExitApp();
        }
    }

}