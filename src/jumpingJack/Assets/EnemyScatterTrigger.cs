using UnityEngine;
using System.Collections;

public class EnemyScatterTrigger : MonoBehaviour {

	private float upwardForce = 3000f;
	private float deltaXForce = 1000f;

	public int numEnemiesCaught = 0;

	private bool enemiesScattered = false;

	void Start () {
		if(numEnemiesCaught == 0)
			numEnemiesCaught = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<FallingEnemyCatcher>().getNumEnemiesCaught();	
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if( !enemiesScattered && col.gameObject.tag == "ground")
		{
			enemiesScattered = true;
			for(int i=0; i < numEnemiesCaught; i++)
			{
				GameObject enemy = PoolManager.Spawn("enemy2");
				enemy.collider2D.enabled = false;
				enemy.rigidbody2D.gravityScale = 1f;
				enemy.rigidbody2D.velocity = Vector2.zero;
				enemy.rigidbody2D.transform.position = gameObject.transform.position;
				enemy.rigidbody2D.AddForce(new Vector2(Random.Range(-deltaXForce, deltaXForce), Random.Range(0, upwardForce)));
			}

			//Destroy(gameObject); // We won't be using this trigger any more
		}
	}
}
