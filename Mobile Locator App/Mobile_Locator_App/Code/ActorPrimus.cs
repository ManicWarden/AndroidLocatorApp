using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;
using Xamarin.Forms;
using Mobile_Locator_App.Database;

namespace Mobile_Locator_App.Code
{
    #region ActorPrimus
    class ActorPrimus
    {
        public static ActorSystem MainActorSystem;

        static void Main(string[] args)
        {
            // Intialise the Actor System
            MainActorSystem = ActorSystem.Create("MainActorSystem");

            //Props to create the Actors that will be used in the ActorSystem
            // using the props as the recipe to create an agent that will 
            // be used to send messages to the relevant actor
            // using props allows for actors with the same properties to be created on
            // different machines, therefore good for scaling up, not necessary for a small program but good practice

            Props databaseActionsProps = Props.Create(() => new DatabaseActionsActor());
            IActorRef databaseActionsActor = MainActorSystem.ActorOf(databaseActionsProps, "databaseActionsActor");
            

            Props navigationProps = Props.Create(() => new NavigationActor());
            IActorRef navigationActor = MainActorSystem.ActorOf(navigationProps, "navigationActor");

            Props validationProps = Props.Create(() => new ValidationActor());
            IActorRef validationActor = MainActorSystem.ActorOf(validationProps, "validationActor");

            // Start Commands

            // blocks the main thread from shutting down until the appropriate command is received
            MainActorSystem.WhenTerminated.Wait(); // always keep it as the last line of code in Main, anything below will not run
        }
    }
    #endregion
}
