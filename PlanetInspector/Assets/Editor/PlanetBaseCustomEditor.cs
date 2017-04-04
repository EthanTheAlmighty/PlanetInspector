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
        serializedObject.Update();
        //base.OnInspectorGUI();
        //set center alignment for main segment labels 
        GUIStyle centerer = GUI.skin.label;
        centerer.alignment = TextAnchor.MiddleCenter;

        //setting a guicontent for tooltips
        GUIContent tip = new GUIContent();

        //System Level Planet Generation
        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.LabelField("System Level Planet Generation", centerer);
        //orbitTime(.1 - 300)
        tip.text = "Orbit Time (days)";
        tip.tooltip = "How many planet days does it take to oribit (planet year), must be above zero";
        myTarget.orbitTime = EditorGUILayout.Slider(tip, myTarget.orbitTime, 1, 25000);
        //revTime (4-100)
        tip.text = "Revolution Time (hrs)";
        tip.tooltip = "How many hours a day cycle takes";
        myTarget.revolutionTime = EditorGUILayout.Slider(tip, myTarget.revolutionTime, 4, 100);
        //moonAmount(0-100)
        tip.text = "Moons";
        tip.tooltip = "Numner of moons orbiting the planet";
        myTarget.moonAmount = EditorGUILayout.IntSlider(tip, myTarget.moonAmount, 0, 100);
        EditorGUILayout.EndVertical();

        //Planet Level Generation
        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.LabelField("Planet Level Generation", centerer);
        //planet size
        tip.text = "Planet Size (miles)";
        tip.tooltip = "Size of the planet in miles, for reference earth's size is about 4,000 miles";
        myTarget.planetSize = EditorGUILayout.IntSlider(tip, myTarget.planetSize, 100, 80000);
        //mainColor
        tip.text = "Planet Color";
        tip.tooltip = "Main color of the planet";
        myTarget.mainColor = EditorGUILayout.ColorField(tip, myTarget.mainColor);
        //min max slider for low and high elevation
        tip.text = "Elevation Range (miles)";
        tip.tooltip = "The minimum and maximum elevations, in miles, that this planet will have";
        EditorGUILayout.LabelField(tip, centerer);
        EditorGUILayout.BeginHorizontal();
        myTarget.lowElevation = EditorGUILayout.FloatField(Mathf.Clamp(myTarget.lowElevation, -1000, myTarget.highElevation - 1));
        EditorGUILayout.MinMaxSlider(ref myTarget.lowElevation, ref myTarget.highElevation, -1000, 1000);
        myTarget.highElevation = EditorGUILayout.FloatField(Mathf.Clamp(myTarget.highElevation, myTarget.lowElevation + 1, 1000));
        EditorGUILayout.EndHorizontal();
        //water
        tip.text = "Water";
        tip.tooltip = "Whether or not this planet has water";
        myTarget.hasWater = EditorGUILayout.Toggle(tip, myTarget.hasWater);
        //elements
        tip.text = "Main Elements";
        tip.tooltip = "Each of the main elements found on the planet";
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(10);
        SerializedProperty elements = serializedObject.FindProperty("mainElements");
        EditorGUILayout.PropertyField(elements, tip, true);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();

        //Livability
        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.LabelField("Livability", centerer);
        //habitable
        tip.text = "Habitable";
        tip.tooltip = "If this planet is habitable or not";
        myTarget.isHabitable = EditorGUILayout.Toggle(tip, myTarget.isHabitable);
        //  hasFlora
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(15);
        tip.text = "Flora";
        tip.tooltip = "If this planet supports plant life";
        GUI.enabled = (myTarget.isHabitable);
        myTarget.flora = EditorGUILayout.Toggle(tip, myTarget.flora);
        GUI.enabled = true;
        EditorGUILayout.EndHorizontal();
        //      hasFauna
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(30);
        tip.text = "Fauna";
        tip.tooltip = "If this planet supports animal life";
        GUI.enabled = (myTarget.isHabitable && myTarget.flora);
        myTarget.fauna = EditorGUILayout.Toggle(tip, myTarget.fauna);
        GUI.enabled = true;
        EditorGUILayout.EndHorizontal();
        //radiationAmount
        tip.text = "Radiation Amount (rads)";
        tip.tooltip = "Amount of radiation in rads";
        myTarget.radiationAmount = EditorGUILayout.Slider(tip, myTarget.radiationAmount, 0, 50);
        //min max slider for low and high temp
        tip.text = "Temperature Range (°C)";
        tip.tooltip = "The low and high temperatures on the planet in degrees celsius";
        EditorGUILayout.LabelField(tip, centerer);
        EditorGUILayout.BeginHorizontal();
        myTarget.lowTemp = EditorGUILayout.FloatField(Mathf.Clamp(myTarget.lowTemp, -273, myTarget.highTemp - 1));
        EditorGUILayout.MinMaxSlider(ref myTarget.lowTemp, ref myTarget.highTemp, -273, 500);
        myTarget.highTemp = EditorGUILayout.FloatField(Mathf.Clamp(myTarget.highTemp, myTarget.lowTemp + 1, 500));
        EditorGUILayout.EndHorizontal();
        //intelligentCreatues
        tip.text = "Intelligent Life";
        tip.tooltip = "If there's intelligent life on this planet, may or may not be native";
        myTarget.intelligentCreatures = EditorGUILayout.Toggle(tip, myTarget.intelligentCreatures);
        //  icPopulation
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(15);
        tip.text = "Population";
        tip.tooltip = "The size of the intelligent population";
        GUI.enabled = (myTarget.intelligentCreatures);
        myTarget.icPopulation = EditorGUILayout.IntField(tip, Mathf.Max(0, myTarget.icPopulation));
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();

        //checks for habitable stuffs
        if(!myTarget.isHabitable)
        {
            myTarget.flora = false;
            myTarget.fauna = false;
        }
        //check for the flora level
        if(!myTarget.flora)
        {
            myTarget.fauna = false;
        }
        //check for intelligent population
        if(!myTarget.intelligentCreatures)
        {
            myTarget.icPopulation = 0;
        }

        serializedObject.ApplyModifiedProperties();
        
    }
}
