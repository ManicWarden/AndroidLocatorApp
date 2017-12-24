using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;
using StackExchange.Redis;

namespace Mobile_Locator_App.Database
{
    class DBSupervisor : UntypedActor
    {


        /// <summary>
        /// Actor that supervises the actors that manipulate the database
        /// will receive commands and send passed data to the relevant actors
        /// </summary>

        //public const string createUserCommand = "createUser";
        //public const string createFriendCommand = "createFriend";
        //public const string getUserCommand = "getUser";
        //public const string getFriendsCommand = "getFriends";
        private readonly IActorRef _createUser;
        private readonly IActorRef _createFriend;
        private readonly IActorRef _getFriends;
        private readonly IActorRef _getUser;

        public class CreateUserCommand
        {
            public CreateUserCommand(string userName, string password, IActorRef createUserActor)
            {
                Username = userName;
                CreateUserActor = createUserActor;
                Password = password;
            }

            public string Username { get; private set; }
            public string Password { get; private set; }

            public IActorRef CreateUserActor { get; private set; }

        }

        public class CreateFriendCommand
        {
            public CreateFriendCommand(string userName, IActorRef createFriendActor)
            {
                Username = userName;
                CreateFriendActor = createFriendActor;
            }

            public string Username { get; private set; }
            public IActorRef CreateFriendActor { get; private set; }


        }

        public class GetUserCommand
        {
            public GetUserCommand(string userName, IActorRef getUserActor)
            {
                Username = userName;
                GetUserActor = getUserActor;
                
            }

            public string Username { get; private set; }

            public IActorRef GetUserActor { get; private set; }

        }

        public class GetFriendsCommand
        {
            public GetFriendsCommand(string userName, IActorRef getFriendsActor)
            {
                Username = userName;
                GetFriendsActor = getFriendsActor;
                
            }

            public string Username { get; private set; }
 
            public IActorRef GetFriendsActor { get; private set; }

        }

        protected override void OnReceive(object message)
        {
            // creating a parent/child relationship between the created 
            // Actor instance and the DBSupervisor

            if (message is CreateUserCommand)
            {
                var msg = message as CreateUserCommand;
                
                Context.ActorOf(Props.Create(
                () => new CreateUser(msg.CreateUserActor, msg.Username, msg.Password)));
            }
            if (message is CreateFriendCommand)
            {
                var msg = message as CreateFriendCommand;

                Context.ActorOf(Props.Create(
                () => new CreateFriend(msg.CreateFriendActor, msg.Username)));
            }
            if (message is GetUserCommand)
            {
                var msg = message as GetUserCommand;

                Context.ActorOf(Props.Create(
                () => new GetUser(msg.GetUserActor, msg.Username)));
            }
            if (message is GetFriendsCommand)
            {
                var msg = message as GetFriendsCommand;

                Context.ActorOf(Props.Create(
                () => new GetFriends(msg.GetFriendsActor, msg.Username)));
            }
        }

    }

}
