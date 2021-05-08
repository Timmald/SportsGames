using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent (typeof(RendererFade))]

public class BallBehavior : MonoBehaviour
{
    public double speed;
    public AudioClip explosionSound;
    public float lifeTime;
    private float zPosition;
    private Vector3 directionToPlayer;
    private bool isDestroyed = false;
    private bool beganFade = false;
    private GameObject player;
    private float curTime;

    private bool isAlive;
    // Start is called before the first frame update
    void Start()
    {
        float rotate = Random.Range(0, 180);
        transform.Rotate(rotate, rotate, 0);
        zPosition = transform.position.z;
        curTime = 0;
        player = GameObject.Find("PlayerCube");
        try
        {
            isAlive = player.GetComponent<PlayerControls>()._isAlive;
        }
        catch (NullReferenceException)
        {
            player=GameObject.Find("PlayerCube(Clone)");
            isAlive = player.GetComponent<PlayerControls>()._isAlive;
        }

        directionToPlayer = player.transform.position - transform.position;
        directionToPlayer = directionToPlayer*Time.deltaTime;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        curTime += Time.deltaTime;
        if (curTime > lifeTime)
        {
            Destroy(gameObject);
        }

        if (!isAlive)
        {
            Destroy(gameObject);
        }
        Vector3 newPosition = transform.position;
        newPosition += directionToPlayer;
        transform.position = newPosition;
    }

    public void Reset()
    {
        isAlive = true;
    }
}
