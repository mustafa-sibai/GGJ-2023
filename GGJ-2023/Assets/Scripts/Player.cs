using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            GameManager.instance.IncreaseGlobalSpeed();
        }
        else
        {
            GameManager.instance.DecreaseGlobalSpeed();
        }

        Vector3 direction = new Vector3(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical"),
            0).normalized;

        transform.position += direction * speed * Time.deltaTime *
                GameManager.instance.globalSpeed;
    }
}