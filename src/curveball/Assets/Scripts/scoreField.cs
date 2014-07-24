using UnityEngine;
using System.Collections;

public class scoreField : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if(Network.isServer || GameManager.isSinglePlayer())
		{
	       	if(transform.position.x == 19){	
				GameManager.handleFirstPaddleScoring();
				GameManager.Restart(false);
			}
			else {
				GameManager.handleSecondPaddleScoring();
				GameManager.Restart(true);			
			}
		}
    }	
}
