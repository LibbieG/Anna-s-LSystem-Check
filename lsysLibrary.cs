using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lsysLibrary : MonoBehaviour {
	static string currentString;
	static float length1;
	static float length2;
	static float length3;
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

	static IEnumerator moveForward1 (GameObject obj, float dist){
		float distanceTravelled = 0;
		length1 = stringLSystem.PrefabBounds * obj.transform.localScale.z * dist;

		Debug.Log (length1);

		while (iterate == true) {
			if (distanceTravelled < length1) {
				speed = stringLSystem.speed * Time.deltaTime * obj.GetComponent<Renderer> ().bounds.size.y;
				obj.transform.position = obj.transform.position + (obj.transform.forward * speed);
				distanceTravelled = distanceTravelled + speed;
				yield return new WaitForEndOfFrame ();
			} else {
				yield break;
			}
		}
	}

	static IEnumerator moveForward2 (GameObject obj, float dist){
		float distanceTravelled = 0;
		length2 = stringLSystem.PrefabBounds * obj.transform.localScale.z * dist;

		Debug.Log (length2);

		while (iterate == true) {
			if (distanceTravelled < length2) {
				speed = stringLSystem.speed * Time.deltaTime * obj.GetComponent<Renderer> ().bounds.size.y;
				obj.transform.position = obj.transform.position + (obj.transform.forward * speed);
				distanceTravelled = distanceTravelled + speed;
				yield return new WaitForEndOfFrame ();
			} else {
				yield break;
			}
		}
	}

	static IEnumerator moveForward3 (GameObject obj, float dist){
		float distanceTravelled = 0;
		length3 = stringLSystem.PrefabBounds * obj.transform.localScale.z * dist;

		Debug.Log (length3);

		while (iterate == true) {
			if (distanceTravelled < length3) {
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
		
		GameObject newObject1 = Instantiate (prefab, activeObject.transform.position, activeObject.transform.rotation);
		GameObject newObject2 = Instantiate (prefab, activeObject.transform.position, activeObject.transform.rotation);
		GameObject newObject3 = Instantiate (prefab, activeObject.transform.position, activeObject.transform.rotation);
		activeObject = newObject3;
		instance.StartCoroutine (moveForward3 (activeObject, 3f));
		instance.StartCoroutine (moveForward2 (newObject1, 1f));
		instance.StartCoroutine (moveForward1 (newObject2, 2f));

	}

	static void gTurtle () {
		instance.StartCoroutine (moveForward3 (activeObject, 3f));
	}

	static IEnumerator plusTurtle (GameObject obj) {
		float angleTravelled = 0; 
		while (iterate == true) {
			if (angleTravelled < stringLSystem.angleLeft) {
				speed = stringLSystem.speed * Time.deltaTime;
				obj.transform.Rotate (0f, stringLSystem.angleLeft * speed, 0f);
				angleTravelled = angleTravelled + (stringLSystem.angleLeft * speed);
				Debug.Log (angleTravelled + "Out of" + stringLSystem.angleLeft);
				yield return new WaitForEndOfFrame ();
			} else {
				float adjustment = angleTravelled - stringLSystem.angleLeft;
				obj.transform.Rotate (0f, (-1f * adjustment), 0f);
				yield return new WaitForEndOfFrame ();
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
				float adjustment = angleTravelled - stringLSystem.angleRight;
				obj.transform.Rotate (0f, adjustment, 0f);
				yield return new WaitForEndOfFrame ();
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
				float adjustment = angleTravelled - stringLSystem.angleUp;
				obj.transform.Rotate (0f, (-1f * adjustment), 0f);
				yield return new WaitForEndOfFrame ();
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
				float adjustment = angleTravelled - stringLSystem.angleDown;
				obj.transform.Rotate (0f, (adjustment), 0f);
				yield return new WaitForEndOfFrame ();
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
				} else if (c == 'u') {
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
