using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class stringLSystem : MonoBehaviour {

	public static GameObject lsystemPrefab;
	public GameObject Prefab;
	public static GameObject lsystemRoot;
	public GameObject rootObject;
	public static float PrefabBounds;


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

	public bool Randomize_Variations;
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
		PrefabBounds = Prefab.GetComponent<Renderer> ().bounds.size.z;

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
		if (Input.GetKeyDown (KeyCode.A)) {
			lsysLibrary.render ();
		}

		if (Input.GetKeyDown (KeyCode.Space)) {

			if (Randomize_Variations == true) {
				int i = Random.Range (0, (Variations.Length - 1));

				lSystemOptions randomVarient = Variations [i];

				if (randomVarient == lSystemOptions.Custom) {
					int v = Random.Range (0, 2);

					angleLeft = Random.Range (Random_AngleLeft_Min, Random_AngleLeft_Max);
					angleRight = Random.Range (Random_AngleRight_Min, Random_AngleRight_Max);
					angleUp = Random.Range (Random_AngleUp_Min, Random_AngleUp_Max);
					angleDown = Random.Range (Random_AngleDown_Min, Random_AngleDown_Max);


					if (v == 0) {
						ruleset = new Rule[Variables_1.Length];
						for (int j = 0; j < Variables_1.Length; j++) {
							ruleset [i] = new Rule (Variables_1 [i].ToString () [0], Custom_LSystemRules_1 [i]);
						}
					} else if (v == 1) {
						ruleset = new Rule[Variables_2.Length];
						for (int j = 0; j < Variables_2.Length; j++) {
							ruleset [i] = new Rule (Variables_2 [i].ToString () [0], Custom_LSystemRules_2 [i]);
						}
					} else {
						ruleset = new Rule[Variables_3.Length];
						for (int j = 0; j < Variables_3.Length; j++) {
							ruleset [i] = new Rule (Variables_3 [i].ToString () [0], Custom_LSystemRules_3 [i]);
						}
					}

					   
				} else if (randomVarient == lSystemOptions.Diamond) {
					angleLeft = 90f;
					angleRight = 90f;
					angleUp = Set_angleUp;
					angleDown = Set_angleDown;

					ruleset = new Rule[1];
					ruleset [0] = new Rule ('F', "FuF+F++F+Fu");

				} else if (randomVarient == lSystemOptions.Fractal_Tree) {
					angleLeft = 45f;
					angleRight = 45f;
					angleUp = Set_angleUp;
					angleDown = Set_angleDown;

					ruleset = new Rule[2];
					ruleset [0] = new Rule ('F', "FuF");
					ruleset [1] = new Rule ('X', "F[X]uX");

				} else if (randomVarient == lSystemOptions.Seaweed) {
					angleLeft = 22f;
					angleRight = 22f;
					angleUp = 25f;
					angleDown = 10f;

					ruleset = new Rule[1];
					ruleset [0] = new Rule ('F', "FF-[-F+F+F]u+[+F-F-F]u");

				} 
				
			} else {
				if (Randomize_Speed == true)
					speed = Random.Range (Random_Speed_Min, Random_Speed_Max); 
				if (Randomize_AngleLeft == true)
					angleLeft = Random.Range (Random_AngleLeft_Min, Random_AngleLeft_Max);
				if (Randomize_AngleRight == true)
					angleRight = Random.Range (Random_AngleRight_Min, Random_AngleRight_Max);
				if (Randomize_AngleUp == true)
					angleUp = Random.Range (Random_AngleUp_Min, Random_AngleUp_Max);
				if (Randomize_AngleDown == true)
					angleDown = Random.Range (Random_AngleDown_Min, Random_AngleDown_Max);
			}

			Debug.Log (angleRight);
			lsysLibrary.render ();
			generate (currentString);
		}
	}
}
