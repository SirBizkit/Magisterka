using UnityEngine;
using System.Collections;

public class PlayerPaddle : Paddle
{
	protected const int mouseSensitivity = 10;
	public static int playerLives = 3;
	
	void Start () {
		playerLives = 3;
	}

	override protected void handlePaddleMovement ()
	{
		Vector3 mouseCoordinates = getMouseCoordinates();
		
		mouseCoordinates *= mouseSensitivity * Time.deltaTime;		
		
		transform.Translate(applyBounds(mouseCoordinates));
	}
	
	protected Vector3 getMouseCoordinates(){
		float mouseY = Input.GetAxis("Mouse Y");
		float mouseX = -Input.GetAxis("Mouse X");
		
		return new Vector3 (0, mouseY, isCameraReversed() ? -1*mouseX : mouseX);
	}
	
	virtual protected bool isCameraReversed(){
		return false;
	}
	
	public override void afterScoreTrigger()
	{
		playerLives--;
		if(playerLives <= 0)
			GameManager.endSinglePlayerGame();			
	}
}
