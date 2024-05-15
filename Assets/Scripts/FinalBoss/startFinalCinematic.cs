using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startFinalCinematic : MonoBehaviour
{
    // This Script will start the final Cinematic at the end of the game.


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Cinématique lancée");
        }
    }
}
