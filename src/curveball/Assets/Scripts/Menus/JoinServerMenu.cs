using UnityEngine;
using System.Collections;

public class JoinServerMenu : Menu
{	
	protected override void showMenuButtons ()
	{
		createLabel("Your Name", 1);
		name = GUI.TextField(new Rect(screenWidth/4, screenHeight/4, (screenWidth/2), (int)(0.10*screenHeight)), name, 25);
		
		string number = hostList!=null ? hostList.Length.ToString() : "0";
		GUI.Label(new Rect (screenWidth/4, (screenHeight/4)+2*getButtonHeight(), (screenHeight/4), getButtonHeight()), "Found (" + number + "):");
		int i = 0;
		if (hostList != null)
        {
            for (i = 0; i < hostList.Length; i++)
            {
                if (GUIButtonTexture2D(new Rect(screenWidth/4, 2*(screenHeight/4)-getButtonHeight()+i*getButtonHeight(), (screenWidth/2), getButtonHeight()), hostList[i].gameName)){
                    menuActive = false;
					GameManager.destroyScene();
					NetworkManager.JoinServer(hostList[i]);
					showDisconnect = true;
					activateMenu<Disconnect>();
				}
            }
        }
		if(GUIButtonTexture2D(new Rect(screenWidth/4, 2*(screenHeight/4)-getButtonHeight()+(i+1)*(getButtonHeight()), (screenWidth/2), getButtonHeight()), "Cancel"))
		{
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
