using UnityEngine;

public class changeZoneIntermission : MonoBehaviour
{
    public LevelLoader levelLoader;     // Connects to the levelLoader script.

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            levelLoader.LoadNextLevel();
        }
    }
}