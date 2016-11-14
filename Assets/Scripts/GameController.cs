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

	private float p1Score = 0;
	private float p2Score = 0;
	private int turn = 0;

	private int left = 0;
	private int right;

	public List<Button> btns = new List<Button>();

	void Start(){
		GetButtons ();
		AddListeners ();
	}

	void GetButtons(){
		GameObject[] objects = GameObject.FindGameObjectsWithTag ("PuzzleButton");



		for (int i = 0; i < objects.Length; i++) {
			float randomCoin = Random.Range (0, 4);
			Debug.Log (randomCoin);
			btns.Add (objects[i].GetComponent<Button>());
			if (randomCoin % 4 == 0) {
				btns [i].image.sprite = penny;
			} else if (randomCoin % 4 == 1) {
				btns [i].image.sprite = nickel ;
			} else if (randomCoin % 4 == 2) {
				btns [i].image.sprite = dime ;
			} else if (randomCoin % 4 == 3) {
				btns [i].image.sprite = quarter ;
			}

		}
		right = objects.Length - 1;
	}

	void AddListeners(){
		foreach (Button btn in btns) {
			btn.onClick.AddListener (() => PickAPuzzle ());
		}
	}

	public void PickAPuzzle(){
		string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
		Debug.Log ("clicking button named " + name);

		int coinIndex = int.Parse (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
		Sprite currentCoin = btns [coinIndex].image.sprite;
		float coinValue = 0f;
		if (currentCoin == penny) {
			coinValue = .01f;
		} else if (currentCoin == nickel) {
			coinValue = .05f;
		} else if (currentCoin == dime) {
			coinValue = .10f;
		} else if (currentCoin == quarter) {
			coinValue = .25f;
		}

		if (coinIndex == left) {
			if (turn % 2 == 0) {
				p1Score += coinValue;
			} else {
				p2Score += coinValue;
			}

			btns [coinIndex].interactable = false;
			btns [coinIndex].image.color = new Color(0,0,0,0);
			left++;
			turn++;
		} else if (coinIndex == right) {
			if (turn % 2 == 0) {
				p1Score += coinValue;
			} else {
				p2Score += coinValue;
			}

			btns [coinIndex].interactable = false;
			btns [coinIndex].image.color = new Color(0,0,0,0);
			right--;
			turn++;
		} else {
			
		}

		StartCoroutine (PickedACoin ());

		if (GameIsFinished ()) {
			Debug.Log ("Player 1: " + p1Score + "; Player 2: " + p2Score);
		}
	}

	IEnumerator PickedACoin(){
		yield return new WaitForSeconds (1f);

	 }

	bool GameIsFinished(){
		return left > right;
	}
}
