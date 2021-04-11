using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	public Vector2 bounds;
	public Vector2 ScreenSizeInWorldSpace;


	private void Awake()
    {
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
		CalculateScreenBounds();
	}

    private void CalculateScreenBounds()
	{	
		//Get the boundaries of the camera
		float camDistance = Vector3.Distance(this.transform.position, Camera.main.transform.position);
		Vector2 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDistance));
		Vector2 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, camDistance));
        bounds = new Vector2(bottomLeft.x, topRight.y);

		ScreenSizeInWorldSpace = new Vector2(topRight.x * 2, topRight.y * 2);
	}
}
