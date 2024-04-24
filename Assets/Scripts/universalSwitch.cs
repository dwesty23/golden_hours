using UnityEngine;
using System.Collections.Generic;

public class SwitchPuzzleManager : MonoBehaviour
{
    public List<int> correctSwitchOrder; // Expected order of switches
    private List<int> currentSwitchOrder = new List<int>(); // Track order of clicked switches
    public List<GameObject> switchPrefabs; // List of all switch GameObjects in the scene
    public List<GameObject> notes; // List of note GameObjects to activate
    public GameObject arcadeMachine; // Reference to the arcade machine

    private void OnEnable()
    {
        SwitchTrigger.OnSwitchClicked += HandleSwitchClicked;
    }

    private void OnDisable()
    {
        SwitchTrigger.OnSwitchClicked -= HandleSwitchClicked;
    }

    // Event handler for when a switch is clicked
    private void HandleSwitchClicked(SwitchesController clickedSwitchController)
    {
        int switchID = clickedSwitchController.switchNumber;

        // Add this switch ID to the current order list
        currentSwitchOrder.Add(switchID);

        // Check if the current order is correct
        if (IsOrderCorrect())
        {
            ActivateNote(); // Activate a note based on correct flips

            

            if (currentSwitchOrder.Count == correctSwitchOrder.Count)
            {
                Debug.Log("Puzzle solved! Correct order.");
                arcadeMachine.SetActive(true); 
            }
        }
        else
        {
            ResetSwitches(); // Reset switches and notes if order is incorrect
        }
    }

    // Activate a note based on the number of correct switch flips
    private void ActivateNote()
    {
        int noteIndex = currentSwitchOrder.Count - 1; // Index for notes (0-based)
        
        if (noteIndex >= 0 && noteIndex < notes.Count) // Ensure within range
        {
            notes[noteIndex].SetActive(true); // Activate the corresponding note
        }
    }

    // Check if the current switch order matches the expected order
    private bool IsOrderCorrect()
    {
        for (int i = 0; i < currentSwitchOrder.Count; i++)
        {
            Debug.Log("Current switch order: " + currentSwitchOrder[i]);
            Debug.Log("Correct switch order: " + correctSwitchOrder[i]);
            if (currentSwitchOrder[i] != correctSwitchOrder[i])
            {
                return false;
            }
        }
        return true;
    }

    // Reset all switches and notes when the order is incorrect
    private void ResetSwitches()
    {
        currentSwitchOrder.Clear(); // Clear the current list
        foreach (var switchPrefab in switchPrefabs)
        {
            var switchController = switchPrefab.GetComponent<SwitchesController>();
            switchController.Reset(); // Call the reset function for each switch
        }

        // Reset the notes to inactive state
        foreach (var note in notes)
        {
            note.SetActive(false); // Deactivate all notes
        }

        Debug.Log("Switches and notes have been reset.");
    }
}
