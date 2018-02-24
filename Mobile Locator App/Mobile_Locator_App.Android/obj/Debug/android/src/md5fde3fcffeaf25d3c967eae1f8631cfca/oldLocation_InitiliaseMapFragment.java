package md5fde3fcffeaf25d3c967eae1f8631cfca;


public class oldLocation_InitiliaseMapFragment
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.android.gms.maps.OnMapReadyCallback
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onMapReady:(Lcom/google/android/gms/maps/GoogleMap;)V:GetOnMapReady_Lcom_google_android_gms_maps_GoogleMap_Handler:Android.Gms.Maps.IOnMapReadyCallbackInvoker, Xamarin.GooglePlayServices.Maps\n" +
			"";
		mono.android.Runtime.register ("Mobile_Locator_App.Xaml.oldLocation+InitiliaseMapFragment, Mobile_Locator_App.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", oldLocation_InitiliaseMapFragment.class, __md_methods);
	}


	public oldLocation_InitiliaseMapFragment ()
	{
		super ();
		if (getClass () == oldLocation_InitiliaseMapFragment.class)
			mono.android.TypeManager.Activate ("Mobile_Locator_App.Xaml.oldLocation+InitiliaseMapFragment, Mobile_Locator_App.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onMapReady (com.google.android.gms.maps.GoogleMap p0)
	{
		n_onMapReady (p0);
	}

	private native void n_onMapReady (com.google.android.gms.maps.GoogleMap p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
