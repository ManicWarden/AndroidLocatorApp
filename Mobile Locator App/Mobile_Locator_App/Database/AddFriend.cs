using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;
using Mobile_Locator_App.Code;

namespace Mobile_Locator_App.Database
{
    // use the RPush redis command to add a friend ID onto the list of 
    // currentUserIDFriends

        /// <summary>
        /// Actor that inserts a users username
        /// into the current users friend list
        /// </summary>
    class AddFriend : UntypedActor
    {

        private readonly IActorRef _addFriendActor;
        private readonly string _username;
        private readonly string _password;



        public AddFriend(IActorRef addFriendActor, string username)
        {
            _addFriendActor = addFriendActor;
            _username = username;
            addFriend();
        }

        private void addFriend()
        {
            // add a new friend to the list 
            // may have to use hashes instead of lists

            Console.WriteLine("********************************** addFriend running");
            DBSupervisor.RedisDB.ListRightPush(User.Username + "Friends", _username);     
        }

        protected override void OnReceive(object message)
        {
            Console.WriteLine("***************************************************** addFriendActor onReceive");
            addFriend();


        }
    }
}
