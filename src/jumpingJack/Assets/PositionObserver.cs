using UnityEngine;
using System.Collections;

public class PositionObserver : MonoBehaviour {

	public GameObject climbingPrefab;
	public GameObject fallingPrefab;
	public GameObject killingPrefab;

	private float fallingThreshold = 20f;
	private float killingThreshold = 50f;

	public GameObject player;
	private GameObject fallingEnemyCreator;
	private bool falling = false;
	private bool killing = false;
	public float highestAltitude = 0f;

	private CameraFollow followCam;


	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		fallingEnemyCreator = GameObject.Find("fallingEnemyCreator");
		fallingEnemyCreator.SetActive(false);
		DataObject.player = player;
		InvokeRepeating ("conductOvservations", 0, 0.5f);
		followCam = Camera.main.GetComponent<CameraFollow>();
	}

	void conductOvservations()
	{
		if(highestAltitude < player.transform.position.y)
			highestAltitude = player.transform.position.y;

		if(!falling && !killing && highestAltitude - player.transform.position.y > fallingThreshold)
		{
			falling = true;

			fallingEnemyCreator.SetActive(true);
			fallingEnemyCreator.GetComponent<FallingEnemyCreator>().resetFirstPass();

			swapPrefabsAndNotifyComponents(fallingPrefab);

			Destroy(GameObject.Find("platformCreator"), 10f); // Throws exceptions till dies, clean it up later
		}

		if(falling && !killing && player.transform.position.y <= killingThreshold)
		{
			falling = false;
			killing = true;			
			//fallingEnemyCreator.SetActive(false);
			//Destroy(GameObject.Find("fallingEnemyCreator"), 10f); // Throws exceptions till dies, clean it up later
			
			swapPrefabsAndNotifyComponents(killingPrefab);
		}
	}

	void swapPrefabsAndNotifyComponents(GameObject prefab)
	{
		GameObject tempPlayerHolder = player;
		player = Instantiate(prefab, tempPlayerHolder.transform.position, tempPlayerHolder.transform.rotation) as GameObject; // Swap player instances
		player.rigidbody2D.velocity = tempPlayerHolder.rigidbody2D.velocity;
		followCam.player = player.transform; // Notify the camera of the change
		DataObject.player = player;

		Destroy(tempPlayerHolder);
	}
}
