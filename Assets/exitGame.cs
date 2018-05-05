using UnityEngine;
using System.Collections;

public class exitGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void quit()
	{
		Application.Quit ();
	}
	public void restart()
	{
		Debug.Log ("clicked");
		Application.LoadLevel ("main");
	}
}
