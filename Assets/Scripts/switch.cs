using UnityEngine;

public class SwitchesController : MonoBehaviour
{
    // These references should be set in the inspector
    public GameObject switchLeft;
    public GameObject switchRight;

    private void OnEnable()
    {
        SwitchTrigger.OnSwitchClicked += HandleSwitchClicked;
    }

    private void OnDisable()
    {
        SwitchTrigger.OnSwitchClicked -= HandleSwitchClicked;
    }

    private void HandleSwitchClicked(SwitchTrigger clickedSwitch)
    {
        // Toggle the switches
        if (clickedSwitch.gameObject == switchLeft)
        {
            // If the left switch was clicked, deactivate it and activate the right one
            switchLeft.SetActive(false);
            switchRight.SetActive(true);
        }
        else if (clickedSwitch.gameObject == switchRight)
        {
            // If the right switch was clicked, deactivate it and activate the left one
            switchRight.SetActive(false);
            switchLeft.SetActive(true);
        }
    }
}
