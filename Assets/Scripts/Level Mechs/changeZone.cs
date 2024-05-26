using UnityEngine;

public class changeZone : MonoBehaviour
{
    public LevelLoader levelLoader;     // Connects to the levelLoader script.
    public LevelChrono levelChrono;     // Connects to the LevelChrono script.

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            levelChrono.PauseTimer();
            levelLoader.LoadNextLevel();
        }
    }
}