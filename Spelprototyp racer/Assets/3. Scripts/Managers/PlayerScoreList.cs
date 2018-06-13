using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerScoreList : MonoBehaviour {

    public GameObject playerScoreEntryPrefab;

    ScoreManager scoreManager;

    int lastChangeCounter;

	// Use this for initialization
	void Start () {
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();

        lastChangeCounter = scoreManager.GetChangeCounter();
	}
	
	// Update is called once per frame
	void Update () {
        if (scoreManager == null)
        {
            Debug.LogError("You forgot to add the score manager component to a game object");
            return;
        }

        if(scoreManager.GetChangeCounter() == lastChangeCounter)
        {
            //No change since lasst update!
            return;
        }

        lastChangeCounter = scoreManager.GetChangeCounter();

        while(this.transform.childCount > 0)
        {
            Transform c = this.transform.GetChild(0);
            c.SetParent(null); //Become Batman
            Destroy(c.gameObject);
        }

        string[] names = scoreManager.GetPlayerNames("Laps");

        foreach (string name in names)
        {
            GameObject go = (GameObject)Instantiate(playerScoreEntryPrefab);
            go.transform.SetParent(this.transform);
            go.transform.Find("Header: Username").GetComponent<Text>().text = name;
            go.transform.Find("Header: Laps").GetComponent<Text>().text = scoreManager.GetScore(name, "Laps").ToString();
            go.transform.Find("Header: Time").GetComponent<Text>().text = scoreManager.GetScore(name, "Time").ToString();
        }
    }
}
