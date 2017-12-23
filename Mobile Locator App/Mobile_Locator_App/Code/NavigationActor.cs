using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mobile_Locator_App.Code
{
    class NavigationActor : UntypedActor
    {

        public static void GoHome()
        {
            //Navigation.PushModalAsync(new HomePage());
        }

        public static void GoFriends()
        {

        }

        public static void GoAddFriends()
        {

        }

        public static void GoLocator()
        {

        }

        public static void GoSettings()
        {

        }

        public static void ExitApp()
        {
            Environment.Exit(0);
        }
        // The method that decides what to do when the Actor receives a message depending on its contents
        protected override void OnReceive(object message)
        {

        }
    }
}
