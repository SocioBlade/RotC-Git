using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMove : MonoBehaviour
{
    public GameObject target;
    public GameObject timer;
    private float speed;
    // Use this for initialization
    void Start ()
    {
        float time = timer.GetComponent<CountDownTimer>().timer;
        //s=v*t
        float distance = Vector3.Distance(target.GetComponent<Transform>().position, transform.position);
        speed = distance / time;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(Vector3.forward * speed*0.9f* Time.deltaTime);
    }
}
