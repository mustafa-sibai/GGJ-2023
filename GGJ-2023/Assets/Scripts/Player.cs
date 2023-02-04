using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : CustomMonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] int groundRayLength;
    [SerializeField] TMP_Text playerHealthText;

    Rigidbody2D rb;
    FlashRed flashRed;

    bool isTouchingGround;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        flashRed = GetComponent<FlashRed>();

        isTouchingGround = false;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.D) ||
            rb.velocity.magnitude > 0.5f)
        {
            GameManager.instance.StartUpdateGame();
        }
        else
        {
            GameManager.instance.StopUpdateGame();
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position,
             Vector2.down,
             groundRayLength,
             LayerMask.GetMask("Ground"));

        if (hit.collider != null)
        {
            isTouchingGround = true;
        }
        else
        {
            isTouchingGround = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isTouchingGround)
        {
            rb.AddForce(new Vector2(0, jumpForce));
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

        transform.position += direction * speed * Time.deltaTime;
    }

    protected override void OnStopUpdate()
    {
        base.OnStopUpdate();
    }

    public void IncreaseHealth(int incrementBy)
    {
        health += incrementBy;
        playerHealthText.text = $"Health: {health}";
    }

    public void ReduceHealth(int reduceBy)
    {
        health -= reduceBy;
        flashRed.FlashColor(100);
        playerHealthText.text = $"Health: {health}";
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, Vector2.down * 1);
    }
}