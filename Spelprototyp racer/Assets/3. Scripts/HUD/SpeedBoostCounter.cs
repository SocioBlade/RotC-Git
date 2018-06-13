using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SpeedBoostCounter : MonoBehaviour
{

    public int nrOfBoost = 0;
    private Text nrBoost;
    public GameObject listRef;
    public Image one, two, three;

    // Use this for initialization
    void Start()
    {

        nrBoost = GetComponent<Text>();

        one.enabled = false;
        two.enabled = false;
        three.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {

        nrOfBoost = listRef.GetComponent<StockpileSpdPU>().nrOfBoost;
        //nrBoost.text = nrOfBoost.ToString("");

        if (nrOfBoost == 0)
        {
            one.enabled = false;
            two.enabled = false;
            three.enabled = false;
        }
        if (nrOfBoost == 1)
        {
            one.enabled = true;
            two.enabled = false;
            three.enabled = false;
        }
        if (nrOfBoost == 2)
        {
            one.enabled = true;
            two.enabled = true;
            three.enabled = false;
        }
        if (nrOfBoost == 3)
        {
            one.enabled = true;
            two.enabled = true;
            three.enabled = true;
        }
    }
}
