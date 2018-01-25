using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;
using Mobile_Locator_App.Code;

namespace Mobile_Locator_App.Database
{
    class ConfirmFriendRequest : UntypedActor
    {

        private readonly IActorRef _addFriendActor;
        private readonly string _username;




        public ConfirmFriendRequest(IActorRef confirmFriendRequestActor, string Username)
        {
            _addFriendActor = confirmFriendRequestActor;
            _username = Username;

            ConfirmRequest();
            
        }

        private void ConfirmRequest()
        {
            Console.WriteLine("********************************** addFriend running");
            DBSupervisor.RedisDB.ListRightPush(User.Username + "Friends", _username);     
        }


        protected override void OnReceive(object message)
        {
            
        }
    }
}
