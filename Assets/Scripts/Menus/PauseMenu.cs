using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;      // Pause Menu
    public GameObject confMenuBox;      // The little confimation box before going back to menu for sure
    public GameObject confQuitBox;      // The little confimation box before quitting the game for sure

    public GameObject gameUI;           // Health bar, dashses etc..

    public Animator transition;         // For transitions yea yea yea

    public TextMeshProUGUI levelText;

    void Awake()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        int sceneBuildIndex = currentScene.buildIndex;
        string sceneName = currentScene.name;
        Debug.Log(sceneName + sceneBuildIndex);


        // ================================================== TITRE DU NIVEAU DU MENU PAUSE: UTILISER LE BUILDINDEX LORS DU RELEASE
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
        else if (sceneName == "SCN_TestBottes")
        {
            levelText.text = "Chapitre 2 - Niveau 1";
        }
        else if (sceneName == "SCN_CoursePoursuite")
        {
            levelText.text = "Chapitre 4 - Couloir";
        }
        else
        {
            levelText.text = "Debugging in an Unknown Place";
        }
    }

    public void Pause(InputAction.CallbackContext context)                   // ============== PAUSING [NEW]
    {
        if(GameIsPaused == false)
        {
            Pause();
        }
        else
        {
            Resume();
        }
    }

    public void Resume()
    {
        gameUI.SetActive(true);

        pauseMenuUI.SetActive(false);
        confMenuBox.SetActive(false);
        confQuitBox.SetActive(false);

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
        Debug.Log("Jeu quitté.");
        Application.Quit();
    }
}
