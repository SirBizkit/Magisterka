using UnityEngine;
using System.Collections;

public class PlatformDestroyer : MonoBehaviour {

	public float timeUntilDestruction = 5f;
	public float wobbleDelta = 3f;
	public float wobbleTime = 0.1f;

	private int minWobbles = 8;
	private int maxWobbles = 16;
	private Vector2 wobbleForce = new Vector2(1000,0);

	void Start () {
		StartCoroutine(destroyPlatform());
	}

	IEnumerator destroyPlatform() {
		yield return new WaitForSeconds(timeUntilDestruction);

		int wobbles = Random.Range(minWobbles, maxWobbles);

		for(int i=0; i<wobbles; i++){
			rigidbody2D.AddForce(wobbleForce);
			yield return new WaitForSeconds(wobbleTime);
			rigidbody2D.AddForce(-wobbleForce);
			yield return new WaitForSeconds(wobbleTime);
		}

		rigidbody2D.isKinematic = false;
		yield return new WaitForSeconds(2f);
		Destroy(gameObject);
	}
}
