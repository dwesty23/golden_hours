using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class JournalControlsManager : MonoBehaviour
{
    [Header("Scene to Load")]
    [SerializeField] private SceneField _SceneJournal;

    [Header("Current Scene")]
    [SerializeField] private SceneField _SceneControls;
    
    public void JournalMenu()
    {
        SceneManager.LoadSceneAsync(_SceneJournal, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(_SceneControls);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadSceneAsync(_SceneJournal, LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(_SceneControls);
        }
    }
}