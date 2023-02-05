using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAnimationEffect : MonoBehaviour
{
    public void StopAttackEffectAnimation()
    {
        gameObject.SetActive(false);
    }
}