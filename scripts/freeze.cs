using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class freeze : MonoBehaviour {

	Rigidbody rb;
	Behaviour rotation;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision){

		if (collision.gameObject.CompareTag ("Plane")) {
			rb.isKinematic = true;
//			transform.parent = null;
			rotation = gameObject.GetComponent<Rotate> ();
			rotation.enabled = false;
		}
	}
}
