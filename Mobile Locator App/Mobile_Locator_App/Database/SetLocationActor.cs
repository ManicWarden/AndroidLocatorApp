using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;

namespace Mobile_Locator_App.Code
{
    class SetLocationActor : UntypedActor
    {


        private readonly IActorRef _setLocationActor;
        private readonly double _longitude;
        private readonly double _latitude;

        // use this to get users current location and send it to the server every x minutes/seconds
        // e.g. someMessage = getUserLocation
        // then pass someMessage to the actor that will update the database

        /*var system = ActorSystem.Create("MySystem");
    var someActor = system.ActorOf<SomeActor>("someActor");
    var someMessage = new FetchFeed() {Url = ...};
    // schedule recurring message
    system
        .Scheduler
        .ScheduleTellRepeatedly(TimeSpan.FromMinutes(30), // initial delay of 30 min
        TimeSpan.FromMinutes(30), // recur every 30 minutes
        someActor, someMessage, ActorRefs.Nobody);
 * */

        public SetLocationActor(IActorRef setLocationActor, double Longitude, double Latitude)
        {
            _setLocationActor = setLocationActor;
            _longitude = Longitude;
            _latitude = Latitude;
        }

        private void setLocation()
        {

        }
        protected override void OnReceive(object message)
        {
            
        }
    }
}
