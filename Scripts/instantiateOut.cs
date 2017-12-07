/*
* UniOSC
* Copyright © 2014-2015 Stefan Schlupek
* All rights reserved
* info@monoflow.org
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
using OSCsharp.Data;


namespace UniOSC{

	/// <summary>
	/// Dispatcher button that forces a OSCConnection to send a OSC Message.
	/// Two separate states: Down and Up 
	/// </summary>
	[AddComponentMenu("UniOSC/EventDispatcherButton")]
	[ExecuteInEditMode]
	public class instantiateOut: UniOSCEventDispatcher {



		#region public
		 
		[HideInInspector]
		public static int objectIndexValue = 0;
		[HideInInspector]
		public UniOSCConnection [] phoneConnections;
		[HideInInspector]
		public static instantiateOut instance;
		public static int phoneIndex;
		#endregion

		#region private
		private bool _btnDown;
		private GUIStyle _gs; 
		#endregion

		public override void Awake()
		{
			base.Awake ();
			instance = this;


		}


		public override void OnEnable ()
		{
			base.OnEnable ();
			ClearData();
			AppendData(0f);




		}
		public override void OnDisable ()
		{
			base.OnDisable ();
		}


		void RenderGUI(){

			_gs = new GUIStyle (GUI.skin.button);
			_gs.fontSize = 11;
			//gs.padding = new RectOffset(2,2,2,2);

			GUIScaler.Begin ();

			Event e = Event.current;
		}

			
		public static void Trigger (string num) {
			Debug.Log ("Trigger");
			instance.phoneConnections = phoneSelector.phoneConnections;
			objectIndexValue = instantiateOSC.objectIndex;
			int i;
			if (Int32.TryParse(num, out i))
				Debug.Log("phone int" + i);
			else
				Debug.Log("String could not be parsed.");
			if (instance.phoneConnections != null) {
				instance._explicitConnection = instance.phoneConnections [i];
				instance.GoOSCMessage ();
			} else
				Debug.Log ("null phone connection");
		}

		/// <summary>
		/// Sends the OSC message with the upOSCDataValue.
		/// </summary>
		public void GoOSCMessage(){

			if(_OSCeArg.Packet is OscMessage)
			{
				Debug.Log ("Object Out" + _OSCeArg.Packet.Address);
				Debug.Log (_explicitConnection);
				((OscMessage)_OSCeArg.Packet).UpdateDataAt(0, objectIndexValue);
			}
			else if(_OSCeArg.Packet is OscBundle)
			{
				foreach (OscMessage m in ((OscBundle)_OSCeArg.Packet).Messages)
				{
					m.UpdateDataAt(0, objectIndexValue);
				}              
			}

			_SendOSCMessage(_OSCeArg);
		}


	}
}