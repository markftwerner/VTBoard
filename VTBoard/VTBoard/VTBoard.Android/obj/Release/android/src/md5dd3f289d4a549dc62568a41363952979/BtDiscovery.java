package md5dd3f289d4a549dc62568a41363952979;


public class BtDiscovery
	extends android.content.BroadcastReceiver
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onReceive:(Landroid/content/Context;Landroid/content/Intent;)V:GetOnReceive_Landroid_content_Context_Landroid_content_Intent_Handler\n" +
			"";
		mono.android.Runtime.register ("VTBoard.Android.Services.BtDiscovery, RasPiBtControl.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", BtDiscovery.class, __md_methods);
	}


	public BtDiscovery ()
	{
		super ();
		if (getClass () == BtDiscovery.class)
			mono.android.TypeManager.Activate ("VTBoard.Android.Services.BtDiscovery, RasPiBtControl.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public BtDiscovery (md592dbce1606f5d9c20a62620fb22b1760.MainActivity p0)
	{
		super ();
		if (getClass () == BtDiscovery.class)
			mono.android.TypeManager.Activate ("VTBoard.Android.Services.BtDiscovery, RasPiBtControl.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "VTBoard.Android.MainActivity, RasPiBtControl.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", this, new java.lang.Object[] { p0 });
	}


	public void onReceive (android.content.Context p0, android.content.Intent p1)
	{
		n_onReceive (p0, p1);
	}

	private native void n_onReceive (android.content.Context p0, android.content.Intent p1);

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
