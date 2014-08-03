using UnityEngine;
using System.Collections;

public class CollisionIgnorerScript : MonoBehaviour {

	//the collider of the main visible platform
	public GameObject platform;
	//this variable is true when the players is just below the platform so that its Box collider can be disabled that will allow the player to pass through the platform
	private bool oneWay;

	void FixedUpdate () {
		//Enabling or Disabling the platform's Box collider to allowing player to pass
		if (oneWay)
			platform.collider2D.enabled = false;
		else
			platform.collider2D.enabled = true;
	}

	// For some reason hero doesn't trigger onTriggerStay!

	void OnTriggerEnter2D(Collider2D other) {
		if(!(other.tag == "Player"))
			return;

		oneWay = true;
	}

	//Checking the collison of the gameobject we created in step 2 for checking if the player is just below the platform and nedded to ignore the collison to the platform
	void OnTriggerStay2D(Collider2D other) {
		if(!(other.tag == "Player"))
			return;

		oneWay = true;
	}
	
	void OnTriggerExit2D(Collider2D other) {
		if(!(other.tag == "Player"))
			return;
		//Just to make sure that the platform's Box Collider does not get permantly disabled and it should be enabeled once the player get its through
		oneWay = false;
	}
}
