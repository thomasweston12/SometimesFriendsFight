using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timer_Script : MonoBehaviour {

    private bool isTimerRunning;
    private float waitTime;
    private float timerCurrent;

    

	// Use this for initialization
	void Start () {
        timerCurrent = 0;
        waitTime = 0;
        isTimerRunning = false;
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (isTimerRunning == true)
            timerCurrent += Time.deltaTime;


    }

    public bool GetIsTimerRunning()
    {
        return isTimerRunning;
    }

    public void SetIsTimerRunning(bool timerRunning)
    {
        this.isTimerRunning = timerRunning;
    }

    public float GetWaitTime()
    {
        return waitTime;
    }

    public void SetWaitTime(float newWaitTime)
    {
        this.waitTime = newWaitTime;
    }

    public float GetCurrentTime()
    {
        return this.timerCurrent;
    }

    public void SetCurrentTime(float currentTime)
    {
        this.timerCurrent = currentTime;
    }
    
}