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
	public partial class LogInPage : ContentPage
	{
        public LogInPage()
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
            Label_Username.TextColor = Constants.MainTextColour;
            Label_Password.TextColor = Constants.MainTextColour;
            ActivitySpinner.IsVisible = false;
            LogInIcon.HeightRequest = Constants.LogInIconHeight;

            Entry_Username.Completed += (s, e) => Entry_Password.Focus(); // when the user has filled in the username and the user has hit the done button on the keyboard the focus will be set to the password textbox
            Entry_Password.Completed += (s, e) => UserLogIn(s, e); // when the password has been filled in and the user has hit the done button on the keyboard the UserLogIn function will run
        }

        void UserLogIn(object sender, EventArgs e) // when the user clicks the log in button
        {
            // Put Some Validation Code
             // will send the data entered into the textboxes by the user to the User class for initialization  
            if (string.IsNullOrWhiteSpace(Entry_Username.Text)) // if CheckUserData returns a false value (Therefore one or both of the username and password values is empty)
            {

                DisplayAlert("Login", "Login Failed, Please enter a username", "OK");
                return;

            }
            if (string.IsNullOrWhiteSpace(Entry_Password.Text))
            {
                DisplayAlert("Login", "Login Failed, Please enter a password", "OK");
                return;
            }

            if (checkUsername())
            {
                 User user = new User(Entry_Username.Text, Entry_Password.Text);
                 DisplayAlert("Login", "Login Succeeded", "OK");// First is label, second is text, third is button
                                                                // move user to the home page
                Navigation.PushModalAsync(new Mobile_Locator_App.Xaml.HomePage());
            }

            else
            {
                DisplayAlert("Login", "Login Failed, the username/password combination was incorrect", "OK ");
                Entry_Username.Text = "";
                Entry_Password.Text = "";
                return;
            }
        }

        bool checkUsername()
        {
            // will use the given username and password combination to check if it already exists

            // if the username exists
            if (DBSupervisor.RedisDB.KeyExists(Entry_Username.Text))
            {
                // if the password for that username is the same as the user entered
                if(DBSupervisor.RedisDB.StringGet(Entry_Username.Text) == Entry_Password.Text)
                {
                    return true;
                }
                
                else
                {
                    return false;
                }
                
            }

            else
            {
                return false;
            }
        }

        void UserRegister(object sender, EventArgs e)
        {
            // code to close the log in page and open the register page
            Navigation.PushModalAsync(new Mobile_Locator_App.Xaml.Registration());
        }

        void ExitApplication(object sender, EventArgs e)
        {
            // Call exit application function in the global class
            NavigationCode.ExitApp();
        }
    }
}