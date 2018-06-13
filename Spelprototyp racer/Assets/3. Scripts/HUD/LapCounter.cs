using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LapCounter : MonoBehaviour {

    public int CurrentLap = 1;
    private Text LapNR;
    public GameObject listRef;
    public Image first, second, third;

	// Use this for initialization
	void Start () {

        LapNR = GetComponent<Text>();
        first.enabled = true;
        second.enabled = false;
        third.enabled = false;

	}
	
	// Update is called once per frame
	void Update () {

        CurrentLap = listRef.GetComponent<CheckList>().currentLap;
        LapNR.text = CurrentLap.ToString("");

        if(CurrentLap == 2)
        {
            second.enabled = true;
        }
        if(CurrentLap == 3)
        {
            third.enabled = true;
        }
	}
}
