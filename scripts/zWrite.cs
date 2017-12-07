using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zWrite : MonoBehaviour {

	Material theMaterial;

	// Use this for initialization
	void Start () {

		theMaterial = gameObject.GetComponent<Renderer> ().material;

	}
	
	// Update is called once per frame
	void Update () {
		theMaterial.SetInt("_ZWrite", 1);
	}
}
