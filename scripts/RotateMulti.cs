using UnityEngine;
using System.Collections;

public class RotateMulti : MonoBehaviour {

	public float rate = 20f;
	public float rate2 = 20f;
	// Update is called once per frame
	void Update () {
		transform.Rotate(rate2*Time.deltaTime,rate*Time.deltaTime,0);
	}
}