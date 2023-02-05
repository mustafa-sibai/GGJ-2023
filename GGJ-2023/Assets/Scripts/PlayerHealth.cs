using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    Transform[] hearts;

    void Start()
    {
        hearts = transform.GetComponentsInChildren<Transform>();
    }

    public int DamagePlayer(int CurrentHealth)
    {
        if (CurrentHealth <= 0)
            return CurrentHealth;

        hearts[CurrentHealth].gameObject.SetActive(false);
        CurrentHealth--;

        return CurrentHealth;
    }
}