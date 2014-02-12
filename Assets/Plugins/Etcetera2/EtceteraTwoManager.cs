using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Prime31;


public class EtceteraTwoManager : AbstractManager
{
#if UNITY_IPHONE
	// Fired when an external screen is detected and is being mirrored onto
	public static event Action screenMirroringDidStartEvent;
	
	// Fired when external screen mirroring is stopped
	public static event Action screenMirroringDidStopEvent;
	
	// Fired when a video completes and the user is returned to your game
	public static event Action moviePlayerDidFinishEvent;
	
	
	static EtceteraTwoManager()
	{
		AbstractManager.initialize( typeof( EtceteraTwoManager ) );
	}


	public void screenMirroringDidStart( string empty )
	{
		screenMirroringDidStartEvent.fire();
	}


	public void screenMirroringDidStop( string empty )
	{
		screenMirroringDidStopEvent.fire();
	}
	
	
	public void moviePlayerDidFinish( string empty )
	{
		moviePlayerDidFinishEvent.fire();
	}

#endif
}

