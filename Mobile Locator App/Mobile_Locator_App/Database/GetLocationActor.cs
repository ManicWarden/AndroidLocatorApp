using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;
using Android.Locations;
using Android.Util;
using Android.OS;
using Android.Runtime;
using Android.App;
using Android.Content;
using Mobile_Locator_App.Droid;

namespace Mobile_Locator_App.Code
{
    [Activity(Label = "CurrentLocation", MainLauncher = true, Icon = "@drawable/icon")]
    class GetLocationActor : UntypedActor
    {


        private readonly IActorRef _getLocationActor;


        

        public IntPtr Handle => throw new NotImplementedException();

        public GetLocationActor(IActorRef getLocationActor)
        {

            _getLocationActor = getLocationActor;

        }



        protected override void OnReceive(object message){}

 
    }

    class getLocation : Java.Lang.Object, ILocationListener
    {
        Location currentLocation;
        LocationManager locationManager;
        private readonly Context mContext;

        //public IntPtr Handle => throw new NotImplementedException();

        public getLocation(Context mContext)
        {
            this.mContext = mContext;
            InitialiseLocationManager();
        }

        private void InitialiseLocationManager()
        {
            locationManager = (LocationManager)Android.App.Application.Context.GetSystemService(Context.LocationService);
            //locationManager = (LocationManager)MainActivity.activity.GetSystemService(Context.LocationService);
        }



        public void OnLocationChanged(Location location)
        {
            throw new NotImplementedException();
        }

        public void OnProviderDisabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnProviderEnabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

    }
}
