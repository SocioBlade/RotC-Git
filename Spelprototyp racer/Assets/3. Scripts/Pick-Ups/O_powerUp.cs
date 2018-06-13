using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_powerUp : MonoBehaviour
{
    public GameObject pickupEffect;
    //public PlayerStats stat;
    public int type = 0;
    public float duration = 4f;
	public float multiplier = 1.5f;
	public GameObject timeRef;

	private GameObject clone;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            StartCoroutine( Pickup(other) );
        }
    }

    IEnumerator Pickup(Collider player)
    {
        //Spawn Cool Effect
        clone = Instantiate(pickupEffect, transform.position, transform.rotation);

        //Waiting allows for the powerUp object to still be visible. This removes it.

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        //pickupEffect.transform.SetParent(gameObject.transform);

        Movement acc = player.GetComponent<Movement>();
        //Apply effect to the player
		if (type == 0) {

			//acc.thrustForce *= multiplier;
		} else if ((type == 1) && (player.GetComponent<sCollisionScript> ().hitCount != 0)) {
			player.GetComponent<sCollisionScript> ().hitCount -= 1;

			if (player.GetComponent<sCollisionScript> ().hitCount == 0) {
				player.GetComponentInChildren<MeshRenderer> ().material = player.GetComponent<sCollisionScript> ().normalMat;
			} else if (player.GetComponent<sCollisionScript> ().hitCount == 1) {
				player.GetComponentInChildren<MeshRenderer> ().material = player.GetComponent<sCollisionScript> ().newMaterialref;
			}
		} 
		else if (type == 2) 
		{
            player.transform.forward.Set(0, 0, 0);
			acc.thrustForce /= multiplier;
            acc.drag *= multiplier;
		}
		else if (type == 3)
		{
			timeRef.GetComponent<CountDownTimer>().timer -= multiplier;
		}

        //wait x amount of seconds

        yield return new WaitForSeconds(1f);

        //Stops the particleSystem from repeating itself during the duration. 
        //Must be placed after small delay as putting it higher destroys the system before it is seen.
        clone.GetComponent<ParticleSystem>().Stop();

        yield return new WaitForSeconds(duration-1);


        //reverse effect on our player
		if (type == 0) 
		{
			//acc.thrustForce /= multiplier;
		} 
		else if (type == 2) 
		{
			acc.thrustForce *= multiplier;
            acc.drag /= multiplier;
        }

        //Remove power up object
        Destroy(clone);
        Destroy(gameObject);

    }
}
