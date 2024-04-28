using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("Scene to Load")]
    [SerializeField] private SceneField _levelScenePolice;
    [SerializeField] private SceneField _levelSceneSettings;
    [SerializeField] private SceneField _levelSceneCredits;
    [SerializeField] private SceneField _levelSceneMainMenu;

    public void StartGame()
    {
        PlayerPrefs.SetInt("Memory1Collected", 0); // Explicitly reset memory flag when starting a new game
        PlayerPrefs.Save();
        // Load the police scene
        SceneManager.LoadScene(_levelScenePolice, LoadSceneMode.Single);
    }

    public void Settings()
    {
        // Load the settings scene
        SceneManager.LoadScene(_levelSceneSettings, LoadSceneMode.Single);
    }

    public void Credits()
    {
        // Load the credits scene
        SceneManager.LoadScene(_levelSceneCredits, LoadSceneMode.Single);
    }

    public void MainMenu()
    {
        // Load the main menu scene
        SceneManager.LoadScene(_levelSceneMainMenu, LoadSceneMode.Single);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MainMenu();
        }
    }

}
