using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Mobile_Locator_App.Code;
using Xamarin.Forms;

namespace Mobile_Locator_App.Droid
{
	[Activity (Label = "Mobile_Locator_App", Icon = "@drawable/icon", Theme="@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
        public static Activity activity = (MainActivity)Forms.Context;
		protected override void OnCreate (Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

            // calling an actor that will in turn call a class that can retrieve the users current location on a seperate thread
            //GetLocationActor getLocationActor = new GetLocationActor(this); 
            //getLocation GetLocation = new getLocation(this);// initialises the getLocationActor and passes the current context


            //Location location = getLocationActor.getLocation();


            base.OnCreate (bundle);

            // Initialises Xamarin Forms throughout the project
            global::Xamarin.Forms.Forms.Init (this, bundle);
            // Initialising Xamarin FormsMaps for the Google Maps API, Initialised after the Xamarin.Forms.Forms.Init call
            Xamarin.FormsMaps.Init(this, bundle);
            LoadApplication (new Mobile_Locator_App.App ());
		}
	}
}

