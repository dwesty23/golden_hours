using UnityEngine;
using UnityEngine.UI;

public class TimerObject : MonoBehaviour
{
    public Image timerImage;
    public int timerOrder; // 1 for the first timer, 2 for the second, 3 for the third
    private Color yellowColor;

    private void Start()
    {
        yellowColor = new Color(228f/255f, 191f/255f, 79f/255f);
        timerImage.fillAmount = 1.0f; // Start with a full timer
        timerImage.color = yellowColor;
    }

    private void Update()
    {
        float timePassed = GlobalTimer.Instance.GetTimePassed();
        float totalTime = GlobalTimer.Instance.totalTime;
        float segmentTime = totalTime / 3; // Divide the total time into 3 segments

        // Calculate the start and end times for this timer
        float startTime = (timerOrder - 1) * segmentTime;
        float endTime = timerOrder * segmentTime;

        if (timePassed >= startTime && timePassed < endTime)
        {
            // This timer's segment is currently running
            // Calculate fillAmount based on the proportion of the timePassed in this segment to the segmentTime
            timerImage.fillAmount = 1 - Mathf.Clamp((timePassed - startTime) / segmentTime, 0, 1);
        }
        else if (timePassed >= endTime)
        {
            // This timer's segment has ended
            timerImage.fillAmount = 0;
        }
        // If timePassed < startTime, this timer's segment hasn't started yet, so we don't need to do anything
    }
}