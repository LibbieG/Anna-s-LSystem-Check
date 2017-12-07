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


		protected SerializedProperty phoneConnectionsProp;



		override public void OnEnable () {
			base.OnEnable();

			phoneConnectionsProp = serializedObject.FindProperty ("phoneConnections");

		}

		override public void OnInspectorGUI(){

			serializedObject.Update();
			EditorGUI.BeginChangeCheck();

			drawDefaultInspectorProp.boolValue = false;//drawDefaultInspectorProp is defined in the base editor
			base.OnInspectorGUI();

			ArrayGUI();

			//EditorGUILayout.PropertyField(phoneConnectionsProp,new GUIContent("Phone Connection","OSC data value that is send when button is pushed. Should be normally 1") );
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

		private void ArrayGUI(){
			for(int i=0; i<phoneConnectionsProp.arraySize; i++){
				EditorGUILayout.PropertyField(phoneConnectionsProp.GetArrayElementAtIndex(i),new GUIContent("Phone Connection " + (i).ToString() + ": "));
			}

			GUILayout.BeginHorizontal();
			if(GUILayout.Button("Add Conection")){
				phoneConnectionsProp.arraySize++;
			}
			if(GUILayout.Button("Remove Connection")){
				phoneConnectionsProp.arraySize--;
			}
			GUILayout.EndHorizontal();
		}

	}
}
