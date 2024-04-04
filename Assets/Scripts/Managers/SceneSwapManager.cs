using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwapManager : MonoBehaviour
{
    public static SceneSwapManager instance;
    private static bool _loadFromEdge;

    private GameObject _player;
    private GameObject _sideCharacter;
    private Collider2D _playerColl;
    private Collider2D _sideCharacterColl;
    private Collider2D _edgeColl;
    private Vector3 _playerSpawnPosition;
    private EdgeInteraction.EdgeToSpawnAT _edgeToSpawnTo;
    // Start is called before the first frame update
    private void Awake() {
        if (instance == null)
        {
            instance = this;
        }

        _player = GameObject.FindGameObjectWithTag("Sophie");    
        _playerColl = _player.GetComponent<Collider2D>();
        _sideCharacter = GameObject.FindGameObjectWithTag("Maya");
        _sideCharacterColl = _sideCharacter.GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable() 
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public static void SwapSceneFromEdgeUse(SceneField myScene, EdgeInteraction.EdgeToSpawnAT edgeToSpawnAt)// = EdgeInteraction.EdgeToSpawnAT.None)
    {
        _loadFromEdge = true;
        instance.StartCoroutine(instance.FadeOutThenChangeScene(myScene, edgeToSpawnAt));
    }
    private IEnumerator FadeOutThenChangeScene(SceneField myScene, EdgeInteraction.EdgeToSpawnAT edgeToSpawnAt = EdgeInteraction.EdgeToSpawnAT.None)
    {
        //start fading to black
        SceneFadeManager.instance.StartFadeOut();

        //keep fading out
        while(SceneFadeManager.instance.isFadingOut)
        {
            yield return null;
        }
    
        _edgeToSpawnTo = edgeToSpawnAt;
        SceneManager.LoadScene(myScene);
        
    }

    // Called whenever a new scene is loaded (including the start of the game)
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //start fading in
        SceneFadeManager.instance.StartFadeIn();
        if(_loadFromEdge)
        {
            //warp player to the edge of the screen
            //do something with the edge
            FindEdge(_edgeToSpawnTo);
            _player.transform.position = _playerSpawnPosition;
            _sideCharacter.transform.position = _playerSpawnPosition;
            _loadFromEdge = false;
        }
    }

    private void FindEdge(EdgeInteraction.EdgeToSpawnAT edgeSpawnNumber)
    {
        EdgeInteraction[] edges = FindObjectsOfType<EdgeInteraction>();
        for(int i = 0; i < edges.Length; i++)
        {
            if(edges[i].CurrentEdgePosition == edgeSpawnNumber)
            {
                _edgeColl = edges[i].gameObject.GetComponent<Collider2D>();

                // calculate spawn position
                CalculateSpawnPosition();
                return;
            }
        }
    }

    private void CalculateSpawnPosition()
    {
        float colliderHeight = _playerColl.bounds.extents.y;
        _playerSpawnPosition = _edgeColl.transform.position - new Vector3(0f, colliderHeight, 0f);
    }

}
