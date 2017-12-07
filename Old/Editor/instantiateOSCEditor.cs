/*
* UniOSC
* Copyright © 2014-2015 Stefan Schlupek
* All rights reserved
* info@monoflow.org
*/
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

namespace UniOSC{

	[CustomEditor(typeof(instantiateOSC))]
	[CanEditMultipleObjects]
	public class instantiateOSCEditor : UniOSCEventTargetEditor {


		protected bool _updateFlag;


		public override void OnEnable () {
			base.OnEnable();

		}



		override public void OnInspectorGUI(){
			GUILayout.Space(5f);
			if(_tex_logo != null){
				UniOSCUtils.DrawClickableTextureHorizontal(_tex_logo,()=>{EditorApplication.ExecuteMenuItem(UniOSCUtils.MENUITEM_EDITOR);});
			}
			//EditorGUIUtility.LookLikeControls(150f,50f);
			EditorGUIUtility.labelWidth =  150f;
			EditorGUIUtility.fieldWidth =  50f;

			DrawDefaultInspector ();
			GUILayout.Space(5f);

			serializedObject.Update();
			EditorGUI.BeginChangeCheck();


			EditorGUILayout.PropertyField(OSCAddressProp,new GUIContent("OSC Address","") );


			DrawConnectionSetup();

			DrawConnectionInfo();

			serializedObject.ApplyModifiedProperties();

			if(EditorGUI.EndChangeCheck() || _updateFlag){

				ForceUpdate();
			}


		}

		protected void ForceUpdate(){

			_updateFlag = false;
		}


	}
}