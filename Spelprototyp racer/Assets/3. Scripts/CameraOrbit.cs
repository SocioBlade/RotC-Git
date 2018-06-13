using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour {

    public GameObject table; // The camera that will rotate 
    public float speed; // speep of rotation 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        OrbitAround();
    }

void OrbitAround()
    {
        transform.RotateAround(table.transform.position, Vector3.up, speed * Time.deltaTime);
        // Makes the camera rotate around the table 
    }


}