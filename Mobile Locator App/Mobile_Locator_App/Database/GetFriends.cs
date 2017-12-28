using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mobile_Locator_App.Database
{
    // retrieve the list of currentUserIDFriends from redis server
    class GetFriends : UntypedActor
    {
        public const string StartCommand = "start";
        public const string ExitCommand = "exit";
        private readonly IActorRef _getFriendsActor;
        private readonly string _username;
        private readonly string _password;

        public GetFriends(IActorRef getFriendsActor, string username)
        {
            _getFriendsActor = getFriendsActor;
            _username = username;
        }

        private void RetrieveFriends()
        {
            /*var aPending = db.StringGetAsync("a");
            var bPending = db.StringGetAsync("b");
            var a = db.Wait(aPending);
            var b = db.Wait(bPending);
             */
            // get list of friends usernames by using the 
            // key "currentUsername"Friends then sending said list to the 
            // display function
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
