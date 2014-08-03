using UnityEngine;
using System.Collections;

public class EnemySpawnerCreator : MonoBehaviour {
	public GameObject spawner;
	public int probabilityOfSpawner = 30;
	public float spawnerXOffset = 100f;

	private PlatformSpawner platformSpawner;
	private Vector3 lastHighestPlatformPosition;
	
	void Start ()
	{
		platformSpawner = gameObject.GetComponent<PlatformSpawner>();
		lastHighestPlatformPosition = platformSpawner.getHighestPlatformPosition();
	}
	
	void Update(){
		if(platformSpawner.getHighestPlatformPosition().y > lastHighestPlatformPosition.y){
			lastHighestPlatformPosition = platformSpawner.getHighestPlatformPosition();
			lastHighestPlatformPosition.x = lastHighestPlatformPosition.x - spawnerXOffset;
			int rand = Random.Range(0,100);
			if(rand < probabilityOfSpawner)
				createEnemySpawner(lastHighestPlatformPosition);
		}
	}	

	void createEnemySpawner(Vector3 spawnerPosition)
	{
		GameObject enemySpawner = (GameObject)(Instantiate(spawner, spawnerPosition, transform.rotation));
		DontDestroyOnLoad(enemySpawner);
		//GameObject enemySpawner = (GameObject)(Instantiate(spawner, spawnerPosition, transform.rotation));
		//enemySpawner.transform.parent = gameObject.transform;
	}
}
