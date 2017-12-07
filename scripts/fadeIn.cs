using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeIn : MonoBehaviour {

    public float smooth = 2f;
	Color stColor;
	Color newColor;
	float alpha;

	void Start(){
		foreach (Renderer rd in GetComponentsInChildren<Renderer>()) {
			stColor = rd.material.color;
			rd.material.color = new Vector4 (stColor.r, stColor.g, stColor.b, 0f);
		}

	}
	void Update() {

		ColorChanging ();
	}

	void ColorChanging(){

		foreach(Renderer transp in GetComponentsInChildren<Renderer>()){

			var color = transp.material.color;
			newColor = new Vector4 (color.r, color.g, color.b, 1f);
			transp.material.color = Color.Lerp(transp.material.color, newColor, Time.deltaTime*smooth);	

//			var color = transp.material.color;
//			newColor = new Vector4 (color.r, color.g, color.b, 1f);
//			transp.material.color = Color.Lerp(transp.material.color, newColor, Time.deltaTime*smooth);				
			}
	}
}
	

