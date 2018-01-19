using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mobile_Locator_App.Code;
using Mobile_Locator_App.Database;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Text.RegularExpressions;
using Akka.Actor;

namespace Mobile_Locator_App.Xaml
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Registration : ContentPage
	{

        //private readonly IActorRef _createUserActor;
        private readonly IActorRef dbSupervisor;
        private readonly IActorRef createUserActor;
        public Registration()
        {
            InitializeComponent();
            
            InitializePageDesign();
            
            ActorPrimus.Initialise();

            Props createUserProps = Props.Create<CreateUser>();
            createUserActor = ActorPrimus.MainActorSystem.ActorOf(createUserProps, "createUserActor");

        }

        void InitializePageDesign() // to set the elements on the Log in page to the colours set in the Constants Class
        {
            BackgroundColor = Constants.BackgroundColour;
            Label_Username.TextColor = Constants.MainTextColour;
            Label_Password.TextColor = Constants.MainTextColour;
            ActivitySpinner.IsVisible = false;
            LogInIcon.HeightRequest = Constants.LogInIconHeight;

            // Will shift user focus to the next entry in the list, therefore username => password => forename => surname => email => phone number
            Entry_Username.Completed += (s, e) => Entry_Password.Focus(); // when the user has filled in the username and the user has hit the done button on the keyboard the focus will be set to the password textbox
            /*Entry_Password.Completed += (s, e) => Entry_Forename.Focus();
            Entry_Forename.Completed += (s, e) => Entry_Surname.Focus();
            Entry_Surname.Completed += (s, e) => Entry_Email.Focus();
            Entry_Email.Completed += (s, e) => Entry_PhoneNo.Focus();*/
            //Entry_PhoneNo.Completed += (s, e) => ValidateRegistration(s, e);// will call the ValidateRegistration function when the phone number has been entered.
            //Button_CreateAccount.Clicked += (s, e) => ValidateRegistration(s, e); 
        }

        void ValidateRegistration(object sender, EventArgs e)
        {
            // code to check the veracity of the entered details and display error messages to the user
            // then either reject the registration attempt or send the data to the database by calling the enter database function in the database script


            if (string.IsNullOrWhiteSpace(Entry_Username.Text) || // checks the entries in the textboxes to ensure that seome value exists (That is not spaces)
                string.IsNullOrWhiteSpace(Entry_Password.Text))
                /*string.IsNullOrWhiteSpace(Entry_Forename.Text) ||
                string.IsNullOrWhiteSpace(Entry_Surname.Text) ||
                string.IsNullOrWhiteSpace(Entry_Email.Text) ||
                string.IsNullOrWhiteSpace(Entry_PhoneNo.Text))*/
            {
                DisplayAlert("Registration", "Registration Failed, please ensure that all fields are filled", "OK"); // a message box will be displayed to the user,
                                                                                                                     // First field is the title, second the main text, third the continue button
                return;
            }
            else
            {
                if (Entry_Username.Text.Contains(" "))
                {
                    DisplayAlert("Username", "Please ensure that the username does not contain any spaces.", "OK");
                    return;
                }

                if(checkUsername(Entry_Username.Text))
                {
                    DisplayAlert("Username", "That username is already in use, please enter another.", "OK");
                    return;
                }

           
                if (Entry_Password.Text.Length < 8 || Entry_Password.Text.Contains(" ")) // To ensure the password is of appropriate length
                {
                    DisplayAlert("Password", "Password should be at least 8 characters long and doesn't contain any spaces.", "OK");
                    return;
                }

                /*if (Entry_Forename.Text.Contains(" "))
                {
                    DisplayAlert("Forename", "Please ensure that the forename field does not contain any spaces.", "OK");
                }

                if (Entry_Surname.Text.Contains(" "))
                {
                    DisplayAlert("Forename", "Please ensure that the forename field does not contain any spaces.", "OK");
                }

                else if (!IsValidEmail(Entry_Email.Text)) // to ensure that the entered email address is valid
                {
                    DisplayAlert("Email Address", "Email Address is invalid, please check the entered address for errors and ensure that the email address is valid.", "OK");
                    return;
                }

                // to validate phone number try using dependency service to get the phone number from the device, this only works on android and not all of the time
                // Maybe send a message to the given phone number with a security code that must be entered before finalising registration.
                if (!IsValidPhoneNo(Entry_PhoneNo.Text))
                {
                    DisplayAlert("Phone Number", "The entered phone number is invalid, please check it and ensure that there are no non-number characters and that all the numbers are present.", "OK");
                    return;
                }*/

                else
                {

                    // if the username does not exist in the database then call the CreateUser Actor
                    Console.WriteLine("*******************************************Actor message sent.");
                    ActorPrimus.DBSupervisorActor.Tell(new DBSupervisor.CreateUserCommand(Entry_Username.Text, Entry_Password.Text, createUserActor));

                    //DatabaseActions.CreateUser(Entry_Username.Text, Entry_Password.Text, Entry_Forename.Text, Entry_Surname.Text, Entry_Email.Text, Entry_PhoneNo.Text);
                    //string username, string password, string forename, string surname, string email, string phoneNumber
                    User user = new User(Entry_Username.Text, Entry_Password.Text);
                    DisplayAlert("Registration Complete", "Registration Complete, Enjoy.", "OK");
                    Navigation.PushModalAsync(new Mobile_Locator_App.Xaml.HomePage());
                    // move user to the home page

                }

            }

        }

        bool IsValidEmail(string email)
        {
            // I think the try sends a message to the email address passed, if it succeeds then the function returns a true, 
            // if it doesn't succeed the the try fails and the catch returns a false. Gotten from https://stackoverflow.com/questions/1365407/c-sharp-code-to-validate-email-address


            if (email.Contains("\"\"")) // if the string email contains ""
            {
                if (email.Substring(email.LastIndexOf('"') + 1).Contains("@")) // if an @ symbol comes after the "" then it can be valid
                {
                    return true;
                }

                else
                {
                    return false;
                }
            }

            try
            {
                var addr = new System.Net.Mail.MailAddress(email); // Doesn't work for Universal Windows Phone, may be missing a reference
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        bool IsValidPhoneNo(string PhoneNo)
        {
            Console.WriteLine("******************************************Phone Validation Phone Number length = " + PhoneNo.Length);
            //Jury rigged for different sized numbers until the text message verification is coded
            // as different areas have different mobile phone number lengths
            if (PhoneNo.Length == 9)
            {
                Console.WriteLine("******************************************Phone Length == 9");
                return Regex.Match(PhoneNo, @"^[0-9]{9}$").Success; // {} dictates the length of the number

            }
            else if (PhoneNo.Length == 10)
            {
                Console.WriteLine("******************************************Phone Length == 10");
                return Regex.Match(PhoneNo, @"^[0-9]{10}$").Success;


            }
            else if (PhoneNo.Length == 11)
            {
                Console.WriteLine("******************************************Phone Length == 11");
                return Regex.Match(PhoneNo, @"^[0-9]{11}$").Success;


            }
            else if (PhoneNo.Length == 12)
            {
                Console.WriteLine("******************************************Phone Length == 12");
                return Regex.Match(PhoneNo, @"^[0-9]{12}$").Success;


            }

            else if (PhoneNo.Length == 13)
            {
                Console.WriteLine("******************************************Phone Length == 13");
                return Regex.Match(PhoneNo, @"^[0-9]{13}$").Success;


            }
            else
            {
                Console.WriteLine("******************************************Phone Length = nothing");
                return false; // if a number is entered that is not between 9 and 13 digits or if the string has characters other than numbers in it


            }
        }

        public static bool checkUsername(string _username)
        {
            // will use the given username to check if it already exists

            if (DBSupervisor.RedisDB.KeyExists(_username))
            {
                return true;
            }

            else
                return false;
        }

        void ToLogInPage(object sender, EventArgs e)
        {
            // close this page and open the Log in page, though not necessarily in that order
            Navigation.PushModalAsync(new LogInPage());
        }

        void ExitApplication(object sender, EventArgs e)
        {
            // Call exit application function in the global class
            NavigationCode.ExitApp();
        }
    }
}