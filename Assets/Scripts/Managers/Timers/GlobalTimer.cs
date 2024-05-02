using UnityEngine;
using System.Collections;
using System;
public class GlobalTimer : MonoBehaviour
{
    public static GlobalTimer Instance { get; private set; }

    public float totalTime = 12f; // Total duration for the timer
    // private float timeRemaining;
    private float timePassed;
    private bool timerStarted = false;

    [RuntimeInitializeOnLoadMethod]
    private static void Initialize()
    {
        if (Instance == null)
        {
            GameObject prefab = Resources.Load<GameObject>("GlobalTimer");
            GameObject scenesObject = Instantiate(prefab);
            Instance = scenesObject.GetComponent<GlobalTimer>();
            DontDestroyOnLoad(scenesObject);
        }
    }

    public void StartTimer()
    {
        // timeRemaining = totalTime;
        if(!timerStarted)
        {
            timePassed = 0;
            timerStarted = true;
        }
        
    }

    private void Update()
    {
        if(timerStarted)
        {
            timePassed += Time.deltaTime;
            timePassed = Mathf.Min(timePassed, totalTime);
            if(timePassed == totalTime)
            {
                timerStarted = false;
                Scenes.Instance.GameOver();
                return;
            }
        }        
    }

    public float GetTimePassed()
    {
        return timePassed;
    }
}