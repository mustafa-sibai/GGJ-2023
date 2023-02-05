using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : CustomMonoBehaviour
{
    [SerializeField] int health = 3;
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] float groundRayLength;
    [SerializeField] float damageRayLength;

    Animator animator;

    Rigidbody2D rb;
    FlashRed flashRed;

    //---- TERRIBLE checkpoint system.

    public GameObject levelStart;
    public GameObject checkpoint;
    public Vector2 curentCheckpoint;

    bool isTouchingGround;

    float groundTimerGracePeriod;


    PlayerHealth playerHealth;

    protected override void Start()
    {
        base.Start();

        levelStart = GameObject.FindWithTag("StartDoor");
        checkpoint = GameObject.FindWithTag("Checkpoint");
        curentCheckpoint = levelStart.transform.position;
    }

    protected override void Awake()
    {
        base.Awake();

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        flashRed = GetComponent<FlashRed>();
        playerHealth = FindObjectOfType<PlayerHealth>();

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

            Vector2 direction = Vector2.zero;

            if (transform.localScale.x > 0)
                direction = Vector2.right;
            else
                direction = Vector2.left;

            RaycastHit2D hit = Physics2D.Raycast(transform.position,
                     direction,
                     damageRayLength,
                     LayerMask.GetMask("Enemies"));

            if (hit.collider != null)
            {
                hit.collider.gameObject.GetComponent<Enemy>().ReduceHealth(1);
            }
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
    }

    public void ReduceHealth()
    {
        health = playerHealth.DamagePlayer(health);
        //flashRed.FlashColor(0.25f);
        animator.SetTrigger("TakeDamage");

        if (health <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void StopAttack()
    {
        animator.SetInteger("Attack", 0);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, Vector2.down * groundRayLength);

        Gizmos.DrawRay(transform.position, Vector2.right * damageRayLength);
        Gizmos.DrawRay(transform.position, Vector2.left * damageRayLength);
    }

    public void RespawnPlayer()
    {
        transform.position = curentCheckpoint;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Checkpoint")
        {
            curentCheckpoint = collision.transform.position;
        }

        if (collision.tag == "Hazard")
        {
            ReduceHealth();
            RespawnPlayer();
        }
    }
}