using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
	
	public float maxVelocity = 20;
	public float velocityDelta = 0.5f;
	const float curveFactor = 2f;

	void Start () {
		if (shouldBeHandledByMe())
		{
			rigidbody.AddForce(new Vector3(1,0,0) * 1000);
		}
		GameManager.lightGameObject_spot.transform.Rotate(new Vector3(0f, 90f, 0f));
		GameManager.lightGameObject_spot_2.transform.Rotate(new Vector3(0f, 270f, 0f));		
	}

	// Update is called once per frame
	void Update () {		
		if(shouldBeHandledByMe())
			keepVelocityConstant();
		
		moveBallLights();
	}
	
	virtual protected bool shouldBeHandledByMe(){
		return true;
	}

	protected virtual void keepVelocityConstant(){		
		Vector3 newVelocity = new Vector3(Mathf.Sign(rigidbody.velocity.x)*maxVelocity,
									  rigidbody.velocity.y,
									  rigidbody.velocity.z);
		
		rigidbody.velocity = newVelocity;
	}
	
	private void moveBallLights(){
		Vector3 v3 = new Vector3(rigidbody.transform.position.x-4f, rigidbody.transform.position.y, rigidbody.transform.position.z);
		GameManager.lightGameObject_spot.transform.position = v3;
		Vector3 v4 = new Vector3(rigidbody.transform.position.x+4f, rigidbody.transform.position.y, rigidbody.transform.position.z);
		GameManager.lightGameObject_spot_2.transform.position = v4;
		GameManager.lightGameObject.transform.position = rigidbody.transform.position;		
	}
	
	public void resetMaxVelocity(){
		maxVelocity = 20;
	}
	
	void OnCollisionEnter(Collision collision){
		if(shouldBeHandledByMe()){
			if(collision.gameObject.GetComponent<Paddle>() != null)
			{
				maxVelocity+=velocityDelta;
				Paddle paddle = collision.gameObject.GetComponent<Paddle>();				
				rigidbody.constantForce.force = paddle.getCurveVector().normalized * 15;				
				ContactPoint contact = collision.contacts[0];
				Vector3 yzReflection = Vector3.Reflect(rigidbody.velocity, contact.normal);
				yzReflection *= 0.75f;
				yzReflection.x = rigidbody.velocity.x;
				rigidbody.velocity = yzReflection;
				paddle.audio.Play();
			}
		}
	}
}