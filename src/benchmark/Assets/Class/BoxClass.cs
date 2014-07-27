using UnityEngine;
using System.Collections;

public class BoxClass : TileClass {

	public override void rotate(){
		this.transform.Rotate(Vector3.up);
		if(Random.Range(0,1) >= 0.5)
			this.transform.Rotate(Vector3.right);
		else
			this.transform.Rotate(-Vector3.right);
	}
}
