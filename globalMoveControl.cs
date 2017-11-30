using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class globalMoveControl : MonoBehaviour {

	public static lsysLibrary [] lsysLibInst;
	public static stringLSystem [] strlsysLibInst;
	public int maxMovers = 25;
	public static int lsysLibI;


	// Use this for initialization
	void Start () {
		lsysLibInst = new lsysLibrary[maxMovers];
		strlsysLibInst = new stringLSystem[maxMovers];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
