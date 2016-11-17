using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManger : MonoBehaviour {

	public Transform mainMenu, optionsMenu;

	public void LoadScene(string name) {
		Application.LoadLevel (name);
	}


	public Dropdown dropdown;


	public int players = 2;
	public void numPlayers(bool clicked) {

		//Debug.Log (dropdown.value); 	

		if (dropdown.value == 0) {
			players = 2;
		} else if (dropdown.value == 1) {
			players = 3;
		} else if (dropdown.value == 1) {
			players = 4;
		}
		Debug.Log (players);
	}


	public void QuitGame() {
		Application.Quit ();
	}

	public void OptionMenu(bool clicked) {
		if (clicked == true) {
			optionsMenu.gameObject.SetActive (clicked);
			mainMenu.gameObject.SetActive (false);
		} else {
			optionsMenu.gameObject.SetActive (clicked);
			mainMenu.gameObject.SetActive (true);
		}

		//Debug.Log ("123");
	}
		

}
