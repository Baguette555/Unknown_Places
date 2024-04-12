using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;      // Pause Menu
    public GameObject gameUI;           // Health bar, dashses etc..

    public Animator transition;         // For transitions yea yea yea

    public TextMeshProUGUI levelText;

    void Awake()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        int sceneBuildIndex = currentScene.buildIndex;
        string sceneName = currentScene.name;
        Debug.Log(sceneName + sceneBuildIndex);

        if (sceneName == "TerrainJeuAlpha1")
        {
            levelText.text = "Chapitre 1 - Niveau 1";
        }
        else if(sceneName == "SCN_TestGrandNiveau" || sceneBuildIndex == 3)
        {
            levelText.text = "Chapitre 1 - Niveau 2";
        }
        else if(sceneName == "SCN_Intermission1")
        {
            levelText.text = "Chapitre 1 - Intermission 1";
        }
        else
        {
            float chance = 0.5f;
            bool heads = Random.value < chance;
            if (heads)
            {
                levelText.text = "where the flip are you";
            }
            else
            {
                levelText.text = "In an Unknown Place";
            }
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        gameUI.SetActive(true);

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        gameUI.SetActive(false);

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        StartCoroutine(LoadMenuT());

    }
    IEnumerator LoadMenuT()
    {
        transition.SetTrigger("MenuStart");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("SCN_MainMenu");
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        StartCoroutine(QuitGameT());
    }
    IEnumerator QuitGameT()
    {
        transition.SetTrigger("MenuStart");
        yield return new WaitForSeconds(1);
        Debug.Log("Jeu quitt�.");
        Application.Quit();
    }
}
