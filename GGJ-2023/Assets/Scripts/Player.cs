using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CustomMonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpForce;

    Rigidbody2D rb;

    bool isTouchingGround;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();

        isTouchingGround = false;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.D) ||
            rb.velocity.magnitude > 0.5f)
        {
            GameManager.instance.UpdateGame();
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position,
             Vector2.down,
             1,
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

    protected override void OnUpdate()
    {
        base.OnUpdate();

        Vector3 direction = new Vector3(
            Input.GetAxisRaw("Horizontal"),
            0,
            0).normalized;

        transform.position += direction * speed * Time.deltaTime;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, Vector2.down * 1);
    }
}