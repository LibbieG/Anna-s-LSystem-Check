using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lsysLibrary : MonoBehaviour {

	[HideInInspector]
	public float angleDown;
	[HideInInspector]
	public float angleUp;
	[HideInInspector]
	public float angleRight;
	[HideInInspector]
	public float angleLeft;
	[HideInInspector]
	public float speed;
	[HideInInspector]
	public float prefabBounds;
	[HideInInspector]
	public float prefabScale;

	public GameObject Prefab;
	public GameObject rootObject;


	float length1;
	float length2;
	float length3;
	GameObject activeObject; 
	GameObject lastactiveObject;
	static bool iterate = true;
	int i = 0;



	public static int lsysLibI;
	[HideInInspector]
	public lsysLibrary lsysItt;
	[HideInInspector]
	public stringLSystem strItt;
	[HideInInspector]
	public string currentString;


	void Start () {
		globalMoveControl.lsysLibInst [lsysLibI] = this;
		strItt = globalMoveControl.strlsysLibInst [globalMoveControl.lsysLibI];
		lsysItt = globalMoveControl.lsysLibInst [globalMoveControl.lsysLibI];
		StartCoroutine (instWait ());


	}

	IEnumerator instWait () {
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		globalMoveControl.lsysLibI++;

	}


	IEnumerator renderWait () {
		
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		Debug.Log ("Fire!");
		render ();

	}

	IEnumerator moveForward1 (GameObject obj, float dist){
		float distanceTravelled = 0;
		length1 = prefabBounds * (obj.transform.localScale.z - prefabScale + 1f) * dist;
		float travel;


		while (iterate == true) {
			if (distanceTravelled < length1) {
				travel = strItt.speed * Time.deltaTime * obj.GetComponent<Renderer> ().bounds.size.y;
				obj.transform.position = obj.transform.position + (obj.transform.forward * travel);
				distanceTravelled = distanceTravelled + travel;
				yield return new WaitForEndOfFrame ();
			} else {
				yield break;
			}
		}
	}

	IEnumerator moveForward2 (GameObject obj, float dist){
		float distanceTravelled = 0;
		length2 = prefabBounds * (obj.transform.localScale.z - prefabScale + 1f) * dist;
		float travel;
		//Debug.Log (length2);

		while (iterate == true) {
			if (distanceTravelled < length2) {
				travel = strItt.speed * Time.deltaTime * obj.GetComponent<Renderer> ().bounds.size.y;
				obj.transform.position = obj.transform.position + (obj.transform.forward * travel);
				distanceTravelled = distanceTravelled + travel;
				yield return new WaitForEndOfFrame ();
			} else {
				yield break;
			}
		}
	}

	IEnumerator moveForward3 (GameObject obj, float dist){
		float distanceTravelled = 0;
		length3 = prefabBounds * (obj.transform.localScale.z - prefabScale + 1f) * dist;
		float travel;
		//Debug.Log (length3);

		while (iterate == true) {
			if (distanceTravelled < length3) {
				travel = strItt.speed * Time.deltaTime * obj.GetComponent<Renderer> ().bounds.size.y;
				obj.transform.position = obj.transform.position + (obj.transform.forward * travel);
				distanceTravelled = distanceTravelled + travel;
				yield return new WaitForEndOfFrame ();
			} else {
				Debug.Log ("move Complete");
				StartCoroutine(renderWait ());
				yield break;
			}
		}
	}



	void fTurtle () {

		GameObject newObject1 = Instantiate (Prefab, activeObject.transform.position, activeObject.transform.rotation);
		GameObject newObject2 = Instantiate (Prefab, activeObject.transform.position, activeObject.transform.rotation);
		GameObject newObject3 = Instantiate (Prefab, activeObject.transform.position, activeObject.transform.rotation);
		activeObject = newObject3;
		StartCoroutine (moveForward3 (activeObject, 3f));
		StartCoroutine (moveForward2 (newObject1, 1f));
		StartCoroutine (moveForward1 (newObject2, 2f));

	}

	void gTurtle () {
		StartCoroutine (moveForward3 (activeObject, 3f));
	}

	IEnumerator plusTurtle (GameObject obj) {
		float angleTravelled = 0; 
		float travel;
		while (iterate == true) {
			if (angleTravelled < strItt.angleLeft) {
				travel = strItt.speed * Time.deltaTime;
				obj.transform.Rotate (0f, strItt.angleLeft * travel, 0f);
				angleTravelled = angleTravelled + (strItt.angleLeft * travel);
				Debug.Log (angleTravelled + "Out of" + strItt.angleLeft);
				yield return new WaitForEndOfFrame ();
			} else {
				float adjustment = angleTravelled - strItt.angleLeft;
				obj.transform.Rotate (0f, (-1f * adjustment), 0f);
				Debug.Log (angleTravelled + "Out of" + strItt.angleLeft);
				yield return new WaitForEndOfFrame ();
				StartCoroutine(renderWait ());
				yield break;
			}
		}
	}

	IEnumerator minusTurtle (GameObject obj){
		float angleTravelled = 0;
		float travel;
		while (iterate == true) {
			if (angleTravelled < strItt.angleRight) {
				travel = strItt.speed * Time.deltaTime;
				obj.transform.Rotate (0f, strItt.angleRight * travel * -1f, 0f);
				angleTravelled = angleTravelled + (strItt.angleRight * travel);
				Debug.Log (angleTravelled + "Out of" + strItt.angleRight);
				yield return new WaitForEndOfFrame ();
			} else {
				float adjustment = angleTravelled - strItt.angleRight;
				obj.transform.Rotate (0f, adjustment, 0f);
				yield return new WaitForEndOfFrame ();
				StartCoroutine(renderWait ());
				yield break;
			}
		}
	}

	IEnumerator uTurtle (GameObject obj){
		float angleTravelled = 0;
		float travel;
		while (iterate == true) {
			if (angleTravelled < strItt.angleUp) {
				travel = strItt.speed * Time.deltaTime;
				obj.transform.Rotate (-1f * strItt.angleUp * travel, 0f, 0f);
				angleTravelled = angleTravelled + (strItt.angleUp * travel);
				Debug.Log (angleTravelled + "Out of" + strItt.angleUp);
				yield return new WaitForEndOfFrame ();
			} else {
				float adjustment = angleTravelled - strItt.angleUp;
				obj.transform.Rotate (0f, (adjustment), 0f);
				yield return new WaitForEndOfFrame ();
				StartCoroutine(renderWait ());
				yield break;
			}
		}
	}

	IEnumerator sTurtle (GameObject obj){
		float angleTravelled = 0;
		float travel;
		while (iterate == true) {
			if (angleTravelled < strItt.angleUp) {
				travel = strItt.speed * Time.deltaTime;
				obj.transform.Rotate (strItt.angleUp * travel, 0f, 0f);
				angleTravelled = angleTravelled + (strItt.angleUp * travel);
				yield return new WaitForEndOfFrame ();
			} else {
				float adjustment = angleTravelled - strItt.angleUp;
				obj.transform.Rotate (0f, (-1f * adjustment), 0f);
				yield return new WaitForEndOfFrame ();
				StartCoroutine(renderWait ());
				yield break;
			}
		}
	}

	IEnumerator dTurtle (GameObject obj) {
		float angleTravelled = 0;
		float travel;
		while (iterate == true) {
			if (angleTravelled < strItt.angleDown) {
				travel = strItt.speed * Time.deltaTime;
				obj.transform.Rotate (strItt.angleDown * travel, 0f, 0f);
				angleTravelled = angleTravelled + (strItt.angleDown * travel);
				yield return new WaitForEndOfFrame ();
			} else {
				float adjustment = angleTravelled - strItt.angleDown;
				obj.transform.Rotate (0f, (adjustment*-1f), 0f);
				yield return new WaitForEndOfFrame ();
				StartCoroutine(renderWait ());
				yield break;
			}
		}
	}

	void saveState () {
		//Save Branch Point
		lastactiveObject = activeObject;
		lsysItt.StartCoroutine(renderWait ());

	}

	void restoreState () {
		//start new branch from position
		activeObject = lastactiveObject;
		StartCoroutine(renderWait ());
	}

	void pTurtle () {
		StartCoroutine (plusTurtle (activeObject));
	}

	void mTurtle () {
		StartCoroutine (minusTurtle (activeObject));
	}

	void upTurtle () {
		StartCoroutine (uTurtle (activeObject));
	}

	void downTurtle () {
		StartCoroutine (dTurtle (activeObject));
	}

	void sideTurtle () {
		StartCoroutine (sTurtle (activeObject));
	}

	void doNothing (){
		StartCoroutine(renderWait ());
	}


	public IEnumerator goAgain() {
		Debug.Log ("Go Again");
		yield return new WaitForEndOfFrame ();
		strItt.nextRound ();

	}

	public void render () {
		Debug.Log ("Away!");
		currentString = strItt.currentString;
		if (i < currentString.Length) { 
			if (activeObject != null && Prefab != null) {
				char c = currentString [i];

				if (c == 'F') {
					Debug.Log (c);
					fTurtle ();
				} else if (c == 'G') {
					Debug.Log (c);
					gTurtle ();
				} else if (c == '+') {
					Debug.Log (c);
					pTurtle ();
				} else if (c == '-') {
					Debug.Log (c);
					mTurtle ();
				} else if (c == 'u') {
					Debug.Log (c);
					upTurtle ();
				} else if (c == 'D') {
					Debug.Log (c);
					downTurtle ();
				} else if (c == 'S') {
					Debug.Log (c);
					sideTurtle ();
				}
				else if (c == '[') {
					Debug.Log (c);
					saveState ();
				} else if (c == ']') {
					Debug.Log (c);
					restoreState ();
				} else {
					Debug.Log (c);
					doNothing ();
				}
				i++;

			} else {
				Debug.Log ("No activeObject");
				prefabBounds = Prefab.GetComponent<Renderer> ().bounds.size.z;
				prefabScale = Prefab.transform.localScale.z;
				activeObject = rootObject;
				StartCoroutine (goAgain ());
			}
		} else {
			Debug.Log ("All Done!");

		}
	}



}