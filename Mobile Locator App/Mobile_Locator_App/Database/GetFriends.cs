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

        protected override void OnReceive(object message)
        {
            if (message.Equals(StartCommand))
            {
                // method call
                
            }

        }
    }
}
