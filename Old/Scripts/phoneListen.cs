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
	public class phoneListen :  UniOSCEventTarget {

		public string number;

		private Type _compType;
		public bool phone = true;



		//private Type _compType;


		void Awake(){
			if (_oscAddress == "/phone/number") {
				phone = true;
			} else {
				phone = false;
			}
			Debug.Log ("Phone On: " + phone);

		}


		private void _Init(){

		}

		/// <summary>
		/// Updates the state of the component.(enabled)
		/// </summary>



		public override void OnEnable(){
			_Init();
			base.OnEnable();
		
		}


		public override void OnOSCMessageReceived(UniOSCEventArgs args){


			if (phone) {
				OscMessage msg = (OscMessage)args.Packet;
				Debug.Log ("Over Here4");

				if (msg.Data.Count < 1)
					return;
				if (!(msg.Data [0]  is  System.Single))
					return;
				number = msg.Data [0].ToString ();

				Debug.Log ("Phone" + number);

				instantiateOut.Trigger (number);

			} else {
				OscMessage msg = (OscMessage)args.Packet;
				Debug.Log ("Over Here2");

				if (msg.Data.Count < 1)
					return;
				if (!(msg.Data [0]  is  System.Single))
					return;
				number = msg.Data [0].ToString ();

				Debug.Log ("ObjectIndex" + number);

				ClientCubeOSC.newAddress (number);

			}
		}

	}

}