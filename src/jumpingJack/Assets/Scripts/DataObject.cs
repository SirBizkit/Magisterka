using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataObject{

	public static GameObject player;

	static public int score = 0;
	static public float altitude = 0;
	static public float xPos = 0;
	static public Vector3 cameraPosition = Vector3.zero;
	static public List<GameObject> enemyList = null;
}
