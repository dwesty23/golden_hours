using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class PlayCredits : MonoBehaviour
{
    [Header("Scene to Load")]
    [SerializeField] private SceneField _SceneMainMenu;
    public GameObject credits;
    public GameObject credits2;
    public Button mainMenuButton;
    public Image mainMenuButtonImage;
   
    public void Start()
    {
        mainMenuButton.enabled = false;
        mainMenuButtonImage.enabled = false;
        StartCoroutine(ShowCredits());
    }

    public IEnumerator ShowCredits()
    {
        yield return new WaitForSeconds(1f);
        while (credits.transform.position.y < 53f)
        {
            credits.transform.position += new Vector3(0, 1, 0) * 1.5f * Time.deltaTime;
            credits2.transform.position += new Vector3(0, 1, 0) * 1.5f * Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        mainMenuButton.enabled = true;
        mainMenuButtonImage.enabled = true;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(_SceneMainMenu, LoadSceneMode.Single);
    }
}