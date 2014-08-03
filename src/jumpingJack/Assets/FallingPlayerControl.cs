using UnityEngine;
using System.Collections;

public class FallingPlayerControl : PlayerControl
{	
	private float triggerKillingThreshold = 50f;

	void Awake()
	{
		// Maybe pass X/Y so it doesnt seem weird when we move around clouds?
		setupReferences();
		if(DataObject.altitude != 0) // If we have altitude from prevoius level
			rigidbody2D.transform.position = new Vector3(DataObject.xPos, DataObject.altitude, 0f); // Move the player up into the air where he fell
		else
			rigidbody2D.transform.position = new Vector3(0f, 500f, 0f); // Move the player up into the air where he fell
	}

	void Start()
	{
		if(DataObject.cameraPosition != Vector3.zero)
			Camera.main.transform.position = DataObject.cameraPosition;
	}

	void Update() // Overrides default Player control, do not delete!
	{
	}

	override protected void handleLevelTriggers()
	{
//		if (gameObject.rigidbody2D.transform.position.y <= triggerKillingThreshold) // If the player is lower than the killing threshold
//		{		
//			DataObject.altitude = gameObject.rigidbody2D.transform.position.y;
//			DataObject.cameraPosition = Camera.main.transform.position;
//			DataObject.xPos = rigidbody2D.transform.position.x;
//			DataObject.enemyList = gameObject.GetComponent<FallingEnemyCatcher>().getEnemyList();
//			Application.LoadLevel ("Killing");
//		}
	}

	override protected void handleJumping()
	{
	}
}
