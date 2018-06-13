using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockpileSpdPU : MonoBehaviour 
{

	public float[] stockpileArr = { 0.0f, 0.0f, 0.0f };
	private float[] durArr = { 0.0f, 0.0f, 0.0f };

	private bool arrIsFull = false;
	public bool[] isFull = {false,false,false};
	private O_powerUp pRef;
	private bool isPowered = false;
    public int nrOfBoost = 0;

	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("SpowerUp"))
		{
			stockpile(other);
		}
	}
		
	void stockpile(Collider powerUp)
	{

		if ((arrIsFull == false) && (powerUp.GetComponent<O_powerUp> ().type == 0))
		{
			pRef = powerUp.GetComponent<O_powerUp> ();

			for(int i=0; i<3; i++)
			{
				if(isFull[i] == false )
				{
					stockpileArr [i] = pRef.multiplier;
					durArr [i] = pRef.duration;
					isFull [i] = true;
                    nrOfBoost += 1;
					i = 3;
				}
			}
			if((stockpileArr[0] > 0.0f) && (stockpileArr[1] > 0.0f) && (stockpileArr[2] > 0.0f))
			{
			arrIsFull = true;	
			}
		}
	}

	IEnumerator applySpeed()
	{
		float tmpMult = stockpileArr[0];
		float tmpDur = durArr [0];
		gameObject.GetComponent<Movement> ().thrustForce *= tmpMult;
		isPowered = true;

		arrIsFull = false;
		stockpileArr [0] = stockpileArr [1];
		stockpileArr [1] = stockpileArr [2];

		durArr [0] = durArr [1];
		durArr [1] = durArr [2];

		//If the slot under the above one is false then the above one is false as well since everything moved up a step.
		if(isFull[1] == true)
		{
			if(isFull[2] == true)
			{
				isFull [1] = true;
				stockpileArr [2] = 0.0f;
				durArr [2] = 0.0f;
				isFull [2] = false;
                nrOfBoost = nrOfBoost - 1;
            }
			else
			{
			isFull [1] = false;
                nrOfBoost = nrOfBoost - 1;
			}
		}
		else
		{
			isFull [0] = false;
            nrOfBoost = nrOfBoost - 1;
        }

		yield return new WaitForSeconds (tmpDur);
		if (gameObject.GetComponent<sCollisionScript> ().respawning == false) 
		{
            if (gameObject.GetComponent<O_RespawnScript>().origSpeed != gameObject.GetComponent<Movement>().thrustForce)
            {
                gameObject.GetComponent<Movement>().thrustForce /= tmpMult;
                isPowered = false;
            }
		}
		gameObject.GetComponent<sCollisionScript> ().respawning = false;
	}

	private void FixedUpdate()
	{
		
		if((Input.GetKeyDown("space") == true) && (isPowered == false))
		{
			if(isFull[0] == true)
			{
				StartCoroutine (applySpeed ());
			}
		}
	}

	public void Reset()
	{
		for(int i=0; i< 3; i++)
		{
			stockpileArr[i] = 0.0f;
			durArr[i] = 0.0f;
			isFull [i] = false;
		}
        nrOfBoost = 0;
		arrIsFull = false;
		isPowered = false;
	}

}	
