using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class IntroFinalBoss : MonoBehaviour
{
    public NewControls newControls;
    IEnumerator coroutine;

    void Start()
    {
        StartCoroutine(Intro());
    }

    IEnumerator Intro()
    {
        // Disable ce input system ou la mort
        newControls.speed = 0f;
        newControls.jumpForce = 0f;
        yield return new WaitForSeconds(3);
        newControls.speed = 8f;
        newControls.jumpForce = 9f;
    }

}
