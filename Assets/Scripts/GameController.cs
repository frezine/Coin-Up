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
	private Sprite mystery;
	[SerializeField]
	private Sprite mysteryH;

	[SerializeField]
	private Sprite swap;
	[SerializeField]
	private Sprite swapH;

	[SerializeField]
	private Sprite shuffle;
	[SerializeField]
	private Sprite shuffleH;

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
	private bool endOption = false;

	public Vector3 defaultAngle;


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
		highlight_score_p1.fontSize = 20;
		highlight_score_p1.fontStyle = FontStyle.Bold;

		highlight_score_p2.text = "";
		highlight_score_p2.fontStyle = FontStyle.Bold;
		highlight_score_p2.fontSize = 20;

	}

	void CountRounds (){
		rounds = options.gameObject.GetComponent<Options> ().numRounds;
		Debug.Log ("initial rounds " + rounds);
	}


	void GetButtons(){
		GameObject[] objects = GameObject.FindGameObjectsWithTag ("PuzzleButton");

		defaultAngle.y = 0;
		Debug.Log ("GetButtons: " + objects.Length);
		for (int i = 0; i < objects.Length; i++) {
			float randomCoin = Random.Range (0, 5);
			//			Debug.Log (randomCoin);
			btns.Add (objects[i].GetComponent<Button>());
			if (randomCoin % 5 == 0) {
				btns [i].image.sprite = penny;
			} else if (randomCoin % 5 == 1) {
				btns [i].image.sprite = nickel ;
			} else if (randomCoin % 5 == 2) {
				btns [i].image.sprite = dime ;
			} else if (randomCoin % 5 == 3) {
				btns [i].image.sprite = quarter ;
			} else if (randomCoin % 5 == 4) {

				float special = Random.Range (0, 3);
				if (special % 3 == 0) {
					btns [i].image.sprite = mystery;
				} else if (special % 3 == 1) {
					btns [i].image.sprite = swap;
				} else if (special % 3 == 2) {
					btns [i].image.sprite = shuffle;
				}

			}
			btns [i].image.transform.eulerAngles = defaultAngle;
			Debug.Log ("Coin angle: " + btns [i].image.transform.eulerAngles.y);
			btns [i].interactable = true;
			btns [i].enabled = true;
			btns [i].image.color = new Color(1,1,1,1);
			btns[i].image.CrossFadeAlpha (1.0f, 1.5f, false);
			StartCoroutine (wait ());
		}
		length = objects.Length - 1;
		right = length;
		HighlightCoins ();



		//		Debug.Log ("GetButtons: " + objects.Length);
		//		for (int i = 0; i < objects.Length; i++) {
		//			float randomCoin = Random.Range (0, 4);
		//			Debug.Log (randomCoin);
		//			btns.Add (objects[i].GetComponent<Button>());
		//			if (randomCoin % 4 == 0) {
		//				btns [i].image.sprite = penny;
		//			} else if (randomCoin % 4 == 1) {
		//				btns [i].image.sprite = nickel ;
		//			} else if (randomCoin % 4 == 2) {
		//				btns [i].image.sprite = dime ;
		//			} else if (randomCoin % 4 == 3) {
		//				btns [i].image.sprite = quarter ;
		//			}
		//			StartCoroutine (wait ());
		//		}
		//		length = objects.Length - 1;
		//		right = length;
		//		HighlightCoins ();
	}

	void AddListeners(){
		foreach (Button btn in btns) {
			btn.onClick.AddListener (() => PickAPuzzle ());
			btn.interactable = true;
			btn.image.color = new Color(1,1,1,1);
		}
	}

	public void ShuffleCoins(int index){
		if (index == left) {
			for (var i = left + 1; i < right +1; i++) {
				Debug.Log (btns [i].image.sprite.name);
				var r = Random.Range (left+1, right+1);
				var tmp = btns [i].image.sprite;
				btns [i].image.sprite = btns [r].image.sprite;
				btns [r].image.sprite = tmp;
			}
		} else {
			for (var i = left; i < right; i++) {
				Debug.Log (btns [i].image.sprite.name);
				var r = Random.Range (left, right);
				var tmp = btns [i].image.sprite;
				btns [i].image.sprite = btns [r].image.sprite;
				btns [r].image.sprite = tmp;
			}
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
		} else if (btns [left].image.sprite == mystery) {
			btns [left].image.sprite = mysteryH;
		} else if (btns [left].image.sprite == shuffle) {
			btns [left].image.sprite = shuffleH;
		} else if (btns [left].image.sprite == swap) {
			btns [left].image.sprite = swapH;
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
		} else if (btns [right].image.sprite == mystery) {
			btns [right].image.sprite = mysteryH;
		} else if (btns [right].image.sprite == shuffle) {
			btns [right].image.sprite = shuffleH;
		} else if (btns [right].image.sprite == swap) {
			btns [right].image.sprite = swapH;
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
			} else if (btns [i].image.sprite == mystery || btns [i].image.sprite == mysteryH) {
				btns [i].image.sprite = mystery;
			} else if (btns [i].image.sprite == shuffle || btns [i].image.sprite == shuffleH) {
				btns [i].image.sprite = shuffle;
			} else if (btns [i].image.sprite == swap || btns [i].image.sprite == swapH) {
				btns [i].image.sprite = swap;
			} else {
			}

		}
		//		rotateObject (btns [left].image);
		//		rotateObject (btns [right].image);
	}

	public void Swap(){
		float t1 = p1Score;
		float t2 = p2Score;
		float coinValue = Mathf.Abs(p1Score - p2Score);

		if (t1 > t2) {
			p1Score -= coinValue;
			p2Score += coinValue;
			highlight_score_p1.text = "-  " + coinValue.ToString("#0.00");
			StartCoroutine("wait_player1");
			highlight_score_p2.text = "+  " + coinValue.ToString("#0.00");
			StartCoroutine("wait_player2");
		} else if (t2 > t1) {
			p1Score += coinValue;
			p2Score -= coinValue;
			highlight_score_p1.text = "+  " + coinValue.ToString("#0.00");
			StartCoroutine("wait_player1");
			highlight_score_p2.text = "-  " + coinValue.ToString("#0.00");
			StartCoroutine("wait_player2");
		} else {
			p1Score += coinValue;
			p2Score += coinValue;
			highlight_score_p1.text = "+  " + coinValue.ToString("#0.00");
			StartCoroutine("wait_player1");
			highlight_score_p2.text = "+  " + coinValue.ToString("#0.00");
			StartCoroutine("wait_player2");
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
		} else if (currentCoin == mysteryH) {
			coinValue = ((float)Random.Range (1, 51)) / 100f;
		} else if (currentCoin == swapH) {
			float temp;
			//			coinValue = p1Score - p2Score;
			//			p1Score = p2Score;
			//			p2Score = temp;

		} else if (currentCoin == shuffleH) {
			coinValue = ((float)Random.Range (1, 51)) / 100f;
		} else {
			return;
		}

		btns [coinIndex].enabled = false;

		if (currentCoin == swapH) {
			Swap ();		
		} else if (currentCoin == shuffleH){
			ShuffleCoins(coinIndex);
		} else {
			if (turn % 2 == 0) {
				p1Score += coinValue;
				highlight_score_p1.text = "+  " + coinValue.ToString ("#0.00");
				StartCoroutine ("wait_player1");

			} else {
				p2Score += coinValue;
				highlight_score_p2.text = "+  " + coinValue.ToString ("#0.00");
				StartCoroutine ("wait_player2");
			}
		}

		//		if (coinIndex == left) {
		//			btns [coinIndex].enabled = false;
		//			if(currentCoin == swapH){
		//				
		//					highlight_score_p1.text = "+  " + coinValue.ToString("#0.00");
		//					StartCoroutine("wait_player1");
		//					highlight_score_p2.text = "+  " + coinValue.ToString("#0.00");
		//					StartCoroutine("wait_player2");
		//				} else {
		//					p2Score += coinValue;
		//					highlight_score_p2.text = "+  " + coinValue.ToString("#0.00");
		//					StartCoroutine("wait_player2");
		//				}
		//
		//
		//				if (RoundIsFinished ()) {
		//					StartCoroutine (EndRound ());
		//				} else {
		//					StartCoroutine (PickedACoin (coinIndex));
		//				}
		//			} else if (coinIndex == right) {
		//				btns [coinIndex].enabled = false;
		//
		//				if (turn % 2 == 0) {
		//					p1Score += coinValue;
		//					highlight_score_p1.text = "+  " + coinValue.ToString("#0.00");
		//					StartCoroutine("wait_player1");
		//
		//				} else {
		//					p2Score += coinValue;
		//					highlight_score_p2.text = "+  " + coinValue.ToString("#0.00");
		//					StartCoroutine("wait_player2");
		//				}
		//			} else{
		//				if (turn % 2 == 0) {
		//					p1Score += coinValue;
		//					highlight_score_p1.text = "+  " + coinValue.ToString("#0.00");
		//					StartCoroutine("wait_player1");
		//				} else {
		//					p2Score += coinValue;
		//					highlight_score_p2.text = "+  " + coinValue.ToString("#0.00");
		//					StartCoroutine("wait_player2");
		//				}
		//
		//
		//				if (RoundIsFinished ()) {
		//					StartCoroutine (EndRound ());
		//				} else {
		//					StartCoroutine (PickedACoin (coinIndex));
		//				}
		//			} 


		//			btns [coinIndex].interactable = false;
		//
		//			btns [coinIndex].image.color = new Color(0,0,0,0);
		//			right--;
		//			turn++;
		player1Text.text = "Player 1: " + p1Score.ToString ("#0.00");
		player2Text.text = "Player 2: " + p2Score.ToString ("#0.00");
		StartCoroutine (PickedACoin (coinIndex));
//		if (RoundIsFinished ()) {
//			StartCoroutine (EndRound ());
//		} else {
//			StartCoroutine (PickedACoin (coinIndex));
//		}
		////
		//		} else {
		//
		//		}
		//		Player1Text.text = p1Score.ToString();
		//		StartCoroutine (PickedACoin ());


		//
		//		if (RoundIsFinished ()) {
		//			StartCoroutine (EndRound ());
		//		} else {
		//		}

		//		} else {
		//			//			ShuffleCoins ();
		//			HighlightCoins ();
		//		}

	}
	public int rotationDirection = -1; // -1 for clockwise
	// 1 for anti-clockwise
	public int rotationStep = 10; // should be less than 90
	// All the objects with which collision will be checked
	private Vector3 currentRotation, targetRotation;

	private void rotateObject(Image img)
	{

		currentRotation = img.transform.eulerAngles;
		targetRotation.y = (currentRotation.y + (90 * rotationDirection));
		StartCoroutine (objectRotationAnimation(img));
	}


	IEnumerator objectRotationAnimation(Image img)
	{

		// add rotation step to current rotation.
		currentRotation.y += (rotationStep * rotationDirection);
		img.transform.eulerAngles = currentRotation;
		yield return new WaitForSeconds (0);
		if (((int)currentRotation.y >
			(int)targetRotation.y && rotationDirection < 0) || // for clockwise
			((int)currentRotation.y < (int)targetRotation.y && rotationDirection > 0)) // for anti-clockwise
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
		yield return new WaitForSeconds(.1f);

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
		if (rounds != 0) {
			RoundsWinnerText.text = "Round " + showrounds + ": winner is " + round_winner;
			RoundsWinnerText.fontStyle = FontStyle.Bold;
		}

		showrounds += 1;
		yield return new WaitForSeconds(2f);
		Debug.Log ("Rounds: " + rounds);

		GameOver (rounds);
		yield return new WaitForSeconds(5f);

	}


	IEnumerator PickedACoin(int index){
		player1Text.text = "Player 1: " + p1Score.ToString ("#0.00");
		player2Text.text = "Player 2: " + p2Score.ToString ("#0.00");


		btns[index].image.CrossFadeAlpha (0.0f, .2f, false);
		rotateObject (btns [index].image);
		yield return new WaitForSeconds (.3f);
		btns [index].enabled = false;
		//		splashImage.CrossFadeAlpha (0.0f, 2.5f, false);

		btns [index].image.color = new Color(0,0,0,0);

		turn++;
		btns [left].enabled = false;
		btns [right].enabled = false;
		if (index == left) {
			left++;
		} else if (index == right) {
			right--;
		}
		yield return new WaitForSeconds (.1f);

		if (RoundIsFinished ()) {
			//EndRound ();
			StartCoroutine (EndRound ());

		} else {
			btns [left].enabled = true;
			btns [right].enabled = true;
			//			ShuffleCoins ();
			HighlightCoins ();
		}
	}

	IEnumerator wait_player1()
	{
		yield return new WaitForSeconds(.5f);
		highlight_score_p1.text = "";
	}


	IEnumerator wait_player2()
	{
		//		Text t = "Fade";
		//		t.CrossFadeAlpha
		//		float t;
		//		var c = highlight_score_p2.color;
		//		Debug.Log (c.a);
		//		while (highlight_score_p2.color.a > 0){
		////			t = highlight_score_p2.color.a;
		////			t -= 2;
		////			highlight_score_p2.GetComponent<GUIText>().color.a
		////			highlight_score_p2.color.a
		////			highlight_score_p2.material.color.a = t;
		//			c.a  -= .01f;
		//			Debug.Log (c.a);
		//			highlight_score_p2.color = c;
		////			yield;    
		//		}    
		////		Fade.use.Colors(highlight_score_p2.text,1.0, 0.0, 2.0, EaseType.Out);
		////		highlight_score_p2.text.
		//		Debug.Log (c.a);
		yield return new WaitForSeconds(.5f);
		highlight_score_p2.text = "";
		//		c.a = 1;
		//		Debug.Log (c.a);
		//		highlight_score_p2.color = c;
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
			endOption = true;
			gameOverText.fontStyle = FontStyle.Bold;
			//OnGUI ();
		} else {
			StartCoroutine(wait ());
			//			Debug.Log ("restart game");
			//			Debug.Log (rounds);

			RestartGame ();
		}
	}

	void OnGUI() {
		if (endOption) {
			GUI.contentColor = Color.yellow;
			if (GUI.Button (new Rect (Screen.width / 2 - 250, Screen.height / 2 + 70, 100, 40), "Retry?")) {
				Application.LoadLevel ("Scene1");
			}
			if (GUI.Button (new Rect (Screen.width / 2 - 50, Screen.height / 2 + 70, 100, 40), "Menu")) {
				Application.LoadLevel ("menu");
			}
			if (GUI.Button (new Rect (Screen.width / 2 + 150, Screen.height / 2 + 70, 100, 40), "Quit")) {
				Application.Quit ();
			}
		}
	}


	public void RestartGame() {

		left = 0;
		right = length;
		GetButtons ();
		AddListeners ();
		player1Text.text = "Player 1: 0.00";
		player2Text.text = "Player 2: 0.00";
		RoundsText.text = "Rounds: " + showrounds;
		RoundsText.fontStyle = FontStyle.Bold;
		RoundsWinnerText.text = " ";
		RoundsWinnerText.fontStyle = FontStyle.Bold;
		p1Score = 0;
		p2Score = 0;
	}


}