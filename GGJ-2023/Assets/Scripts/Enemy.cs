using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CustomMonoBehaviour
{
    Player player;

    [SerializeField] float speed;

    protected override void Start()
    {
        base.Start();
        player = FindObjectOfType<Player>();
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

        Vector3 direction = (player.transform.position - transform.position).normalized;

        transform.position += direction * speed * Time.deltaTime;
    }
}