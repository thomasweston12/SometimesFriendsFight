using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timerText_Script : MonoBehaviour {

    private GameManager gm;
    private Text timerText;
    private string display = "Time Left: ";

	// Use this for initialization
	void Start () {
        gm = GameObject.Find("GameManagerObject").GetComponent<GameManager>();
        timerText = GetComponent<Text>();
        gm.setIsTimerRunning(true);
    }

    // Update is called once per frame
    void Update () {

        if (gm.GetIsGameTimed() == true)
            timerText.text = ("Time Left: " + (int)(gm.getGameTimer() - gm.getGameCurrentTime()));
        else if (gm.GetIsGameTimed() == false)
            timerText.gameObject.SetActive(false);
        //Debug.Log(gm.getGameCurrentTime().ToString());
	}
}
