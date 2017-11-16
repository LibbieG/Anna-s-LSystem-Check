using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lSystems : MonoBehaviour {

	public GameObject meteor;
	public GameObject diamondPrefab;
	public float waitTime = 3;
	public float framesperSecond = 4;
	public bool iterateBool = true;
	public float fractalAngley = 90;


	//Lists of Variables (the zeros and the ones etc)
	List<GameObject>fractalZeros = new List<GameObject> ();
		

		

	IEnumerator fractalTree (float fractalAngley){
		fractalZeros.Add (meteor);
		float meteorSizeY = meteor.GetComponent<Renderer> ().bounds.size.y;
		float meteorSizeX = meteor.GetComponent<Renderer> ().bounds.size.x;		//Establish Axiom Root Object
		//Variable 0  = For Each 0 create a meteor object attached to a previous meteor object, this is 0
		//Variable 1 = For Each 1 create a meteor object attached to a previous meteor object
		//Variable a = push position and angle turn left 90 degrees
		//Variable b = push position and angle turn right 90 degrees

		while (iterateBool == true) {
			var zeroCopy = new List<GameObject> (fractalZeros);
			Debug.Log ("Hi There" + fractalAngley);
			if (fractalZeros.Count != 0) {
				foreach (GameObject fractalZero in zeroCopy) {	
					fractalZeroFunction (fractalZero,meteorSizeY, fractalAngley);
				}
			}

			yield return new WaitForSeconds (waitTime + 1);

		}
	}
		

	void fractalZeroFunction (GameObject fractalZero, float meteorSize, float fractalAngley) {


		//Make 0 inactive, it's a parent now, it has served it's purpose
		fractalZeros.Remove (fractalZero);

		//Create a new static 
		GameObject newStatic = Instantiate (diamondPrefab, fractalZero.transform.position, fractalZero.transform.rotation);

		// push position of static by one
		StartCoroutine (OneGrowth(newStatic));

		// Create Two new 0s
		GameObject newObject1 = Instantiate (diamondPrefab, fractalZero.transform.position, fractalZero.transform.rotation);
		GameObject newObject2 = Instantiate (diamondPrefab, fractalZero.transform.position, fractalZero.transform.rotation);
		fractalZeros.Add(newObject1);
		fractalZeros.Add(newObject2);

		//push position by one, rotate 90 degrees and then push position by one again
		StartCoroutine (ZeroGrowth(newObject1, true, fractalAngley));
		StartCoroutine (ZeroGrowth(newObject2, false, fractalAngley));
	}

	IEnumerator OneGrowth (GameObject newStatic){
		float timeElapsed = 0;
		float meteorSize = newStatic.GetComponent<Renderer> ().bounds.size.y;
		float speed = (meteorSize/(waitTime *.5f)/framesperSecond);
		while (timeElapsed < (waitTime/2)){

			newStatic.transform.position = newStatic.transform.position + (newStatic.transform.forward * speed);
			timeElapsed = timeElapsed + (1f / framesperSecond);

			yield return new WaitForSeconds (1f/framesperSecond);
		}	
	
	}

	IEnumerator ZeroGrowth (GameObject newZero, bool left, float fractalAngley){
		Debug.Log (fractalAngley);
		float meteorSize = newZero.GetComponent<Renderer> ().bounds.size.y;
		float timeElapsed = 0f;
		float speed = (meteorSize/(waitTime *.5f)/framesperSecond);
		float speed2 = (meteorSize / (waitTime * .4f) / framesperSecond);
		while (timeElapsed < (waitTime * .5f)){
			newZero.transform.position = newZero.transform.position + (newZero.transform.forward * speed) + (newZero.transform.up * speed);
			timeElapsed = timeElapsed + (1f / framesperSecond);
			yield return new WaitForSeconds (1f / framesperSecond);
		}
		if (left == true){
			while (timeElapsed >= (waitTime* .5f) && timeElapsed < (waitTime * .6f)){
				newZero.transform.Rotate(0f,fractalAngley, 0f);
				timeElapsed = timeElapsed + (1f / framesperSecond);
				yield return new WaitForSeconds (1f / framesperSecond);
			}
		}
		if (left != true) {
			while (timeElapsed >= (waitTime* .5f) && timeElapsed < (waitTime * .6f)){
				newZero.transform.Rotate(0f,fractalAngley * -1f, 0f);
				timeElapsed = timeElapsed + (1f / framesperSecond);
				yield return new WaitForSeconds (1f / framesperSecond);
			}
		}
		while (timeElapsed >= (waitTime * .6f) && timeElapsed < waitTime) {
			newZero.transform.position = newZero.transform.position + (newZero.transform.forward * speed2);
			timeElapsed = timeElapsed + (1f / framesperSecond);
			yield return new WaitForSeconds (1f / framesperSecond);
			Debug.Log (timeElapsed);
		
		}
	}

	// Update is called once per frame
	void Update () {

		//fractalAngley = meteor.transform.eulerAngles.y/(waitTime*.1f)/framesperSecond;

		if (Input.GetKeyDown(KeyCode.Space)){
			StartCoroutine(fractalTree(fractalAngley));
		}
			
	}
}
