using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Scenes : MonoBehaviour
{

    [SerializeField] private GameObject sophie;
    [SerializeField] private GameObject maya;
    [SerializeField] private Camera mainCamera;

    public bool puzzle1Finished;
    public bool puzzle2Finished;
    public bool puzzle3Finished;

    public bool loadFromSavedData;

    // private bool initialSaveDone = false;


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

    public void AssignGameObjects(GameObject sophie, GameObject maya, Camera mainCamera)
    {
        this.sophie = sophie;
        this.maya = maya;
        this.mainCamera = mainCamera;
    }

    public void InitialSave()
    {
        SaveSystem.SavePlayerData(sophie.transform, "Sophie");
        SaveSystem.SavePlayerData(maya.transform, "Maya");
        SaveSystem.SaveCameraData(mainCamera);
        //initialSaveDone = true;
    }

    public void SwitchToCutscene(SceneField scene)
    {
        SaveSystem.SavePlayerData(sophie.transform, "Sophie");
        SaveSystem.SavePlayerData(maya.transform, "Maya");
        SaveSystem.SaveCameraData(mainCamera);

        SceneManager.LoadScene(scene.SceneName);
    }

    public IEnumerator LoadMap(bool loadFromSavedData = false)
    {
        this.loadFromSavedData = loadFromSavedData;
        Debug.Log("Scene loaded, loadFromSavedData: " + loadFromSavedData);
        //SceneManager.LoadSceneAsync(_persistentScene.SceneName);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_persistentScene.SceneName);
        foreach (SceneField scene in _scenesToLoad)
        {
            SceneManager.LoadSceneAsync(scene.SceneName, LoadSceneMode.Additive);
        }

        while (!asyncLoad.isDone)
        {
            yield return null;
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
            if (puzzleObject != null)
            {
                puzzleObject.CheckPuzzleCompletion();
            }
        }
    }
}