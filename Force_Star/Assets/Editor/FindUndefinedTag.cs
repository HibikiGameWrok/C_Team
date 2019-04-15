using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class FindUndefinedTag : EditorWindow
{
    public bool LimitResultCount = false;
    public int MaxResults = 1;

    public List<GameObject> Results = new List<GameObject>();
    private Vector2 ResultScrollPos;

    void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        {
            if (GUILayout.Button("Find Undefined Tag Object"))
                Find();
            if (GUILayout.Button("Fix Undefined Tag To Untagged"))
                Fix();
            if (LimitResultCount = EditorGUILayout.Foldout(LimitResultCount, "Limit Result Count (Limit:"
                    + (LimitResultCount ? MaxResults.ToString() : "None") + ")"))
                MaxResults = EditorGUILayout.IntField("Result Max:", MaxResults);

            EditorGUILayout.LabelField("Results", EditorStyles.boldLabel);
            {
                if (Results != null)
                {
                    EditorGUILayout.LabelField("Objects found:", Results.Count.ToString(), EditorStyles.boldLabel);

                    ResultScrollPos = EditorGUILayout.BeginScrollView(ResultScrollPos);
                    {
                        if (LimitResultCount)
                        {
                            for (int i = 0; i < Mathf.Min(MaxResults, Results.Count); i++)
                                EditorGUILayout.ObjectField(Results[i], typeof(GameObject), false);
                        }
                        else
                        {
                            foreach (GameObject go in Results)
                                EditorGUILayout.ObjectField(go, typeof(GameObject), false);
                        }
                    }
                    EditorGUILayout.EndScrollView();
                }
            }
        }
        EditorGUILayout.EndVertical();
    }

    void Find()
    {
        Results.Clear();

        // Search In Project View   
        List<GameObject> ResultsTmp = Resources.FindObjectsOfTypeAll(typeof(GameObject)).Cast<GameObject>().ToList();

        GameObject goTmp = null;

        for (int i = 0; i < ResultsTmp.Count; i++)
        {
            try
            {
                goTmp = ResultsTmp[i];

                // Dummy Check  
                if (ResultsTmp[i].tag == "Untagged") { }
            }
            catch (UnityException e)
            {
                // Catch Undefined Tag  
                Results.Add(goTmp);
                //Debug.Log(e.Message);  
            }
        }

        // Search In Hierarchy View   
        ResultsTmp.Clear();
        ResultsTmp = GameObject.FindObjectsOfTypeAll(typeof(GameObject)).Cast<GameObject>().ToList();

        for (int i = 0; i < ResultsTmp.Count; i++)
        {
            try
            {
                goTmp = ResultsTmp[i];

                // Dummy Check  
                if (ResultsTmp[i].tag == "Untagged") { }
            }
            catch (UnityException e)
            {
                // Catch Undefined Tag  
                Results.Add(goTmp);
                //Debug.Log(e.Message);  
            }
        }
    }

    void Fix()
    {
        for (int i = 0; i < Results.Count; i++)
        {
            Results[i].tag = "Untagged";
        }
    }

    [MenuItem("Tools/Find Undefined Tag...")]
    static void Init()
    {
        FindUndefinedTag window = EditorWindow.GetWindow<FindUndefinedTag>("Find Undefined Tag");

        window.ShowPopup();
    }
}