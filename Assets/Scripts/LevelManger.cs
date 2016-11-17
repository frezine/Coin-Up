using UnityEngine;
using System.Collections;

public class LevelManger : MonoBehaviour {

	public Transform mainMenu, optionsMenu;

	public void LoadScene(string name) {
		Application.LoadLevel (name);
	}

	void Awake(){
		
	}

	public void Player(){
//		Debug.Log(optionsMenu.GetChild ());
	}
	public void Player23(){
//		Debug.Log(optionsMenu.GetChild ());
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
	}
		

}
