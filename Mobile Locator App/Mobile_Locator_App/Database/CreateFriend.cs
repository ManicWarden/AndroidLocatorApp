using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mobile_Locator_App.Database
{
    // use the RPush redis command to add a friend ID onto the list of 
    // currentUserIDFriends
    class CreateFriend : UntypedActor
    {
        public const string StartCommand = "start";
        public const string ExitCommand = "exit";
        private readonly IActorRef _createFriendActor;
        private readonly string _username;
        private readonly string _password;



        public CreateFriend(IActorRef createFriendActor, string username)
        {
            _createFriendActor = createFriendActor;
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
