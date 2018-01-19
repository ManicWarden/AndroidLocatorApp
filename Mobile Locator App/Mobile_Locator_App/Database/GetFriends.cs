using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;
using Mobile_Locator_App.Code;

namespace Mobile_Locator_App.Database
{
    // retrieve the list of currentUserIDFriends from redis server
    class GetFriends : UntypedActor
    {
        public const string StartCommand = "start";
        public const string ExitCommand = "exit";
        private readonly IActorRef _getFriendsActor;
        private readonly string _password;

        public GetFriends(IActorRef getFriendsActor)
        {
            _getFriendsActor = getFriendsActor;
            
            List<string> Friends = new List<string>();
            Friends = RetrieveFriends();
        }

        private List<string> RetrieveFriends()
        {
            List <string> Friends = new List<string>();
            /*var aPending = db.StringGetAsync("a");
            var bPending = db.StringGetAsync("b");
            var a = db.Wait(aPending);
            var b = db.Wait(bPending);
             */
            // get list of friends usernames by using the 
            // key "currentUsername"Friends then sending said list to the 
            // display function
            if (DBSupervisor.RedisDB.KeyExists(User.Username + "Friends"))
            {
                // get the list of friends

                var length = DBSupervisor.RedisDB.ListLength(User.Username + "Friends");
                for (int i = 0; i < length; i++)
                {
                    var value = DBSupervisor.RedisDB.ListGetByIndex(User.Username + "Friends", i);
                    Friends.Add(value.ToString());
                }
                
            }
            return Friends;
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
