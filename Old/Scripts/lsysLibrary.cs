using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lsysLibrary : MonoBehaviour {

	// Set Prefab
	public GameObject[] prefabs;
	public GameObject[] porches;
	public GameObject[] towers;
	public GameObject[] bridges;
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
	//GameObject activeObject; 
	GameObject lastactiveObject;

	public static bool globalIterate = true;
	[HideInInspector]
	public bool localIterate = true;

	//Version Control
	public static int lsysLibI;
	[HideInInspector]
	public lsysLibrary lsysItt;




	public virtual void Start () {

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


	public void generate(string str, int l, int r, int u, int d, float size, GameObject active, int x) {
		// An empty StringBuffer that we will fill
		string nextgen = "";
		Debug.Log ("generate string: " + str);

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
		Debug.Log ("current" + current);
		// Increment generation
		currentGeneration++;
		render (current, active, l, r, u, d, size, x);
	}
		


	IEnumerator renderWait (string str, GameObject active, int l, int r, int u, int d, float size, int i) {
		
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		Debug.Log ("Fire!");
		Debug.Log ("renderWait i:  " + i);
		render (str, active, l, r, u, d, size, i);

	}

	public void render (string str, GameObject active, int l, int r, int u, int d, float size , int i) {
		Debug.Log ("Away!");
		GameObject prefab;

		if (prefabs.Length == 1) {
			prefab = prefabs [0];
		} else if (prefabs.Length > 1) {
			prefab = prefabs [Random.Range (0, (prefabs.Length - 1))];
		} else {
			prefab = null;
			Debug.Log ("prefab null");
		}

		Debug.Log ("string: " + str);
		Debug.Log ("i:  " +i);
		if (i < str.Length) { 
			if (prefab != null) {
				prefabBounds = prefab.GetComponent<BoxCollider> ().bounds.size.z;
				prefabScale = prefab.transform.localScale.z;
				char c = str [i];
		
				if (c == 'f') {
					Debug.Log (c);
					GameObject obj = Instantiate (active, active.transform.position, new Quaternion (0, 0, 0, 0), gameObject.transform);
					obj.transform.localScale = obj.transform.localScale * size;
					obj.transform.localEulerAngles = new Vector3 ((-1 * u), l, 0f);
					float bound = obj.transform.localScale.x * 2;

					i++;

					StartCoroutine (fTurtle (str, obj, l, r, u, d, size, bound, i));
 
				} else if (c == 'b') {
					Debug.Log (c);
					i++;
					StartCoroutine (goAgain ("F", active));
				} else if (c == '[') {
					Debug.Log (c);
					i++;
					saveState (str, active, l, r, u, d, size, i);
				} else if (c == ']') {
					Debug.Log (c);
					i++;
					restoreState (str, active, l, r, u, d, size, i);
				} else if (c == 'n') {
					Debug.Log (c);
					GameObject obj = Instantiate (prefab, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform);
					obj.transform.localEulerAngles = new Vector3 ((-1 * u), l, 0f);
					i++;
					StartCoroutine (nTurtle (str, obj, l, r, u, d, size, i));
				} else if (c == 'c') {
					Debug.Log (c);
					active = gameObject;
					i++;
					restoreState (str, active, l, r, u, d, size, i);
				} else if (c == 'p') {
					Debug.Log (c);
					GameObject prev = active;
					GameObject obj = Instantiate (porches[Random.Range (0, porches.Length)], active.transform.position, active.transform.rotation,gameObject.transform);
					StartCoroutine (pturtle(str, obj, l, r, u, d, size, i, active));
					//instantiate porch
					//start IEnumerator pTurtle
					i++;
					restoreState (str, active, l, r, u, d, size, i);
				} else if (c == 'r') {
					Debug.Log (c);
					i++;
					restoreState (str, active, l, r, u, d, size, i);
				} else if (c == 't') {
					Debug.Log (c);
					i++;
					restoreState (str, active, l, r, u, d, size, i);
				} 
				else {
					Debug.Log (c);
					i++;
					doNothing (str, active, l, r, u, d, size, i);
				}


			} else {
				Debug.Log ("No activeObject");

				StartCoroutine (goAgain (str,active));
			}
		} else {
			Debug.Log ("All Done!");
			StartCoroutine (goAgain (str, active));


		}
	}

	IEnumerator pturtle(string str, GameObject obj, int l, int r, int u, int d, float size, int i, GameObject active){
		yield return new WaitForEndOfFrame();
		float travelled;
		float distance = 0;
		Vector3 endpointMove = new Vector3(0,0,0);
		float endpointRotate = 0;
		float endpointScale = 0;
		Vector3 travelMove;
		float travelRotate = 0;
		float travelScale;
		while (globalIterate == true) {
			if (distance < travelRotate) {
				//travel = speed * Time.deltaTime;
				//obj.transform.Rotate (uu * travel, la * travel * -1f, 0f);
				//angleTravelled = angleTravelled + (la * travel);
				travelMove = speed * Time.deltaTime * endpointMove;
				travelRotate = speed * Time.deltaTime * endpointRotate;
				travelScale = speed * Time.deltaTime * endpointScale;
					
				//obj.transform.position = obj.transform.position + (obj.transform.forward * travel2);
				distance = distance + travelRotate;
				Debug.Log (distance + "Out of" + endpointRotate);
				yield return new WaitForEndOfFrame ();
			} else  {

				/*float adjustment = distance - endpoint;
				obj.transform.position = obj.transform.position + (obj.transform.forward * adjustment * -1);
				if (bound < .2f) {
					yield return new WaitForEndOfFrame ();
					GameObject prefab; 
					if (prefabs.Length == 1) {
						prefab = prefabs [0];
					} else{ 
						prefab = prefabs [Random.Range (0, (prefabs.Length))];
					}*/


					yield return new WaitForEndOfFrame ();
					StartCoroutine (renderWait (str, obj, l, r, u, d, size, i));
					yield break;
				}

			}
		}





	IEnumerator fTurtle (string str,GameObject obj, int l, int r, int u, int d, float size, float bound, int i){
		//float angleTravelled = 0;
		//float travel;
		yield return new WaitForEndOfFrame();
		float travel2;
		//float la = (float)l;
		//float uu = (float)u;
		float distance  = 0;
		float endpoint = bound;

		while (globalIterate == true) {
			if (distance < endpoint) {
									//travel = speed * Time.deltaTime;
									//obj.transform.Rotate (uu * travel, la * travel * -1f, 0f);
									//angleTravelled = angleTravelled + (la * travel);
				travel2 = speed * Time.deltaTime * endpoint;
				obj.transform.localPosition = obj.transform.localPosition + (obj.transform.forward * travel2);
				distance = distance + travel2;
				//Debug.Log (distance + "Out of" + endpoint);

									yield return new WaitForEndOfFrame ();
								} else  {
				
				float adjustment = distance - endpoint;
				obj.transform.localPosition = obj.transform.localPosition + (obj.transform.forward * adjustment * -1);

				if (bound < .01) {
					yield return new WaitForEndOfFrame ();
					GameObject prefab; 
					if (prefabs.Length == 1) {
							prefab = prefabs [0];
					} else{ 
						prefab = prefabs [Random.Range (0, (prefabs.Length))];
					}
					
					StartCoroutine (goAgain ("m", prefab));
					yield break;

				} else {
					yield return new WaitForEndOfFrame ();
					Debug.Log ("Fturtle I:  " + i);
					StartCoroutine (renderWait (str, obj, l, r, u, d, size, i));
					yield break;
				}
								
				}
							}
						}


	IEnumerator nTurtle (string str,GameObject obj, int l, int r, int u, int d, float size, int i){
		//float angleTravelled = 0;
		//float travel;
		yield return new WaitForEndOfFrame();
		float travel2;
		//float la = (float)l;
		//float uu = (float)u;
		float distance  = 0;
		float endpoint = .22f;

		while (globalIterate == true) {
			if (distance < endpoint) {
				//travel = speed * Time.deltaTime;
				//obj.transform.Rotate (uu * travel, la * travel * -1f, 0f);
				//angleTravelled = angleTravelled + (la * travel);
				travel2 = speed * Time.deltaTime * endpoint;
				obj.transform.localPosition = obj.transform.localPosition + (obj.transform.forward * travel2);
				distance = distance + travel2;
				//Debug.Log (distance + "Out of" + endpoint);

				yield return new WaitForEndOfFrame ();
			} else  {
				float adjustment = distance - endpoint;
				obj.transform.localPosition = obj.transform.localPosition + (obj.transform.forward * adjustment * -1);

				yield return new WaitForEndOfFrame ();
				StartCoroutine(renderWait (str, obj, l, r, u, d, size, i));
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
	} */

	void saveState (string str, GameObject obj, int l, int r, int u, int d, float size, int i) {
		//Save Branch Point
		lastactiveObject = obj;
		StartCoroutine(renderWait (str,obj,l,r,u,d,size, i));

	}

	void restoreState (string str, GameObject obj, int l, int r, int u, int d, float size, int i) {
		//start new branch from position
		obj = lastactiveObject;
		StartCoroutine(renderWait (str,obj,l,r,u,d,size, i));
	}
		

	void doNothing (string str, GameObject obj, int l, int r, int u, int d, float size, int i){
		StartCoroutine(renderWait (str,obj,l,r,u,d,size, i));
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