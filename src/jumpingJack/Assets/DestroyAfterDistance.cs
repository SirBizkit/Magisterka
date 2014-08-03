using UnityEngine;
using System.Collections;

public class DestroyAfterDistance : MonoBehaviour {

	public GameObject target;
	public float distanceThreshold = 20f;

	void Start ()
	{
		target = GameObject.FindGameObjectWithTag("Player");
		InvokeRepeating ("checkDistanceAndDestroy", 0, 3f);
	}
	
	void checkDistanceAndDestroy()
	{
		if(target == null)
			target = DataObject.player;

		if( Vector3.Distance(gameObject.transform.position, target.transform.position) > distanceThreshold)
			PoolManager.Despawn(gameObject);
	}
}
