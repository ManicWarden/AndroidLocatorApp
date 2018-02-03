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
using System.Linq;
using Xamarin.Forms;

namespace Mobile_Locator_App.Code
{
    [Activity(Label = "CurrentLocation", MainLauncher = true, Icon = "@drawable/icon")]
    class GetLocationActor : UntypedActor
    {


        private readonly IActorRef _getLocationActor;


        

        public IntPtr Handle => throw new NotImplementedException();

        public GetLocationActor(/*IActorRef getLocationActor*/ Context mContext)
        {

            //_getLocationActor = getLocationActor;
            getLocation GetLocation = new getLocation(mContext);
            GetLocation.findLocation();
        }



        protected override void OnReceive(object message){}

 
    }

    class getLocation : Java.Lang.Object, ILocationListener
    {
        Location currentLocation;
        LocationManager locationManager;
        private readonly Context mContext;
        string locationProvider;
        //public IntPtr Handle => throw new NotImplementedException();

        public getLocation(Context mContext)
        {
            this.mContext = mContext;
            InitialiseLocationManager();
        }

        private void InitialiseLocationManager()
        {
            locationManager = (LocationManager)Android.App.Application.Context.GetSystemService(Context.LocationService);
            Criteria criteriaForLocationService = new Criteria
            {
                Accuracy = Accuracy.Fine
            };

            IList<string> acceptableLocationProviders = locationManager.GetProviders(criteriaForLocationService, true);

            if(acceptableLocationProviders.Any())
            {
                locationProvider = acceptableLocationProviders.First();
            }
            else
            {
                locationProvider = string.Empty;
            }
            Console.WriteLine("*************************************** Location Provider = " + locationProvider);
            //locationManager = (LocationManager)MainActivity.activity.GetSystemService(Context.LocationService);
        }

        public void findLocation()
        {
            locationManager.RequestLocationUpdates(locationProvider, 0, 0, this);
            
            
        }

        public void Stop()
        {
            locationManager.RemoveUpdates(this);
        }
        
        private void OnResume()
        {

        }

        public void OnLocationChanged(Location location)
        {
            MessagingCenter.Send<GetPendingFriends, List<string>>(this, "latitude", currentLocation);
            
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
