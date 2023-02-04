using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class Player : CustomMonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] float groundRayLength;
    [SerializeField] TMP_Text playerHealthText;

    Animator animator;

    Rigidbody2D rb;
    FlashRed flashRed;

    bool isTouchingGround;

    float groundTimerGracePeriod;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        flashRed = GetComponent<FlashRed>();

        isTouchingGround = false;
    }

    void Update()
    {
        groundTimerGracePeriod += Time.deltaTime;

        if (Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.D) ||
            Input.GetMouseButtonDown(0) ||
            rb.velocity.magnitude > 0.5f)
        {
            GameManager.instance.StartUpdateGame();
        }
        else
        {
            GameManager.instance.StopUpdateGame();
        }

        if (groundTimerGracePeriod > 0.05f)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position,
                 Vector2.down,
                 groundRayLength,
                 LayerMask.GetMask("Ground"));

            if (hit.collider != null)
            {
                groundTimerGracePeriod = 0;
                isTouchingGround = true;
                animator.SetBool("Jump", false);
            }
            else
            {
                isTouchingGround = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && isTouchingGround)
        {
            rb.AddForce(new Vector2(0, jumpForce));
            animator.SetBool("Jump", true);
            groundTimerGracePeriod = 0;
            isTouchingGround = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetInteger("Attack", Random.Range(1, 4));
        }
    }

    protected override void OnStartUpdate()
    {
        base.OnStartUpdate();

        Vector3 direction = new Vector3(
            Input.GetAxisRaw("Horizontal"),
            0,
            0).normalized;

        if (direction.x > 0 && transform.localScale.x < 0 ||
            direction.x < 0 && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1,
                transform.localScale.y,
                transform.localScale.z);
        }

        if (direction.magnitude > 0.25f)
        {
            animator.SetBool("Run", true);
        }

        transform.position += direction * speed * Time.deltaTime;
    }

    protected override void OnStopUpdate()
    {
        base.OnStopUpdate();

        animator.SetBool("Run", false);
    }

    public void IncreaseHealth(int incrementBy)
    {
        health += incrementBy;
        playerHealthText.text = $"Health: {health}";
    }

    public void ReduceHealth(int reduceBy)
    {
        health -= reduceBy;
        flashRed.FlashColor(0.25f);
        playerHealthText.text = $"Health: {health}";
        animator.SetTrigger("TakeDamage");
    }

    public void StopAttack()
    {
        animator.SetInteger("Attack", 0);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, Vector2.down * groundRayLength);
    }
}