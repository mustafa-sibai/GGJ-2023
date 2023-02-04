using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomMonoBehaviour : MonoBehaviour
{
    protected virtual void OnDestroy()
    {
        GameManager.instance.UpdateGame -= OnUpdate;
    }

    protected virtual void Awake()
    {
        GameManager.instance.UpdateGame += OnUpdate;
    }

    protected virtual void Start()
    {
    }

    protected virtual void OnUpdate()
    {
    }
}