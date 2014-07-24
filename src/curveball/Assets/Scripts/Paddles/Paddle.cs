using UnityEngine;
using System.Collections.Generic;

public class Paddle : MonoBehaviour
{	
	protected const float zPositiveLimit = 5f;
	protected const float yPositiveLimit = 5.75f;
	protected const float yNegativeLimit = 1.75f;
	protected Vector3 lastPosition;
	protected const int MAX_POSITION_VECTORS = 10;
	protected Queue<Vector3> pastPositions = new Queue<Vector3>(MAX_POSITION_VECTORS);
	private Vector3 lastEnqueuedPosition = Vector3.zero;
	private Vector3 lastEnqueuedVector = Vector3.zero;
	protected int frameCounter = 0;

	void Update()
	{
		handlePaddleMovement();
		captureThisPosition();
	}
	
	virtual protected void handlePaddleMovement()
	{
	}

	public Vector3 getCurveVector(){
		Vector3 curveVector = Vector3.zero;
		while(pastPositions.Count > 0){
			curveVector += pastPositions.Dequeue();
		}
		
		return curveVector;
	}
	
	public void captureThisPosition(){
		if(pastPositions.Count > MAX_POSITION_VECTORS)
			pastPositions.Dequeue();
		
		if(Vector3.Dot(lastEnqueuedVector, lastEnqueuedPosition - transform.position)  <= 0f ) // Vectors don't point in the same direction
			pastPositions.Clear();
		
		lastEnqueuedVector = lastEnqueuedPosition - transform.position;
		pastPositions.Enqueue(lastEnqueuedVector);
		
		lastEnqueuedPosition = transform.position;
	}
	
	protected virtual Vector3 applyBounds(Vector3 coordinates){
		if(transform.position.z + coordinates.z > zPositiveLimit)
			coordinates.z = zPositiveLimit - transform.position.z;
		
		if(transform.position.z + coordinates.z < -zPositiveLimit)
			coordinates.z = -zPositiveLimit - transform.position.z;
		
		if(transform.position.y + coordinates.y > yPositiveLimit)
			coordinates.y = yPositiveLimit - transform.position.y;
		
		if(transform.position.y + coordinates.y < yNegativeLimit)
			coordinates.y = yNegativeLimit - transform.position.y;
		
		return coordinates;		
	}
	
	public virtual void afterScoreTrigger()
	{
	}
}
