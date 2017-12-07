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

	[HideInInspector]
	public bool Randomize_Size;


	[HideInInspector]
	public float speed;


	//Rule Settings
	[HideInInspector]
	public Rule[] ruleset;
	public string [] customCurrentString;



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
		globalMoveControl.lsysLibInst [lsysLibI] = this;
		lsysItt = globalMoveControl.lsysLibInst[lsysLibrary.lsysLibI];
		globalMoveControl.lsysLibI++;



	}



/*	IEnumerator instWait () {
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		globalMoveControl.lsysLibI++;

	}
	*/


	public void generate(string str, int l, int r, int u, int d, float size, GameObject active) {
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
		string current = nextgen;
		// Increment generation
		currentGeneration++;
		render (current, active, l, r, u, d, size);
	}
		


	IEnumerator renderWait (string str, GameObject active, int l, int r, int u, int d, float size) {
		
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		Debug.Log ("Fire!");
		render (str, active, l, r, u, d, size);

	}

	public void render (string str, GameObject active, int l, int r, int u, int d, float size) {
		Debug.Log ("Away!");
		GameObject prefab;
		if (prefabs.Length == 1) {
			prefab = prefabs [0];
		} else if (prefabs.Length > 1) {
			prefab = prefabs [Random.Range (0, (prefabs.Length - 1))];
		} else
			prefab = null;

		if (i < str.Length) { 
			if (activeObject != null && prefab != null) {
				prefabBounds = prefab.GetComponent<Renderer> ().bounds.size.z;
				prefabScale = prefab.transform.localScale.z;
				char c = str [i];

				if (c == 'f') {
					Debug.Log (c);
					GameObject obj = Instantiate (prefab, active.transform.position, active.transform.rotation, gameObject.transform);
					obj.transform.localScale = obj.transform.localScale * size;

					StartCoroutine(fTurtle(str, obj, l, r, u, d, size));
 
				} else if (c == 'b') {
					Debug.Log (c);
					StartCoroutine (goAgain ("F", active));
				} else if (c == '[') {
					Debug.Log (c);
					saveState (str, active, l, r, u, d, size);
				} else if (c == ']') {
					Debug.Log (c);
					restoreState (str, active, l, r, u, d, size);
				} else if (c == 'n') {
					GameObject obj = Instantiate (prefab, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform);
					StartCoroutine (fTurtle (str, obj, l, r, u, d, size));
				}else {
					Debug.Log (c);
					doNothing (str, active, l, r, u, d, size);
				}
				i++;

			} else {
				Debug.Log ("No activeObject");

				activeObject = gameObject;
				StartCoroutine (goAgain (str,active));
			}
		} else {
			Debug.Log ("All Done!");
			StartCoroutine (goAgain (str, active));


		}
	}

	IEnumerator fTurtle (string str,GameObject obj, int l, int r, int u, int d, float size){
		float angleTravelled = 0;
		float angleTravelledu = 0;
		float travel;
		float travel2;
		float la = (float)l;
		float uu = (float)u;

							while (globalIterate == true) {
								if (angleTravelled < la) {
									travel = speed * Time.deltaTime;
									obj.transform.Rotate (uu * travel, la * travel * -1f, 0f);
									angleTravelled = angleTravelled + (la * travel);
									travel2 = speed * Time.deltaTime * obj.GetComponent<Renderer> ().bounds.size.y;
									obj.transform.position = obj.transform.position + (obj.transform.forward * travel);
									Debug.Log (angleTravelled + "Out of" + la);
									yield return new WaitForEndOfFrame ();
								} else  {
									float adjustmentLeft = angleTravelled - la;
									yield return new WaitForEndOfFrame ();
				StartCoroutine(renderWait (str, obj, l, r, u, d, size));
									yield break;
								}
							}
						}








	/*IEnumerator uTurtle (GameObject obj){
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
	}*/

	void saveState (string str, GameObject obj, int l, int r, int u, int d, float size) {
		//Save Branch Point
		lastactiveObject = activeObject;
		StartCoroutine(renderWait (str,obj,l,r,u,d,size));

	}

	void restoreState (string str, GameObject obj, int l, int r, int u, int d, float size) {
		//start new branch from position
		activeObject = lastactiveObject;
		StartCoroutine(renderWait (str,obj,l,r,u,d,size));
	}
		

	void doNothing (string str, GameObject obj, int l, int r, int u, int d, float size){
		StartCoroutine(renderWait (str,obj,l,r,u,d,size));
	}


	public IEnumerator goAgain(string curr, GameObject act) {
		Debug.Log ("Go Again");
		yield return new WaitForEndOfFrame ();
		nextRound (curr, act);

	}

	public virtual void nextRound (string curr, GameObject act) {
		if (randomizeSpeed) {
			speed = Random.Range (randomSpeedMin, randomSpeedMax);
		} else {
			speed = setSpeed;
			Debug.Log ("speed =" + speed);
		}
		
	}







}