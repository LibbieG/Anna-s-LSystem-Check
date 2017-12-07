using System.Collections.Generic;
using UnityEngine;
using System.Collections;


public class offsetTex : MonoBehaviour 
{
	public float scrollSpeed = 0.1F;
	public Renderer rend;
	void Start() {
		rend = GetComponent<Renderer>();
	}
	void Update() {
		rend.material.shader = Shader.Find ("MK/Glow/Selective/Normal/DiffuseBumpSpec");
		float offset = Time.time * scrollSpeed;
		rend.material.SetTextureOffset("_MainTex", new Vector2(0, -offset));
		rend.material.SetTextureOffset("_MKGlowTex", new Vector2(offset/5,0));
	}
}
	