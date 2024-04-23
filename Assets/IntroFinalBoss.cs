using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        //newControls.input.YourActionMap.Disable();
        newControls.speed = 0f;
        newControls.jumpForce = 0f;
        yield return new WaitForSeconds(3);
        newControls.speed = 8f;
        newControls.jumpForce = 9f;
    }

}
