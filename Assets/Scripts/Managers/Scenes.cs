using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SceneData
{
    public SceneField scene;
    public SceneField alternativeScene;
}

public class Scenes : MonoBehaviour
{

    // player collider
    private GameObject _player;
    private Collider2D _playerColl;
    
    // scenes to load
    [Header("Scenes to Load")]
    [SerializeField] private SceneField[] _scenesToLoad;

    public bool firstPuzzle = false;
    // scenes to swap after first puzzle
    [Header("After Puzzle 1")]
    [SerializeField] private SceneField[] _swap_puzzle_1;
    
    [Header("After Puzzle 2")]
    public bool secondPuzzle = false;
    // scenes to swap after second puzzle
    [SerializeField] private SceneField[] _swap_puzzle_2;
    
    [Header("After Puzzle 3")]
    public bool thirdPuzzle = false;
    // scenes to swap after third puzzle
    [SerializeField] private SceneField[] _swap_puzzle_3;
    


    public void Awake()
    {
        LoadScenes();
    }

    public void LoadScenes()
    {
        for (int i = 0; i < _scenesToLoad.Length; i++)
        {
            bool isSceneLoaded = false;
            for (int j = 0; j < SceneManager.sceneCount; j++)
            {
                Scene loaddedscene = SceneManager.GetSceneAt(j);
                if (loaddedscene.name == _scenesToLoad[i].SceneName)
                {
                    isSceneLoaded = true;
                    break;
                }
            }

            if (!isSceneLoaded)
            {
                SceneManager.LoadSceneAsync(_scenesToLoad[i], LoadSceneMode.Additive);
            }
        }
    }






}
