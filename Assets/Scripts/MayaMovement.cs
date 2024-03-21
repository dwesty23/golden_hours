using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MayaMovement : MonoBehaviour
{
    public Rigidbody2D playerRb;
    public float speed;
    public float stoppingDistance;


    //private bool facingRight = true;
    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Sophie").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, target.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }
}
