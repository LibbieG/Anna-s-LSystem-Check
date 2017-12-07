/*
* UniOSC
* Copyright © 2014-2015 Stefan Schlupek
* All rights reserved
* info@monoflow.org
*/
using UnityEngine;
using UnityEditor;
using System.Collections;

namespace UniOSC{

	/// <summary>
	/// Uni OSC event dispatcher button editor.
	/// </summary>
	[CustomEditor(typeof(instantiateOut))]
	[CanEditMultipleObjects]
	public class instantiateOutEditor : UniOSCEventDispatcherEditor {


		protected SerializedProperty controlObjectProp;




		override public void OnEnable () {
			base.OnEnable();

			//controlObjectProp = serializedObject.FindProperty ("controlObject");

		}

		override public void OnInspectorGUI(){

			serializedObject.Update();
			EditorGUI.BeginChangeCheck();

			drawDefaultInspectorProp.boolValue = false;//drawDefaultInspectorProp is defined in the base editor
			base.OnInspectorGUI();



			//EditorGUILayout.PropertyField(controlObjectProp,new GUIContent("Control Object","") );
			//EditorGUILayout.PropertyField(upOSCDataValueProp,new GUIContent("Up data value","OSC data value that is send when button is released. Should be normally 0") );
			//EditorGUILayout.PropertyField(phoneConnectionsProp, true);
		




			serializedObject.ApplyModifiedProperties();

			if(EditorGUI.EndChangeCheck()){
				//update data (EditorUtility.SetDirty(_target) doesn't work)
				_target.ForceSetupChange(true);
				_target.enabled = !_target.enabled;
				//_target.enabled = !_target.enabled;


				EditorGUIUtility.LookLikeControls();
			}

		}
			

	}
}
