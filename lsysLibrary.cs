using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lsysLibrary : MonoBehaviour {
	static string currentString = "F+F";
	static float length;
	static float speed;
	static GameObject prefab;
	static GameObject activeObject = stringLSystem.lsystemRoot; 
	static GameObject lastactiveObject = stringLSystem.lsystemRoot;
	static bool iterate = true;
	static int i = 0;

	public static lsysLibrary instance;

	lsysLibrary (){
		instance = this;
	}

	void Awake () {
		instance = this;
	}

	static IEnumerator renderWait () {
		Debug.Log ("Hi There");
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		render ();

	}

	static IEnumerator moveForward (GameObject obj){
		float distanceTravelled = 0;
		length = obj.GetComponent<Renderer> ().bounds.size.y;

		while (iterate == true) {
			if (distanceTravelled < length) {
				speed = stringLSystem.speed * Time.deltaTime * obj.GetComponent<Renderer> ().bounds.size.y;
				obj.transform.position = obj.transform.position + (obj.transform.forward * speed);
				distanceTravelled = distanceTravelled + speed;
				yield return new WaitForEndOfFrame ();
			} else {
				instance.StartCoroutine(renderWait ());
				yield break;
			}
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
		while (iterate == true) {
			if (angleTravelled < stringLSystem.angleLeft) {
				speed = stringLSystem.speed * Time.deltaTime;
				obj.transform.Rotate (0f, stringLSystem.angleLeft * speed, 0f);
				angleTravelled = angleTravelled + (stringLSystem.angleLeft * speed);
				yield return new WaitForEndOfFrame ();
			} else {
				instance.StartCoroutine(renderWait ());
				yield break;
			}
		}
	}

	static IEnumerator minusTurtle (GameObject obj){
		float angleTravelled = 0;
		while (iterate == true) {
			if (angleTravelled < stringLSystem.angleRight) {
				speed = stringLSystem.speed * Time.deltaTime;
				obj.transform.Rotate (0f, stringLSystem.angleRight * speed * -1f, 0f);
				angleTravelled = angleTravelled + (stringLSystem.angleRight * speed);
				Debug.Log (angleTravelled + "Out of" + stringLSystem.angleRight);
				yield return new WaitForEndOfFrame ();
			} else {
				instance.StartCoroutine(renderWait ());
				yield break;
			}
		}
	}

	static IEnumerator uTurtle (GameObject obj){
	float angleTravelled = 0;
	while (iterate == true) {
			if (angleTravelled < stringLSystem.angleUp) {
			speed = stringLSystem.speed * Time.deltaTime;
				obj.transform.Rotate (stringLSystem.angleUp * speed, 0f, 0f);
				angleTravelled = angleTravelled + (stringLSystem.angleUp * speed);
			yield return new WaitForEndOfFrame ();
		} else {
				instance.StartCoroutine(renderWait ());
				yield break;
		}
	}
}

	static IEnumerator dTurtle (GameObject obj) {
		float angleTravelled = 0;
		while (iterate == true) {
			if (angleTravelled < stringLSystem.angleDown) {
				speed = stringLSystem.speed * Time.deltaTime;
				obj.transform.Rotate (-1f * stringLSystem.angleDown * speed, 0f, 0f);
				angleTravelled = angleTravelled + (stringLSystem.angleDown * speed);
				yield return new WaitForEndOfFrame ();
			} else {
				instance.StartCoroutine(renderWait ());
				yield break;
			}
		}
	}

	static void saveState () {
		//Save Branch Point
		Debug.Log ("I'm Here!");
		lastactiveObject = activeObject;
		instance.StartCoroutine(renderWait ());

	}

	static void restoreState () {
		//start new branch from position
		activeObject = lastactiveObject;
		instance.StartCoroutine(renderWait ());
	}

	static void pTurtle () {
		instance.StartCoroutine (plusTurtle (activeObject));
	}

	static void mTurtle () {
		instance.StartCoroutine (minusTurtle (activeObject));
	}

	static void upTurtle () {
		instance.StartCoroutine (uTurtle (activeObject));
	}

	static void downTurtle () {
		instance.StartCoroutine (dTurtle (activeObject));
	}

	static void doNothing (){
		instance.StartCoroutine(renderWait ());
	}

	public static void render () {
		currentString = stringLSystem.currentString;
		if (i < currentString.Length) { 
			if (activeObject != null && prefab != null) {
				char c = currentString [i];
				Debug.Log (c);
				if (c == 'F') {
					fTurtle ();
				} else if (c == 'G') {
					gTurtle ();
				} else if (c == '+') {
					pTurtle ();
				} else if (c == '-') {
					mTurtle ();
				} else if (c == 'U') {
					upTurtle ();
				} else if (c == 'D') {
					downTurtle ();
				} else if (c == '[') {
					saveState ();
				} else if (c == ']') {
					restoreState ();
				} else {
					doNothing ();
				}
				i++;
		
			} else {
				Debug.Log ("No activeObject");
				activeObject = stringLSystem.lsystemRoot;
				prefab = stringLSystem.lsystemPrefab;
			}
		} else {
			Debug.Log ("All Done!");
		}
	}



}
