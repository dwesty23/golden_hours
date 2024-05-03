using UnityEngine;

public class PuzzleTest : MonoBehaviour
{
    // Start is called before the first frame update
    public bool puzzle1;
    public bool puzzle2;
    public bool puzzle3;
    public bool puzzle4;
    public bool puzzle5;

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
        if(puzzle4)
        {
            Scenes.Instance.CompletePuzzle(4);
        }
        if(puzzle5)
        {
            Scenes.Instance.CompletePuzzle(5);
        }
    }
}