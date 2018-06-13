using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_RespawnScript : MonoBehaviour {

	public Vector3 respawnLoc;
	public Quaternion respawnRot;
	public GameObject cam;
    public float origSpeed;
	bool respawned = false;
	private bool checkP= false;
	// Use this for initialization
	void Start () 
	{
		respawnLoc = gameObject.GetComponent<Transform> ().position;
		respawnRot = gameObject.GetComponent<Transform> ().rotation;
		respawnLoc.y += 10;
        origSpeed = gameObject.GetComponent<Movement>().thrustForce;


    }

	public void Respawn()
	{
		respawned = true;
		gameObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		gameObject.GetComponent<Movement> ().thrustForce = origSpeed;
		gameObject.GetComponent<Transform>().SetPositionAndRotation(respawnLoc, respawnRot);
		if (checkP == true) 
		{
			gameObject.GetComponent<Transform>().Rotate(0f,0f,0f);
		}
		gameObject.GetComponent<sCollisionScript> ().hitCount = 0;
        gameObject.GetComponent<sCollisionScript>().respawning = false;
        GetComponentInChildren<MeshRenderer> ().material = gameObject.GetComponent<sCollisionScript> ().normalMat;
		gameObject.GetComponent<StockpileSpdPU> ().Reset ();
		respawned = false;
	}

	public void checkChange(Vector3 loc, Quaternion rot)
	{
		respawnLoc = loc;
		respawnRot = rot;
		checkP = true;
	}
}

