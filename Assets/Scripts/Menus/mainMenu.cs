using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static System.TimeZoneInfo;

public class mainMenu : MonoBehaviour
{
    public LevelLoader levelLoader;     // Connects to the levelLoader script.

    public void TitleButton()
    {
        this.transform.Rotate(5, 6, 7, Space.World);
    }

    public void ButtonPlay()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        levelLoader.transition.SetTrigger("MenuStart");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Jeu quitté");
        Application.Quit();
    }
}
