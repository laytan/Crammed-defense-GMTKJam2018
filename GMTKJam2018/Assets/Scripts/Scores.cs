using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scores : MonoBehaviour {
    public GameObject timeLastedNumber, yourBestNumber;
	// Use this for initialization
	void Start () {
		if(!PlayerPrefs.HasKey("lastedTime") || !PlayerPrefs.HasKey("highscore"))
        {
            Debug.LogError("There is no value saved for lasted time or highscore!");
            return;
        }
        string lastedTime = PlayerPrefs.GetFloat("lastedTime").ToString();
        string highscore = PlayerPrefs.GetFloat("highscore").ToString();

        timeLastedNumber.GetComponent<TextMeshProUGUI>().text = lastedTime;
        yourBestNumber.GetComponent<TextMeshProUGUI>().text = highscore;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
