using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;
using Xamarin.Forms;

namespace Mobile_Locator_App.Database
{
    class CheckUsernameActor : UntypedActor
    {
        public const string StartCommand = "start";
        public const string ExitCommand = "exit";

        public static bool checkUsername(string _username)
        {
            // will use the given username to check if it already exists
            if (DBSupervisor.RedisDB.KeyExists(_username))
            {
                return true;
            }

            else
                return false;
        }

        protected override void OnReceive(object message)
        {
            var msg = message as string;


        }
    }
}
