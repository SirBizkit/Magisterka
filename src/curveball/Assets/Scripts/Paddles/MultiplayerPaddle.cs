using UnityEngine;
using System.Collections;

public class MultiplayerPaddle : PlayerPaddle {
	
	override protected bool isCameraReversed(){
		return Network.isClient;
	}
	
	override protected void handlePaddleMovement ()
	{
		if(networkView.isMine){
			Vector3 mouseCoordinates = getMouseCoordinates();
			
			mouseCoordinates *= mouseSensitivity * Time.deltaTime;
			
			transform.Translate(applyBounds(mouseCoordinates));
		}
	}
}
