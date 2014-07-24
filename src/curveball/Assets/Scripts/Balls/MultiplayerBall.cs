using UnityEngine;
using System.Collections;

public class MultiplayerBall : Ball {

	override protected bool shouldBeHandledByMe(){
		return Network.isServer;
	}
}
