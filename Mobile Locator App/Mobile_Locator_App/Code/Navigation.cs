using Akka.Actor;
using Mobile_Locator_App.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mobile_Locator_App.Code
{

    

    public class NavigationCode
    {
        private readonly IActorRef DBSupervisorActor;

        public NavigationCode()
        {
            Props DBSupervisorProps = Props.Create<DBSupervisor>();
            DBSupervisorActor = ActorPrimus.MainActorSystem.ActorOf(DBSupervisorProps, "DBSupervisorActor");
        }
        public static void GoHome()
        {
            //Navigation.PushModalAsync(new Sequential.Xaml.LogInPage());
            // to stop actors while navigating
            
        }

        public void GoFriends()
        {
            // stops the DBSupervisorActor and all child actors after they 
            // have finished reading all messages in the inbox
            // a bit slower than the Stop message but safer as there will be less remnants
            DBSupervisorActor.Tell(PoisonPill.Instance);

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
    }
}
