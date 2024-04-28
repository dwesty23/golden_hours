using UnityEngine;
using System.Collections;

public class SwitchesController : MonoBehaviour
{
    // These references should be set in the inspector
    public GameObject switchLeft;
    public GameObject switchRight;
    public GameObject redLight;
    public GameObject greenLight;
    public GameObject orangeLight;
    public int switchNumber = 0; // Unique ID for each switch

    private void OnEnable()
    {
        // Subscribe to a custom event with this instance as a parameter
        SwitchTrigger.OnSwitchClicked += HandleSwitchClicked;
    }

    private void OnDisable()
    {
        // Unsubscribe the event when the object is disabled
        SwitchTrigger.OnSwitchClicked -= HandleSwitchClicked;
    }

    private void HandleSwitchClicked(SwitchesController clickedSwitchController)
    {
        // Get the switch number from the controller that triggered the event
        Debug.Log("Switch clicked: " + clickedSwitchController.switchNumber);

        int switchID = clickedSwitchController.switchNumber;

        // Toggle switches based on the switch ID
        if (switchID == switchNumber) // Ensure correct switch is toggled
        {
            Debug.Log("Switch toggled: " + switchNumber);
     
                switchLeft.SetActive(false);
                switchRight.SetActive(true);
                greenLight.SetActive(true);
                redLight.SetActive(false);
                orangeLight.SetActive(false);
       
            
        }
    }

    public void Reset()
    {
        StartCoroutine(ResetCoroutine());
    }

    private IEnumerator ResetCoroutine()
    {
        redLight.SetActive(true);
        orangeLight.SetActive(false);
        greenLight.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        switchLeft.SetActive(true);
        switchRight.SetActive(false);
        redLight.SetActive(false);
        orangeLight.SetActive(true);
    }
}
