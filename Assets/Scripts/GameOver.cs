using UnityEngine;
using System.Collections;

public class OverOptions : MonoBehaviour {

	void Start() {
	}

	void OnGUI() {
		GUI.contentColor = Color.red;
		if (GUI.Button (new Rect (Screen.width / 2 - 50, Screen.height / 2 + 150, 100, 40), "Retry?")) {
			Application.LoadLevel ("Scene1");
		}
		if (GUI.Button (new Rect (Screen.width / 2 - 50, Screen.height / 2 + 200, 100, 40), "Menu")) {
			Application.LoadLevel ("menu");
		}
		if (GUI.Button (new Rect (Screen.width / 2 - 50, Screen.height / 2 + 250, 100, 40), "Quit")) {
			Application.Quit ();
		}

	}
	
}

