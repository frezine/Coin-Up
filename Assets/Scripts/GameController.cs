using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class GameController : MonoBehaviour {

	[SerializeField]
	private Sprite penny;
	[SerializeField]
	private Sprite pennyH;

	[SerializeField]
	private Sprite nickel;
	[SerializeField]
	private Sprite nickelH;

	[SerializeField]
	private Sprite dime;
	[SerializeField]
	private Sprite dimeH;

	[SerializeField]
	private Sprite quarter;
	[SerializeField]
	private Sprite quarterH;

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
	[SerializeField]
	private GUIText RoundsText;
	[SerializeField]
	private GUIText RoundsWinnerText;


	[SerializeField]
	private GUIText highlight_score_p1;

	[SerializeField]
	private GUIText highlight_score_p2;

	private int turn = 0;

	private int left = 0;
	private int right;

	private int length;

	private int rounds;
	private int p1win = 0;
	private int p2win = 0;
	private int showrounds = 1;
	private string round_winner;


	public List<Button> btns = new List<Button>();


	IEnumerator wait()
	{
		//		Debug.Log (Time.time);
		yield return new WaitForSeconds(5f);
		//		Debug.Log (Time.time);
	}


	void Start(){
		//		Debug.Log (Time.time);
		StartCoroutine (wait ());
		//		Debug.Log (Time.time);
		GetButtons ();
		AddListeners ();
		CountRounds ();
		options.numCoins = 12;
		options.numPlayers = 2;
		options.numRounds = 1;
		player1Text.text = "Player 1: 0.00";
		player2Text.text = "Player 2: 0.00";
		gameOverText.text = "";
		RoundsText.text = "Rounds: 1";

		highlight_score_p1.text = "";
		highlight_score_p1.fontSize = 30;
		highlight_score_p2.text = "";
		highlight_score_p2.fontSize = 30;

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
			StartCoroutine (wait ());
		}
		length = objects.Length - 1;
		right = length;
		HighlightCoins ();
	}

	void AddListeners(){
		foreach (Button btn in btns) {
			btn.onClick.AddListener (() => PickAPuzzle ());
			btn.interactable = true;
			btn.image.color = new Color(1,1,1,1);
		}
	}

	public void ShuffleCoins(){
		for (var i = right; i > left; i--) {
			var r = Random.Range (0, i);
			var tmp = btns [i].image.sprite;
			btns [i].image.sprite = btns [r].image.sprite;
			btns [r].image.sprite = tmp;
		}

		//		for (var i = left; i < right; i++) {
		//			if (btns [i].image.sprite == penny) {
		//				btns [i].image.sprite = penny;
		//			} else if (btns [i].image.sprite == nickel) {
		//				btns [i].image.sprite = nickel;
		//			} else if (btns [i].image.sprite == dime) {
		//				btns [left].image.sprite = dime;
		//			} else if (btns [i].image.sprite == quarter) {
		//				btns [i].image.sprite = quarter;
		//			} else {
		//			}
		//		}
	}

	public void HighlightCoins(){
		if (btns [left].image.sprite == penny) {
			btns [left].image.sprite = pennyH;
		} else if (btns [left].image.sprite == nickel) {
			btns [left].image.sprite = nickelH;
		} else if (btns [left].image.sprite == dime) {
			btns [left].image.sprite = dimeH;
		} else if (btns [left].image.sprite == quarter) {
			btns [left].image.sprite = quarterH;
		} else {
		}

		if (btns [right].image.sprite == penny) {
			btns [right].image.sprite = pennyH;
		} else if (btns [right].image.sprite == nickel) {
			btns [right].image.sprite = nickelH;
		} else if (btns [right].image.sprite == dime) {
			btns [right].image.sprite = dimeH;
		} else if (btns [right].image.sprite == quarter) {
			btns [right].image.sprite = quarterH;
		} else {
		}
		for (int i = left + 1; i < right; i++) {
			if (btns [i].image.sprite == penny || btns [i].image.sprite == pennyH) {
				btns [i].image.sprite = penny;
			} else if (btns [i].image.sprite == nickel || btns [i].image.sprite == nickelH) {
				btns [i].image.sprite = nickel;
			} else if (btns [i].image.sprite == dime || btns [i].image.sprite == dimeH) {
				btns [i].image.sprite = dime;
			} else if (btns [i].image.sprite == quarter || btns [i].image.sprite == quarterH) {
				btns [i].image.sprite = quarter;
			} else {
			}

		}
	}

	public void PickAPuzzle(){

		string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
		Debug.Log ("clicking button named " + name);

		int coinIndex = int.Parse (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
		Sprite currentCoin = btns [coinIndex].image.sprite;
		float coinValue = 0f;
		if (currentCoin == pennyH) {
			coinValue = .01f;
		} else if (currentCoin == nickelH) {
			coinValue = .05f;
		} else if (currentCoin == dimeH) {
			coinValue = .10f;
		} else if (currentCoin == quarterH) {
			coinValue = .25f;
		}

		if (coinIndex == left) {
			if (turn % 2 == 0) {
				p1Score += coinValue;
				highlight_score_p1.text = "+  " + coinValue.ToString("#0.00");
				StartCoroutine("wait_player1");
			} else {
				p2Score += coinValue;
				highlight_score_p2.text = "+  " + coinValue.ToString("#0.00");
				StartCoroutine("wait_player2");
			}
			rotateObject (btns [coinIndex].image);
			btns [coinIndex].interactable = false;
			btns [coinIndex].image.color = new Color(0,0,0,0);
			left++;
			turn++;
			StartCoroutine (PickedACoin ());
		} else if (coinIndex == right) {
			if (turn % 2 == 0) {
				p1Score += coinValue;
				highlight_score_p1.text = "+  " + coinValue.ToString("#0.00");
				StartCoroutine("wait_player1");

			} else {
				p2Score += coinValue;
				highlight_score_p2.text = "+  " + coinValue.ToString("#0.00");
				StartCoroutine("wait_player2");
			}

			btns [coinIndex].interactable = false;

			btns [coinIndex].image.color = new Color(0,0,0,0);
			right--;
			turn++;
			StartCoroutine (PickedACoin ());
		} else {

		}
		//		Player1Text.text = p1Score.ToString();
		//		StartCoroutine (PickedACoin ());

		player1Text.text = "Player 1: " + p1Score.ToString ("#0.00");
		player2Text.text = "Player 2: " + p2Score.ToString ("#0.00");



		if (RoundIsFinished ()) {
			StartCoroutine (EndRound ());

		} else {
			//			ShuffleCoins ();
			HighlightCoins ();
		}

	}
	public int rotationDirection = -1; // -1 for clockwise
	// 1 for anti-clockwise
	public int rotationStep = 10; // should be less than 90
	// All the objects with which collision will be checked
	private Vector3 currentRotation, targetRotation;

	private void rotateObject(Image img)
	{
		currentRotation = img.transform.eulerAngles;
		targetRotation.z = (currentRotation.z + (90 * rotationDirection));
		StartCoroutine (objectRotationAnimation(img));
	}


	IEnumerator objectRotationAnimation(Image img)
	{
		// add rotation step to current rotation.
		currentRotation.z += (rotationStep * rotationDirection);
		img.transform.eulerAngles = currentRotation;
		yield return new WaitForSeconds (0);
		if (((int)currentRotation.z >
			(int)targetRotation.z && rotationDirection < 0) || // for clockwise
			((int)currentRotation.z < (int)targetRotation.z && rotationDirection > 0)) // for anti-clockwise
		{
			StartCoroutine (objectRotationAnimation(img));
		}
		else
		{
			img.transform.eulerAngles = targetRotation;
			//			isRotating = false;
		}
	}

	IEnumerator rotateObjectAgain()
	{
		yield return new WaitForSeconds (0.2f);
		//		rotateObject();
	}

	IEnumerator EndRound()
	{
		yield return new WaitForSeconds(2f);

//		player1Text.text = "Player 1: " + p1Score;
//		player2Text.text = "Player 2: " + p2Score;
//		Debug.Log ("Player 1: " + p1Score + "; Player 2: " + p2Score);

		if (p1Score > p2Score) {
			round_winner ="Player 1!";
			p1win += 1;
		}
		if (p1Score < p2Score) {
			round_winner = "Player 2!";
			p2win += 1;
		}
		player1Text.text = "Player 1: " + p1Score;
		player2Text.text = "Player 2: " + p2Score;
		rounds -= 1;
//		if (rounds != 0) {
//			RoundsWinnerText.text = "Round " + showrounds + ": winner is " + round_winner;
//		}
			
		showrounds += 1;
		GameOver (rounds);
		yield return new WaitForSeconds(5f);

	}


	IEnumerator PickedACoin(){

		btns [left].enabled = false;
		btns [right].enabled = false;
		yield return new WaitForSeconds (1f);
		btns [left].enabled = true;
		btns [right].enabled = true;

	}

	IEnumerator wait_player1()
	{
		yield return new WaitForSeconds(1f);
		highlight_score_p1.text = "";
	}


	IEnumerator wait_player2()
	{
		yield return new WaitForSeconds(1f);
		highlight_score_p2.text = "";
	}

	bool RoundIsFinished(){
		return left > right;
	}



	public void GameOver(int n) {
		string winner;
		if (rounds == 0) {
			if (p1win > p2win) {
				winner = "Player 1 wins!";
			} else if (p1win < p2win) {
				winner = "Player 2 wins!";
			} else {
				winner = "No winner!";
			}
			StartCoroutine(wait ());

			gameOverText.text = "Game over! " + winner;
		} else {
			RoundsWinnerText.text = "Round " + showrounds + ": winner is " + round_winner;
			Debug.Log (round_winner);
			StartCoroutine(wait ());
//			Debug.Log ("restart game");
//			Debug.Log (rounds);
			RestartGame ();
		}
	}


	public void RestartGame() {
		GetButtons ();
		AddListeners ();
		player1Text.text = "Player 1: 0.00";
		player2Text.text = "Player 2: 0.00";
		RoundsText.text = "Rounds: " + showrounds;
		RoundsWinnerText.text = " ";
		p1Score = 0;
		p2Score = 0;
		left = 0;
		right = length;
	}

	IEnumerator BackToMenu() {
		yield return new WaitForSeconds (0.5f);
		Application.LoadLevel ("menu");
	}


}
