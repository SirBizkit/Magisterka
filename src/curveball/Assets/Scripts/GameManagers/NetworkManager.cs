using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {
	
	private const string typeName  = "PsiBall";
	static public string gameName  = "MyGame";
	private static NetworkView staticNetworkView;
		
	static public void StartServer()
	{
		GameManager.destroyScene();
	    Network.InitializeServer(2, 25000, !Network.HavePublicAddress());
	    MasterServer.RegisterHost(typeName, gameName);		
		GameManager.resetPlayerScore();
		
		GameManager.singlePlayer = false;
		GameManager.gamePaused = false;
		Menu.waitForPlayer = true;
	}
	
	void OnServerInitialized()
	{
	    GameManager.firstPaddle = GameManager.spawnPlayer(new Vector3(-12f, 3.75f, 0f));
		GameManager.ball = GameManager.spawnBall(new Vector3(0f, 3.75f, 0f));
		
		GameManager.instantiateLights();
		staticNetworkView = networkView;
	}
	
	[RPC]
    void myRemoteCall(string someText)
    {
		GameManager.resetPlayerScore();		
        Menu.enemyName = someText;
        Menu.waitForPlayer = false;
		networkView.RPC("syncNames", RPCMode.Others, Menu.name, Menu.enemyName);
    }
	
	public static void ServerDisconnect()
	{
		// kurwa kurwa kurwa kurwa
		// zastanawiaj sie dlaczego to nie jest wywolywane przez godzine.... a potem sie okaze ze trzeba dodac pierdolonego waita
		// normalnie ttcn3 :( :(
		//Debug.Log("DiscA?");
		//yield return new WaitForSeconds(1);
		//Debug.Log("DiscB?");
		Network.Disconnect();
		MasterServer.UnregisterHost();
	}
	
	public static void ClientDisconnect()
	{
			
		Network.Destroy(GameManager.firstPaddle);
		Network.Destroy(GameManager.secondPaddle);
		Network.Destroy(GameManager.ball);
		Network.RemoveRPCs(Network.player);
		//yield return new WaitForSeconds(1);
	}
	
	void OnDisconnectedFromServer(NetworkDisconnection info) {
		if (Network.isServer) {
			Debug.Log("Local server connection disconnected");
			GameManager.startDemoMatch();
		}
		else {
			Debug.Log("Server is Dead");
			GameManager.startDemoMatch();
			Camera.current.transform.position = new Vector3(-18, Camera.current.transform.position.y, Camera.current.transform.position.z);
			Camera.current.transform.Rotate(new Vector3(0,180,0));
		}
	}
	
	void OnConnectedToServer()
	{
		Camera.current.transform.position = new Vector3(18, Camera.current.transform.position.y, Camera.current.transform.position.z);
		Camera.current.transform.Rotate(new Vector3(0,180,0));
		GameManager.secondPaddle = GameManager.spawnPlayer(new Vector3(12f, 3.75f, 0f));
		GameManager.instantiateLights();
		
		networkView.RPC("myRemoteCall", RPCMode.Others, Menu.name);
		
		GameManager.singlePlayer = false;
		GameManager.gamePaused = false;
		Menu.waitForPlayer = false;
		GameManager.ResumeGame();
		
		
	}
		
	void OnPlayerConnected(NetworkPlayer player) {
		Debug.Log("NOPl" + GameManager.NoPlayer);
		GameManager.NoPlayer = false;
        GameManager.ResumeGame();
    }
	
	static public void RefreshHostList()
	{
	    MasterServer.RequestHostList(typeName);
	}
	 
	void OnMasterServerEvent(MasterServerEvent msEvent)
	{
	    if (msEvent == MasterServerEvent.HostListReceived)
	        Menu.hostList = MasterServer.PollHostList();
	}
	
	public static void JoinServer(HostData hostData)
	{
	    Network.Connect(hostData);
	}
	
	public static void synchronizeScore(int firstPaddleScore, int secondPaddleScore){
		staticNetworkView.RPC("syncScore", RPCMode.Others, firstPaddleScore, secondPaddleScore);
	}
	
	[RPC]
    void syncScore(int scoreForFirstPaddle, int scoreForSecondPaddle)
    {
        GameManager.firstPaddleScore = scoreForFirstPaddle;
        GameManager.secondPaddleScore = scoreForSecondPaddle;
    }
	
	[RPC]
    void syncNames(string firstName, string secondName)
    {
        Menu.name = firstName;
		Menu.enemyName = secondName;
    }
	
	[RPC]
    void endMultiplayerGame(string winningPlayerName)
    {
		Network.Destroy(GameManager.secondPaddle);
        Menu.winningPlayerName = winningPlayerName;
		Menu.endGameMulti = true;
		Menu.endGameSingle = false;
        Menu.waitForPlayer = false;
		GameManager.gamePaused = true;
		GameManager.singlePlayer = false;
		
		if(Network.isClient){
			Camera.current.transform.position = new Vector3(-18, Camera.current.transform.position.y, Camera.current.transform.position.z);
			Camera.current.transform.Rotate(new Vector3(0,180,0));
			Network.Disconnect();
		}
		
		GameManager.destroyScene();
    }
	
	public static void endGame(string winningPlayerName){
		staticNetworkView.RPC("endMultiplayerGame", RPCMode.All, winningPlayerName);
	}
	
    void OnPlayerDisconnected(NetworkPlayer player)
    {
	    Debug.Log("Clean up after player " + player);
    	Network.RemoveRPCs(player);
    	Network.DestroyPlayerObjects(player);
		Network.Disconnect();
		GameManager.NoPlayer = true;
    }
}
