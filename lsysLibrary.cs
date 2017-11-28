using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lsysLibrary : MonoBehaviour {
	static string currentString = "F+F";
	static float length;
	static float angle;
	static float speed;
	static GameObject prefab;
	static GameObject activeObject; 

	static public lsysLibrary instance;

	lsysLibrary (string s, float l, float th, GameObject p, GameObject a){
		currentString = s;
		prefab = p;
		activeObject = a;
		instance = this;
	}

	static IEnumerator moveForward (GameObject obj) {
		float distanceTravelled = 0;

		if (distanceTravelled < length) {
			speed = stringLSystem.speed * Time.deltaTime * obj.GetComponent<Renderer> ().bounds.size.y;
			obj.transform.position = obj.transform.position + (obj.transform.forward * speed);
			distanceTravelled = distanceTravelled + speed;
			yield return new WaitForEndOfFrame ();
		} else {
			render ();
			yield break;
		}
	}


	static void fTurtle () {
		GameObject newObject = Instantiate (prefab, activeObject.transform.position, activeObject.transform.rotation);
		activeObject = newObject;
		instance.StartCoroutine (moveForward (activeObject));

	}

	static void gTurtle () {
		instance.StartCoroutine (moveForward (activeObject));
	}

	static IEnumerator plusTurtle (GameObject obj) {
		float angleTravelled = 0;
		angle = stringLSystem.angleLeft;
		if (angleTravelled < length) {
			speed = stringLSystem.speed * Time.deltaTime * obj.GetComponent<Renderer> ().bounds.size.y;
			obj.transform.position = obj.transform.position + (obj.transform.forward * speed);
			angleTravelled = angleTravelled + speed;
			yield return new WaitForEndOfFrame ();
		} else {
			render ();
			yield break;
		}

	}

	static void minusTurtle (){
		//Turn Right
	}

	static void uTurtle (){
		//Turn Up (For What?!)
	}

	static void dTurtle () {
		//Turn Down
	}

	static void saveState () {
		//Save Branch Point
	}

	static void restoreState () {
		//start new branch from position
	}



	public static void render () {
		for (int i = 0; i < currentString.Length; i++) {
			char c = currentString[i];
			Debug.Log (c);
			if (c == 'F') {
				fTurtle ();
			} else if (c == 'G') {
				gTurtle ();
			} else if (c == '+') {
				instance.StartCoroutine(plusTurtle (activeObject));
			} else if (c == '-') {
				minusTurtle ();
			} else if (c == 'U') {
				uTurtle ();
			} else if (c == 'D') {
				dTurtle ();
			} else if (c == '[') {
				saveState ();
			} else if (c == ']') {
				restoreState ();
			}
		
		}

	}



}
