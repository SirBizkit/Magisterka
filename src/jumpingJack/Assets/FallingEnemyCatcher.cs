using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FallingEnemyCatcher : MonoBehaviour {

	private int numEnemiesCaught = 0;

	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.gameObject.tag == "Enemy")
		{
			GameObject enemy = collider.gameObject;

			if(numEnemiesCaught < 2) // Capture 2 enemies to drag them down
			{
				enemy.transform.parent = gameObject.transform;
				enemy.transform.position = gameObject.transform.position;
				enemy.collider2D.enabled = false;
				numEnemiesCaught++;
				return;
			}

			numEnemiesCaught++;
			PoolManager.Despawn(enemy); // If we captured more enemies than 2 we put them back into the pool
		}
	}

	public int getNumEnemiesCaught()
	{
		return numEnemiesCaught;
	}
}
