using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CountUpTimer : MonoBehaviour
{
    public CountDownTimer gameTimer;
    public float playerTimer = 0f;
    private Text timerUpSeconds;
    public int endCheck = 0;
    // Use this for initialization
    void Start()
    {

        timerUpSeconds = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(endCheck == 0) {
            if (gameTimer.timer != 0)
            {
                playerTimer += Time.deltaTime;
                timerUpSeconds.text = playerTimer.ToString("f2");
            }
        }
        else
        {
            playerTimer += 0;
        }
        
        
    }
}
