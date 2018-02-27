using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;
using Mobile_Locator_App.Code;
using Xamarin.Forms;

namespace Mobile_Locator_App.Database
{
    class GetPendingFriends : UntypedActor
    {
        private List<string> PendingFriends = new List<string>();

        public GetPendingFriends(IActorRef getPendingFriendsActor)
        {
            if (!Code.User.CheckInternetConnection())
            {
                throw new Exception();
            }
            RetrievePendingFriends();
        }

        private void RetrievePendingFriends()
        {
            Console.WriteLine("*********************************************************RetrieveFriends After");
            if (DBSupervisor.RedisDB.KeyExists(User.Username + "PendingFriends"))
            {
                Console.WriteLine("**********************************************************DBSupervisor.RedisDB.KeyExists");
                // get the list of friends

                var length = DBSupervisor.RedisDB.ListLength(User.Username + "PendingFriends");
                for (int i = 0; i < length; i++)
                {
                    Console.WriteLine("**********************************************************Pendingfor");
                    Console.WriteLine("********************************************* Current PendingFriend value = " + DBSupervisor.RedisDB.ListGetByIndex(User.Username + "PendingFriends", i));
                    var value = DBSupervisor.RedisDB.ListGetByIndex(User.Username + "PendingFriends", i);
                    PendingFriends.Add(value.ToString());
                }
                MessagingCenter.Send<GetPendingFriends, List<string>>(this, "hasFriends", PendingFriends);
            }
            else
            {
                Console.WriteLine("**********************************************************DBSupervisor.RedisDB.KeyExists(User.Username + PendingFriends) not found ");
                // if a friends list is not found
                MessagingCenter.Send<GetPendingFriends>(this, "hasNoFriends");
            }
        }

        protected override void OnReceive(object message)
        {

        }
    }
}
