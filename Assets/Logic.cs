using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Logic : MonoBehaviour {

	/*
	 * 1 - exist, there is a pin 
	 * 0 - exist, there is not a pin
	 * -1 - not exist
	 */

	int[] pinTable = new int[50]; 

	//If pin can go in direction of
	int[] up = new int[50]; //0
	int[] down = new int[50]; //1
	int[] left = new int[50]; //2
	int[] right = new int[50]; //3


	public Sprite ON;
	public Sprite OFF;

	EventSystem eventSystem;
	GameObject[] listOfPins = new GameObject[49];
	public Text highScore;

	int pinChecked;
	int actualScore;

	void Start () {
		actualScore = 32;
		if (!PlayerPrefs.HasKey ("highScore")) {
			PlayerPrefs.SetInt ("highScore", 32);
		}
		highScore.GetComponent<Text> ().text = "Best: " + PlayerPrefs.GetInt ("highScore").ToString() + "\n" + "Actual: " + actualScore.ToString();
		eventSystem = EventSystem.current;
	
		pinChecked = -1;
		for (int i = 0; i < 49; i++) {
			
			if (GameObject.Find ("" + i)) {
				listOfPins [i] = GameObject.Find ("" + i);
			}
		}
		

		//Placing pins in table
		for (int i = 0; i < 49; i++) {
			if (i == 0 || i == 1 || i == 5 || i == 6 ||
			    i == 7 || i == 8 || i == 12 || i == 13 ||
			    i == 35 || i == 36 || i == 40 || i == 41 ||
			    i == 42 || i == 43 || i == 47 || i == 48) {
				pinTable [i] = -1;
			} else {
				pinTable [i] = 1;
			}
		}
		pinTable [24] = 0;
		//listOfPins [25].SetActive (false);
		listOfPins[24].GetComponent<Image>().sprite = OFF;

		//Directions
		for (int i = 0; i < 49; i++) {
			
			//UP
			if (i == 2 || i == 3 || i == 4 || i == 9 || i == 10 || i == 11 || i == 14 || i == 15 || i == 19 || i == 20 || i == 21 || i == 22 || i == 26 || i == 27) {
				up [i] = 0;
			} else {
				up [i] = 1;
			}

			//DOWN
			if (i == 21 || i == 22 || i == 26 || i == 27 || i == 28 || i == 29 || i == 33 || i == 34 || i == 37 || i == 38 || i == 39 || i == 44 || i == 45 || i == 46) {
				down [i] = 0;
			} else {
				down [i] = 1;
			}

			//LEFT
			if (i == 2 || i == 3 || i == 9 || i == 10 || i == 14 || i == 15 || i == 21 || i == 22 || i == 28 || i == 29 || i == 37 || i == 38 || i == 44 || i == 45) {
				left [i] = 0;
			} else {
				left [i] = 1;
			}

			//RIGHT
			if (i == 3 || i == 4 || i == 10 || i == 11 || i == 19 || i == 20 || i == 26 || i == 27 || i == 33 || i == 34 || i == 38 || i == 39 || i == 45 || i == 46) {
				right [i] = 0;
			} else {
				right [i] = 1;
			}
			
		}
	


	
	} // < START
	

	void Update () {
		if (PlayerPrefs.GetInt ("highScore") >= actualScore) {
			PlayerPrefs.SetInt ("highScore",actualScore);
		}
		highScore.GetComponent<Text> ().text = "Best: " + PlayerPrefs.GetInt ("highScore").ToString() + "\n" + "Actual: " + actualScore.ToString();

		if (Input.GetKey("escape"))
			Application.Quit();
		if (Input.GetMouseButtonDown (0)) {


			if (EventSystem.current.currentSelectedGameObject == null) {
				
			} else {
				if (EventSystem.current.currentSelectedGameObject.GetComponent<Image> ().sprite == ON) {
					pinChecked = int.Parse(EventSystem.current.currentSelectedGameObject.name);


				} else if (EventSystem.current.currentSelectedGameObject.GetComponent<Image> ().sprite == OFF) {
					int pinPlace = int.Parse(EventSystem.current.currentSelectedGameObject.name);
					if (pinChecked != -1) {
						if (pinChecked - 2 == pinPlace) { //Lewo
							movePin(pinChecked,2);
							EventSystem.current.SetSelectedGameObject (null);
						} else if (pinChecked + 2 == pinPlace) {
							movePin(pinChecked,3);
							EventSystem.current.SetSelectedGameObject (null);
						} else if (pinChecked - 14 == pinPlace) {
							movePin(pinChecked,0);
							EventSystem.current.SetSelectedGameObject (null);
						} else if (pinChecked + 14 == pinPlace) {
							movePin(pinChecked,1);
							EventSystem.current.SetSelectedGameObject (null);
						}
					}

				}

			}
		}
	}
		


	public void movePin(int pinNumber, int direction) //0 - up, 1 - down, 2 - left, 3 - right
	{
		
		if (pinTable [pinNumber] == 1) { //If pin exist
			switch (direction) {
				case 0:
				if (up [pinNumber] == 1) { //Can go up?
					if (pinTable [pinNumber - 7] == 1) { //Is there pin in the middle of ?

						print (pinTable [pinNumber - 14]);
						if (pinTable [pinNumber - 14] == 0) { //Is place where i want to go empty?
							pinTable[pinNumber] = 0;
							listOfPins [pinNumber].GetComponent<Image> ().sprite = OFF;
							pinTable[pinNumber - 14] = 1;
							listOfPins [pinNumber - 14].GetComponent<Image> ().sprite = ON;
							pinTable [pinNumber - 7] = 0;
							listOfPins [pinNumber - 7].GetComponent<Image> ().sprite = OFF;
							actualScore--;

						}
					}
						
				}
					break;
				case 1:
				if (down [pinNumber] == 1) { //Can go down?
					if (pinTable [pinNumber + 7] == 1) { //Is there pin in the middle of ?
						if (pinTable [pinNumber + 14] == 0) { //Is place where i want to go empty?
							
							pinTable[pinNumber] = 0;
							listOfPins [pinNumber].GetComponent<Image> ().sprite = OFF;
							pinTable[pinNumber + 14] = 1;
							listOfPins [pinNumber + 14].GetComponent<Image> ().sprite = ON;
							pinTable [pinNumber + 7] = 0;
							listOfPins [pinNumber + 7].GetComponent<Image> ().sprite = OFF;
							actualScore--;
						}
			
					}
				}
					break;
				case 2:
				if (left [pinNumber] == 1) { //Can go left?
					if (pinTable [pinNumber - 1] == 1) { //Is there pin in the middle of ?
						if (pinTable [pinNumber - 2] == 0) { //Is place where i want to go empty?
							
							pinTable[pinNumber] = 0;
							listOfPins [pinNumber].GetComponent<Image> ().sprite = OFF;
							pinTable[pinNumber - 2] = 1;
							listOfPins [pinNumber - 2].GetComponent<Image> ().sprite = ON;
							pinTable [pinNumber - 1] = 0;
							listOfPins [pinNumber - 1].GetComponent<Image> ().sprite = OFF;
							actualScore--;
						}
					}
				}
					break;
				case 3:
				if (right [pinNumber] == 1) { //Can go right?
					if (pinTable [pinNumber + 1] == 1) { //Is there pin in the middle of ?
						if (pinTable [pinNumber + 2] == 0) { //Is place where i want to go empty?
							
							pinTable[pinNumber] = 0;
							listOfPins [pinNumber].GetComponent<Image> ().sprite = OFF;
							pinTable[pinNumber + 2] = 1;
							listOfPins [pinNumber + 2].GetComponent<Image> ().sprite = ON;
							pinTable [pinNumber + 1] = 0;
							listOfPins [pinNumber + 1].GetComponent<Image> ().sprite = OFF;
							actualScore--;
						}
					}
				}
					break;
			}
				
		}

	}
}
