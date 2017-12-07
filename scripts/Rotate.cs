using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

	public float rate = 20f;
	// Update is called once per frame
	void Update () {
		transform.Rotate(0,rate*Time.deltaTime,0);
	}
}