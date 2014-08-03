using UnityEngine;
using System.Collections;

public class KillingPlayerControl : PlayerControl
{
	void Awake()
	{
		// Maybe pass X/Y so it doesnt seem weird when we move around clouds?
		setupReferences();
		if(DataObject.altitude != 0) // If we have altitude from prevoius level
			rigidbody2D.transform.position = new Vector3(DataObject.xPos, DataObject.altitude, 0f); // Move the player up into the air where he fell
		else
			rigidbody2D.transform.position = new Vector3(0f, 500f, 0f); // Move the player up into the air where he fell

		if(DataObject.cameraPosition != Vector3.zero)
			Camera.main.transform.position = DataObject.cameraPosition;
	}

	void Start() // Check if overriding doesnt cause errors with Input.acceleration!
	{
//		if(DataObject.cameraPosition != Vector3.zero)
//			Camera.main.transform.position = DataObject.cameraPosition;
	}

	void Update() // Overrides default Player control, do not delete!
	{
	}

	override protected void handleLevelTriggers()
	{
	}

	override protected void handleJumping()
	{
	}


}
