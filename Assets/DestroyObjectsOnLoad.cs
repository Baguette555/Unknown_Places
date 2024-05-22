using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectsOnLoad : MonoBehaviour
{
    // Used to destroy everything that need to be destroyed when restarting the game.
    void Start()
    {
        Destroy(GameObject.Find("### PermaUI ###"));    // UI used for chrono
        Destroy(GameObject.Find("TutorialMusic"));      // Music
    }
}
