using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_ThrusterScript : MonoBehaviour {

    public float thrusterStength;
    public float thrusterDistance;
    public Transform[] thrusters;
	// Update is called once per frame
	void FixedUpdate ()
    {
        RaycastHit hit;
        
        foreach(Transform thruster in thrusters)
        {
            Vector3 downwardForce;
            float distancePercentage;

            if(Physics.Raycast (thruster.position, thruster.up *-1, out hit, thrusterDistance))
            {
                //The thruster is within thrusterdistance to the ground. How far away?
                distancePercentage = 1 - (hit.distance / thrusterDistance);

                //calculate how hard to push
                downwardForce = transform.up * thrusterStength * distancePercentage;
                // Correct it for the mass of the mesh and deltatime
                downwardForce = downwardForce * Time.deltaTime * GetComponent<Rigidbody>().mass;

                //apply at thruster location
                GetComponent<Rigidbody>().AddForceAtPosition(downwardForce, thruster.position);
            }
        }
	}
}
