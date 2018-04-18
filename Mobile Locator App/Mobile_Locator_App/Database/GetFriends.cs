using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;
using Mobile_Locator_App.Code;
using Mobile_Locator_App.Xaml;
using Xamarin.Forms;

namespace Mobile_Locator_App.Database
{
    // retrieve the list of currentUserIDFriends from redis server
    class GetFriends : UntypedActor
    { 
        public const string StartCommand = "start";
        public const string ExitCommand = "exit";
        private readonly IActorRef _getFriendsActor;
        
        private List<string> Friends = new List<string>();

        public GetFriends(IActorRef getFriendsActor)
        {
            Console.WriteLine("//////////////Actor getFriends: " + Self);
            _getFriendsActor = getFriendsActor;

            //List<string> Friends = new List<string>();
            //Friends = RetrieveFriends();
            if (!Code.User.CheckInternetConnection())
            {
                throw new Exception();
            }
            Console.WriteLine("**********************************************************GetFriends***");
            RetrieveFriends();
            
        }


        private void RetrieveFriends()
        {
            Console.WriteLine("**********************************************************RetrieveFriends***");
            ///////////////////////////THIS IS WHERE IT BREAKS
            //HomePage home = new HomePage(); // THIS IS THE CAUSE


            //Console.WriteLine("**********************************************************HomePage home = new HomePage");


            
            /*var aPending = db.StringGetAsync("a");
            var bPending = db.StringGetAsync("b");
            var a = db.Wait(aPending);
            var b = db.Wait(bPending);
             */
            // get list of friends usernames by using the 
            // key "currentUsername"Friends then sending said list to the 
            // display function
            if (!Code.User.CheckInternetConnection())
            {
                throw new Exception();
            }
            Console.WriteLine("**********************************************************RetrieveFriends After");
            if (DBSupervisor.RedisDB.KeyExists(User.Username + "Friends"))
            {

                Console.WriteLine("**********************************************************DBSupervisor.RedisDB.KeyExists");
                // get the list of friends

                var length = DBSupervisor.RedisDB.ListLength(User.Username + "Friends");
                for (int i = 0; i < length; i++)
                {

                    Console.WriteLine("**********************************************************for");
                    var value = DBSupervisor.RedisDB.ListGetByIndex(User.Username + "Friends", i);
                    Friends.Add(value.ToString());
                }
                MessagingCenter.Send/*<GetFriends, List<string>>*/(this, "hasFriends", Friends);
            }
            else
            {
                Console.WriteLine("**********************************************************DBSupervisor.RedisDB.KeyExists(User.Username + Friends) not found ");
                // if a friends list is not found
                MessagingCenter.Send<GetFriends>(this, "hasNoFriends");
            }
            // once finished and the list has at least one value the home page loadFriends function will
            // be called and the list will be passed to it.
            /*Console.WriteLine("**********************************************************QWERERY");
            Console.WriteLine("**********************************************************Friends.Count = " + Friends.Count);
            if (Friends.Count > 0)
            {
                Console.WriteLine("**********************************************************Friends.Count > 0");
                home.loadFriends(Friends);
            }
            else
            {
                Console.WriteLine("**********************************************************else");
                home.noFriends();
            }*/

            

        }

        protected override void OnReceive(object message)
        {
            if (message.Equals(StartCommand))
            {
                // method call
                
            }

        }
    }
}
