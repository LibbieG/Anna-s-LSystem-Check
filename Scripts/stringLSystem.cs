using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class stringLSystem : lsysLibrary {











	// Use this for initialization


	// Generate the next generation

		




	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Space)) {
			Debug.Log ("I'm in Update!");
			render ();
		}
	}
}