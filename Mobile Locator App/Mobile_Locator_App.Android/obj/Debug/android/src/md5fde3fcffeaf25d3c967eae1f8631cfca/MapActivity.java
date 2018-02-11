package md5fde3fcffeaf25d3c967eae1f8631cfca;


public class MapActivity
	extends java.lang.Throwable
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
		mono.android.Runtime.register ("Mobile_Locator_App.Xaml.MapActivity, Mobile_Locator_App.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MapActivity.class, __md_methods);
	}


	public MapActivity ()
	{
		super ();
		if (getClass () == MapActivity.class)
			mono.android.TypeManager.Activate ("Mobile_Locator_App.Xaml.MapActivity, Mobile_Locator_App.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public MapActivity (java.lang.String p0)
	{
		super (p0);
		if (getClass () == MapActivity.class)
			mono.android.TypeManager.Activate ("Mobile_Locator_App.Xaml.MapActivity, Mobile_Locator_App.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "System.String, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0 });
	}


	public MapActivity (java.lang.String p0, java.lang.Throwable p1)
	{
		super (p0, p1);
		if (getClass () == MapActivity.class)
			mono.android.TypeManager.Activate ("Mobile_Locator_App.Xaml.MapActivity, Mobile_Locator_App.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "System.String, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e:Java.Lang.Throwable, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0, p1 });
	}


	public MapActivity (java.lang.String p0, java.lang.Throwable p1, boolean p2, boolean p3)
	{
		super (p0, p1, p2, p3);
		if (getClass () == MapActivity.class)
			mono.android.TypeManager.Activate ("Mobile_Locator_App.Xaml.MapActivity, Mobile_Locator_App.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "System.String, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e:Java.Lang.Throwable, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:System.Boolean, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e:System.Boolean, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1, p2, p3 });
	}


	public MapActivity (java.lang.Throwable p0)
	{
		super (p0);
		if (getClass () == MapActivity.class)
			mono.android.TypeManager.Activate ("Mobile_Locator_App.Xaml.MapActivity, Mobile_Locator_App.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Java.Lang.Throwable, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
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
