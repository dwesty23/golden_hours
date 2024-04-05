using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenuManager : MonoBehaviour
{
    [Header("Scenes to Load")]  
    [SerializeField] private SceneField _persistentGameplay;
    [SerializeField] private SceneField _levelScene;



    // Start is called before the first frame update
    public void StartGame()
    {
        SceneManager.LoadSceneAsync(_persistentGameplay);
        SceneManager.LoadSceneAsync(_levelScene, LoadSceneMode.Additive);
        //start loading the scenes we need

    }
}
