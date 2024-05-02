using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentScene : MonoBehaviour
{
    public GameObject sophie;
    public GameObject maya;
    public Camera mainCamera;

    [SerializeField] private SceneField _persistentScene;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == _persistentScene.SceneName)
        {
            Debug.Log("Assigning GameObjects");
            Scenes.Instance.AssignGameObjects(sophie, maya, mainCamera);

            Debug.Log("LoadFromSavedData: " + Scenes.Instance.loadFromSavedData);
            if (!Scenes.Instance.loadFromSavedData)
            {
                Debug.Log("Initial Save");
                Scenes.Instance.InitialSave();
            }
            //Scenes.Instance.InitialSave();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        if (scene.name == _persistentScene.SceneName)
        {
            Debug.Log("Assigning GameObjects");
            Scenes.Instance.AssignGameObjects(sophie, maya, mainCamera);

            Debug.Log("LoadFromSavedData: " + Scenes.Instance.loadFromSavedData);
            if(!Scenes.Instance.loadFromSavedData)
            {
                Debug.Log("Initial Save");
                Scenes.Instance.InitialSave();
            }
            else
            {
                PlayerData sophieData = SaveSystem.LoadPlayerData("Sophie");
                PlayerData mayaData = SaveSystem.LoadPlayerData("Maya");
                CameraData cameraData = SaveSystem.LoadCameraData();

                if (sophieData != null)
                {
                    Debug.Log("Accessing sophie: " + (sophie != null ? sophie.name : "null"));
                    sophie.transform.position = new Vector3(sophieData.position[0], sophieData.position[1], sophieData.position[2]);
                }

                if (mayaData != null)
                {
                    maya.transform.position = new Vector3(mayaData.position[0], mayaData.position[1], mayaData.position[2]);
                }

                if (cameraData != null)
                {
                    mainCamera.transform.position = new Vector3(cameraData.position[0], cameraData.position[1], cameraData.position[2]);
                    mainCamera.fieldOfView = cameraData.fieldOfView;
                }
            }
            //Scenes.Instance.InitialSave();
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}