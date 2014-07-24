using UnityEngine;
using System.Collections;

public class ServerNameMenu : Menu
{	
	protected override void showMenuButtons ()
	{		
		createLabel("Your Name", 1);
		name = GUI.TextField(new Rect(screenWidth/4, screenHeight/4, (screenWidth/2), (int)(0.10*screenHeight)), name, 25);		
		
		GUI.Label (new Rect (screenWidth / 4, (screenHeight / 4) + 2*getButtonHeight(), (screenHeight / 4), getButtonHeight()), "Game Name:");
		NetworkManager.gameName = GUI.TextField (new Rect (screenWidth / 4, (screenHeight / 4) + 2.5f*getButtonHeight(), (screenWidth / 2) , (int)(0.10 * screenHeight)), NetworkManager.gameName, 25);
		if (GUIButtonTexture2D (new Rect (screenWidth / 4, (screenHeight / 4) + 8 * (int)(0.04 * screenHeight), (screenWidth / 4), getButtonHeight()), "OK")) {
			NetworkManager.StartServer();
			Debug.Log ("NetworkManager.StartServer()");
			showDisconnect = true;
			activateMenu<Disconnect>();
		}
		if (GUIButtonTexture2D (new Rect (2 * screenWidth / 4, (screenHeight / 4) + 8 * (int)(0.04 * screenHeight), (screenWidth / 4), getButtonHeight()), "Cancel")) {			
			NetworkManager.gameName = "MyGame"; // ???
			Debug.Log ("Cancel");
			activateMenu<Menu>();
		}		
	}
		
	protected override bool isActiveByDefaul ()
	{
		return false;
	}
	
	protected override void handleInGameMenus(){
	}
}
