using UnityEngine;
using System.Collections;

public class SinglePlayerMenu : Menu {
	
	protected override void showMenuButtons ()
	{
		createLabel("Your Name", 1);
		name = GUI.TextField(new Rect(screenWidth/4, screenHeight/4, (screenWidth/2), (int)(0.10*screenHeight)), name, 25);
		
		if(GUIButtonTexture2D(new Rect (screenWidth/4, (screenHeight/4)+3*(int)(0.04*screenHeight), (screenWidth/4), getButtonHeight()), "OK"))
		{
			menuActive = false;
			
			GameManager.startSinglePlayerMatch();
			activateMenu<Menu>();
		}
		
		if(GUIButtonTexture2D(new Rect (2*screenWidth/4, (screenHeight/4)+3*(int)(0.04*screenHeight), (screenWidth/4), getButtonHeight()), "Cancel"))
		{
			activateMenu<Menu>();
		}
	}	
		
	protected override bool isActiveByDefaul(){
		return false;
	}
	
	protected override void handleInGameMenus(){
	}
}
