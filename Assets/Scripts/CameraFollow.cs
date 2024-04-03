using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3F;
    private Vector2 velocity = Vector2.zero;

    void Update()
    {
        Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);
        Vector2 currentPosition = Vector2.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        transform.position = new Vector3(currentPosition.x, currentPosition.y, transform.position.z);
    }
}