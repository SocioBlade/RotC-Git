using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheSelectedBook : MonoBehaviour {

    private GameObject[] lvlCharacterList;

	// Use this for initialization
	void Start ()
    {

        int index = PlayerPrefs.GetInt("CharacterSelected");

        lvlCharacterList = new GameObject[transform.childCount];

        //Fill the array with the models 
        for (int i = 0; i < transform.childCount; i++)
        {
            lvlCharacterList[i] = transform.GetChild(i).gameObject;
        }

        // toggle off their renderer 
        foreach (GameObject go in lvlCharacterList)
        {
            go.SetActive(false);
        }

        //Toggle in the first //selected character
        if (lvlCharacterList[index])
        {
            lvlCharacterList[index].SetActive(true);
        }
    }

}
