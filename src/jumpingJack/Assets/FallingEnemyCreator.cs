using UnityEngine;
using System.Collections;

public class FallingEnemyCreator : MonoBehaviour
{
	public GameObject enemyCreatorTarget;

	public float minHeightToGenerate = 50f;
	public int minEnemies = 5;
	public int maxEnemies = 15;
	public float enemiesPositionDeltaX = 6f;
	public float enemiesPositionDeltaY = 6f;
	public GameObject[] enemies; // Array of platform prefabs.

	private Vector3 lowestEnemiesGeneration = Vector3.zero;
	private bool firstPass = true;

	void Start ()
	{
		lowestEnemiesGeneration = new Vector3(getTargetPosition().x, getTargetPosition().y - 3 * enemiesPositionDeltaY, getTargetPosition().z);
		InvokeRepeating ("createNewEnemies", 1f, 0.5f);
	}

	void createNewEnemies ()
	{		
		if(enemyCreatorTarget == null)
			enemyCreatorTarget = DataObject.player;

		if(firstPass)
		{
			lowestEnemiesGeneration = new Vector3(getTargetPosition().x, getTargetPosition().y - 3 * enemiesPositionDeltaY, getTargetPosition().z);
			firstPass = false;
			return;
		}

		if(getTargetPosition().y > minHeightToGenerate)
		{
			if(Vector3.Distance(getLowestEnemyGenerationPosition(), getTargetPosition()) < 15)
			   spawnEnemies(new Vector3 (getTargetPosition().x, getLowestEnemyGenerationPosition().y - enemiesPositionDeltaY, getTargetPosition().z));
		}
	}

	private Vector3 getTargetPosition ()
	{
		return enemyCreatorTarget.transform.position;
	}

	private float getRandomRangeX()
	{
		return Random.Range (-enemiesPositionDeltaX, enemiesPositionDeltaX);
	}

	private float getRandomRangeY()
	{
		return Random.Range (-enemiesPositionDeltaY, enemiesPositionDeltaY);
	}

	public Vector3 getLowestEnemyGenerationPosition()
	{
		return lowestEnemiesGeneration;
	}

	void spawnEnemies (Vector3 enemyPos)
	{
		int numEnemies = Random.Range(minEnemies, maxEnemies);

		for(int i=0; i<numEnemies; i++)
		{
			Vector3 tempPos = new Vector3(enemyPos.x + getRandomRangeX(), enemyPos.y + getRandomRangeY());
			// Instantiate a random enemy
			int enemyIndex = Random.Range (0, enemies.Length);
			//GameObject enemy = (GameObject)(Instantiate (enemies[enemyIndex], tempPos, transform.rotation));
			GameObject enemy = (GameObject)(PoolManager.Spawn(enemies[enemyIndex]));
			enemy.transform.position = tempPos;
			enemy.transform.parent = gameObject.transform;
		}

		lowestEnemiesGeneration = enemyPos;
	}

	public void resetFirstPass()
	{
		firstPass = true;
	}
}
