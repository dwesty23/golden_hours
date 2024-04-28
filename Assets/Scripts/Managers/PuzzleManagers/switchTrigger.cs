using UnityEngine;
using System;

public class SwitchTrigger : MonoBehaviour
{
    public static event Action<SwitchesController> OnSwitchClicked;

    public SwitchesController switchesController; // Reference to SwitchesController

    private void OnMouseDown()
    {
        // When the switch is clicked, trigger the event with this instance
        OnSwitchClicked?.Invoke(switchesController);
    }
}
