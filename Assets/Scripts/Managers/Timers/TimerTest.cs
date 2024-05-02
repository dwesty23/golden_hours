using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerTest : MonoBehaviour
{
    // Start is called before the first frame update
    public bool starttimer;
    public bool timerStarted = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(starttimer && !timerStarted)
        {
            GlobalTimer.Instance.StartTimer();
            timerStarted = true;
        }

        
    }
}