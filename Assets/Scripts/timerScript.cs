using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Image timerImage;
    public float totalTime = 12f; // Total duration for the timer
    public int numberOfSegments = 12;
    private float segmentTime;
    private float timeRemaining;
    private Color yellowColor;

    

    private void Start()
    {
        yellowColor = new Color(221f/255f, 192f/255f, 98f/255f);
        timerImage.fillAmount = 1.0f;
        segmentTime = totalTime / numberOfSegments;
        timeRemaining = segmentTime;

        timerImage.color = yellowColor;
    }

    private void Update()
    {
        if (timeRemaining > 0)
        {
            // Update the timer
            timeRemaining -= Time.deltaTime;
            timerImage.fillAmount = timeRemaining / segmentTime;

            timerImage.color = yellowColor;

        }
        else
        {
           Debug.Log("Time's up!");
        }
    }
}
