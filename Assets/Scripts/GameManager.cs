using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] dodgeBalls;

    public GameObject Player;
    
    public int totalItemsOnScreen;
    
    public Text txt;
    
    public GameObject screenBalls;

    public GameObject spawnPoint;
    
    private float randomTime;

    private float curTime;

    private System.Random rand = new System.Random();

    private double speed;

    public int score=1;

    private bool isAlive;

    private PlayerControls controls;
    // Start is called before the first frame update
    void Start()
    {
        randomTime = 4;
        speed = 1;
        GameObject player= GameObject.Find("PlayerCube");
        controls = player.GetComponent<PlayerControls>();
    }

    // Update is called once per frame
    void Update()
    {
        curTime += Time.deltaTime;
        if (curTime > randomTime && screenBalls.transform.childCount < totalItemsOnScreen)
        {
            Vector3 spawnPoint = this.spawnPoint.transform.position;
            int index = rand.Next(0, dodgeBalls.Length);
            GameObject debrisItem = Instantiate(dodgeBalls[index],spawnPoint,Quaternion.identity) as GameObject;
            int[] vectorVals = {rand.Next(-1,1),rand.Next(-1,1),rand.Next(-1,1)};

            Vector3 newPosition =new Vector3(vectorVals[0]+spawnPoint.x, vectorVals[1]+spawnPoint.y, vectorVals[2]+spawnPoint.z);
            //make newPosition random
            debrisItem.transform.position = newPosition;
            BallBehavior behaviorScript = debrisItem.GetComponent<BallBehavior>();
            behaviorScript.speed = speed;
            curTime = 0;
            speed += .1;
            float quotient = (float)1.1;
            randomTime = randomTime / quotient;
            if (controls._isAlive)
            {
                txt.text = "Score: " + score.ToString();
                score++;
            }
        }
    }

    public void Reset()
    {
        isAlive = true;
        GameObject debrisItem = Instantiate(Player,Vector3.zero, Quaternion.identity) as GameObject;
    }
}
