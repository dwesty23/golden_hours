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

    public void MainMenu()
    {
        SceneManager.LoadScene(_SceneMainMenu, LoadSceneMode.Single);
    }

    public void Settings()
    {
    }

    public void Controls()
    {
        SceneManager.LoadSceneAsync(_SceneControls, LoadSceneMode.Additive);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.UnloadSceneAsync(_SceneControls);
        }
    }
}