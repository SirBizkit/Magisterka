using UnityEngine;
using System.Collections;

public class SwitchScenesButton : MonoBehaviour {

	public int levelToLoad = 0;
	
	void OnGUI () {
		if(GUI.Button(new Rect(Screen.width/2+10,10+10,Screen.width/8+10,Screen.height/8+10), "SwitchLevel")) {
			Application.LoadLevel(levelToLoad);
		}
	}
}