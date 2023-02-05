using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteScrollingClouds : MonoBehaviour
{
    [SerializeField] GameObject[] clouds;
    [SerializeField] float speed;
    [SerializeField] float paralaxSpeed;

    void Start()
    {

    }

    void Update()
    {
        clouds[0].transform.position += Vector3.left * paralaxSpeed * Time.deltaTime;
        clouds[1].transform.position += Vector3.left * speed * Time.deltaTime;
    }
}