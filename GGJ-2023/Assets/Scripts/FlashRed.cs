using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlashRed : MonoBehaviour
{
    [SerializeField] float speed;

    SpriteRenderer spriteRenderer;

    bool flashToColor;
    bool flashBack;
    float dely;
    float timer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        flashToColor = false;
        flashBack = false;
        dely = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer <= dely)
            return;

        if (flashToColor)
        {
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, Color.red, speed * Time.deltaTime);
            if (spriteRenderer.color == Color.red)
            {
                spriteRenderer.color = Color.red;
                flashToColor = false;
                flashBack = true;
            }
        }

        if (flashBack)
        {
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, Color.white, speed * Time.deltaTime);
            if (spriteRenderer.color == Color.white)
            {
                spriteRenderer.color = Color.white;
                flashToColor = false;
                flashBack = false;
            }
        }
    }

    public void FlashColor(int dely = 0)
    {
        flashToColor = true;
        flashBack = false;
        spriteRenderer.color = Color.white;
        timer = 0;
    }
}