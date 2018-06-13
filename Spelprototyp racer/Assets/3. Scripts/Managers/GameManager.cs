//GameManager handles the timing in the game. Responsible for 
//UI behavior

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //GameManager holds a public static reference to itself.
    //Therefore it is an "singleton" which allows it to be accessed from
    //all other objects in the scene.
    public static GameManager instance;

    [Header("Race Settings")]
    public int nrOfLaps = 3;            //Number of laps needed to complete
    public Movement bookMovement;   //Referece to the books movement script

    bool isGameOver;
    bool raceStarted;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
    /*
    void OnEnable()
    {
        //This coroutine allows for potential cutscene intros.
        StartCoroutine(Init());    
    }
    */
    IEnumerator Init()
    {
        //Wait a litte while for initialisation
        yield return new WaitForSeconds(.1f);

        raceStarted = true;
    }
    // Update is called once per frame
    void Update()
    {

    }

    public bool gameActive()
    {
        return raceStarted && !isGameOver;
    }

}
