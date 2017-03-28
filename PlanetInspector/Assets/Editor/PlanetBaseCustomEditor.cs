using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlanetBase))]
public class PlanetBaseCustomEditor : Editor
{
    PlanetBase myTarget;
    void OnEnable()
    {
        myTarget = (PlanetBase)target;
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        //set center alignment for main segment labels 
        GUIStyle centerer = GUI.skin.label;
        centerer.alignment = TextAnchor.MiddleCenter;

        //System Level Planet Generation
        EditorGUILayout.BeginHorizontal("Box");
        EditorGUILayout.LabelField("System Level Planet Generation", centerer);
        //orbitTime(.1 - 300)
        //revTime
        //moonAmount(0-100)
        EditorGUILayout.EndHorizontal();

        //Planet Level Generation
        EditorGUILayout.BeginHorizontal("Box");
        EditorGUILayout.LabelField("Planet Level Generation", centerer);
        //planet size
        //mainColor
        //min max slider for low and high elevation
        //water
        EditorGUILayout.EndHorizontal();

        //Livability
        EditorGUILayout.BeginHorizontal("Box");
        EditorGUILayout.LabelField("Livability", centerer);
        //habitable
        //  hasFlora
        //      hasFauna
        //radiationAmount
        //min max slider for low and high temp
        //intelligentCreatues
        //  icPopulation
        EditorGUILayout.EndHorizontal();

    }
}
