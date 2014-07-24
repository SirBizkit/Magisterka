using UnityEngine;
using System;

public class Menu : MonoBehaviour
{
	public const float nativeWidth = 1920;
	public const float nativeHeight = 1200;
	
	public float screenWidth = nativeWidth;
	public float screenHeight = nativeHeight;
	
	static public bool waitForPlayer;
	static public bool endGameSingle = false;
	static public bool endGameMulti = false;
	static public bool showDisconnect = false;
	
	public bool menuActive = false;

	static public HostData[] hostList;
	
	static public string name      = "Player 1";
	static public string enemyName = "Computer";
	static public string winningPlayerName = "Winner";
	
	GameManager gameManager;
	
	void Start() {
		menuActive = isActiveByDefaul();
		gameManager = GameObject.Find("gameManager").GetComponent<GameManager>();
		waitForPlayer = false;
	}
	
	protected virtual bool isActiveByDefaul(){
		return true;
	}
	
	public bool isActive(){
		return menuActive;
	}
	
	virtual protected void showMenuButtons(){
		Font myFont = (Font)Resources.Load("Fonts/CollegiateFLF", typeof(Font));
		GUI.skin.textField.fontSize = getFontSizeLogo();
		GUI.backgroundColor = Color.cyan;

		GUI.skin.font = myFont;
		GUIStyle labelStyle;
		labelStyle = new GUIStyle();
		labelStyle.fontSize = getFontSizeLogo();
		labelStyle.normal.textColor = Color.white;
		GUI.Label(new Rect (screenWidth/4, (screenHeight/4)-getButtonHeight(), (screenHeight/2), getButtonHeight()), "PsiBall", labelStyle);
	
		setFontParameters();
		
		if (createGuiButton("Single Player", screenWidth/2-0.5*getButtonWidth(), screenHeight/2-1.5*getButtonHeight()))
		{
			activateMenu<SinglePlayerMenu>();			
		}
	    if (createGuiButton("Create Server", screenWidth/2-0.5*getButtonWidth(), screenHeight/2-0.5*getButtonHeight()))
		{ 
			activateMenu<ServerNameMenu>();
		}
	    if (createGuiButton("Join Server", screenWidth/2-0.5*getButtonWidth(), screenHeight/2+0.5*getButtonHeight()))
		{
			NetworkManager.RefreshHostList();
			activateMenu<JoinServerMenu>();
		}		
	}
	
	void OnGUI()
	{
		scaleMenus();
		setFontParameters();
		if(!GameManager.isInGame())
		{
			if(isActive() && !endGameMulti && !endGameSingle)
				showMenuButtons();
			else if(endGameSingle)
			{
				Font myFont = (Font)Resources.Load("Fonts/CollegiateFLF", typeof(Font));
				GUI.skin.textField.fontSize = getFontSizeLogo();
				GUI.backgroundColor = Color.cyan;
				GUI.skin.font = myFont;
				GUIStyle labelStyle;
				labelStyle = new GUIStyle();
				labelStyle.fontSize = getFontSizeLogo()/2;
				labelStyle.normal.textColor = Color.white;
				GUI.Label(new Rect (screenWidth/4, (screenHeight/4)-getButtonHeight(), (screenHeight/2), getButtonHeight()), "You reached level " + AiPaddle.dampingFactor / AiPaddle.dampingFactorDelta, labelStyle);			
				GUI.Label(new Rect (screenWidth/4, (screenHeight/2)-getButtonHeight(), (screenHeight/2), getButtonHeight()), "With a score of " + GameManager.firstPaddleScore, labelStyle);
				
				if (createGuiButton("Back", screenWidth/2-0.5*getButtonWidth(), screenHeight/2+3*getButtonHeight()))
				{
					activateMenu<Menu>();
					endGameSingle = false;
					gameManager.getRevmobHandler().getFullscreenAd().Show();
					gameManager.getRevmobHandler().fullscreenAdShouldBeReloaded = true;
				}
			}			
			else if(endGameMulti)
			{
				Font myFont = (Font)Resources.Load("Fonts/CollegiateFLF", typeof(Font));
				GUI.skin.textField.fontSize = getFontSizeLogo();
				GUI.backgroundColor = Color.cyan;
				GUI.skin.font = myFont;
				GUIStyle labelStyle;
				labelStyle = new GUIStyle();
				labelStyle.fontSize = getFontSizeLogo()/2;
				labelStyle.normal.textColor = Color.white;
				GUI.Label(new Rect (0, (screenHeight/4)-getButtonHeight(), (screenHeight/2), getButtonHeight()), "Player " + winningPlayerName + " wins", labelStyle);
				
				if(winningPlayerName.Equals(name))
					GUI.Label(new Rect (0, (screenHeight/2)-getButtonHeight(), (screenHeight/2), getButtonHeight()), "With a score of " + GameManager.firstPaddleScore + " to " + GameManager.secondPaddleScore, labelStyle);
				else if(winningPlayerName.Equals(enemyName))
					GUI.Label(new Rect (0, (screenHeight/2)-getButtonHeight(), (screenHeight/2), getButtonHeight()), "With a score of " + GameManager.secondPaddleScore + " to " + GameManager.firstPaddleScore, labelStyle);
				
				if (createGuiButton("Back", screenWidth/2-0.5*getButtonWidth(), screenHeight/2+3*getButtonHeight()))
				{
					activateMenu<Menu>();
					endGameMulti = false;
					showDisconnect = false;
					gameManager.getRevmobHandler().getFullscreenAd().Show();
					gameManager.getRevmobHandler().fullscreenAdShouldBeReloaded = true;
				}
			}
		}
		else if(waitForPlayer)
		{
			Font myFont = (Font)Resources.Load("Fonts/CollegiateFLF", typeof(Font));
			GUI.skin.textField.fontSize = getFontSizeLogo();
			GUI.backgroundColor = Color.cyan;
	//		Debug.Log (getFontSizeLogo());
			GUI.skin.font = myFont;
			GUIStyle labelStyle;
			labelStyle = new GUIStyle();
			labelStyle.fontSize = getFontSizeLogo()/2;
			labelStyle.normal.textColor = Color.white;
			GUI.Label(new Rect (screenWidth/4, (screenHeight/4)-getButtonHeight(), (screenHeight/2), getButtonHeight()), "wait for player", labelStyle);		
		}		
		else
		{
			handleInGameMenus();			
		}
		
	}
	
	public void scaleMenus(){
		float rx = Screen.width / nativeWidth;
		float ry = Screen.height / nativeHeight;
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(rx, ry, 1));
	}
	
	protected bool GUIButtonTexture2D(Rect r, string text)
	{
		Texture2D center = (Texture2D) Resources.Load("Assets/tlo");
		if (!center) {
            Debug.LogError("No texture found, assign one in the inspector.");
            return false;
        }
		
		GUI.DrawTexture( r, center);
		bool isClicked = GUI.Button( r, "", "");
		GUI.Button(r, text);
		return isClicked;
	}
	
	protected bool createGuiButton(string buttonText, double x, double y){
		return GUIButtonTexture2D(new Rect((float)x, (float)y, getButtonWidth(), getButtonHeight()), buttonText);
	}
	
	protected void createLabel(string labelText, int buttonIndex){
		GUI.Label(new Rect (screenWidth/4, (screenHeight/4)-buttonIndex*getButtonHeight(), (screenHeight/2), getButtonHeight()), labelText);
	}
	
	protected int getButtonHeight(){
		return (int)(0.08*screenHeight);
	}
	
	protected int getButtonWidth(){
		return (int)(screenWidth/2);
	}
	
	protected int getFontSize(){
		return (int)(0.06*screenHeight);
	}
	
	protected int getFontSizeLogo(){
		return (int)(0.20*screenHeight);
	}
	
	void setFontParameters(){	
		Font myFont = (Font)Resources.Load("Fonts/Pilsen", typeof(Font));
		
		if (!myFont) {
	        Debug.LogError("No font found, assign one in the inspector.");
	        return;
	    }
		
		GUI.skin.textField.fontSize = getFontSize();
		GUI.backgroundColor = Color.cyan;
		
		GUI.skin.font = myFont;
	}
	
	public void activateMenu<MenuType>() where MenuType : Menu {
		Menu newMenu = (Menu)gameManager.GetComponent<MenuType>();
		this.menuActive = false;
		newMenu.menuActive = true;		
	}
	
	protected virtual void handleInGameMenus(){
		GUI.color = Color.green;
		GUI.Label(new Rect (0, 0, (screenHeight/4), (int)(0.08*screenHeight)), name + ": " + GameManager.getFirstPlayerScore().ToString());		
		GUI.color = Color.white;
		
		if(GameManager.isSinglePlayer()){
			activateMenu<Menu>();
			GUI.color = Color.green;
			GUI.Label(new Rect (250, 0, (screenHeight/4), (int)(0.08*screenHeight)), "Lives: " + new String('O', PlayerPaddle.playerLives));
			GUI.color = Color.red;
			GUI.Label(new Rect (0, (int)(0.06*screenHeight), (screenHeight/2), (int)(0.08*screenHeight)), "Level: " + AiPaddle.dampingFactor / AiPaddle.dampingFactorDelta);
			GUI.Label(new Rect (250, (int)(0.06*screenHeight), (screenHeight/2), (int)(0.08*screenHeight)), "Lives: " + new String('O', AiPaddle.aiLives));
			GUI.color = Color.white;
		}
		
		if(Network.isServer || Network.isClient)
		{
			GUI.color = Color.red;
			GUI.Label(new Rect (0, (int)(0.06*screenHeight), (screenHeight/2), (int)(0.08*screenHeight)), enemyName + ": " + GameManager.getSecondPlayerScore().ToString());
			GUI.color = Color.white;		
		}
	}
}
