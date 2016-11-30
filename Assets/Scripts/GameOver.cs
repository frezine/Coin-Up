using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

	[SerializeField]
	private GUIText GameOverText;

	[SerializeField]
	private GameController gameController;

	public string winner;

	void GetWinner(){
		winner = gameController.gameObject.GetComponent<GameController> ().winner;
		GameOverText.text = "Game over! " + winner; 
	}

	public void BackToMenu() {
		Application.LoadLevel ("Scene1");
	}

	public void Exit(){
		Application.Quit ();
	}


}
