using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lSystems : MonoBehaviour {

	public GameObject meteor;
	public GameObject diamondPrefab;
	public float waitTime = 1;
	public bool iterateBool = true;

	//Lists of Variables (the zeros and the ones etc)
	List<GameObject> meteors = new List<GameObject> ();
	List<GameObject>fractalZeros = new List<GameObject> ();
	List<GameObject> fractalOnes = new List<GameObject> ();


	// Use this for initialization
	void Start () {
		
	}
		

	IEnumerator fractalTree (){
		

		meteors.Add (meteor);
		fractalZeros.Add (meteor);
		//Establish Axiom Root Object
		//Variable 0  = For Each 0 create a meteor object attached to a previous meteor object, this is 0
		//Variable 1 = For Each 1 create a meteor object attached to a previous meteor object
		//Variable a = push position and angle turn left 90 degrees
		//Variable b = push position and angle turn right 90 degrees


		while (iterateBool == true) {
			var zeroCopy = new List<GameObject> (fractalZeros);
			var oneCopy = new List<GameObject> (fractalOnes);
			foreach (GameObject fractalZero in zeroCopy) {	
				fractalZeroFunction (fractalZero);
			}
			foreach (GameObject fractalOne in oneCopy) {
				
			}
			yield return new WaitForSeconds (waitTime);
			Debug.Log ("Hi There");
		}
	}

	void fractalZeroFunction (GameObject fractalZero) {
		Vector3 newFractalPosition = new Vector3 (fractalZero.transform.position.x + 1.0f, fractalZero.transform.position.y, fractalZero.transform.position.z);
		//Quaternion newFractalAngle = new Quaternion (fractalZero.transform.eulerAngles.x, fractalZero.transform.eulerAngles.y + 90f, fractalZero.transform.eulerAngles.z);
		GameObject newObject1 = Instantiate (diamondPrefab, fractalZero.transform.position, fractalZero.transform.rotation);
		GameObject newObject2 = Instantiate (diamondPrefab, fractalZero.transform.position, fractalZero.transform.rotation);
		fractalZeros.Add(newObject1);
		fractalZeros.Add(newObject2);
		fractalZeros.Remove (fractalZero);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)){
			StartCoroutine(fractalTree());
		}
			
	}
}
