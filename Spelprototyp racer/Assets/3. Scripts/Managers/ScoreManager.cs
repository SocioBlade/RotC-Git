using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ScoreManager : MonoBehaviour {

    //Create a map looking like this:
    //
    // LIST OF USERS -> A User -> LIST OF SCORES for User

    //This structure "dictionary in dictionary" enables us to write stuff like
    //"userScores["SocioBlade"]["Laps"] = 2;"
    //"userScores["SocioBlade"]["Time"] = 12034;"
    Dictionary<string, Dictionary<string, float>> userScores;

    int changeCounter = 0;

    void Start()
    {
        SetScore("SocioBlade", "Laps", 0);
        SetScore("SocioBlade", "Time", 0.0f);

        SetScore("Odralix", "Laps", 2);
        SetScore("Odralix", "Time", 0.0f);

        SetScore("Ofelie", "Laps", 1);
        SetScore("Ofelie", "Time", 0.0f);

        Debug.Log(GetScore("SocioBlade", "Laps"));
    }

    void Init()
    {
        if (userScores != null)
            return;

        userScores = new Dictionary<string, Dictionary<string, float>>();
    }

    public float GetScore(string username, string scoreType)
    {
        Init();
        if(userScores.ContainsKey(username) == false)
        {
            return 0;
        }

        if(userScores[username].ContainsKey(scoreType) == false)
        {
            return 0;
        }
        return userScores[username][scoreType];
    }

    public void SetScore(string username, string scoreType, float value)
    {
        Init();

        changeCounter++;

        if(userScores.ContainsKey(username) == false)
        {
            userScores[username] = new Dictionary<string, float>();
        }
        userScores[username][scoreType] = value;
    }

    public void ChangeScore(string username, string scoreType, float amount)
    {
        Init();
        float currScore = GetScore(username, scoreType);
        SetScore(username, scoreType, currScore + amount);
    }

    public string[] GetPlayerNames()
    {
        Init();
        return userScores.Keys.ToArray();
    }

    public string[] GetPlayerNames(string sortingScoreType)
    {
        Init();
        return userScores.Keys.OrderByDescending(n => GetScore(n, sortingScoreType)).ToArray();
    }

    public void _DEBUG_ADD_LAPS_SOCIOBLADE()
    {
        ChangeScore("SocioBlade", "Laps", 1);
    }

    public void _DEBUG_ADD_TIME()
    {
        ChangeScore("SocioBlade", "Time", 2.43f);
    }

    public int GetChangeCounter()
    {
        return changeCounter;
    }


}
