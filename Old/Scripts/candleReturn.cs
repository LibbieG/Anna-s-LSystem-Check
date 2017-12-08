using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class candleReturn : MonoBehaviour {

	string type;
	string rootType;
	Vector3 [] locArray;
	float baseScale;
	GameObject prefab;
	//Vector3 rotation;

	public candleReturn (string _type, string _rootType, float _baseScale, Vector3[] _locArray, GameObject _prefab){
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
		if (locArray.Length == 1)
			return locArray [0];
		int i = Random.Range (0, locArray.Length);
		return locArray [i];


	}

	public float getScale (){
		return baseScale;
		
	}

	public int getLeft() {
		int min = 1;
		int max = 5;
		int left = Random.Range (min, max) * 90;

		return left;


	}

	public GameObject getPrefab(){
		return prefab;
	}

}
