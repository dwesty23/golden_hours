using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Initializer
{
    // Start is called before the first frame update
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]

    public static void execute()
    {
        Debug.Log("Loaded by the Persist obj from the initializer script");
        Object.DontDestroyOnLoad(Object.Instantiate(Resources.Load("PERSISTOBJECTS")));
        // Add your initialization code here
    }
}
