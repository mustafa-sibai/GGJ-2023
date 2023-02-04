using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [HideInInspector] public float globalSpeed = 0;

    [SerializeField] float globalSpeedIncrementBy;
    [SerializeField] float globalSpeedDecrementBy;
    [SerializeField] float maxGlobalSpeed;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void IncreaseGlobalSpeed()
    {
        globalSpeed = 1;
        //globalSpeed += globalSpeedIncrementBy * Time.deltaTime;
        //globalSpeed = Mathf.Clamp(globalSpeed, 0, maxGlobalSpeed);
    }

    public void DecreaseGlobalSpeed()
    {
        globalSpeed = 0;
        //globalSpeed -= globalSpeedDecrementBy * Time.deltaTime;
        //globalSpeed = Mathf.Clamp(globalSpeed, 0, maxGlobalSpeed);
    }
}