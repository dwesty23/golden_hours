using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer; // Assign your Audio Mixer here
    public Slider volumeSlider; // Assign your volume slider here

    private void Start()
    {
        // Load the saved volume level at the start or default to full volume
        volumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
        ApplyVolume(); // Apply the loaded volume level
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0); // Load the main menu scene
        }
    }

    public void OnVolumeSliderChanged()
    {
        ApplyVolume();
    }

    void ApplyVolume()
    {
        float volume = Mathf.Log10(volumeSlider.value) * 20; // Convert slider value to decibels
        audioMixer.SetFloat("MasterVolume", volume); // Set the volume in the AudioMixer
        PlayerPrefs.SetFloat("MasterVolume", volumeSlider.value); // Save the volume level
    }
}
