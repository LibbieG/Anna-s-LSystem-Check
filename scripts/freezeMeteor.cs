using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniOSC;

public class freezeMeteor: MonoBehaviour {

	Rigidbody rb;
	Behaviour rotation;
	Behaviour targetpos;
	public float seconds = 1f;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator goLsystem (){
		yield return new WaitForSeconds (seconds);
		gameObject.GetComponentInChildren<meteorLSystem> ().enabled = true;

	}

	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.CompareTag ("Plane")) {
				gameObject.GetComponentInParent<fallingOSC> ().falling = false;
				gameObject.GetComponentInParent<instantiateOut> ().trigger2 ();
			StartCoroutine (goLsystem());


			rb.isKinematic = true;
			targetpos = gameObject.GetComponent<impactAngle> ();
			targetpos.enabled = false;
//			transform.parent = null;
			rotation = gameObject.GetComponent<Rotate> ();
			rotation.enabled = false;
		}
	}
}
