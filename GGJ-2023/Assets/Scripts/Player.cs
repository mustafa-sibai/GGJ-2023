using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CustomMonoBehaviour
{
    [SerializeField] float speed;

    Rigidbody2D rb;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            GameManager.instance.UpdateGame();
        }
    }

    void FixedUpdate()
    {
        if(rb.velocity.magnitude > 0.5f)
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