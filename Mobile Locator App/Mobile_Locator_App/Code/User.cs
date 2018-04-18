using System;
using System.Collections.Generic;
using System.Net;
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
        public static bool locationEnabled = true;
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


        public static void addToFriendsToLocate(string username)
        {
            friendsToLocate.Add(username);
        }



        /// <summary>
        /// To check if the user is connected to the internet
        /// if so a true is returned if not a false
        /// </summary>
        /// <returns>Bool</returns>
        public static bool CheckInternetConnection()
        {
            string url = "http://google.com";

            try
            {
                HttpWebRequest internetRequest = (HttpWebRequest)WebRequest.Create(url);
                internetRequest.Timeout = 5000;

                WebResponse internetResponse = internetRequest.GetResponse();

                Console.WriteLine("*******************************Connection established " + internetRequest.ToString());

                internetResponse.Close();

                return true;
            }
            catch (WebException ex)
            {
                Console.WriteLine("******************************Connection Failed" + ex.ToString());
                return false;
            }
        }
    }
}
