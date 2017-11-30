using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class stringLSystem : MonoBehaviour {

	public static GameObject lsystemPrefab;
	public GameObject Prefab;
	public static GameObject lsystemRoot;
	public GameObject rootObject;


	//Static Variables:
	public static float speed;
	public float Set_Speed;
	public bool Randomize_Speed;
	public float Random_Speed_Min;
	public float Random_Speed_Max;

	public static float angleLeft;
	public float Set_angleLeft;
	public bool Randomize_AngleLeft;
	public float Random_AngleLeft_Min;
	public float Random_AngleLeft_Max;

	public static float angleRight;
	public float Set_angleRight;
	public bool Randomize_AngleRight;
	public float Random_AngleRight_Min;
	public float Random_AngleRight_Max;

	public static float angleUp;
	public float Set_angleUp;
	public bool Randomize_AngleUp;
	public float Random_AngleUp_Min;
	public float Random_AngleUp_Max;

	public static float angleDown;
	public float Set_angleDown;
	public bool Randomize_AngleDown;
	public float Random_AngleDown_Min;
	public float Random_AngleDown_Max;

	static Rule[] ruleset;

	//L-System Type Variables
	public enum lSystemOptions {Fractal_Tree, Seaweed, Diamond, Custom};
	public enum variables {X, Y, F, G
	};


	public lSystemOptions [] Variations;

	public variables[] Variables_1;
	public string [] Custom_LSystemRules_1;

	public variables[] Variables_2;
	public string [] Custom_LSystemRules_2;

	public variables[] Variables_3;
	public string [] Custom_LSystemRules_3;

	public static int currentGeneration;
	public static string currentString;
	public string Custom_CurrentString;



	//Control Randomiation and Variations:


	// The Turtle Functions:



	// Use this for initialization
	void Start () {

		speed = Set_Speed;
		lsystemPrefab = Prefab;
		lsystemRoot = rootObject;

		if (Variations [0] == lSystemOptions.Custom) {
			angleLeft = Set_angleLeft;
			angleRight = Set_angleRight;
			angleUp = Set_angleUp;
			angleDown = Set_angleDown;

			currentString = Custom_CurrentString;

			ruleset = new Rule[Variables_1.Length];
			for(int i = 0; i < Variables_1.Length; i++ ) {
				ruleset [i] = new Rule (Variables_1 [i].ToString () [0], Custom_LSystemRules_1 [i]);   
			}

		} else if (Variations [0] == lSystemOptions.Diamond) {
			angleLeft = 90f;
			angleRight = 90f;
			angleUp = Set_angleUp;
			angleDown = Set_angleDown;

			currentString = "F+F+F+F";

			ruleset = new Rule[1];
			ruleset[0] = new Rule ('F', "FF+F++F+F");
			
		} else if (Variations [0] == lSystemOptions.Fractal_Tree) {
			angleLeft = 45f;
			angleRight = 45f;
			angleUp = Set_angleUp;
			angleDown = Set_angleDown;

			currentString = "X";

			ruleset = new Rule[2];
			ruleset[0] = new Rule ('F', "FF");
			ruleset[1] = new Rule ('X', "F[X]X");
			
		} else if (Variations [0] == lSystemOptions.Seaweed) {
			angleLeft = 22f;
			angleRight = 22f;
			angleUp = 25f;
			angleDown = 10f;

			currentString = "F";

			ruleset = new Rule[1];
			ruleset[0] = new Rule ('F', "FF-[-F+F+F]+[+F-F-F]");
			
		}
			


		currentGeneration = 0; 


		
	}

	// Generate the next generation
	void generate(string str) {
		// An empty StringBuffer that we will fill
		string nextgen = "";

		//StringBuffer nextgen = new StringBuffer();
		// For every character in the sentence
		for (int i = 0; i < str.Length; i++) {
			// What is the character
			char curr = str[i];
			// We will replace it with itself unless it matches one of our rules
			string replace = "" + curr;
			// Check every rule
			for (int j = 0; j < ruleset.Length; j++) {
				char a = ruleset[j].getA();
				// if we match the Rule, get the replacement String out of the Rule
				if (a == curr) {
					replace = ruleset[j].getB();
					break; 
				}
			}
			// Append replacement String
			nextgen = nextgen + replace;
		}
		// Replace sentence
		currentString = nextgen;
		// Increment generation
		currentGeneration++;
	}
		
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)){


			generate (currentString);
			lsysLibrary.render ();

		}
	}
}
