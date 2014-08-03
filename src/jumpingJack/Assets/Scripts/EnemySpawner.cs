using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
	public float maxSpawnTime = 5f;
	public float minSpawnTime = 2f;
	public float spawnDelay = 0f;		// The amount of time before spawning starts.
	public float timeUntilDestruction = 10f;
	public GameObject[] enemies;		// Array of enemy prefabs.


	void Start ()
	{
		float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
		// Start calling the Spawn function repeatedly after a delay .
		InvokeRepeating("Spawn", spawnDelay, spawnTime);
		StartCoroutine(destroySpawner());
	}

	void Spawn ()
	{
		// Instantiate a random enemy.
		int enemyIndex = Random.Range(0, enemies.Length);
		GameObject enemy = (GameObject)(Instantiate(enemies[enemyIndex], transform.position, transform.rotation));
		enemy.transform.parent = gameObject.transform;

		// Play the spawning effect from all of the particle systems.
		foreach(ParticleSystem p in GetComponentsInChildren<ParticleSystem>())
		{
			p.Play();
		}
	}

	IEnumerator destroySpawner() { // Wait until destruction, make sure all the enemies under this spawner have disapeared then destroy it
		yield return new WaitForSeconds(timeUntilDestruction);
		CancelInvoke("Spawn");

		while(gameObject.transform.childCount > 0)
			yield return new WaitForSeconds(1);

		Destroy(gameObject);
	}
}
