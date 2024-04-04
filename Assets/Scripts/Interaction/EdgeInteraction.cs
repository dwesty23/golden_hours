using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeInteraction : Interactable
{
    public enum EdgeToSpawnAT
    {
        None,
        One,
        Two,
        Three,
        Four,
        Five,
        Six,

    }
    
    [Header("Spwan TO")]
    [SerializeField] private EdgeToSpawnAT _spawnTo;

    [SerializeField] private SceneField _sceneToLoad;

    [Space(10f)]
    [Header("This Edge")]
    public EdgeToSpawnAT CurrentEdgePosition;
    
    public override void Interact()
    {
        // load new scene
        SceneSwapManager.SwapSceneFromEdgeUse(_sceneToLoad, CurrentEdgePosition);
    }
}