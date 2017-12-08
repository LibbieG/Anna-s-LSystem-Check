using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class candleLSystem : lsysLibrary {


	public GameObject smallBuilding;
	public GameObject largeBuilding;
	public GameObject smallBridge;
	public GameObject largeBridge;
	public GameObject porch;
	public GameObject [] bridges; 

	public Vector3[] porchTowerSmall;
	public float ptsScale;
	public Vector3[] porchTowerLarge;
	public float ptlScale;
	public Vector3[] porchCandle;
	public float pcScale;
	public Vector3[] porchBridge;
	public float pbScale;

	public Vector3[] towerSmallTowerLarge;
	public float tstlScale;
	public Vector3[] towerSmallCandle;
	public float tscScale;
	public Vector3[] towerSmallBridge;
	public float tsbScale;

	public Vector3[] towerLargetowerSmall;
	public float tltsScale;
	public Vector3[] towerLargeCandle;
	public float tlcScale;
	public Vector3[] towerLargeBridge;
	public float tlbScale;

	public Vector3[] bridgeSmallTower;
	public float bstScale;
	public Vector3[] bridgeLargeTower;
	public float bltScale;
	public Vector3[] bridgeCandle;
	public float bcScale;


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

		returnRule (gameObject);

		candleSet = new candleReturn [13];

		candleSet [0] = new candleReturn("porch", "tower small", ptsScale, porchTowerSmall, porch);
		candleSet [1] = new candleReturn ("porch", "tower large", ptlScale, porchTowerLarge, porch);
		candleSet [2] = new candleReturn ("porch", "porch candle", pcScale, porchCandle, porch);
		candleSet [3] = new candleReturn ("porch", "porch bridge", pbScale, porchBridge, porch);
		candleSet [4] = new candleReturn ("tower small", "tower large", tstlScale, towerSmallTowerLarge, smallBuilding);
		candleSet [5] = new candleReturn ("tower small", "candle", tscScale, towerSmallCandle, smallBuilding);
		candleSet [6] = new candleReturn ("tower small", "bridge", tsbScale, towerSmallBridge, smallBuilding);
		candleSet [7] = new candleReturn ("tower large", "tower small", tsbScale, towerLargetowerSmall, largeBuilding);
		candleSet [8] = new candleReturn ("tower large", "candle", tlcScale, towerLargeCandle, largeBuilding);
		candleSet [9] = new candleReturn ("tower large", "bridge", tlbScale, towerLargeBridge, largeBuilding);
		candleSet [10] = new candleReturn ("bridge", "tower large", bltScale, bridgeLargeTower, bridges [Random.Range(0,2)]);
		candleSet [11] = new candleReturn ("bridge", "tower small", bstScale, bridgeSmallTower, bridges [Random.Range(0,2)]);
		candleSet [12] = new candleReturn ("bridge", "candle", bcScale, bridgeCandle, bridges[Random.Range(0,2)]);

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
