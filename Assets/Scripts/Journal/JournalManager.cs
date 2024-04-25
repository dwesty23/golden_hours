using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class JournalManager : MonoBehaviour
{
    [Header("Scene to Load")]
    [SerializeField] private SceneField _SceneMainMenu;
    [SerializeField] private SceneField _SceneSettings;
    [SerializeField] private SceneField _SceneControls;
    [SerializeField] private SceneField _SceneJournal;
    
    [Header("Memory 1")]
    public Image MemoryImage1;
    public Button MemoryButton1;
    [SerializeField] private SceneField _SceneMemory1;

    public bool Memory1Collected = false;

    void Start()
    {
        MemoryImage1.enabled = false;
        MemoryButton1.enabled = false;
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "DinerFinishCutScene")
        {
            Memory1Collected = true;
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(_SceneMainMenu, LoadSceneMode.Single);
    }

    public void Settings()
    {
        SceneManager.LoadSceneAsync(_SceneSettings, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(_SceneJournal);
    }

    public void Controls()
    {
        SceneManager.LoadSceneAsync(_SceneControls, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(_SceneJournal);
    }

    public void PlayMemory1()
    {
        SceneManager.LoadSceneAsync(_SceneMemory1, LoadSceneMode.Additive);
        foreach (GameObject go in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            go.SetActive(false);
        }
        SceneManager.UnloadSceneAsync(_SceneJournal);
    }

    void Update()
    {
        if (Memory1Collected == true)
        {
            MemoryImage1.enabled = true;
            MemoryButton1.enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.UnloadSceneAsync(_SceneJournal);
        }
    }
}