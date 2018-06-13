using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NrOfCheckpoints))]
public class CheckpointWindow : Editor {

    public int CheckNr = 0;
    CheckList tries;
    public List<GameObject> checkpoints2 = new List<GameObject>();

    public override void OnInspectorGUI()
    {
        GUILayout.TextArea("Checkpoints");
        CheckNr = EditorGUILayout.IntSlider(CheckNr, 1, 100);
        if( GUILayout.Button("Create Checkoints") )
        {
            Debug.Log("Create");
            DeleteCheckpoints();
            CreateCheckpoints(CheckNr);

        }
        if ( GUILayout.Button("Create Checkpoint List"))
        {
            //checkpoints2 = tries.CreateTheList();
            CreateList();
        }

        if ( GUILayout.Button("GIZMO") )
        {
            Debug.Log("Gizmo");
            GameObject.Find("Checkpoints").GetComponent<CheckList>().CallGiz();

        }    
    }

    void CreateCheckpoints(int x)
    {
        for (int i = 0; i < x; ++i)
        {
            GameObject checkpoint = GameObject.CreatePrimitive(PrimitiveType.Cube);
            checkpoint.transform.position = new Vector3(5 * i, 1, 1);
            checkpoint.tag = "Checkpoint";
            checkpoint.name = "Checkpoint" + i;
            checkpoint.GetComponent<Collider>().isTrigger = true;
            
        }
    }

    void DeleteCheckpoints()
    {
        GameObject[] tempArr;
        int tempNr;

        tempArr = GameObject.FindGameObjectsWithTag("Checkpoint");
        tempNr = tempArr.Length;

        foreach (GameObject go in tempArr)
        {
            DestroyImmediate(go);
        }
    }

    void CreateList()
    {
        Debug.Log("HEJ!");
        checkpoints2 = GameObject.Find("Checkpoints").GetComponent<CheckList>().CreateTheList();
    }

}