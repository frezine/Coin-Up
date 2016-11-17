using UnityEngine;
using System.Collections;

public class Options : MonoBehaviour {

	public int numPlayers = 2;
	public int numRounds = 1;
	public int numCoins = 12;

	public static Options option;

//	[SerializeField]
//	public GameObject player;
//	[SerializeField]
//	public GameObject rounds;
//	[SerializeField]
//	public GameObject coins;


	void Awake(){
		
		if (option == null) {
			DontDestroyOnLoad (gameObject);
			option = this;
		} else if (option != this) {
			Destroy (gameObject);

		}

		//		DontDestroyOnLoad (transform.gameObject);
	}

	public void getPlayers(int val){

		if (val == 0) {
			numPlayers = 2;
		}else if(val == 1)
		{
			numPlayers = 3;	
		}else if(val == 2)
		{
			numPlayers = 4;
		}

		Debug.Log (numPlayers);
		//		return numPlayers;
	} 

	public void getRounds(int val){
		Debug.Log (val);
		if (val == 0) {
			numRounds = 1;
		}else if(val == 1)
		{
			numRounds = 3;	
		}else if(val == 2)
		{
			numRounds = 5;
		}
		Debug.Log (numRounds);
		//		return numRounds;
	} 

	public void getCoins(int val){
		Debug.Log (val);
		if (val == 0) {

			numCoins = 12;

		}else if(val == 1)
		{
			numCoins = 16;	

		}else if(val == 2)
		{
			numCoins = 20;
		}
		Debug.Log (numCoins);
		//		return numCoins;

	}

	public void setOptions(bool clicked){
		GameObject pl = GameObject.FindGameObjectWithTag ("players");
//		pl.GetComponent<>
	}

}
