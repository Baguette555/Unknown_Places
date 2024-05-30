using UnityEngine;

public class startFinalCinematic : MonoBehaviour
{
    // This Script will start the final Cinematic at the end of the game.

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Chrono.TimerPaused();
            Debug.Log("Cinématique lancée");
            Time.timeScale = 0f;
        }
    }
}
