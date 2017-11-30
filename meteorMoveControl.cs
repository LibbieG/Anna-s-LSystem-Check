using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meteorMoveControl : MonoBehaviour {
	void Start (){
		gameObject.GetComponent<stringLSystem>().enabled = false;
		gameObject.GetComponent<lsysLibrary>().enabled = false;
	}


	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.A)){
			gameObject.GetComponent<stringLSystem>().enabled = true;
			gameObject.GetComponent<lsysLibrary>().enabled = true;


		} 
		if (Input.GetKeyUp (KeyCode.D)) {
			gameObject.GetComponent<stringLSystem>().enabled = false;
			gameObject.GetComponent<lsysLibrary>().enabled = false;
		}

	}
}
