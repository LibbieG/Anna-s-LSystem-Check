using UnityEngine;
using System.Collections;
using EasyWiFi.Core;

public class ActivateObj : MonoBehaviour {

	bool isPressed;
	public GameObject tree;


	// Use this for initialization
	void Start (){
		
	}
	void activateObj (ButtonControllerType button){
		
		isPressed = button.BUTTON_STATE_IS_PRESSED;
		if (isPressed) {
			
				Debug.Log ("pressed!!!");

				StartCoroutine (DelayActivate ());

		}
	}

	IEnumerator DelayActivate(){

		yield return new WaitForSeconds(1f);

		Instantiate (tree, new Vector3(0f,0f,0f),Quaternion.identity);

		yield return new WaitForSeconds(1f);

		//		yield return new WaitForSeconds (1.5f);

//		foreach (Transform child in this.transform)
//		{
////				if (isPressed)
////			{
//				child.gameObject.SetActive(true);
////			}
//		}
	}
}

