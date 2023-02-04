using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Player player;

    [SerializeField] float speed;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;

        transform.position += direction * speed * Time.deltaTime *
            GameManager.instance.globalSpeed;
    }
}