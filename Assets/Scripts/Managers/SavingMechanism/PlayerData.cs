using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {
    public float[] position;


    public PlayerData(Transform transform)
    {
        position = new float[3];
        position[0] = transform.position.x;
        position[1] = transform.position.y;
        position[2] = transform.position.z;
    }
}


[System.Serializable]
public class CameraData : PlayerData {
    public float fieldOfView;

    public CameraData(Camera camera) : base(camera.transform)
    {
        fieldOfView = camera.fieldOfView;
    }
}