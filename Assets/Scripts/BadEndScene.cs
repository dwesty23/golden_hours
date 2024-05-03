using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class BadEndScene : MonoBehaviour
{
    [Header("Scene to Load")]
    [SerializeField] private SceneField _SceneMainMenu;
    [SerializeField] private SceneField _ScenePoliceStation;

    public void PLayMainMenu()
    {
        SceneManager.LoadScene(_SceneMainMenu, LoadSceneMode.Single);
    }

    public void PlayPoliceStation()
    {
        SceneManager.LoadScene(_ScenePoliceStation, LoadSceneMode.Single);
    }
}