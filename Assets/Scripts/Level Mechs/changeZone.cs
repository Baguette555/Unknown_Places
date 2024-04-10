using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeZone : MonoBehaviour
{
    public LevelLoader levelLoader;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            levelLoader.LoadNextLevel();
            //SceneManager.LoadScene("SCN_TestGrandNiveau");
        }
    }
}
