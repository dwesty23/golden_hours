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

    [Header("Memory Scenes")]
    [SerializeField] private SceneField _SceneMemory1;
    [SerializeField] private SceneField _SceneMemory2;
    [SerializeField] private SceneField _SceneMemory3;

    [Header("Memory 1 Button")]
    public Image MemoryImage1;
    public Button MemoryButton1;

    [Header("Memory 2 Button")]
    public Image MemoryImage2;
    public Button MemoryButton2;

    [Header("Memory 3 Button")]
    public Image MemoryImage3;
    public Button MemoryButton3;
    
    private bool Memory1Collected
    {
        get { return PlayerPrefs.GetInt("Memory1Collected", 0) == 1; }
        set { PlayerPrefs.SetInt("Memory1Collected", value ? 1 : 0); PlayerPrefs.Save(); }
    }

    private bool Memory2Collected
    {
        get { return PlayerPrefs.GetInt("Memory2Collected", 0) == 1; }
        set { PlayerPrefs.SetInt("Memory2Collected", value ? 1 : 0); PlayerPrefs.Save(); }
    }

    private bool Memory3Collected
    {
        get { return PlayerPrefs.GetInt("Memory3Collected", 0) == 1; }
        set { PlayerPrefs.SetInt("Memory3Collected", value ? 1 : 0); PlayerPrefs.Save(); }
    }

    void Start()
    {
        UpdateMemoryUI();
    }

    private void UpdateMemoryUI()
    {
        MemoryImage1.enabled = Memory1Collected;
        MemoryButton1.enabled = Memory1Collected;
        MemoryImage2.enabled = Memory2Collected;
        MemoryButton2.enabled = Memory2Collected;
        MemoryImage3.enabled = Memory3Collected;
        MemoryButton3.enabled = Memory3Collected;
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

    public void PlayMemory2()
    {
        SceneManager.LoadSceneAsync(_SceneMemory2, LoadSceneMode.Additive);
        foreach (GameObject go in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            go.SetActive(false);
        }
        SceneManager.UnloadSceneAsync(_SceneJournal);
    }

    public void PlayMemory3()
    {
        SceneManager.LoadSceneAsync(_SceneMemory3, LoadSceneMode.Additive);
        foreach (GameObject go in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            go.SetActive(false);
        }
        SceneManager.UnloadSceneAsync(_SceneJournal);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.UnloadSceneAsync(_SceneJournal);
        }
    }
}