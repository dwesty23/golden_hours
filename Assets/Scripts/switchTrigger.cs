using UnityEngine;
using System; // Needed for Action

public class SwitchTrigger : MonoBehaviour
{
    public static event Action<SwitchTrigger> OnSwitchClicked;

    void OnMouseDown()
    {
        OnSwitchClicked?.Invoke(this);
    }
}
