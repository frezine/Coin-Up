using UnityEngine;
using System.Collections;

public class AddButtons : MonoBehaviour {

	[SerializeField]
	private Transform puzzlefield;

	[SerializeField]
	private GameObject btn;

	[SerializeField]
	private GameObject options;

	void Awake(){
		int n = options.gameObject.GetComponent<Options> ().numCoins;	
		for (int i = 0; i < n; i++) {
			GameObject button = Instantiate (btn);
			button.name = "" + i;
			button.transform.SetParent (puzzlefield, false);	
		}
	}

}
