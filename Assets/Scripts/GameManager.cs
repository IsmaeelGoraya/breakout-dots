using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	public Vector2 bounds;

	private List<Vector2> newVertices;
	private EdgeCollider2D edgeCollider;

    private void Awake()
    {
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
	}

    private void Start()
    {
		newVertices = new List<Vector2>();
		edgeCollider = this.GetComponent<EdgeCollider2D>();
        CreateBoundryEdges();
        
    }

    private void CreateBoundryEdges()
	{	
		//Get the boundaries of the camera
		float camDistance = Vector3.Distance(this.transform.position, Camera.main.transform.position);
		Vector2 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDistance));
		Vector2 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, camDistance));
        bounds = new Vector2(bottomLeft.x, topRight.y);
		//Set these to the new vertices for the Edge Collider
		newVertices.Add (new Vector2(bottomLeft.x, bottomLeft.y)); // Sets the start point
		newVertices.Add (new Vector2(bottomLeft.x, topRight.y)); // Creates the Top Side
		newVertices.Add (new Vector2(topRight.x, topRight.y)); // Creates the Right Side
		newVertices.Add (new Vector2(topRight.x, bottomLeft.y)); // Creates the Bottom Side
		newVertices.Add (new Vector2(bottomLeft.x, bottomLeft.y)); //Creates the Left Side
		
		//Update the edge collider
		edgeCollider.points = newVertices.ToArray();
	}
}
