using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KillingEnemyScater : MonoBehaviour {

	public List<GameObject> enemyList;
	public GameObject enemyPrefab;
	private bool killingTime = false;
	private bool enemiesScattered = false;

	void Start () {
		if(DataObject.enemyList != null)
		{
			enemyList = DataObject.enemyList;
		}
		else
		{
			enemyList = new List<GameObject>();

			for(int i=0; i<20; i++)
			{
				GameObject enemy = (GameObject)(Instantiate(enemyPrefab, transform.position, transform.rotation));
				enemy.collider2D.enabled = false;
				enemyList.Add(enemy);
			}
		}
	}
	
	void FixedUpdate () {

		if(!killingTime){
			foreach(GameObject enemy in enemyList)
			{
				enemy.rigidbody2D.transform.position = gameObject.rigidbody2D.transform.position;
			}
		}

		if(killingTime && !enemiesScattered)
		{
			foreach(GameObject enemy in enemyList)
			{
				enemy.rigidbody2D.velocity = new Vector2(0f,0f);
				enemy.rigidbody2D.transform.position = gameObject.rigidbody2D.transform.position;
				enemy.rigidbody2D.AddForce(new Vector2(Random.Range(-10000f, 10000f), Random.Range(1000f, 1500f)));
			}

			//Time.timeScale = 0.2f;
			//Time.fixedDeltaTime = 0.2f;
			enemiesScattered = true;
		}	
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log("Collision enter");
		if(collision.gameObject.tag == "ground")
		{
			killingTime = true;
			Debug.Log("Killing time!");
		}
	}
}
