using System;
using System.Collections.Generic;
using System.Text;

namespace Mobile_Locator_App.Code
{
    public class User
    {
        public int ID { get; set; } // to get the user ID
        public static string Username { get; set; } // a unique string, static so that it can be called throughout the app
        public static string Password { get; set; } // a unique string, static so that it can be called throughout the app
        // will store the users location at a given time
        public static string Latitude { get; set; } 
        public static string Longitude { get; set; }
        public static List<string> friendsToLocate = new List<string>();
        //public static int UserID { get; set; }

        public User() { } // for database manipulation later
        public User(string username, string password) // creates an instance of the user using the relevant username and password
        {
            Username = username;
            Password = password;

            //UserID = Database.DatabaseActions.GetUserID(); // sets UserID to the currents users UserID inside the Users Database dependant on the current username
                                                           //this.Username = Username;
                                                           // this.Password = Password;
        }

        public bool CheckUserData()
        {
            /*if (!this.Username.Equals("") && !this.Password.Equals("")) // if both the username and password have values, this. can only be used for non-static variables
            {
                return true;
            }*/
            if (!Username.Equals("") && !Password.Equals("")) // if both the username and password have values
            {
                return true;
            }
            else// if the username or password is empty (Therefore not filled in by the user on the log in page)
                return false;

        }

        public static void addToFriendsToLocate(string username)
        {
            friendsToLocate.Add(username);
        }
    }
}
