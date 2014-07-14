using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
	public float spawnTime = 1f;		// The amount of time between each spawn.
	public float spawnDelay = 3f;		// The amount of time before spawning starts.
	public GameObject[] enemies;		// Array of enemy prefabs.
	public HUDFPS fps;
	public float  smoothFpsThreshold = 30f;
	public float spawnIncrement = 0.1f;
	public LinkedList<GameObject> enemyList;


	void Start ()
	{
		// Start calling the Spawn function repeatedly after a delay .
		fps = GameObject.Find("fpsCounter").GetComponent<HUDFPS>();
		enemyList = new LinkedList<GameObject>();
		StartCoroutine("Spawn");
		//InvokeRepeating("Spawn", spawnDelay, spawnTime);
	}

	IEnumerator Spawn() {
		while(true){
			if(fps.lastFps > smoothFpsThreshold )
			{
				// Instantiate a random enemy.
				int enemyIndex = Random.Range(0, enemies.Length);
				enemyList.AddLast((GameObject)Instantiate(enemies[enemyIndex], transform.position, transform.rotation));
				if(spawnTime - spawnIncrement > 0){
					spawnTime -= spawnIncrement;
					yield return new WaitForSeconds(spawnTime);
				} else {
					spawnIncrement/=10;
				}
			} else {
				if(enemyList.Count > 0)
				{
					GameObject enemyToBeRemoved = enemyList.First.Value;
					enemyList.RemoveFirst();
					Destroy(enemyToBeRemoved);
				}
				spawnTime += spawnIncrement;
				yield return new WaitForSeconds(spawnTime);
			}

			fps.objects = enemyList.Count;
		}
	}
}
