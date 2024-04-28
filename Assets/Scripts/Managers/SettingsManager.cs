using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    public AudioMixer audioMixer; // Assign your Audio Mixer here
    public Slider volumeSlider; // Assign your volume slider here

    private void Start()
    {
        // Load the saved volume level at the start or default to full volume
        float savedVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        volumeSlider.value = savedVolume; // Set the slider to the saved volume
        UpdateMixerVolume(savedVolume); // Apply the saved volume to the mixer
    }

    public void OnVolumeSliderChanged()
    {
        ApplyVolume();  // Apply volume when the slider value changes
    }

    void ApplyVolume()
    {
        float volume = Mathf.Log10(volumeSlider.value) * 20; // Convert slider value to decibels
        UpdateMixerVolume(volumeSlider.value); // Apply the new volume to the mixer and save
        PlayerPrefs.SetFloat("MasterVolume", volumeSlider.value); // Save the new volume level
    }

    private void UpdateMixerVolume(float sliderValue)
    {
        float volume = Mathf.Log10(sliderValue) * 20;
        audioMixer.SetFloat("MasterVolume", volume); // Set the volume in the AudioMixer
    }
}


