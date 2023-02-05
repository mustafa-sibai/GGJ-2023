using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSystem : MonoBehaviour
{
    [SerializeField] CharacterWobble characterWobble;

    [SerializeField][TextArea(3, 10)] string[] dialog;

    int index = 0;

    void Start()
    {
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            characterWobble.SetText(dialog[index]);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            index++;

            if (index >= dialog.Length)
            {
                index = 0;
            }

            characterWobble.SetText(dialog[index]);
        }
    }

    public void StartDialog()
    {
        index = 0;
    }
}
