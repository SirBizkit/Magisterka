using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour
{
	public int score = 0;					// The player's score.
	private PositionObserver positionObserver;	// Reference to the player control script.

	void Awake (){
		positionObserver = GameObject.FindGameObjectWithTag("positionObserver").GetComponent<PositionObserver>();
		score = DataObject.score;
	}

	void Update (){
		// Set the score text.
		//if(positionObserver.getCurrentHeight() > score)
		score = (int) positionObserver.highestAltitude;

		guiText.text = "Score: " + score;
		DataObject.score = score;
	}
}
