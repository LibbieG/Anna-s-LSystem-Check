﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class candleReturn {

	string type;
	string rootType;
	Vector3 [][] locArray;
	float baseScale;
	GameObject prefab;
	int selection;
	Vector3 startpoint;
	//Vector3 rotation;

	public candleReturn (string _type, string _rootType, float _baseScale, Vector3[][] _locArray, GameObject _prefab){
		type = _type;
		rootType = _rootType;
		locArray = _locArray;
		baseScale = _baseScale;
		prefab = _prefab;
			
	}

	public string getType () {
		return type;
	}

	public string getRoot () {
		return rootType;
	}

	public Vector3 getLoc (){
		
		Vector3 location = locArray [selection] [0];
		return location;

	}
		


	public float getScale (){
		return baseScale;
		
	}

	public int getLeft() {
		selection = Random.Range (0, locArray.Length);
		int left = (int)locArray [selection] [1].y;

		return left;


	}

	public GameObject getPrefab(){
		return prefab;
	}

}
