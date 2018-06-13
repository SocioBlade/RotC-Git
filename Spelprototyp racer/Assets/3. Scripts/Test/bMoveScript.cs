using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bMoveScript : MonoBehaviour
{

    public float moveSpeed;
    public float rotSpeed;
    //public Rigidbody rb;

    // Use this for initialization
    void Start ()
    {
        moveSpeed = 10f;
        rotSpeed = 50f;

    }

    // Update is called once per frame
    void Update ()
    {
        //rb.AddForce(moveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime, 0f, moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime);
        transform.Translate(moveSpeed*Input.GetAxis("Horizontal") * Time.deltaTime, 0f, moveSpeed*Input.GetAxis("Vertical")* Time.deltaTime);
        if(Input.GetKey("x"))
        {
            transform.Rotate(0f, rotSpeed* Time.deltaTime, 0f);
        }
        else if(Input.GetKey("z"))
        {
            transform.Rotate(0f, -rotSpeed * Time.deltaTime, 0f);
        }
    }
}
