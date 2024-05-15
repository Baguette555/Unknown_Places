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

    public int levelInt;
    public int chapterInt;

    public string levelTxt;

    public Animator rideauUI;

    //[SerializeField] string[] Intermissions;      Au cas où il faudrait stocker toutes les scènes d'intermissions pour aller + vite ce sera là sinon ce sera tout écrit un à un vive l'optimisation

    void Awake()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        int sceneBuildIndex = currentScene.buildIndex;
        string sceneName = currentScene.name;
        Debug.Log(sceneName + sceneBuildIndex);


        // ====================================================== TITRE DU NIVEAU DU MENU PAUSE & AFFICHAGE NIVEAU DRP
        // ================================================== Tout est rangé dans l'ordre d'apparition des niveaux en jeu.
        if (sceneName == "TerrainJeuAlpha1")
        {
            levelText.text = "Chapitre 1 - Niveau 1";
            levelTxt = "1"; // Ce qui sera affiché sur Discord sous le format : "Niveau (levelTxt)"
            chapterInt = 1;
            //levelInt = 1;
        }
        else if (sceneName == "SCN_TutoLevel1") // Tuto avant le jeu
        {
            levelText.text = "Chapitre 0 - Introduction";
            levelTxt = "d'introduction";
            chapterInt = 0;
        }
        else if (sceneName == "SCN_Intermission01_01") // SCN_Intermission[Chapitre]_[Numéro]
        {
            levelText.text = "Chapitre 1 - Avant-Jeu";
            levelTxt = "d'intermission 0";
            chapterInt = 0;
        }
        else if (sceneName == "SCN_C01_L01")    // SCN_C[Chapitre]_L[Niveau]
        {
            levelText.text = "Chapitre 1 - Niveau 1";
            levelTxt = "1";
            chapterInt = 1;
        }
        else if (sceneName == "SCN_C01_L02")
        {
            levelText.text = "Chapitre 1 - Niveau 2";
            levelTxt = "2";
            chapterInt = 1;
        }
        else if (sceneName == "SCN_TestGrandNiveau") //|| sceneBuildIndex == 3)
        {
            levelText.text = "Chapitre 1 - Niveau 2";
            levelTxt = "2";
            chapterInt = 1;
        }
        else if (sceneName == "SCN_Intermission1")
        {
            levelText.text = "Chapitre 1 - Intermission 1";
            levelTxt = "Intermission 1";
            chapterInt = 1;
        }
        else if (sceneName == "SCN_TestBottes")
        {
            levelText.text = "Chapitre 2 - Niveau 1";
            levelTxt = "1";
            chapterInt = 2;
        }
        else if (sceneName == "SCN_CoursePoursuite")
        {
            levelText.text = "Chapitre 4 - Couloir";
            levelTxt = "4";
            chapterInt = 4;
        }
        else
        {
            levelText.text = "Debugging in an Unknown Place";
            levelTxt = "Debug Mode";
            levelInt = 0;
            chapterInt = 0;
        }
    }

    public void Pause(InputAction.CallbackContext context)                   // ======================== PAUSING [NEW]
    {
        if(GameIsPaused == false)
        {
            Pause();
            rideauUI.SetBool("Paused", true);           // Anim rideau
        }
        else
        {
            Resume();
            rideauUI.SetBool("Resumed", true);
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
