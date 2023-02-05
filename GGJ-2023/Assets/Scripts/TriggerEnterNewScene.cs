using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnterNewScene : MonoBehaviour
{

    SceneLoading sceneLoadScript;

    void Start()
    {
        sceneLoadScript = FindObjectOfType<SceneLoading>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
        sceneLoadScript.LoadTheScene();

        }
    }

    public void ANNALOADSCENE()
    {
        sceneLoadScript.LoadTheScene();

    }

}
