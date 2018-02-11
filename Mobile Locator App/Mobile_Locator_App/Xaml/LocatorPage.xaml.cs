using System;

using Android.App;
using Android.Gms.Maps;
using Mobile_Locator_App.Code;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Mobile_Locator_App.Xaml
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LocatorPage : ContentPage
	{
        public LocatorPage()
        {
            InitializeComponent();
            InitializePageDesign();

        }

        void InitializePageDesign() // to set the elements on the Log in page to the colours set in the Constants Class
        {
            BackgroundColor = Constants.BackgroundColour;
            /*************************** ******************/
            //SHA1 Key:  38:F4:D1:37:DD:AC:5F:9A:50:52:11:8B:F4:1F:47:77:C8:46:A4:D2
            // Google Maps API Key: AIzaSyA0NaUhV_6er4i0t0nz_XPf0dwAkrAXlg4
        }



        private void Button_NavHome_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new HomePage());
            //NavigationCode.GoHome();
        }

        private void Button_NavPending_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Mobile_Locator_App.Xaml.PendingFriendRequests());

        }

        private void Button_NavAddFriends_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new AddFriendsPage());
            //NavigationCode.GoAddFriends();
        }


        private void Button_Exit_Clicked(object sender, EventArgs e)
        {
            NavigationCode.ExitApp();
        }
    }
    
    public class MapActivity : /*Activity*/ Java.Lang.Throwable, IOnMapReadyCallback
    {
        // A fragment that acts as part of the page that will contain the GoogleMap object
        
        private GoogleMap _map;

       /* public MapActivity()
        {
            InitiliaseMapFragment();
        }

        // no method found to override
        public void InitiliaseMapFragment()
        {
            MapFragment _mapFragment = FragmentManager.FindFragmentByTag("map") as MapFragment;



            // the way below changes the settings once the map object is created
            _mapFragment.GetMapAsync(this);
            // after the above call is returned the settings are configured
            if (_map != null)
            {
                _map.UiSettings.ZoomControlsEnabled = true;
                _map.UiSettings.CompassEnabled = true;
                _map.MapType = GoogleMap.MapTypeHybrid; // should take longer to load than normal or terrain but not as long as satellite
            }

            // the way below changes the settings on creation of the mapFragment
            /*
            // if the current _mapFragment hasn't been configured
            if (_mapFragment == null)
            {
                GoogleMapOptions mapOptions = new GoogleMapOptions()
                       .InvokeMapType(GoogleMap.MapTypeSatellite)
                       .InvokeZoomControlsEnabled(true)
                       .InvokeCompassEnabled(true);

                FragmentTransaction fragTx = FragmentManager.BeginTransaction();
                _mapFragment = MapFragment.NewInstance(mapOptions);
                // in order to fix the problem below I think a property needs to be 
                // created so that map can be added in Resource.Designer
                // So call works as (integer ID, Fragment fragment, string tag)
                // Therefore ResourceDesigner now contains public static int map { get;  set; } at line 2692
                // No idea if it will work or not, though this being here either means it works or it hasnt
                // been run yet.
                fragTx.Add(Droid.Resource.Id.map, _mapFragment, "map");
                fragTx.Commit();
            }
            _mapFragment.GetMapAsync(this);*/
        //}

        public void OnMapReady(GoogleMap map)
        {
            _map = map;
        }

    }

}