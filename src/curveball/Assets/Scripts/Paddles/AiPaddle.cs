using UnityEngine;
using System.Collections;

public class AiPaddle : Paddle
{
	public static int aiLives = 3;
	public static float dampingFactor = 0.02f;
	public static float dampingFactorDelta = 0.02f;
	
	void Start () {
		aiLives = 3;
		dampingFactor = 0.02f;
	}
	
	protected override void handlePaddleMovement(){
		Vector3 newPositionDelta = GameManager.ball.rigidbody.position - transform.position;
		newPositionDelta *= dampingFactor;
		newPositionDelta.x = 0;
		transform.Translate(applyBounds(newPositionDelta));
	}
	
	public override void afterScoreTrigger()
	{
		aiLives--;
		if(aiLives <= 0){
			aiLives = 3;
			dampingFactor += dampingFactorDelta;
		}
	}
}
