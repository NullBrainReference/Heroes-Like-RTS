#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(HeroesHTTPClient))]
public class HeroesHTTPClient_Inspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        HeroesHTTPClient client = (HeroesHTTPClient)target;

        

        if (GUILayout.Button("Login"))
        {
            client.TryLogin();
        }

        if (GUILayout.Button("Save"))
        {
            client.TrySave();
        }

        if (GUILayout.Button("Load last"))
        {
            client.TryLoad();
        }
    }
}

#endif