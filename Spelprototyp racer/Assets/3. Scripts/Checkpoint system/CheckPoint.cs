using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

    public int cPassed = 0; 
    public Vector3 currentPos;
    public Quaternion currentRot;
    public GameObject listHolder;
    public GameOverManager gameOverManager;

    private void OnTriggerEnter(Collider other)
    {
        //Get the checkpoint element in the list. and put the the element nr in index
        if (other.CompareTag("Checkpoint"))
        {
			Debug.Log ("CHECK");
			if (cPassed < listHolder.GetComponent<CheckList> ().nrOfCheckpoints) 
			{
			
				if (other.gameObject == listHolder.GetComponent<CheckList> ().checkpoints [cPassed]) 
				{
					// Gets the checkpoints location and increase current checkpoint by 1 
					GetLocation (other);
					cPassed += 1;
					print ("HIT");

                    if(cPassed == listHolder.GetComponent<CheckList>().nrOfCheckpoints)
                    {
                        gameOverManager.winScreen();
                    }
                    for(int i = 0; i < (listHolder.GetComponent<CheckList>().nrOfLaps - 1); i++)
                    {
                        if(cPassed == listHolder.GetComponent<CheckList>().LapNrArr[i])
                        {
                            listHolder.GetComponent<CheckList>().currentLap += 1; 
                            Debug.Log("A NEW LAP!");
                           
                        }
                    }
				}
			}
        }

    }
    private void GetLocation(Collider checkpoint)
    {
        currentPos = checkpoint.transform.parent.position;
        currentRot = checkpoint.transform.parent.rotation;
		//Change respawn
		gameObject.GetComponent<O_RespawnScript> ().checkChange (currentPos, currentRot);
    }
}
