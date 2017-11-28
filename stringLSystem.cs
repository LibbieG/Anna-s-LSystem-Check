using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class stringLSystem : MonoBehaviour {

	public GameObject lsystemPrefab;

	//Static Variables:
	public static float speed;
	public static float angleLeft;
	public static float angleRight;
	public static float angleUp;
	public static float angleDown;

	Rule [] ruleset = new Rule [1];

	//L-System Type Variables
	public enum lSystemOptions {Fractal_Tree, Seaweed, Diamond, Custom};
	public lSystemOptions Primary_LSystem;
	public string Custom_Variable;
	public string Primary_Rule1;
	public string Primary_Rule2;

	public int curentGeneration;
	public string currentString;



	//Control Randomiation and Variations:


	// The Turtle Functions:



	// Use this for initialization
	void Start () {
		curentGeneration = 0; 

		if (Primary_LSystem == lSystemOptions.Fractal_Tree) {
			currentString = "X";
		} else if (Primary_LSystem == lSystemOptions.Seaweed) {
			currentString = "X";
		} else if (Primary_LSystem == lSystemOptions.Diamond){
			currentString = "X+X+X+X";
		} else if (Primary_LSystem == lSystemOptions.Custom){
			currentString = Custom_Variable;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
