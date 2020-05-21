package crc646201cb5e001c7b0e;


public class LocationServiceBinder
	extends android.os.Binder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("LocationService.Droid.LocationService.LocationServiceBinder, LocationService.Android", LocationServiceBinder.class, __md_methods);
	}


	public LocationServiceBinder ()
	{
		super ();
		if (getClass () == LocationServiceBinder.class)
			mono.android.TypeManager.Activate ("LocationService.Droid.LocationService.LocationServiceBinder, LocationService.Android", "", this, new java.lang.Object[] {  });
	}

	public LocationServiceBinder (crc646201cb5e001c7b0e.LocationService p0)
	{
		super ();
		if (getClass () == LocationServiceBinder.class)
			mono.android.TypeManager.Activate ("LocationService.Droid.LocationService.LocationServiceBinder, LocationService.Android", "LocationService.Droid.LocationService.LocationService, LocationService.Android", this, new java.lang.Object[] { p0 });
	}

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
