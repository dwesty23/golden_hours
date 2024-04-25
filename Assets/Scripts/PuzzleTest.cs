using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTest : MonoBehaviour
{
    // Start is called before the first frame update
    public bool puzzle1;
    public bool puzzle2;
    public bool puzzle3;

    // Update is called once per frame
    void Update()
    {
        if(puzzle1)
        {
            Scenes.Instance.CompletePuzzle(1);
        }
        if(puzzle2)
        {
            Scenes.Instance.CompletePuzzle(2);
        }
        if(puzzle3)
        {
            Scenes.Instance.CompletePuzzle(3);
        }
    }
}
