using UnityEngine;
using UnityEngine.UI;

public class TimerObject : MonoBehaviour
{
    public Image timerImage;
    public int numberOfSegments = 12;
    public int durationInDays = 1; // How many "days" the timer represents
    private float segmentTime;
    private Color yellowColor;

    private void Start()
    {
        yellowColor = new Color(228f/255f, 191f/255f, 79f/255f);
        timerImage.fillAmount = 1.0f;
        timerImage.color = yellowColor;
        segmentTime = GlobalTimer.Instance.totalTime / (float)(numberOfSegments * durationInDays);
        
    }

    private void Update()
    {

        float timePassed = GlobalTimer.Instance.GetTimePassed();

        float totalTimeThisObject = ((float)GlobalTimer.Instance.totalTime / durationInDays);
        

        if (timePassed > totalTimeThisObject)
        {
            timerImage.fillAmount = 1;
        }
        else
        {
            timerImage.fillAmount = Mathf.Clamp(timePassed / totalTimeThisObject, 0, 1);
        }

        timerImage.color = yellowColor;


    }
}