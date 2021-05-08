using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        string path = @"Assets/Scripts/HS.txt";
        StreamReader sr = File.OpenText(path);
        string score = sr.ReadLine();
        sr.Close();
        string scoreString = ("High Score: " + score.ToString());
        text.text = scoreString;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
