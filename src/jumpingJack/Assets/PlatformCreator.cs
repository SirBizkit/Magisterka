using UnityEngine;
using System.Collections;

public class PlatformCreator : MonoBehaviour {

	public GameObject platformCreatorTarget; // The game object the platform creator will create around
	public float distanceBetweenPlatforms = 6f;
	public float platformPositionDeltaX = 6f;
	public GameObject[] platforms; // Array of platform prefabs.

	private Vector3 highestPlatformPosition = Vector3.zero;

	void Start () {
		InvokeRepeating("createNewPlatforms", 0, 0.3f);
	}

	void createNewPlatforms() {
		if(getHighestPlatformPosition().y < getTargetPosition().y + 3 * distanceBetweenPlatforms)
			spawnPlatform(new Vector3(getTargetPosition().x + getRandomRange(), getHighestPlatformPosition().y + distanceBetweenPlatforms, getTargetPosition().z));
	}

	private Vector3 getTargetPosition()
	{
		return platformCreatorTarget.transform.position;
	}

	private float getRandomRange()
	{
		return Random.Range(-platformPositionDeltaX, platformPositionDeltaX);
	}

	public Vector3 getHighestPlatformPosition(){
		return highestPlatformPosition;
	}
	
	void spawnPlatform(Vector3 platPos)
	{
		// Instantiate a random platform.
		int plaftormIndex = Random.Range(0, platforms.Length);
		GameObject platform = (GameObject)(Instantiate(platforms[plaftormIndex], platPos, transform.rotation));
		platform.transform.parent = gameObject.transform;
		highestPlatformPosition = platPos;
	}
}
