using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_HoverController : MonoBehaviour
{
    //values for control
    public float acceleration;
    public float rotationRate;

    //values for faking a nice turn
    public float turnRotationAngle;
    public float turnRotationSeekSpeed;

    //reference variables
    private float rotationVelocity;
    private float groundAngleVelocity;

	// Update is called once per frame
	void Update ()
    {
		//Check if touch ground
        if(Physics.Raycast (transform.position, transform.up*-1, 3f))
        {
            //we are on ground; enamble accelerator and increase drag
            GetComponent<Rigidbody>().drag = 1;

            //Calc forwardForce
            Vector3 forwardForce = transform.forward * acceleration * Input.GetAxis("Vertical");
            //Correct force for deltatime and vehicle mass
            forwardForce = forwardForce * Time.deltaTime * GetComponent<Rigidbody>().mass;

            GetComponent<Rigidbody>().AddForce(forwardForce);
        }
        else
        {
            //We aren't on the ground and don't wanna halt in mid air
            GetComponent<Rigidbody>().drag = 0;
        }

        //You can turn on both air and ground:
        Vector3 turnTorque = Vector3.up * rotationRate * Input.GetAxis("Horizontal");
        //Force correction
        turnTorque = turnTorque * Time.deltaTime * GetComponent<Rigidbody>().mass;

        GetComponent<Rigidbody>().AddTorque(turnTorque);

        // "fake" rotation
        Vector3 newRotation = transform.eulerAngles;

        newRotation.z = Mathf.SmoothDampAngle(newRotation.z, Input.GetAxis("Horizontal") * -turnRotationAngle, ref rotationVelocity, turnRotationSeekSpeed);
        transform.eulerAngles = newRotation;
    }
}
