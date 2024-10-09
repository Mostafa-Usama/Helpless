using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class editorScript : EditorWindow{

    [MenuItem("tools/vehicle")]

    public static void Open(){
        GetWindow<editorScript>();
    }


    public GameObject car;

    void OnGUI(){

        // car Game Object
        displayValue("car" );

    }

    void displayValue(string a ){
        SerializedObject i = new SerializedObject(this);
        EditorGUILayout.PropertyField(i.FindProperty(a));
        i.ApplyModifiedProperties();
    }



}