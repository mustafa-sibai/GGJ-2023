using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : CustomMonoBehaviour
{
    Player player;
    Animator animator;

    [SerializeField] float speed;
    [SerializeField] float radius;
    [SerializeField] float attackFrequancy;
    [SerializeField] int attackRayLength;
    [SerializeField] int health;

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

        if (health <= 0)
            return;

        timer += Time.deltaTime;

        if (timer >= attackFrequancy)
        {
            float distanceToPlayer = Vector3.Distance(
                new Vector3(player.transform.position.x, transform.position.y, 0),
                transform.position);

            if (distanceToPlayer < radius)
            {
                animator.SetBool("Run", false);
                animator.SetTrigger("Attack");

                RaycastHit2D rightRay = Physics2D.Raycast(transform.position,
                     Vector2.right,
                     attackRayLength,
                     LayerMask.GetMask("Player"));

                RaycastHit2D leftRay = Physics2D.Raycast(transform.position,
                     Vector2.left,
                     attackRayLength,
                     LayerMask.GetMask("Player"));

                if (rightRay.collider != null || leftRay.collider != null)
                {
                    player.ReduceHealth();
                }

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

    public void IncreaseHealth(int incrementBy)
    {
        health += incrementBy;
    }

    public void ReduceHealth(int reduceBy)
    {
        health -= reduceBy;
        animator.SetTrigger("GetHit");

        if (health <= 0)
        {
            animator.SetTrigger("Die");
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.25f);
        Gizmos.DrawSphere(transform.position, radius);

        Gizmos.color = new Color(1, 1, 0, 0.25f);
        Gizmos.DrawRay(transform.position, Vector2.right * attackRayLength);
        Gizmos.DrawRay(transform.position, Vector2.left * attackRayLength);
    }
}