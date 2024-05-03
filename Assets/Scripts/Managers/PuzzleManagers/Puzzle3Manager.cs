using UnityEngine;
using UnityEngine.SceneManagement;

public class Puzzle3Manager : MonoBehaviour
{
    public static bool variable1 = false;
    public static bool variable2 = false;
    public static bool variable3 = false;

    public static void Check()
    {
        Debug.Log(variable1 + " " + variable2 + " " + variable3);
        if (variable1 && variable2 && variable3)
        {
            Debug.Log("Puzzle 3 Complete");
           Scenes.Instance.CompletePuzzle(4);
        }
    }
}