using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EmisiveSopt))]

public class CubeEditor : Editor
{
    public override void OnInspectorGUI(){

        base.OnInspectorGUI();

        EmisiveSopt cube = (EmisiveSopt)target;

        if (GUILayout.Button("change color")){
            cube.Color();
        }

   }
}
