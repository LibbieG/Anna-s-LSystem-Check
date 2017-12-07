using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lsysLibrary : MonoBehaviour {

	// Set Prefab
	public GameObject[] prefabs;
	[HideInInspector]
	public float prefabBounds;
	[HideInInspector]
	public float prefabScale;


	//Variable Speed, Angles, & Size
	public float setSpeed;
	public bool randomizeSpeed;
	public float randomSpeedMin;
	public float randomSpeedMax;

	[HideInInspector]
	public bool randomAngleRange;
	public float setAngleLeft = 90;
	public float setAngleRight;


	public float setAngleUp = 45;
	public float setAngleDown;

	[HideInInspector]
	public bool Randomize_Size;


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
	public float size;

	//Rule Settings
	[HideInInspector]
	public Rule[] ruleset;
	public string Custom_CurrentString;
	[HideInInspector]
	public string currentString;



	//Game Control
	[HideInInspector]
	public int currentGeneration;


	float length1;
	float length2;
	float length3;
	GameObject activeObject; 
	GameObject lastactiveObject;

	public static bool globalIterate = true;
	[HideInInspector]
	public bool localIterate = true;
	int i = 0;

	//Version Control
	public static int lsysLibI;
	[HideInInspector]
	public lsysLibrary lsysItt;




	public virtual void Start () {

		activeObject = gameObject;
		globalMoveControl.lsysLibInst [lsysLibrary.lsysLibI] = this;
		lsysItt = globalMoveControl.lsysLibInst[lsysLibrary.lsysLibI];
		globalMoveControl.lsysLibI++;



	}



/*	IEnumerator instWait () {
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		globalMoveControl.lsysLibI++;

	}
	*/


	public void generate(string str) {
		// An empty StringBuffer that we will fill
		string nextgen = "";

		//StringBuffer nextgen = new StringBuffer();
		// For every character in the sentence
		for (int i = 0; i < str.Length; i++) {
			// What is the character
			char curr = str[i];
			// We will replace it with itself unless it matches one of our rules
			string replace = "" + curr;
			// Check every rule
			for (int j = 0; j < ruleset.Length; j++) {
				char a = ruleset[j].getA();
				// if we match the Rule, get the replacement String out of the Rule
				if (a == curr) {
					replace = ruleset[j].getB();
					break; 
				}
			}
			// Append replacement String
			nextgen = nextgen + replace;
		}
		// Replace sentence
		currentString = nextgen;
		// Increment generation
		currentGeneration++;
		render ();
	}
		



	IEnumerator renderWait () {
		
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		Debug.Log ("Fire!");
		render ();

	}

	public void render () {
		Debug.Log ("Away!");
		GameObject prefab;
		if (prefabs.Length == 1) {
			prefab = prefabs [0];
		} else if (prefabs.Length > 1) {
			prefab = prefabs [Random.Range (0, (prefabs.Length - 1))];
		} else
			prefab = null;

		if (i < currentString.Length) { 
			if (activeObject != null && prefab != null) {
				prefabBounds = prefab.GetComponent<Renderer> ().bounds.size.z;
				prefabScale = prefab.transform.localScale.z;
				char c = currentString [i];

				if (c == 'F') {
					Debug.Log (c);
					fTurtle (prefab);
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

				activeObject = gameObject;
				StartCoroutine (goAgain ());
			}
		} else {
			Debug.Log ("All Done!");
			StartCoroutine (goAgain ());


		}
	}



	IEnumerator moveForward1 (GameObject obj, float dist){
		float distanceTravelled = 0;
		length1 = prefabBounds * (obj.transform.localScale.z - prefabScale + 1f) * dist;
		float travel;


		while (globalIterate == true) {
			if (distanceTravelled < length1) {
				travel = speed * Time.deltaTime * obj.GetComponent<Renderer> ().bounds.size.y;
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

		while (globalIterate == true) {
			if (distanceTravelled < length2) {
				travel = speed * Time.deltaTime * obj.GetComponent<Renderer> ().bounds.size.y;
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

		while (globalIterate == true) {
			if (distanceTravelled < length3) {
				travel = speed * Time.deltaTime * obj.GetComponent<Renderer> ().bounds.size.y;
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



	void fTurtle (GameObject prefab) {

		GameObject newObject1 = Instantiate (prefab, activeObject.transform.position, activeObject.transform.rotation, gameObject.transform);
		GameObject newObject2 = Instantiate (prefab, activeObject.transform.position, activeObject.transform.rotation, gameObject.transform);
		GameObject newObject3 = Instantiate (prefab, activeObject.transform.position, activeObject.transform.rotation, gameObject.transform);
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
		while (globalIterate == true) {
			if (angleTravelled < angleLeft) {
				travel = speed * Time.deltaTime;
				obj.transform.Rotate (0f, angleLeft * travel, 0f);
				angleTravelled = angleTravelled + (angleLeft * travel);
				Debug.Log (angleTravelled + "Out of" + angleLeft);
				yield return new WaitForEndOfFrame ();
			} else {
				float adjustment = angleTravelled - angleLeft;
				obj.transform.Rotate (0f, (-1f * adjustment), 0f);
				Debug.Log (angleTravelled + "Out of" + angleLeft);
				yield return new WaitForEndOfFrame ();
				StartCoroutine(renderWait ());
				yield break;
			}
		}
	}

	IEnumerator minusTurtle (GameObject obj){
		float angleTravelled = 0;
		float travel;
		while (globalIterate == true) {
			if (angleTravelled < angleRight) {
				travel = speed * Time.deltaTime;
				obj.transform.Rotate (0f, angleRight * travel * -1f, 0f);
				angleTravelled = angleTravelled + (angleRight * travel);
				Debug.Log (angleTravelled + "Out of" + angleRight);
				yield return new WaitForEndOfFrame ();
			} else {
				float adjustment = angleTravelled - angleRight;
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
		while (globalIterate == true) {
			if (angleTravelled < angleUp) {
				travel = speed * Time.deltaTime;
				obj.transform.Rotate (-1f * angleUp * travel, 0f, 0f);
				angleTravelled = angleTravelled + (angleUp * travel);
				Debug.Log (angleTravelled + "Out of" + angleUp);
				yield return new WaitForEndOfFrame ();
			} else {
				float adjustment = angleTravelled - angleUp;
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
		while (globalIterate == true) {
			if (angleTravelled < angleUp) {
				travel = speed * Time.deltaTime;
				obj.transform.Rotate (angleUp * travel, 0f, 0f);
				angleTravelled = angleTravelled + (angleUp * travel);
				yield return new WaitForEndOfFrame ();
			} else {
				float adjustment = angleTravelled - angleUp;
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
		while (globalIterate == true) {
			if (angleTravelled < angleDown) {
				travel = speed * Time.deltaTime;
				obj.transform.Rotate (angleDown * travel, 0f, 0f);
				angleTravelled = angleTravelled + (angleDown * travel);
				yield return new WaitForEndOfFrame ();
			} else {
				float adjustment = angleTravelled - angleDown;
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
		StartCoroutine(renderWait ());

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
		nextRound ();

	}

	public virtual void nextRound () {
		if (randomizeSpeed) {
			speed = Random.Range (randomSpeedMin, randomSpeedMax);
		} else {
			speed = setSpeed;
			Debug.Log ("speed =" + speed);
		}
		
	}





}