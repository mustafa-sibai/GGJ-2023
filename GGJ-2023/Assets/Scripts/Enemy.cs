using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CustomMonoBehaviour
{
    Player player;
    Animator animator;

    [SerializeField] float speed;
    [SerializeField] float radius;
    [SerializeField] float attackFrequancy;

    float timer;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        player = FindObjectOfType<Player>();
        animator = GetComponent<Animator>();

        timer = 0;
    }

    protected override void OnStartUpdate()
    {
        base.OnStartUpdate();

        timer += Time.deltaTime;

        float distanceToPlayer = Vector3.Distance(
            new Vector3(player.transform.position.x, transform.position.y, 0),
            transform.position);

        if (distanceToPlayer < radius)
        {
            animator.SetBool("Run", false);

            if (timer >= attackFrequancy)
            {
                animator.SetTrigger("Attack");
                timer = 0;
            }
        }
        else
        {
            animator.SetBool("Run", true);

            Vector3 direction = (new Vector3(
                        player.transform.position.x,
                        transform.position.y,
                        0) - transform.position).normalized;

            if (direction.x < 0 && transform.localScale.x < 0 ||
                direction.x > 0 && transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1,
                    transform.localScale.y,
                    transform.localScale.z);
            }

            transform.position += direction * speed * Time.deltaTime;
        }
    }

    protected override void OnStopUpdate()
    {
        base.OnStopUpdate();

        animator.SetBool("Run", false);
        animator.SetBool("Jump", false);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.25f);
        Gizmos.DrawSphere(transform.position, radius);
    }
}