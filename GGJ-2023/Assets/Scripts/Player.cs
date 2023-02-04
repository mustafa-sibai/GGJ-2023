using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CustomMonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpForce;

    Rigidbody2D rb;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
    }

    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0, jumpForce));
        }

        if (Input.GetKey(KeyCode.A) || 
            Input.GetKey(KeyCode.D) ||
            rb.velocity.magnitude > 0.5f)
        {
            GameManager.instance.UpdateGame();
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
}