using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CountDownTimer : MonoBehaviour {

    public float timer = 10f;
    private Text timerSeconds; 

	// Use this for initialization
	void Start () {

        timerSeconds = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {

        timer -= Time.deltaTime;
        timerSeconds.text = timer.ToString("f2");

        if(timer <= 0)
        {
            
            timer = 0f;
            timerSeconds.text = timer.ToString("f2");
            
            // Game over animation and screen. 

        }

	}
}
