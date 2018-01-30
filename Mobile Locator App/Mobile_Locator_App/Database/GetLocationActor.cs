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
    class GetLocationActor : UntypedActor, ILocationListener
    {


        private readonly IActorRef _getLocationActor;
        private readonly double _longitude;
        private readonly double _latitude;
        private readonly Context mContext;
        Location currentLocation;
        LocationManager locationManager;

        public IntPtr Handle => throw new NotImplementedException();

        /*public GetLocationActor(IActorRef getLocationActor)
        {

            _getLocationActor = getLocationActor;
            _longitude = Longitude;
            _latitude = Latitude;
            InitialiseLocationManager();
        }*/

        public GetLocationActor(Context mContext)
        {
            this.mContext = mContext;
        }

        private void InitialiseLocationManager()
        {
            locationManager = (LocationManager)Android.App.Application.Context.GetSystemService(Context.LocationService);
            locationManager = (LocationManager)MainActivity.activity.GetSystemService(Context.LocationService);





        }

        private void getLocation()
        {

        }

        protected override void OnReceive(object message)
        {

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
