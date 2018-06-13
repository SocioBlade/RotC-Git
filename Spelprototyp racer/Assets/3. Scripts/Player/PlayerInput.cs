using UnityEngine;

public class PlayerInput : MonoBehaviour {

    public string verticalAxis = "Vertical";        //Name of the truster axis
    public string horizontalAxis = "Horizontal";    //Name of the rudder axis
    public string brakeKey = "Brake";                   //Name of the brake button

    //"HideInInspector" makes the variables untoucheable in the editor
    [HideInInspector] public float thruster;
    [HideInInspector] public float rudder;
    [HideInInspector] public bool isBrakeing;

	// Update is called once per frame
	void Update () {

        //If the GameManager exists and the game isn't active...
        if (GameManager.instance != null && !GameManager.instance.gameActive())
        {
            //...set all inputs to neutral values.
            thruster = 0f; 
            rudder = 0f;
            isBrakeing = false;
            return;
        }

        thruster = Input.GetAxis(verticalAxis);
        rudder = Input.GetAxis(horizontalAxis);
        isBrakeing = Input.GetButton(brakeKey);
	}
}
