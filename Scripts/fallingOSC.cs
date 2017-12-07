﻿/*
* UniOSC
* Copyright © 2014-2015 Stefan Schlupek
* All rights reserved
* info@monoflow.org
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using OSCsharp.Data;


namespace UniOSC{

	/// <summary>
	/// Moves a GameObject in normalized coordinates (ScreenToWorldPoint)
	/// </summary>
	[AddComponentMenu("UniOSC/MoveGameObject")]
	public class fallingOSC :  UniOSCEventTarget {

		[HideInInspector]
		public Transform transformToMove;
		public float nearClipPlaneOffset = 1;
		public enum Mode{Screen,Relative}
		public Mode movementMode;
		public int myIndex;
		public bool falling = true;

		[HideInInspector]
		public string[] myAddresses = new string[2];
		//movementModeProp = serializedObject.FindProperty ("movementMode");

		private Vector3 pos;

		void Awake(){
			
		}


		public override void OnEnable()
		{
			base.OnEnable();





			if(transformToMove == null){
				Transform hostTransform = GetComponent<Transform>();
				if(hostTransform != null) transformToMove = hostTransform;
			}
		}

		public override void Update ()
		{
			base.Update ();

			if (falling) {
				myAddresses[0] = "/" + myIndex.ToString () + "/xz";
				myAddresses[1] = "/" + myIndex.ToString () + "/explode";
				_oscAddress = myAddresses [0];
				_Init ();
			} else {
				_oscAddress = myAddresses [1];
				_Init ();
			}
		}


		public override void OnOSCMessageReceived(UniOSCEventArgs args)
		{
			if(transformToMove == null) return;
			OscMessage msg = (OscMessage)args.Packet;
			if(msg.Data.Count <1)return;

			if (falling) {

				float x = transformToMove.transform.position.x;
				float y = transformToMove.transform.position.y;
				float z = transformToMove.transform.position.z;


				switch (movementMode) {

				case Mode.Screen:

					y = Screen.height * (float)msg.Data [0];

					if (msg.Data.Count >= 2) {
						x = Screen.width * (float)msg.Data [1];
					}

					pos = new Vector3 (x, y, Camera.main.nearClipPlane + nearClipPlaneOffset);
				//pos = transformToMove.transform.position; pos[0] = x;pos[1] = y;pos[2] = Camera.main.nearClipPlane + nearClipPlaneOffset;                   
					transformToMove.transform.position = Camera.main.ScreenToWorldPoint (pos);

					break;

				case Mode.Relative:
					z = 0f;
					x = (float)msg.Data [0];
					if (msg.Data.Count >= 2) {
						z = (float)msg.Data [1];
					}
					if (msg.Data.Count >= 3) {
						y = (float)msg.Data [2];
					}

					pos = new Vector3 (x, y, z);
					transformToMove.transform.position += pos; 
					break;

				}
			} else {
				Debug.Log ("Special Event");

			}


		}

	}

}