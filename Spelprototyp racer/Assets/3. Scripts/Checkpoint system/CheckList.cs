using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckList : MonoBehaviour {

    public int nrOfCheckpoints = 0;
    public int currentCheckpoint = 0;
    public Vector3 startPos;
    public int currentLap;
    public int nrOfLaps;
    public int[] LapNrArr;
    public GameObject[] arr;
	public GameObject firstCheck;
    public List<GameObject> checkpoints = new List<GameObject>();

    // Use this for initialization
    public List<GameObject> CreateTheList () 
	{
        checkpoints = new List<GameObject>();

        Debug.Log("DÅ! :3");
        firstCheck = GameObject.Find("Checkpoint0");

        // Player start position
        startPos = transform.position;
        currentCheckpoint = 0;
        nrOfLaps = 3;
        currentLap = 1;
        

        // Find all checkpoint gameobjects, placed in arr and then over to List.
        arr = GameObject.FindGameObjectsWithTag("Checkpoint");
        nrOfCheckpoints = arr.Length;
        print(nrOfCheckpoints);

        for (int i = 0; i < nrOfCheckpoints; i++)
        {
            checkpoints.Add(arr[i]);
        }

		//Ensure that the first object in the list is the first checkpoint.
		//This is a requirement for listCorrect
        for (int i = 0; i < nrOfCheckpoints; i++)
        {
			if(firstCheck == checkpoints[i])
            {
				checkpoints.RemoveAt(i);
				checkpoints.Insert(0, firstCheck);
            }
        }

		//Orders the checkpoints correctly in the list as long as the closest checkpoint is the next one.
		listCorrect (checkpoints);

		for (int i = 0; i < nrOfCheckpoints; i++)
		{
			Debug.Log (checkpoints [i]);
		}

        LapCheckpoint();
        return checkpoints;

    }

	private void listCorrect(List<GameObject> checkList)
	{
        Debug.Log("RE!");
		float shortestLine = 500000f;
		float tmpVal = 5000f;
		int nextCheckpoint = 554;

		for (int i = 0; i < nrOfCheckpoints; i++)
		{
			shortestLine = 500000f;
			for (int j = i+1; j < nrOfCheckpoints; j++) 
			{
				tmpVal = Vector3.Distance (checkList [i].transform.position, checkList [j].transform.position);

				if (tmpVal < shortestLine) 
				{
					shortestLine = tmpVal;
					nextCheckpoint = j;
				}

				if (j == nrOfCheckpoints - 1)
				{
					GameObject tmp;
					tmp = checkList [nextCheckpoint];
					checkList.RemoveAt (nextCheckpoint);
					checkList.Insert(i+1,tmp);
				}
			}
		}
	}

    public void CallGiz()
    {
        gameObject.GetComponent<GizmosDraw>().assignChecks(checkpoints, nrOfCheckpoints);
    }

    void LapCheckpoint()
    {
        Debug.Log("N!");
        int temp;
        int temp2;

        temp = nrOfCheckpoints / nrOfLaps;
        print (temp);
        temp2 = nrOfLaps - 1;

        LapNrArr = new int [temp2];

        int next = 0;

        for(int i = 0; i < temp2; i++)
        {
            LapNrArr[i] = (next + temp);

            next += temp;

            print(LapNrArr[i]);
        }

    }
}
