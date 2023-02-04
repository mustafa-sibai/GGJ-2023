using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CustomMonoBehaviour
{
    Player player;

    [SerializeField] float speed;
    [SerializeField] float radius;

    protected override void Start()
    {
        base.Start();
        player = FindObjectOfType<Player>();
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

        float distanceToPlayer = Vector3.Distance(player.transform.position,
            transform.position);

        if (distanceToPlayer < radius)
        {
            print("Attack");
        }
        else
        {
            Vector3 direction = (new Vector3(
                        player.transform.position.x,
                        transform.position.y,
                        0) - transform.position).normalized;

            transform.position += direction * speed * Time.deltaTime;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.25f);
        Gizmos.DrawSphere(transform.position, radius);
    }
}