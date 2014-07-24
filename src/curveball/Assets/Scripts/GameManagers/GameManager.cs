using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	
	public static GameObject firstPaddle;
	public static GameObject secondPaddle;
	public static GameObject ball;
	public static GameObject lightGameObject;
	public static GameObject lightGameObject_spot;
	public static GameObject lightGameObject_spot_2;
	private RevmobHandler revmobHandler;
	
	public static int firstPaddleScore = 0;
	public static int secondPaddleScore = 0;

	public static bool gamePaused = true;
	public static bool singlePlayer = false;
	public static bool NoPlayer = true;
	
	private float lightDelta = 0.5f;
	private Color currentLightColor = new Color();
	private float lightsColorChangeTimer = 1;
	private const int MULTIPLAYER_SCORE_LIMIT = 11;
		
	void Start () {
		Application.runInBackground = true;		
		
		if(!GameManager.isInGame()){
			GameManager.startDemoMatch();
		}
		
		StartCoroutine(changeLightColors());
	}
	
	void Awake(){
		revmobHandler = this.GetComponent<RevmobHandler>();			
	}
	
	// Update is called once per frame
	void Update () {		
		if(GameManager.isInGame())
			Screen.lockCursor = true;
		else
			Screen.lockCursor = false;
		
//		changeLightColors(); // Jezeli swiatla spieprzyly sie w multi dla klienta, to trzeba to odkomentowac!
		
		if(Input.GetKeyUp(KeyCode.Escape))
		{
			if(!(isSinglePlayer() || Network.isClient || Network.isServer))
				return;
				
			if (gamePaused)
				GameManager.ResumeGame();
			else
				GameManager.PauseGame();
		}
	}
	
	IEnumerator changeLightColors(){
		while(true)
		{
			if(lightGameObject_spot != null && lightGameObject_spot_2 != null) {
				Light l = lightGameObject_spot.GetComponent<Light>();
				currentLightColor.r += Random.Range(-0.5f, 0.5f) * lightDelta;
				currentLightColor.g += Random.Range(-0.5f, 0.5f) * lightDelta;
				currentLightColor.b += Random.Range(-0.5f, 0.5f) * lightDelta;
				
				l.color = currentLightColor;
				
				l = lightGameObject_spot_2.GetComponent<Light>();
				l.color = currentLightColor;
			}
			
			yield return new WaitForSeconds(lightsColorChangeTimer);			
		}		
	}
	
	// Load instances and get their components
	public static void startSinglePlayerMatch(){
		destroyScene();
		ball = (GameObject)Instantiate(Resources.Load("Prefabs/BallPrefab"), new Vector3(0f, 3.75f, 0f), Quaternion.identity);
		firstPaddle = (GameObject)Instantiate(Resources.Load("Prefabs/PlayerPaddlePrefab"), new Vector3(-12f, 3.75f, 0f), Quaternion.identity);
		secondPaddle = (GameObject)Instantiate(Resources.Load("Prefabs/AiPaddlePrefab"), new Vector3(12f, 3.75f, 0f), Quaternion.identity);
		instantiateLights();

		GameManager.resetPlayerScore();
		
		singlePlayer = true;
		GameManager.ResumeGame();
	}
	
	public static void startDemoMatch(){
		destroyScene();
		ball = (GameObject)Instantiate(Resources.Load("Prefabs/BallPrefab"), new Vector3(0f, 3.75f, 0f), Quaternion.identity);
		firstPaddle = (GameObject)Instantiate(Resources.Load("Prefabs/AiPaddlePrefab"), new Vector3(-12f, 3.75f, 0f), Quaternion.identity);
		secondPaddle = (GameObject)Instantiate(Resources.Load("Prefabs/AiPaddlePrefab"), new Vector3(12f, 3.75f, 0f), Quaternion.identity);
		instantiateLights ();

		ball.rigidbody.AddForce(Random.Range(0,200),Random.Range(0,200),Random.Range(0,200));
	}
	public static void destroyScene(){
		Menu.waitForPlayer = false;
		singlePlayer = false;
		gamePaused = true;
		Destroy(firstPaddle);
		Destroy(ball);
		Destroy(secondPaddle);
		destroyLights();
		resetPlayerScore();
	}
	
	public static void destroyLights ()
	{
		Destroy(lightGameObject);
		Destroy(lightGameObject_spot);
		Destroy(lightGameObject_spot_2);
	}
	
	public static void instantiateLights()
	{
		lightGameObject = (GameObject)Instantiate(Resources.Load("Prefabs/BallLight"), new Vector3(0f, 3.75f, 0f), Quaternion.identity);
		lightGameObject_spot = (GameObject)Instantiate(Resources.Load("Prefabs/Spotlight"), new Vector3(0f, 3.75f, 0f), Quaternion.identity);
		lightGameObject_spot_2 = (GameObject)Instantiate(Resources.Load("Prefabs/Spotlight"), new Vector3(0f, 3.75f, 0f), Quaternion.identity);
	}
	
	// Move this to Network Manager?
	public static GameObject spawnPlayer(Vector3 position){
		return (GameObject)Network.Instantiate(Resources.Load("Prefabs/MultiplayerPaddlePrefab"), position, Quaternion.identity, 0);
	}
	
	public static GameObject spawnBall(Vector3 position){
		return (GameObject)Network.Instantiate(Resources.Load("Prefabs/MultiplayerBallPrefab"), position, Quaternion.identity, 0);
	}
	
	private static void PauseGame()
	{
		gamePaused = true;
		Time.timeScale = 0.0f;	
	}
	
	public static void ResumeGame()
	{
		gamePaused = false;
		Time.timeScale = 1.0f;
	}
	
	static public bool isSinglePlayer(){
		return singlePlayer;
	}
	
	public static void handleFirstPaddleScoring(){
		firstPaddleScore++;
		
		if(Network.isServer && firstPaddleScore >= MULTIPLAYER_SCORE_LIMIT && Network.connections.Length > 0){
			NetworkManager.endGame(Menu.name);
			return;
		}		
		
		if(Network.isServer)
			NetworkManager.synchronizeScore(firstPaddleScore, secondPaddleScore);

		if(GameManager.singlePlayer)
			secondPaddle.gameObject.GetComponent<Paddle>().afterScoreTrigger();
	}
	
	public static void handleSecondPaddleScoring(){
		secondPaddleScore++;
		
		if(Network.isServer && secondPaddleScore >= MULTIPLAYER_SCORE_LIMIT && Network.connections.Length > 0){
			NetworkManager.endGame(Menu.enemyName);
			return;
		}
		
		if(Network.isServer)
			NetworkManager.synchronizeScore(firstPaddleScore, secondPaddleScore);

		if(GameManager.singlePlayer)
			firstPaddle.gameObject.GetComponent<Paddle>().afterScoreTrigger();
	}
	
	public static void endSinglePlayerGame(){
		gamePaused = true;
		singlePlayer = false;
		Menu.waitForPlayer = false;
		Menu.endGameMulti = false;
		Menu.endGameSingle = true;
	}
	
	public static int getFirstPlayerScore(){
		return firstPaddleScore;
	}
	
	public static int getSecondPlayerScore(){
		return secondPaddleScore;
	}
	
	public static bool isInGame()
	{
		return !gamePaused;			
	}
	
	public static void resetPlayerScore(){
		firstPaddleScore = 0;
		secondPaddleScore = 0;
	}
	
	public static void Restart(bool forward)
	{
		(GameManager.ball.GetComponent<Ball>()).resetMaxVelocity();
		GameManager.ball.constantForce.force = new Vector3(0f,0f,0f);
		
		if(forward){
			GameManager.ball.transform.position = new Vector3(11f, 3.75f, 0f);
			GameManager.ball.rigidbody.velocity = new Vector3(-1f,0f,0f);
		}
		else{
			GameManager.ball.transform.position = new Vector3(-11f, 3.75f, 0f);
			GameManager.ball.rigidbody.velocity = new Vector3(1f,0f,0f);
		}
	}
	
	public RevmobHandler getRevmobHandler(){
		return revmobHandler;
	}
}
