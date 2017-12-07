using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chairLsystem : lsysLibrary {

	public bool randomizeLeft;
	public float randomLeftMin;
	public float randomLeftMax;

	public bool randomizeUp;
	public float randomUpMin;
	public float randomUpMax;

	//Rule Setup
	public enum lSystemOptions {Fractal_Tree, Seaweed, Diamond, Custom};
	public enum variables {X, F
	};

	public bool Randomize_Variations;
	public lSystemOptions [] Variations;

	public variables[] Variables_1;
	public string [] Custom_LSystemRules_1;


	public variables[] Variables_2;
	public string [] Custom_LSystemRules_2;

	public float[] customAngleLeft;
	public float[] customAngleRight;
	public float[] customAngleUp;
	public float[] customAngleDown;



	public override void Start () {
		base.Start ();
		randomAngleRange = true;

		returnRule (0);
		currentGeneration = 0;

		generate (currentString);

	}

	void OnCollisionEnter (Collision col){
	}



	public void returnRule (int num){
		if (Variations [num] == lSystemOptions.Custom) {
			angleLeft = customAngleLeft[num];
			angleRight = customAngleRight[num];
			angleUp = customAngleUp[num];
			angleDown = customAngleDown[num];


			currentString = Custom_CurrentString;

			ruleset = new Rule[Variables_1.Length];
			for(int i = 0; i < Variables_1.Length; i++ ) {
				ruleset [i] = new Rule (Variables_1 [i].ToString () [0], Custom_LSystemRules_1 [i]);   
			}

		} else if (Variations [num] == lSystemOptions.Diamond) {
			angleLeft = 90f;
			angleRight = 90f;
			angleUp = setAngleUp;
			angleDown = 0;

			currentString = "F+F+F+F";

			ruleset = new Rule[1];
			ruleset[0] = new Rule ('F', "FF+F++UFS+F");

		} else if (Variations [num] == lSystemOptions.Fractal_Tree) {
			angleLeft = 45f;
			angleRight = 45f;
			angleUp = setAngleUp;
			angleDown = 0;

			currentString = "F";

			ruleset = new Rule[2];
			ruleset[0] = new Rule ('F', "FF");
			ruleset[1] = new Rule ('X', "F[X]X");

		} else if (Variations [num] == lSystemOptions.Seaweed) {
			angleLeft = 22f;
			angleRight = 22f;
			angleUp = 25f;
			angleDown = 10f;

			currentString = "F";

			ruleset = new Rule[1];
			ruleset[0] = new Rule ('F', "FF-[-F+F+F]+[+F-F-F]");

		}
	}


	public override void nextRound () {
		base.nextRound ();

		if (Randomize_Variations == true) {
			int variationNum = Random.Range (0, (Variations.Length - 1));
			returnRule (variationNum);

		} else { 
			returnRule (0);

		}
		if (randomizeLeft)
			angleLeft = Random.Range (randomLeftMin, randomLeftMax);
		if (randomizeUp)
			angleUp = Random.Range (randomUpMin, randomUpMax);

		Debug.Log ("Next Round");
		generate (currentString);

	}



	// Update is called once per frame
	void Update () {
		
	}
}
