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

	[SerializeField]
	private Options options;



	private float p1Score = 0;
	private float p2Score = 0;

	[SerializeField]
	private GUIText player1Text;
	[SerializeField]
	private GUIText player2Text; 
	[SerializeField]
	private GUIText gameOverText; 

	private int turn = 0;

	private int left = 0;
	private int right;

	private int length;

	private int rounds;
	private int p1win = 0;
	private int p2win = 0;

	public List<Button> btns = new List<Button>();

	void Start(){
		GetButtons ();
		AddListeners ();
		CountRounds ();
		options.numCoins = 12;
		options.numPlayers = 2;
		options.numRounds = 1;		
		player1Text.text = "Player 1: 0.00";
		player2Text.text = "Player 2: 0.00";
		gameOverText.text = "";
	}

	void CountRounds (){
		rounds = options.gameObject.GetComponent<Options> ().numRounds;
		Debug.Log ("initial rounds " + rounds);
	}
		

	void GetButtons(){
		GameObject[] objects = GameObject.FindGameObjectsWithTag ("PuzzleButton");


		Debug.Log ("GetButtons: " + objects.Length);		
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
		length = objects.Length - 1;
		right = length;
	}

	void AddListeners(){
		foreach (Button btn in btns) {
			btn.onClick.AddListener (() => PickAPuzzle ());
			btn.interactable = true;
			btn.image.color = new Color(1,1,1,1);
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
//		Player1Text.text = p1Score.ToString();
		StartCoroutine (PickedACoin ());
		player1Text.text = "Player 1: " + p1Score.ToString ("#0.00");
		player2Text.text = "Player 2: " + p2Score.ToString ("#0.00");
		if (RoundIsFinished ()) {
			player1Text.text = "Player 1: " + p1Score; 
			player2Text.text = "Player 2: " + p2Score; 
			Debug.Log ("Player 1: " + p1Score + "; Player 2: " + p2Score);

			if (p1Score > p2Score) {
				p1win += 1;
			}
			if (p1Score < p2Score) {
				p2win += 1;
			}

			rounds -= 1;
			GameOver (rounds);
		}
	}

	IEnumerator PickedACoin(){
		yield return new WaitForSeconds (1f);
	}
		

	bool RoundIsFinished(){
		return left > right;
	}

	public void GameOver(int n) {
		if (rounds == 0) {
			string winner;
			if (p1win > p2win) {
				winner = "Player 1 wins!";
			} else if (p1win < p2win) {
				winner = "Player 2 wins!";
			} else {
				winner = "No winner!";
			}
			gameOverText.text = "Game over! " + winner; 
		} else {
			Debug.Log ("restart game");
			Debug.Log (rounds);
			RestartGame ();
		}
	}

	public void RestartGame() {
		GetButtons ();
		AddListeners ();
		player1Text.text = "Player 1: 0.00";
		player2Text.text = "Player 2: 0.00";
		p1Score = 0;
		p2Score = 0;
		left = 0;
		right = length;
	}
							

}
