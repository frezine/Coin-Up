using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	[SerializeField]
	private Sprite penny;
	[SerializeField]
	private Sprite nickel;
	[SerializeField]
	private Sprite dime;
	[SerializeField]
	private Sprite quarter;



	public List<Button> btns = new List<Button>();

	void Start(){
		GetButtons ();
		AddListeners ();
	}

	void GetButtons(){
		GameObject[] objects = GameObject.FindGameObjectsWithTag ("PuzzleButton");



		for (int i = 0; i < objects.Length; i++) {
			btns.Add (objects[i].GetComponent<Button>());
			if (i % 4 == 0) {
				btns [i].image.sprite = penny;
			} else if (i % 4 == 1) {
				btns [i].image.sprite = nickel ;
			} else if (i % 4 == 2) {
				btns [i].image.sprite = dime ;
			} else if (i % 4 == 3) {
				btns [i].image.sprite = quarter ;
			}

		}
	}

	void AddListeners(){
		foreach (Button btn in btns) {
			btn.onClick.AddListener (() => PickAPuzzle ());
		}
	}

	public void PickAPuzzle(){
		string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
		Debug.Log ("clicking button named " + name);
	}
}
