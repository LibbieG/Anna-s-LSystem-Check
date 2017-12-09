using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chairLSystem : lsysLibrary {


	public float randomsizeMin;
	public float randomsizeMax;

	//Rule Setup
	public enum lSystemOptions {Candle, Custom};
	public enum variables {c, p, t, b

	};
		
	public int[] randomLeft;
	public int [] randomUp;

	public lSystemOptions [] Variations;



	//custom variables




	public override void Start () {
		base.Start ();
		randomAngleRange = false;
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
		float size = 1;
		string currentString = "hi";

		if (Variations [0] == lSystemOptions.Candle) {
			currentString = "c";
			size = Random.Range (randomsizeMin, randomsizeMax);

			string[] arule = new string [3] { "a", "a", "[a]" };

			string a = arule [Random.Range (0, 3)];

			ruleset = new Rule[1];
			ruleset [0] = new Rule ('a', a);
			currentString = "a";

		}
		generate (currentString, angleLeft, angleRight, angleUp, angleDown, size, act, 0);
	}



	public override void nextRound (string curr, GameObject act){
		base.nextRound ("non", act);
		int angleLeft = 0;
		int angleRight = 0;
		int angleUp = 0;
		int angleDown = 0;
		float size = 1;


		if (Variations [0] == lSystemOptions.Candle) {
			size = Random.Range (randomsizeMin, randomsizeMax);

			string[] arule = new string [3] { "a", "a", "[a]" };

			string a = arule [Random.Range (0, 3)];

			ruleset = new Rule[1];
			ruleset [0] = new Rule ('a', a);

		}
		generate (curr, angleLeft, angleRight, angleUp, angleDown, size, act, 0);
	}


}
