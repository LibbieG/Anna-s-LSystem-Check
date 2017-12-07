/*
* UniOSC
* Copyright © 2014-2015 Stefan Schlupek
* All rights reserved
* info@monoflow.org
*/

// Modifications by Anna Grossman


using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using OSCsharp.Data;


namespace UniOSC{

	[AddComponentMenu("UniOSC/Toggle")]
	public class instantiateOSC :  UniOSCEventTarget {

		//public Component componentToToggle;
		public GameObject userPrefab;

		public Vector3 instantiatePosition;
		public Quaternion instantiateQuaternion;
		public static GameObject[] instantiatedObjects;
		public static int objectIndex;
		public int codeSelection;
		public string number;


		private Type _compType;


		void Awake(){

			objectIndex = 0;
			instantiatedObjects = new GameObject[25];
		}



		/// <summary>
		/// Updates the state of the component.(enabled)
		/// </summary>



		public override void OnEnable(){
			_Init();
			base.OnEnable();
		}


		public override void OnOSCMessageReceived(UniOSCEventArgs args){


			Debug.Log ("Hit!");
			OscMessage msg = (OscMessage)args.Packet;

			if(msg.Data.Count <1)return;
			//if(!( msg.Data[0]  is  System.Single))return;

			if (codeSelection == 1) {
				Debug.Log ("Hi Bitch");
				instantiatedObjects [objectIndex] = Instantiate (userPrefab, instantiatePosition, instantiateQuaternion);
				instantiatedObjects [objectIndex].GetComponent<fallingOSC> ().myIndex = objectIndex;
				Debug.Log (objectIndex);
				objectIndex = objectIndex + 1;
				Debug.Log (objectIndex);
			} else if (codeSelection == 2) {
				Debug.Log ("Yo Bitch");
				number = msg.Data [0].ToString ();
				Debug.Log ("Yo: " + msg.Data [0].ToString ());
				Debug.Log (number);
				instantiateOut.Trigger (number);


			} else if (codeSelection == 3) {
				Debug.Log ("What up bitch?");
				number = msg.Data [0].ToString ();
				Debug.Log ("What?: " + msg.Data [0].ToString ());
				ClientCubeOSC.newAddress (number);
			}

		}

	}

}