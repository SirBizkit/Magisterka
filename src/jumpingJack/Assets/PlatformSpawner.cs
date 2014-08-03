using UnityEngine;
using System.Collections;

public class PlatformSpawner : MonoBehaviour {

	public float distanceBetweenPlatforms = 6f;
	public float platformPositionDeltaX = 6f;
	public float destroyAfter = 5f;

	public GameObject[] platforms;					// Array of platform prefabs.

	private Camera camera;
	private Vector3 highestPlatformPosition = Vector3.zero;
	private GameObject platformHolder;
	
	void Start ()
	{
		camera = Camera.main;
		platformHolder = GameObject.Find("platformHolder");
		DontDestroyOnLoad(platformHolder);
	}

	void Update(){
		if(getHighestPlatformPosition().y < camera.transform.position.y + camera.orthographicSize * 2 ){ // Screen above what the camera can see
			spawnPlatform(new Vector3(camera.transform.position.x + Random.Range(-platformPositionDeltaX, platformPositionDeltaX), getHighestPlatformPosition().y + distanceBetweenPlatforms, 0));
		}
	}	

	public Vector3 getHighestPlatformPosition(){
		return highestPlatformPosition;
	}
	
	void spawnPlatform(Vector3 platPos)
	{
		// Instantiate a random platform.
		int plaftormIndex = Random.Range(0, platforms.Length);
		GameObject platform = (GameObject)(Instantiate(platforms[plaftormIndex], platPos, transform.rotation));
		platform.transform.parent = platformHolder.transform;
		highestPlatformPosition = platPos;
	}
}
