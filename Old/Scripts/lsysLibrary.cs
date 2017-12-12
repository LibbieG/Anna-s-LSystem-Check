using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lsysLibrary : MonoBehaviour {

	// Set Prefab

	public GameObject[] prefabs;
	public Material meteorMaterial;



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
	[HideInInspector]
	public candleReturn [] candleSet;
	[HideInInspector]
	public chairReturn [] chairSet;


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
 
				} else if (c == 'a') {
					Debug.Log (c);
					GameObject obj = Instantiate (prefab, active.transform.position, active.transform.rotation, gameObject.transform);
					l = (Random.Range (0,3) * 90) - 90;
					u = (Random.Range (0, 3) * 90) - 90;
					i++;
					StartCoroutine(aTurtle (str, obj, l, r, u, d, size, i));


				}else if (c == 'b') {
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
					i++;
					doNothing (str, active, l, r, u, d, size, i);


				} else if (c == 'p') {
					string b = active.name;
					Debug.Log (c);
					for (int j = 0; j < candleSet.Length; j++) {
						string a = candleSet[j].getType();
						string root = candleSet [j].getRoot ();
						Vector3 loc;

						// if we match the Rule, get the replacement String out of the Rule
						if (a == "porch") {
								if (root == b){
								bool go = false;
								float sc = 1;
								loc = new Vector3 (0,0,0);
								while (go == false){
									prefab = candleSet[j].getPrefab();
								l = candleSet[j].getLeft();
								sc = candleSet[j].getScale();
									float mult = active.transform.localScale.y;
									if (active.name == "candle") {
										mult = mult / 5;
										sc = sc / 5;
									}
									loc = (candleSet [j].getLoc () * mult) + active.transform.localPosition;
									Debug.Log ("Left: " + l);
									go = checkLocation(loc, (sc*active.transform.localScale.y*prefab.GetComponentInChildren<BoxCollider>().bounds.size.y*100f), root);
								
								}
										GameObject obj = Instantiate (prefab, active.transform.position, active.transform.rotation, gameObject.transform);
										StartCoroutine (pturtle(str, obj, l, r, u, d, size, i, sc, loc, active));
								i++;
								return;
						}

					}
										}
				} else if (c == 'r') {
					Debug.Log (c);
					string b = active.name;
											string bridge;
											int min = 0;
											int max = 2;
											int choose = Random.Range(min, max);
											if (choose == 0)bridge = "bridge small";
											else bridge = "bridge large";
													
					for (int j = 0; j < candleSet.Length; j++) {
						string a = candleSet[j].getType();
						string root = candleSet [j].getRoot ();
						Vector3 loc;

						// if we match the Rule, get the replacement String out of the Rule
												if (a == bridge) {
													if (root == b){
														bool go = false;
								loc = new Vector3 (0,0,0);
								float sc = 1;
														while (go == false){
															prefab = candleSet[j].getPrefab();
															l = candleSet[j].getLeft();
															sc = candleSet[j].getScale();
									float mult = active.transform.localScale.y;
									if (active.name == "candle") {
										mult = mult / 5;
										sc = sc / 5;
									}
									loc = (candleSet [j].getLoc () * mult) + active.transform.localPosition;
									go = checkLocation(loc, (sc*active.transform.localScale.y*prefab.GetComponentInChildren<BoxCollider>().bounds.size.y*100f), root);
									Debug.Log ("Left: " + l);
																}
								GameObject obj = Instantiate (prefab, active.transform.position, active.transform.rotation, gameObject.transform);
							
								StartCoroutine (pturtle(str, obj, l, r, u, d, size, i, sc, loc, active));
								i++;
								return;
																}

																}

					}


				} else if (c == 't') {
					Debug.Log (c);
					string b = active.name;
					string[] towerChoice = new string[2]{ "tower small", "tower large" };
					string choice = towerChoice [Random.Range (0, 2)];
					for (int j = 0; j < candleSet.Length; j++) {
						string a = candleSet[j].getType();
						string root = candleSet [j].getRoot ();
						Vector3 loc;


						// if we match the Rule, get the replacement String out of the Rule
																		if (a == choice) {
																			if (root == b){
																				bool proceed = false;
								loc = new Vector3 (0,0,0);
								float sc = 1;
																				while (proceed == false){
																					prefab = candleSet[j].getPrefab();
																					l = candleSet[j].getLeft();
																					sc = candleSet[j].getScale();
									float mult = active.transform.localScale.y;
									if (active.name == "candle") {
										mult = mult / 5;
										sc = sc / 5;
									}
									loc = (candleSet [j].getLoc () * mult) + active.transform.localPosition;
									Debug.Log ("Left: " + l);
				
									proceed = checkLocation(loc, (sc*active.transform.localScale.y*prefab.GetComponentInChildren<BoxCollider>().bounds.size.y*100f), root);

																						}
																						GameObject obj = Instantiate (prefab, active.transform.position, active.transform.rotation, gameObject.transform);
							
																						StartCoroutine (pturtle(str, obj, l, r, u, d, size, i, sc, loc, active));

								i++;
								return;
																						}

																						}

					}

					i++;
				} else if (c == '/') {
					i++;
					doNothing (str, active, l, r, u, d, size, i);
					char b;
					if (i > 1) {
						b = str [i - 2];
					} else if (active.name == "candle")
						b = 'c';
					else if (active.name == "tower small" ||active.name == "tower large")
						b = 't';
					else if (active.name == "bridge")
						b = 'r';
					else
						b = 'm';
					Debug.Log (c + "and then" + b);

					str = char.ToString (b);
					StartCoroutine (goAgain (str, active));


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


	IEnumerator aTurtle (string str, GameObject obj, int l, int r, int u, int d, float size, int i){
		float travelled = 0;
							float left = (float)l;
							float up = (float)u;
		Vector3 startRotation = obj.transform.localEulerAngles;
							Vector3 travelRotate;
							Vector3 endRotate = new Vector3 (up, left, 0f);
		if (travelled < endRotate.x) {
			travelRotate = speed * Time.deltaTime * new Vector3 (up, left, 0f);
			obj.transform.localEulerAngles = obj.transform.localEulerAngles + travelRotate;
			travelled = travelled + travelRotate.x;
			Debug.Log (travelled + "Out of" + travelRotate.x);
			yield return new WaitForEndOfFrame ();
		} else {
								obj.transform.localEulerAngles = startRotation + endRotate;	
								yield return new WaitForEndOfFrame ();
								StartCoroutine (renderWait (str, obj, l, r, u, d, size, i));

		}
	}

	/*IEnumerator tTurtle(string str, GameObject obj, int l, int r, int u, int d, float size, int i, float sc, Vector3 loc){
		yield return new WaitForEndOfFrame();
		float travelled = 0;
		Vector3 startMove = obj.transform.localPosition;
		Vector3 startRotate = obj.transform.localEulerAngles;
		Vector3 startScale = obj.transform.localScale;
		Vector3 endpointMove = sc * loc;
		float endpointRotate = (float)l;
		Vector3 endpointScale = (obj.transform.localScale * sc) - obj.transform.localScale;
		Vector3 travelMove;
		float travelRotate;
		Vector3 travelScale;
		while (globalIterate == true) {
			if (travelled < endpointRotate) {

				travelMove = speed * Time.deltaTime * endpointMove;
				travelRotate = speed * Time.deltaTime * endpointRotate;
				travelScale = speed * Time.deltaTime * endpointScale;

				obj.transform.localPosition = obj.transform.localPosition + travelMove;
				obj.transform.localEulerAngles = obj.transform.localEulerAngles + new Vector3 (0f, travelRotate, 0f);
				obj.transform.localScale = obj.transform.localScale + travelScale;

				//obj.transform.position = obj.transform.position + (obj.transform.forward * travel2);
				travelled = travelled + travelRotate;
				Debug.Log (travelled + "Out of" + travelRotate);
				yield return new WaitForEndOfFrame ();
			} else  {
				obj.transform.localPosition = startMove + endpointMove;
				obj.transform.localEulerAngles = startRotate + new Vector3 (0f, endpointRotate, 0f);
				obj.transform.localScale = obj.transform.localScale * sc;

				yield return new WaitForEndOfFrame ();
				StartCoroutine (renderWait (str, obj, l, r, u, d, size, i));
				yield break;
			}

		}
	}
	*/


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

	IEnumerator pturtle(string str, GameObject obj, int l, int r, int u, int d, float size, int i, float sc, Vector3 loc, GameObject act){
		yield return new WaitForEndOfFrame();
		float travelled = 0;

		Vector3 startMove = obj.transform.localPosition;
		Vector3 startRotate = obj.transform.localEulerAngles;
		Vector3 startScale = obj.transform.localScale;
		Vector3 endpointMove = loc;
		float endpointRotate = l - obj.transform.localEulerAngles.y;
		if (endpointRotate == 0) {
			endpointRotate = 360;
		}

		Vector3 endpointScale = sc * act.transform.localScale;

		Vector3 travelMove;
		float travelRotate;
		Vector3 travelScale;
		while (globalIterate == true) {
			if (travelled < endpointRotate) {

				travelMove = speed * Time.deltaTime * endpointMove;
				travelRotate = speed * Time.deltaTime * endpointRotate;
				travelScale = speed * Time.deltaTime * endpointScale;

				obj.transform.localPosition = obj.transform.localPosition + travelMove;
				obj.transform.localEulerAngles = obj.transform.localEulerAngles + new Vector3 (0f, travelRotate, 0f);
				obj.transform.localScale = obj.transform.localScale + travelScale;

				//obj.transform.position = obj.transform.position + (obj.transform.forward * travel2);
				travelled = travelled + travelRotate;
				Debug.Log (travelled + "Out of" + endpointRotate);
				yield return new WaitForEndOfFrame ();
			} else  {
				obj.transform.localPosition = startMove + endpointMove;
				obj.transform.localEulerAngles = startRotate + new Vector3 (0f, endpointRotate, 0f);
				obj.transform.localScale = endpointScale;

				yield return new WaitForEndOfFrame ();
				StartCoroutine (renderWait (str, obj, l, r, u, d, size, i));
				yield break;
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

	bool checkLocation (Vector3 center, float radius, string name)
	{
		Collider[] hitColliders = Physics.OverlapSphere(center, radius);
		foreach  (Collider hit in hitColliders)
		{
			if (hit.name == name) {
				Debug.Log ("All Good");
			} else {
				Debug.Log ("collision :  " + hit.name);
				return false;
			}
		}
		return true;
	}







}