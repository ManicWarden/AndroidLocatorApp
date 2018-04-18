using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;
using StackExchange.Redis;
using Xamarin.Forms;
namespace Mobile_Locator_App.Database
{
    public class DBSupervisor : UntypedActor
    {

        #region ActorAgents
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
        private readonly IActorRef _confirmFriendRequestActor;

        #endregion

        #region DBServer Variables
        /// <summary>
        /// These variables control access to the server on which the database
        /// resides and will be intialised as soon as a user attempts to log in, register
        /// or otherwise accesses the application.
        /// The variables are disposable but will be maintained for as long as
        /// the program is running as it allows for connection to the server 
        /// throughout the runtime of the application. 
        /// </summary>
        /// 
        private static ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("redis-15181.c6.eu-west-1-1.ec2.cloud.redislabs.com:15181");
        public static IDatabase RedisDB = redis.GetDatabase();


        //public static IServer RedisServer = redis.GetServer("redis-15181.c6.eu-west-1-1.ec2.cloud.redislabs.com:15181");

        #endregion

        #region Commands

        public class CreateUserCommand
        {

            public CreateUserCommand(string userName, string password, IActorRef createUserActor)
            {

                string value = userName;
                //RedisDB.StringSet(userName, password); // sets the key to the unique username and the value to the password
                Username = userName;
                CreateUserActor = createUserActor;
                Password = password;
            }

            public string Username { get; private set; }
            public string Password { get; private set; }

            public IActorRef CreateUserActor { get; private set; }

        }


        public class AddFriendCommand
        {
            public AddFriendCommand(string userName, IActorRef addFriendActor)
            {
                Username = userName;
                AddFriendActor = addFriendActor;
            }

            public string Username { get; private set; }
            public IActorRef AddFriendActor { get; private set; }


        }

       

        public class GetFriendsCommand
        {
            public GetFriendsCommand(IActorRef getFriendsActor)
            {
                GetFriendsActor = getFriendsActor;
            }
            public IActorRef GetFriendsActor { get; private set; }

        }

        public class ConfirmFriendRequestCommand
        {
            public ConfirmFriendRequestCommand(IActorRef confirmFriendRequestActor, string username)
            {
                ConfirmFriendRequestActor = confirmFriendRequestActor;
                Username = username;
            }

            public IActorRef ConfirmFriendRequestActor { get; private set; }
            public string Username { get; private set; }
        }

        public class GetPendingFriendsCommand
        {
            public GetPendingFriendsCommand(IActorRef getPendingFriendsActor)
            {
                GetPendingFriendsActor = getPendingFriendsActor;
            }

            public IActorRef GetPendingFriendsActor { get; private set; }
        }

        

        #endregion

        protected override void OnReceive(object message)
        {
            // creating a parent/child relationship between the created 
            // Actor instance and the DBSupervisor
            Console.WriteLine("*****************************************On Receive DBSupervisor");
            if (message is CreateUserCommand)
            {
                Console.WriteLine("*****************************************CreateUserCommand triggered");
                var msg = message as CreateUserCommand;

                Context.ActorOf(Props.Create(
                () => new CreateUser(msg.CreateUserActor, msg.Username, msg.Password)));
            }
            if (message is AddFriendCommand)
            {
                Console.WriteLine("*****************************************AddFriendCommand triggered");
                var msg = message as AddFriendCommand;

                Context.ActorOf(Props.Create(
                () => new AddFriend(msg.AddFriendActor, msg.Username)));
            }
            
            if (message is GetFriendsCommand)
            {
                Console.WriteLine("**********************************************************GetFriendsCommand");
                var msg = message as GetFriendsCommand;

                Context.ActorOf(Props.Create(
                () => new GetFriends(msg.GetFriendsActor)));
            }
            if(message is ConfirmFriendRequestCommand)
            {
                Console.WriteLine("**********************************************************ConfirmFriendRequestCommand");
                var msg = message as ConfirmFriendRequestCommand;

                Context.ActorOf(Props.Create(
                () => new ConfirmFriendRequest(msg.ConfirmFriendRequestActor, msg.Username)));
            }
            if(message is GetPendingFriendsCommand)
            {
                Console.WriteLine("**********************************************************GetPendingFriendsCommand");
                var msg = message as GetPendingFriendsCommand;

                Context.ActorOf(Props.Create(
                () => new GetPendingFriends(msg.GetPendingFriendsActor)));
            }
        }

        protected override SupervisorStrategy SupervisorStrategy()
        {
            // One for One strategy means that every child actor will be treated seperately
            // so that if one encounters an error then only that actor will be effected by
            // the Supervisor strategy. This is opposed to the AllforOneStrategy that
            // will in the case of a child actor experiencing an error whatever action is  
            // taken to attempt to fix said actor will be applied to all child actors
            return new OneForOneStrategy(
                maxNrOfRetries: 10, // giving the child actor 10 attempts to recover
                withinTimeRange: TimeSpan.FromSeconds(10), // giving the child actor 10 seconds to recover
                localOnlyDecider: ex =>
                {
                    // decides what the child actor should do dependant on 
                    // a specific exception
                    switch (ex)
                    {
                        case ArithmeticException ae:
                            return Directive.Resume;
                        case Exception e:
                            NoInternetError();
                            return Directive.Restart;
                        default:
                            return Directive.Stop;
                    }
                }
                );
        }

        /// <summary>
        /// this will send a message to anything that is listening
        /// in this case it will be whatever form the user is currently using 
        /// which will subsequently interpret the message and display something to the user
        /// such as "There is no internet connectivity"
        /// </summary>

        private void NoInternetError()
        {
            string Error = "Internet was disconnected";
            MessagingCenter.Send< DBSupervisor, string>(this, "noInternet", Error);
            //throw new Exception();
        }
    }

}
