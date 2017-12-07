using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meteorLSystem : lsysLibrary {

	public bool randomizeLeft;
	public int randomLeftMin;
	public int randomLeftMax;

	public bool randomizeUp;
	public int randomUpMin;
	public int randomUpMax;

	public float randomsizeMin;
	public float randomsizeMax;

	//Rule Setup
	public enum lSystemOptions {Meteor, Custom};
	public enum variables {X, F, M
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


	//custom variables
	List<int> claimedL = new List<int>();
	List<int> claimedU = new List<int>();


	public override void Start () {
		base.Start ();
		randomAngleRange = true;
		currentGeneration = 0;

		returnRule (gameObject);

	}

	void OnCollisionEnter (Collision col){
	}



	public void returnRule (GameObject act){
		base.nextRound ("non", act);
		int angleLeft = 0;
		int angleRight = 0;
		int angleUp = 0;
		int angleDown = 0;
		float size =1;
		string currentString = "hi";

		if (Variations [0] == lSystemOptions.Custom) {
			angleLeft = (int)customAngleLeft[0];
			angleRight = (int)customAngleRight[0];
			angleUp = (int)customAngleUp[0];
			angleDown = (int)customAngleDown[0];


			currentString = customCurrentString[0];

			ruleset = new Rule[Variables_1.Length];
			for(int i = 0; i < Variables_1.Length; i++ ) {
				ruleset [i] = new Rule (Variables_1 [i].ToString () [0], Custom_LSystemRules_1 [i]);   
			}

		} else if (Variations [0] == lSystemOptions.Meteor) {
			currentString = "m";

			angleLeft = Random.Range(randomLeftMin, randomLeftMax) * 5;
			angleRight = 0;
			angleUp = Random.Range(randomUpMin, randomUpMax) * 5;
			angleDown = 0;
			size = Random.Range (randomsizeMin, randomsizeMax);

			foreach (int i in claimedL) {
				if (i == angleLeft) {
					goAgain(currentString, act);
					return;
				}

			}

			foreach (int u in claimedU) {
				if (u == angleUp) {
					goAgain (currentString, act);
					return;
				}
			}
			claimedL.Add (angleLeft);
			claimedU.Add (angleUp);
		

			string [] mrule = new string [2] {"m", "mn"};
			string [] frule = new string[7]{"f", "f", "f", "[FF]", "[FFFF]", "[FFF]", "o"};
			string [] Onerule = new string[3]{"f", "[","o"};
			string m = mrule [Random.Range (0, 2)];
			string f = frule [Random.Range (0, 7)];
			string o = Onerule [Random.Range (0, 3)];


			ruleset = new Rule[5];
			ruleset[0] = new Rule ('m', m);
			ruleset[1] = new Rule ('q', f);
			ruleset[2] = new Rule ('[' , o);
			ruleset [3] = new Rule ('n', "f");
			ruleset [4] = new Rule ('b', "o");

		} 
		generate (currentString, angleLeft, angleRight, angleUp, angleDown, size, act, 0);
	}



	public override void nextRound (string curr, GameObject act){
		base.nextRound (curr, act);
		int angleLeft = 0;
		int angleRight = 0;
		int angleUp = 0;
		int angleDown = 0;
		float size = 1;
		int startInt = 0;

		if (Variations [0] == lSystemOptions.Custom) {
			angleLeft = (int)customAngleLeft[0];
			angleRight = (int)customAngleRight[0];
			angleUp = (int)customAngleUp[0];
			angleDown = (int)customAngleDown[0];


			ruleset = new Rule[Variables_1.Length];
			for(int i = 0; i < Variables_1.Length; i++ ) {
				ruleset [i] = new Rule (Variables_1 [i].ToString () [0], Custom_LSystemRules_1 [i]);   
			}

		} else if (Variations [0] == lSystemOptions.Meteor) {

			angleLeft = Random.Range(randomLeftMin, randomLeftMax) * 5;
			angleRight = 0;
			angleUp = Random.Range(randomUpMin, randomUpMax) * 5;
			angleDown = 0;
			size = Random.Range (randomsizeMin, randomsizeMax);

			foreach (int i in claimedL) {
				if (i == angleLeft) {
					goAgain(curr, act);
					return;
				}
			}


			string [] mrule = new string [2] {"m", "mn"};
			string[] frule = new string[7]{ "f", "f", "f", "[FF]", "[FFFF]", "[FFF]", "o" };
			string [] Onerule = new string[3]{"f", "[","O"};
			string m = mrule [Random.Range (0, 2)];
			string f = frule [Random.Range (0, 7)];
			string o = Onerule [Random.Range (0, 3)];

			Debug.Log ("m: " + m + "f: " + f);


			ruleset = new Rule[5];
			ruleset[0] = new Rule ('m', m);
			ruleset[1] = new Rule ('q', f);
			ruleset[2] = new Rule ('[' , o);
			ruleset [3] = new Rule ('n', "f");
			ruleset [4] = new Rule ('b', "o");

		} 
		generate (curr, angleLeft, angleRight, angleUp, angleDown, size, act, startInt);
		Debug.Log ("curr:" + curr);
	}



	// Update is called once per frame
	void Update () {

	}
}
