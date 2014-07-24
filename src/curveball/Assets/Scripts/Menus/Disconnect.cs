using UnityEngine;
using System.Collections;

public class Disconnect : Menu {
	
	protected override void showMenuButtons ()
	{
		if(showDisconnect)
		{
			Font myFont = (Font)Resources.Load("Fonts/CollegiateFLF", typeof(Font));
			GUI.skin.textField.fontSize = (int)(0.50*Screen.height);
			GUI.backgroundColor = Color.cyan;
			GUI.skin.font = myFont;
			GUIStyle labelStyle;
			labelStyle = new GUIStyle();
			labelStyle.fontSize = (int)(0.20*Screen.height);
			labelStyle.normal.textColor = Color.white;
			GUI.Label(new Rect (screenWidth/4, (screenHeight/4)-getButtonHeight(), (screenHeight/2), getButtonHeight()), "Disconnect?", labelStyle);
	
			if(GUIButtonTexture2D(new Rect (screenWidth/4, (screenHeight/4)+3*(int)(0.04*screenHeight), (screenWidth/4), getButtonHeight()), "Yes"))
			{
				showDisconnect = false;
				disconnectPlayers ();
			}
			
			if(GUIButtonTexture2D(new Rect (2*screenWidth/4, (screenHeight/4)+3*(int)(0.04*screenHeight), (screenWidth/4), getButtonHeight()), "No"))
			{
				GameManager.ResumeGame();
			}
		}
	}
	
	public void disconnectPlayers ()
	{
		if(Network.isClient)
		{
			NetworkManager.ClientDisconnect();
			networkView.RPC("RemoteDisconnect", RPCMode.Others);
		} // server
		else
		{
			Network.Destroy(GameManager.firstPaddle);
			Network.Destroy(GameManager.secondPaddle);
			Network.Destroy(GameManager.ball);
			Network.RemoveRPCs(Network.player);
			networkView.RPC("RemoteDisconnect", RPCMode.Others);
			Debug.Log (GameManager.NoPlayer);
			if(GameManager.NoPlayer){
				Network.Disconnect(200);
			}
		}
		activateMenu<Menu>();
	}
		
	protected override bool isActiveByDefaul(){
		return false;
	}
	
	public static void DisconnectFromServer()
	{
		//Debug.Log("Disconnecting from " + serverIP + ":" + serverPort);
		Network.RemoveRPCs(Network.player);
		Network.Disconnect();
	}
	
	[RPC]
	public void RemoteDisconnect()
	{
		Network.Destroy(GameManager.firstPaddle);
		Network.Destroy(GameManager.secondPaddle);
		Network.Destroy(GameManager.ball);	
		Debug.Log("Remove object");
		activateMenu<Menu>();
		DisconnectFromServer();
	}
	
	protected override void handleInGameMenus(){
	}
}
