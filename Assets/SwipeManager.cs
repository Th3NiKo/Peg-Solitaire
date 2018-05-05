using UnityEngine;
using System.Collections;


public enum swipeDirection
{
	None = 0,
    Left = 1,
	Right = 2,
	Up = 4,
	Down = 8,
}

public class SwipeManager : MonoBehaviour {


	private static SwipeManager instance;
	public static SwipeManager Instance{get {return instance;}}
	public swipeDirection Direction{ set; get; }
	public Vector3 touchPosition;
	private float swipeResistanceX = 50.0f;
	private float swipeResistanceY = 50.0f;


	void Start()
	{
		instance = this;
	}

	void Update()
	{
		Direction = swipeDirection.None;

		if (Input.GetMouseButtonDown (0)) {
			touchPosition = Input.mousePosition;


		}

		if (Input.GetMouseButtonUp (0)) {
			Vector2  deltaSwipe = touchPosition - Input.mousePosition;

			if (Mathf.Abs (deltaSwipe.x) > swipeResistanceX) {
				//Swipe on the x
				Direction |= (deltaSwipe.x < 0) ?  swipeDirection.Right : swipeDirection.Left;
			}
			if (Mathf.Abs (deltaSwipe.y) > swipeResistanceY) {
				//Swipe on the y
				Direction |= (deltaSwipe.y < 0) ?  swipeDirection.Up : swipeDirection.Down;
			}



		}

	}


	public bool isSwiping(swipeDirection dir)
	{
		return (Direction & dir) == dir;
	}





}
