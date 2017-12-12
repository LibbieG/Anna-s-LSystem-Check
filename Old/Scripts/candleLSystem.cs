using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class candleLSystem : lsysLibrary {


	public GameObject smallBuilding;
	public GameObject largeBuilding;
	public GameObject porch;
	public GameObject bridgeSmall;
	public GameObject bridgeLarge;

	Vector3[][] porchTowerSmall;
	Vector3[][] towerLargetowerSmall;
	Vector3 [][] towerSmalltowerSmall;
	Vector3[][] bridgeSmalltowerSmall;
	Vector3[][] bridgeLargetowerSmall;

	Vector3 [][] towerLargetowerLarge;
	Vector3[][] porchTowerLarge;
	Vector3[][] bridgeSmalltowerLarge;
	Vector3 [][] bridgeLargetowerLarge;
	Vector3 [][] towerSmalltowerLarge;

	Vector3 [][] towerSmallbridgeSmall;
	Vector3 [][] towerLargebridgeSmall;
	Vector3 [][] porchbridgeSmall;

	Vector3 [][] towerSmallbridgeLarge;
	Vector3 [][] towerLargebridgeLarge;
	Vector3 [][] porchbridgeLarge;

	Vector3 [][] towerSmallCandle;
	Vector3 [][] towerLargeCandle;
	Vector3 [][] porchCandle;



	public float randomsizeMin;
	public float randomsizeMax;

	//Rule Setup
	public enum lSystemOptions {Candle, Custom};
	public enum variables {c, p, t, b

	};
		
	public lSystemOptions [] Variations;



	//custom variables




	public override void Start () {
		base.Start ();
		randomAngleRange = false;
		currentGeneration = 0;
		candleArray ();
		returnRule (gameObject);
	}


	public void candleArray(){


		porchTowerSmall = new Vector3[4][]{
			new Vector3[2]{new Vector3 (0, Random.Range(0,3), -6), new Vector3 (0f,0f,0f)},
			new Vector3[2] {new Vector3 (-6, Random.Range(0,3), 0), new Vector3 (0, 90, 0)},
			new Vector3 [2] {new Vector3 (0, Random.Range(0,3), 6), new Vector3 (0, 180, 0)},
			new Vector3 [2] {new Vector3 (6, Random.Range(0,3), 0), new Vector3 (0, -90, 0)}
		};

		towerLargetowerSmall = new Vector3[4][] {
			new Vector3[2]{ new Vector3 (0, Random.Range (-5, 37), -10), new Vector3 (0f, 0f, 0f) },
			new Vector3[2] { new Vector3 (-10, Random.Range (-5, 37), 0), new Vector3 (0, 90, 0) },
			new Vector3 [2] { new Vector3 (0, Random.Range (-5, 37), 10), new Vector3 (0, 180, 0) },
			new Vector3 [2] { new Vector3 (10, Random.Range (-5, 37), 0), new Vector3 (0, -90, 0) }
		};

		towerSmalltowerSmall = new Vector3[4][] {
			new Vector3[2]{ new Vector3 (Random.Range(-5,5), Random.Range (-6, 6), -10), new Vector3 (0f, 0f, 0f) },
			new Vector3[2] { new Vector3 (-10, Random.Range (-6, 6), Random.Range(-5,5)), new Vector3 (0, 90, 0) },
			new Vector3 [2] { new Vector3 (Random.Range(-5,5), Random.Range (-6, 6), 10), new Vector3 (0, 180, 0) },
			new Vector3 [2] { new Vector3 (10, Random.Range (-6, 6), Random.Range(-5,5)), new Vector3 (0, -90, 0) }
		};

		bridgeSmalltowerSmall = new Vector3[4][] {
			new Vector3[2]{ new Vector3 (Random.Range(-5,5), Random.Range (0, 4), -20), new Vector3 (0f, 0f, 0f) },
			new Vector3[2] { new Vector3 (-20, Random.Range (-6, 6), Random.Range(0,4)), new Vector3 (0, 90, 0) },
			new Vector3 [2] { new Vector3 (Random.Range(-5,5), Random.Range (0, 4), 20), new Vector3 (0, 180, 0) },
			new Vector3 [2] { new Vector3 (20, Random.Range (-6, 6), Random.Range(-5,5)), new Vector3 (0, -90, 0) }
		};

		bridgeLargetowerSmall = new Vector3[4][] {
			new Vector3[2]{ new Vector3 (Random.Range(-5,5), Random.Range (-4, 0), -43), new Vector3 (0f, 0f, 0f) },
			new Vector3[2] { new Vector3 (-43, Random.Range (-4, 0), Random.Range(-5,5)), new Vector3 (0, 90, 0) },
			new Vector3 [2] { new Vector3 (Random.Range(-5,5), Random.Range (-4, 0), 43), new Vector3 (0, 180, 0) },
			new Vector3 [2] { new Vector3 (43, Random.Range (-4, 0), Random.Range(-5,5)), new Vector3 (0, -90, 0) }
		};


		porchTowerLarge = new Vector3[4][]{
			new Vector3[2]{new Vector3 (0, Random.Range(0,37), -6), new Vector3 (0f,0f,0f)},
			new Vector3[2] {new Vector3 (-6, Random.Range(0,37), 0), new Vector3 (0, 90, 0)},
			new Vector3 [2] {new Vector3 (0, Random.Range(0,37), 6), new Vector3 (0, 180, 0)},
			new Vector3 [2] {new Vector3 (6, Random.Range(0,37), 0), new Vector3 (0, -90, 0)}
		};

		towerLargetowerLarge = new Vector3[4][] {
			new Vector3[2]{ new Vector3 (0, Random.Range (-37, 37), -10), new Vector3 (0f, 0f, 0f) },
			new Vector3[2] { new Vector3 (-10, Random.Range (-37, 37), 0), new Vector3 (0, 90, 0) },
			new Vector3 [2] { new Vector3 (0, Random.Range (-37, 37), 10), new Vector3 (0, 180, 0) },
			new Vector3 [2] { new Vector3 (10, Random.Range (-37, 37), 0), new Vector3 (0, -90, 0) }
		};

		towerSmalltowerLarge = new Vector3[4][] {
			new Vector3[2]{ new Vector3 (Random.Range(-5,5), Random.Range (-6, 40), -10), new Vector3 (0f, 0f, 0f) },
			new Vector3[2] { new Vector3 (-10, Random.Range (-6, 40), Random.Range(-5,5)), new Vector3 (0, 90, 0) },
			new Vector3 [2] { new Vector3 (Random.Range(-5,5), Random.Range (-6, 40), 10), new Vector3 (0, 180, 0) },
			new Vector3 [2] { new Vector3 (10, Random.Range (-6, 40), Random.Range(-5,5)), new Vector3 (0, -90, 0) }
		};

		bridgeSmalltowerLarge = new Vector3[4][] {
			new Vector3[2]{ new Vector3 (Random.Range(-5,5), Random.Range (0, 40), -20), new Vector3 (0f, 0f, 0f) },
			new Vector3[2] { new Vector3 (-20, Random.Range (0,40), Random.Range(-5,5)), new Vector3 (0, 90, 0) },
			new Vector3 [2] { new Vector3 (Random.Range(-5,5), Random.Range (0, 40), 20), new Vector3 (0, 180, 0) },
			new Vector3 [2] { new Vector3 (20, Random.Range (0,40), Random.Range(-5,5)), new Vector3 (0, -90, 0) }
		};

		bridgeLargetowerLarge = new Vector3[4][] {
			new Vector3[2]{ new Vector3 (Random.Range(-5,5), Random.Range (-4, 32), -43), new Vector3 (0f, 0f, 0f) },
			new Vector3[2] { new Vector3 (-43, Random.Range (-4, 32), Random.Range(-5,5)), new Vector3 (0, 90, 0) },
			new Vector3 [2] { new Vector3 (Random.Range(-5,5), Random.Range (-4, 32), 43), new Vector3 (0, 180, 0) },
			new Vector3 [2] { new Vector3 (43, Random.Range (-4, 32), Random.Range(-5,5)), new Vector3 (0, -90, 0) }
		};

		//Next Set


		porchbridgeSmall = new Vector3[2][]{
			new Vector3[2]{new Vector3 (0, 10, -6), new Vector3 (0f,0f,0f)},
			new Vector3[2] {new Vector3 (0, 10, 6), new Vector3 (0, 180, 0)},

		};

		towerLargebridgeSmall = new Vector3[2][] {
			new Vector3[2] { new Vector3 (-20, Random.Range (-38,0), 0), new Vector3 (0, 90, 0) },
			new Vector3 [2] { new Vector3 (20, Random.Range (-38,0), 0), new Vector3 (0, -90, 0) }
		};

		towerSmallbridgeSmall = new Vector3[2][] {
			new Vector3[2] { new Vector3 (-20, Random.Range (-6, 0), 0), new Vector3 (0, 90, 0) },
			new Vector3 [2] { new Vector3 (20, Random.Range (-6, 0), 0), new Vector3 (0, -90, 0) }
		};

		porchbridgeLarge = new Vector3[4][]{
			new Vector3[2]{new Vector3 (Random.Range(-20,20), 18, 6), new Vector3 (0f,180f,0f)},
			new Vector3[2]{new Vector3 (Random.Range(-20,20), 4, 6), new Vector3 (0f,180f,0f)},
			new Vector3[2]{new Vector3 (Random.Range(-20,20), 18, -6), new Vector3 (0f,0f,0f)},
			new Vector3[2]{new Vector3 (Random.Range(-20,20), 4, -6), new Vector3 (0f,0f,0f)},

		};

		towerLargebridgeLarge = new Vector3[2][] {
			new Vector3[2] { new Vector3 (-40, Random.Range (-34,0), 0), new Vector3 (0, 90, 0) },
			new Vector3 [2] { new Vector3 (40, Random.Range (-34,0), 0), new Vector3 (0, -90, 0) }
		};

		towerSmallbridgeLarge = new Vector3[2][] {
			new Vector3[2] { new Vector3 (-40, Random.Range (0,4), 0), new Vector3 (0, 90, 0) },
			new Vector3 [2] { new Vector3 (40, Random.Range (0,4), 0), new Vector3 (0, -90, 0) }
		};


		//New section


		porchCandle = new Vector3[13][]{
			new Vector3[2]{new Vector3 (.74f, 8.45f, -4.12f), new Vector3 (0f,0f,0f)},
			new Vector3[2]{new Vector3 (.6f, 6f, -3.76f), new Vector3 (0f,0f,0f)},
			new Vector3[2]{new Vector3 (.2f, 2.92f, -3.76f), new Vector3 (0f,0f,0f)},
			new Vector3[2] {new Vector3 (-1.09f, 7.56f, -1.02f), new Vector3 (0, 90, 0)},
			new Vector3[2] {new Vector3 (-1.09f, 4.52f, -1.02f), new Vector3 (0, 90, 0)},
			new Vector3[2] {new Vector3 (-1.83f, .49f, -2.32f), new Vector3 (0, 90, 0)},
			new Vector3 [2] {new Vector3 (1.7f, 8.2f, .45f), new Vector3 (0, 180, 0)},
			new Vector3 [2] {new Vector3 (1.1f, 5.62f, .77f), new Vector3 (0, 180, 0)},
			new Vector3 [2] {new Vector3 (-.2f, 3.4f, .82f), new Vector3 (0, 180, 0)},
			new Vector3 [2] {new Vector3 (.78f, 1f, 2.06f), new Vector3 (0, 180, 0)},
			new Vector3 [2] {new Vector3 (3.22f, 7.56f, -1.4f), new Vector3 (0, -90, 0)},
			new Vector3 [2] {new Vector3 (3.22f, 4.53f, -1.4f), new Vector3 (0, -90, 0)},
			new Vector3 [2] {new Vector3 (4.38f, .44f, -1.4f), new Vector3 (0, -90, 0)}
		};

		towerLargeCandle = new Vector3[4][] {
			new Vector3[2]{ new Vector3 (.49f, Random.Range(-6,8), -4.6f), new Vector3 (0f, 0f, 0f) },
			new Vector3[2] { new Vector3 (-1.2f, Random.Range(-6,8), -1.27f), new Vector3 (0, 90, 0) },
			new Vector3 [2] { new Vector3 (.76f, Random.Range(-6,8), 1.53f), new Vector3 (0, 180, 0) },
			new Vector3 [2] { new Vector3 (4.4f, Random.Range(-6,8), -1.71f), new Vector3 (0, -90, 0) }
		};

		towerSmallCandle = new Vector3[11][] {
			new Vector3[3]{ new Vector3 (.92f, .92f, -4.42f), new Vector3 (0f, 0f, 0f), new Vector3 (0f, 0f, 0f)  },
			new Vector3[3]{ new Vector3 (.92f, 4.42f, -4.42f), new Vector3 (0f, 0f, 0f), new Vector3 (0f, 0f, 0f) },
			new Vector3[3]{ new Vector3 (.92f, 8.37f, -4.42f), new Vector3 (0f, 0f, 0f), new Vector3 (0f, 0f, 0f) },
			new Vector3[3] { new Vector3 ( - 1.96f, .63f, .03f), new Vector3 (0, 90, 0), new Vector3 (0f, 0f, 0f) },
			new Vector3[3] { new Vector3 (-2.17f, 2.2f, -2.27f), new Vector3 (0, 90, 0), new Vector3 (0f, 0f, 0f) },
			new Vector3[3] { new Vector3 ( - 1.95f, 5.79f, -2.27f), new Vector3 (0, 90, 0), new Vector3 (0f, 0f, 0f) },
			new Vector3[3] { new Vector3 ( -1.77f, 9.35f, -2.27f), new Vector3 (0, 90, 0), new Vector3 (0f, 0f, 0f) },
			new Vector3 [3] { new Vector3 (.79f, 8.62f, .28f) , new Vector3 (0, 180, 0), new Vector3 (0f, 0f, 0f) },
			new Vector3 [3] { new Vector3 (2.01f, 3.29f, 1.04f) , new Vector3 (0, 180, 0), new Vector3 (0f, 0f, 0f) },
			new Vector3 [3] { new Vector3 (3.56f, 2.44f, -1.85f), new Vector3 (0, -90, 0), new Vector3 (0f, 0f, 0f) },
			new Vector3 [3] { new Vector3 (2.76f, 6.03f, -1.85f), new Vector3 (0, -90, 0), new Vector3 (0f, 0f, 0f) }
		};



		candleSet = new candleReturn [19];

		candleSet [0] = new candleReturn("porch", "tower small", .5f , porchTowerSmall, porch);
		candleSet [1] = new candleReturn ("tower large", "tower small", Random.Range (.5f, 1.2f), towerLargetowerSmall, largeBuilding);
		candleSet [2] = new candleReturn ("tower small", "tower small", Random.Range (.8f, 1.2f), towerSmalltowerSmall, smallBuilding);
		candleSet [3] = new candleReturn ("bridge small", "tower small", Random.Range (.5f, 1.2f), bridgeSmalltowerSmall, bridgeSmall);
		candleSet [4] = new candleReturn ("bridge large", "tower small", Random.Range(.8f, 1.2f), bridgeLargetowerSmall, bridgeLarge);
		candleSet [5] = new candleReturn("porch", "tower large", .5f , porchTowerLarge, porch);
		candleSet [6] = new candleReturn ("tower large", "tower large", Random.Range (.5f, 1.2f), towerLargetowerLarge, largeBuilding);
		candleSet [7] = new candleReturn ("tower small", "tower large", Random.Range (.8f, 1.2f), towerSmalltowerLarge, smallBuilding);
		candleSet [8] = new candleReturn ("bridge small", "tower large", Random.Range (.5f, 1.2f), bridgeSmalltowerLarge, bridgeSmall);
		candleSet [9] = new candleReturn ("bridge large", "tower large", Random.Range(.8f, 1.2f), bridgeLargetowerLarge, bridgeLarge);
		candleSet [10] = new candleReturn("porch", "bridge small", .5f , porchbridgeSmall, porch);
		candleSet [11] = new candleReturn ("tower large", "bridge small", Random.Range (.5f, 1.2f), towerLargebridgeSmall, largeBuilding);
		candleSet [12] = new candleReturn ("tower small", "bridge small", Random.Range (.8f, 1.2f), towerSmallbridgeSmall, smallBuilding);
		candleSet [13] = new candleReturn("porch", "bridge large", .5f , porchbridgeLarge, porch);
		candleSet [14] = new candleReturn ("tower large", "bridge large", Random.Range (.5f, 1.2f), towerLargebridgeLarge, largeBuilding);
		candleSet [15] = new candleReturn ("tower small", "bridge large", Random.Range (.8f, 1.2f), towerSmallbridgeLarge, smallBuilding);
		candleSet [16] = new candleReturn("porch", "candle", .25f , porchCandle, porch);
		candleSet [17] = new candleReturn ("tower large", "candle", .25f, towerLargeCandle, largeBuilding);
		candleSet [18] = new candleReturn ("tower small", "candle", .25f, towerSmallCandle, smallBuilding);

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

			string[] crule = new string [9] { "p", "p","p", "t", "t", "[t]","/t", "r", "/r" };
			string[] prule = new string[9]{  "p", "p","p", "t", "t", "[t]","/t", "r", "/r" };
			string[] trule = new string[9]{  "p", "p","pp", "t", "t", "[t]","/t", "r", "/r" };
			string [] rrule = new string[7]{ "p", "p","p", "t", "t", "[t]","/t"};
			string c = crule [Random.Range (0, 7)];
			string p = prule [Random.Range (0, 3)];
			string t = trule [Random.Range (0, 3)];
			string r = rrule [Random.Range (0, 2)];

			ruleset = new Rule[4];
			ruleset [0] = new Rule ('c', c);
			ruleset [1] = new Rule ('p', p);
			ruleset [2] = new Rule ('t', t);
			ruleset [3] = new Rule ('r', r);

		}
		generate (currentString, angleLeft, angleRight, angleUp, angleDown, size, act, 0);
	}



	public override void nextRound (string curr, GameObject act){
		candleArray();
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

			string[] crule = new string [9] { "p", "p","p", "t", "t", "[t]","/t", "r", "/r" };
			string[] prule = new string[9]{  "p", "p","p", "t", "t", "[t]","/t", "r", "/r" };
			string[] trule = new string[9]{  "p", "p","pp", "t", "t", "[t]","/t", "r", "/r" };
			string [] rrule = new string[7]{ "p", "p","p", "t", "t", "[t]","/t"};
			string c = crule [Random.Range (0, 7)];
			string p = prule [Random.Range (0, 3)];
			string t = trule [Random.Range (0, 3)];
			string r = rrule [Random.Range (0, 2)];

			ruleset = new Rule[4];
			ruleset [0] = new Rule ('c', c);
			ruleset [1] = new Rule ('p', p);
			ruleset [2] = new Rule ('t', t);
			ruleset [3] = new Rule ('r', r);

		}
		generate (currentString, angleLeft, angleRight, angleUp, angleDown, size, act, 0);
	}


}
