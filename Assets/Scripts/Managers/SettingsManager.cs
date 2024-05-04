using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public AudioMixer audioMixer; // Assign your Audio Mixer here
    public Slider masterVolumeSlider; // Assign your master volume slider here
    public Slider sfxVolumeSlider; // Assign your FX volume slider here
    public Slider bgVolumeSlider; // Assign your Background volume slider here

    private void Start()
    {
        // Load saved volume levels at the start or default to full volume
        masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
        bgVolumeSlider.value = PlayerPrefs.GetFloat("BGVolume", 1f);

        UpdateMixerVolume("MasterVolume", masterVolumeSlider.value);
        UpdateMixerVolume("SFXVolume", sfxVolumeSlider.value);
        UpdateMixerVolume("BGVolume", bgVolumeSlider.value);
    }

    public void OnMasterVolumeChanged()
    {
        UpdateMixerVolume("MasterVolume", masterVolumeSlider.value);
        PlayerPrefs.SetFloat("MasterVolume", masterVolumeSlider.value);
    }

    public void OnSFXVolumeChanged()
    {
        UpdateMixerVolume("SFXVolume", sfxVolumeSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolumeSlider.value);
    }

    public void OnBGVolumeChanged()
    {
        UpdateMixerVolume("BGVolume", bgVolumeSlider.value);
        PlayerPrefs.SetFloat("BGVolume", bgVolumeSlider.value);
    }

    private void UpdateMixerVolume(string parameterName, float sliderValue)
    {
        // Ensure sliderValue never hits exactly 0 to avoid -infinity dB
        float minVolume = 0.0001f;
        float volume = Mathf.Max(sliderValue, minVolume);

        // Convert linear slider value to dB, clamped between -80 dB and 0 dB
        float dB = 60 * Mathf.Log10(volume);
        Debug.Log(parameterName + " volume: " + dB + " dB");
        dB = Mathf.Clamp(dB, -80f, 0f);

        audioMixer.SetFloat(parameterName, dB);
    }
}

