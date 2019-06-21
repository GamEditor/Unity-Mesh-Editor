using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimationController : MonoBehaviour
{
    public static UIAnimationController Instanse;

    private void Awake()
    {
        Instanse = this;
    }
}