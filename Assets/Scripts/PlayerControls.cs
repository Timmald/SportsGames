using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour
{
    public float moveSpeed;



    public float rotationSpeed;

    private Vector3 _startPosition;

    private Quaternion _startRotation;

    private Vector3 _moveDirection;

    private bool _canMove = true;

    public bool _isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        _moveDirection = new Vector3();
        _startPosition = transform.position;
        _startRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(change());
    }

    IEnumerator change()
    {
        if (_canMove)
        {
            _moveDirection.y = 0;
            if (Input.GetKey(KeyCode.UpArrow))
            {
                _moveDirection.z = 1;
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                _moveDirection.z = -1;
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Rotate(0, -90 * Time.deltaTime * rotationSpeed, 0);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Rotate(0, 90 * Time.deltaTime * rotationSpeed, 0);
            }

            if (Input.GetKey(KeyCode.Space))
            {
                _moveDirection.y = (float) .2;
            }

            Vector3 newPosition = transform.position;
            newPosition += transform.forward * (_moveDirection.z * moveSpeed * Time.deltaTime);
            newPosition.y += _moveDirection.y;
            transform.position = newPosition;
            if (transform.rotation.z != 0 || transform.rotation.x != 0)
            {
                yield return new WaitForSeconds(2);
            }

            if (transform.position.y < -100)
            {
                _isAlive = false;
                using (StreamReader sr = File.OpenText(@"Assets/Scripts/HS.txt"))
                {
                    int s;
                    s=Int32.Parse(sr.ReadLine());
                    sr.Close();
                    GameManager manager = GameObject.Find("gamemanager").GetComponent<GameManager>();
                    int score = manager.score;
                    print("found score");
                    if (score > s)
                    {
                        StreamWriter sw = File.CreateText(@"Assets/Scripts/HS.txt");
                        sw.WriteLine(score.ToString());
                        sw.Close();
                    }


                }

                SceneManager.LoadScene(0);
            }

            if (!_isAlive)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isAlive)
        {
            SkinnedMeshRenderer[] renderers = GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach (SkinnedMeshRenderer renderer in renderers)
            {
                renderer.enabled = false;
            }

            if (other.gameObject != GameObject.Find("Plane"))
            {
                _canMove = false;
                _isAlive = false;
                using (StreamReader sr = File.OpenText(@"Assets/Scripts/HS.txt"))
                {
                    int s;
                    s=Int32.Parse(sr.ReadLine());;
                    sr.Close();
                    GameManager manager = GameObject.Find("gamemanager").GetComponent<GameManager>();
                    int score = manager.score;
                    print("found score!");
                    print(score.ToString());
                    print(s);
                    if (score > s)
                    {
                        print("gonna write to file");
                        StreamWriter sw = File.CreateText(@"Assets/Scripts/HS.txt");
                        sw.WriteLine(score.ToString());
                        sw.Close();
                        print("wrote to file");
                    }
                    //TODO: remember to close streams

                    SceneManager.LoadScene(0);
                }

            }

            StartCoroutine(Reset());
        }
    }

    public IEnumerator Reset()
        {
            yield return new WaitForSeconds(2);
            SkinnedMeshRenderer[] renderers = GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach (SkinnedMeshRenderer renderer in renderers)
            {
                renderer.enabled = true;
            }

            _moveDirection = new Vector3();
            _canMove = true;
            _isAlive = true;
            transform.position = _startPosition;
            transform.rotation = _startRotation;
        }

    }

