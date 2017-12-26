using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mobile_Locator_App.Database
{
    // use HMGET to retrieve user data based on the unique id
    // username
    class GetUser : UntypedActor
    {
        public const string StartCommand = "start";
        public const string ExitCommand = "exit";
        private readonly IActorRef _getUserActor;
        private readonly string _username;
        private readonly string _password;

        public GetUser(IActorRef getUserActor, string username)
        {
            _getUserActor = getUserActor;
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
