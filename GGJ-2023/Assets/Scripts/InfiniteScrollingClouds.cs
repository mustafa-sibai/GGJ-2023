using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteScrollingClouds : MonoBehaviour
{
    [SerializeField] GameObject[] clouds;
    [SerializeField] GameObject startTeleportPoint;
    [SerializeField] GameObject endTeleportPoint;
    [SerializeField] float speed;
    [SerializeField] float paralaxSpeed;

    void Start()
    {

    }

    void Update()
    {
        clouds[0].GetComponent<SpriteRenderer>().size += new Vector2(paralaxSpeed * Time.deltaTime, 0);

        for (int i = 1; i < clouds.Length; i++)
        {
            clouds[i].transform.position += Vector3.left * speed * Time.deltaTime;

            if (clouds[i].transform.position.x <= endTeleportPoint.transform.position.x)
            {
                clouds[i].transform.position = new Vector3(startTeleportPoint.transform.position.x,
                    startTeleportPoint.transform.position.y, 0);
            }
        }
    }
}