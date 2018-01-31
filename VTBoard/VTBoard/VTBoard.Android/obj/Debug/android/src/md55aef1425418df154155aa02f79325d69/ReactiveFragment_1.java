package md55aef1425418df154155aa02f79325d69;


public class ReactiveFragment_1
	extends md55aef1425418df154155aa02f79325d69.ReactiveFragment
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("ReactiveUI.ReactiveFragment`1, ReactiveUI, Version=7.2.0.0, Culture=neutral, PublicKeyToken=null", ReactiveFragment_1.class, __md_methods);
	}


	public ReactiveFragment_1 ()
	{
		super ();
		if (getClass () == ReactiveFragment_1.class)
			mono.android.TypeManager.Activate ("ReactiveUI.ReactiveFragment`1, ReactiveUI, Version=7.2.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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
