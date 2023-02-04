using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomMonoBehaviour : MonoBehaviour
{
    protected virtual void OnDestroy()
    {
        GameManager.instance.StartUpdateGame -= OnStartUpdate;
        GameManager.instance.StopUpdateGame -= OnStopUpdate;
    }

    protected virtual void Awake()
    {
        GameManager.instance.StartUpdateGame += OnStartUpdate;
        GameManager.instance.StopUpdateGame += OnStopUpdate;
    }

    protected virtual void Start()
    {
    }

    protected virtual void OnStartUpdate()
    {
    }

    protected virtual void OnStopUpdate()
    {
    }
}