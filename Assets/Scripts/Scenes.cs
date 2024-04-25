using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    public bool puzzle1Finished;
    public bool puzzle2Finished;
    public bool puzzle3Finished;


    public static Scenes Instance { get; private set; }

    [Header("Persistent Scene: ")]
    [SerializeField] private SceneField _persistentScene;

    [Header("Scenes to Load: ")]
    [SerializeField] private SceneField[] _scenesToLoad;

    [RuntimeInitializeOnLoadMethod]
    private static void Initialize()
    {
        if (Instance == null)
        {
            GameObject prefab = Resources.Load<GameObject>("Scenes");
            GameObject scenesObject = Instantiate(prefab);
            Instance = scenesObject.GetComponent<Scenes>();
            DontDestroyOnLoad(scenesObject);
        }
    }

    public void LoadMap()
    {
        SceneManager.LoadSceneAsync(_persistentScene.SceneName);
        foreach (SceneField scene in _scenesToLoad)
        {
            SceneManager.LoadSceneAsync(scene.SceneName, LoadSceneMode.Additive);
        }
    }

    public void CompletePuzzle(int puzzleNumber)
    {
        switch (puzzleNumber)
        {
            case 1:
                puzzle1Finished = true;
                break;
            case 2:
                puzzle2Finished = true;
                break;
            case 3:
                puzzle3Finished = true;
                break;
        }

        // Check puzzle objects
        foreach (PuzzleObject puzzleObject in FindObjectsOfType<PuzzleObject>())
        {
            puzzleObject.CheckPuzzleCompletion();
        }
    }
}