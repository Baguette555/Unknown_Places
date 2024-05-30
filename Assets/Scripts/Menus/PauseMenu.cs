using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public bool isPaused = false; // For other scripts
    public bool canPause = true;

    [Header("Menus UIs")]
    public GameObject pauseMenuUI;      // Pause Menu
    public GameObject confMenuBox;      // The little confimation box before going back to menu for sure
    public GameObject confQuitBox;      // The little confimation box before quitting the game for sure
    public GameObject settingsMenu;

    public GameObject gameUI;           // Health bar, dashses etc..

    [Header("Player's stuff")]
    public NewControls newControls;
    public InputActionAsset inputActions;

    [Header("Pause menu transition")]
    public Animator transition;         // For transitions yea yea yea

    [Header("Menu Pause tracking level")]
    public TextMeshProUGUI levelText;

    public int levelInt;
    public int chapterInt;

    public string levelTxt;

    [Header("Transition du rideau")]
    public Animator rideauUI;

    void Awake()    // Reduce this part for better visibility. Awake() is used for Pause & Discord only.
    {
        Scene currentScene = SceneManager.GetActiveScene();
        int sceneBuildIndex = currentScene.buildIndex;
        string sceneName = currentScene.name;
        Debug.Log(sceneName + sceneBuildIndex);


        // ====================================================== LEVEL NAMING ON PAUSE + ON DISCORD RICH PRESENCE
        // ================================================== Will be shown when pausing the game and on the Discord's profile.
        // ================================================== Sorted by level appereance in game.
        if (sceneName == "SCN_TutoLevel1")
        {
            levelText.text = "Chapitre 0 - Introduction";   // The text that will be shown in the Pause Menu.
            levelTxt = "d'introduction";                    // Will be shown in Discord in this form: "Niveau (levelTxt)"
            chapterInt = 0;                                 // Will be shown in Discord in this form: "Chapitre (chapterInt)"
        }
        else if (sceneName == "SCN_Intermission1") // SCN_Intermission[NuméroX]
        {
            levelText.text = "Chapitre 1 - Avant-Jeu";
            levelTxt = "d'intermission 1";
            chapterInt = 1;
        }
        else if (sceneName == "SCN_C01_L01")    // SCN_C[Chapitre0X]_L[Niveau0X]
        {
            levelText.text = "Chapitre 1 - Niveau 1";
            levelTxt = "1";
            chapterInt = 1;
        }
        else if (sceneName == "SCN_Intermission2")
        {
            levelText.text = "Chapitre 1 - Intermission 2";
            levelTxt = "d'intermission 2";
            chapterInt = 1;
        }
        else if (sceneName == "SCN_C01_L02")
        {
            levelText.text = "Chapitre 1 - Niveau 2";
            levelTxt = "2";
            chapterInt = 1;
        }
        else if (sceneName == "SCN_Intermission3")
        {
            levelText.text = "Chapitre 2 - Intermission 1";
            levelTxt = "d'intermission 3";
            chapterInt = 2;
        }
        else if (sceneName == "SCN_C02_L01")
        {
            levelText.text = "Chapitre 2 - Niveau 1";
            levelTxt = "1";
            chapterInt = 2;
        }
        else if (sceneName == "SCN_Intermission4")
        {
            levelText.text = "Chapitre 2 - Intermission 2";
            levelTxt = "d'intermission 4";
            chapterInt = 2;
        }
        else if (sceneName == "SCN_C02_L02")
        {
            levelText.text = "Chapitre 2 - Niveau 2";
            levelTxt = "2";
            chapterInt = 2;
        }
        else if (sceneName == "SCN_Intermission5")
        {
            levelText.text = "Chapitre 2 - Intermission 2";
            levelTxt = "d'intermission 5";
            chapterInt = 2;
        }
        else if (sceneName == "SCN_C03_L01")
        {
            levelText.text = "Chapitre 3 - Niveau 1";
            levelTxt = "1";
            chapterInt = 3;
        }
        else if (sceneName == "SCN_Intermission6")
        {
            levelText.text = "Chapitre 3 - Intermission 1";
            levelTxt = "d'intermission 6";
            chapterInt = 3;
        }
        else if (sceneName == "SCN_C04_L01")
        {
            levelText.text = "Chapitre 4 - Backstage";
            levelTxt = "1";
            chapterInt = 4;
        }
        else if (sceneName == "SCN_CoursePoursuite")
        {
            levelText.text = "Chapitre 4 - Couloir";
            levelTxt = "Run!";
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


    public void DisablePlayerInputs()
    {
        newControls.canFlip = false;
        newControls.canDash = false;
        newControls.canMove = false;
        newControls.canJump = false;
        canPause = false;

        newControls.speed = 0f;
        //var playerActionMap = inputActions.FindActionMap("Player");           // too laggy to test this out. Booleans works better for now.
        //playerActionMap.Disable();
        Debug.Log("Player inputs disabled");
    }

    public void EnablePlayerInputs()
    {
        newControls.canFlip = true;
        newControls.canDash = true;
        newControls.canMove = true;
        newControls.canJump = true;
        canPause = true;

        newControls.speed = 8f;
        //var playerActionMap = inputActions.FindActionMap("Player");
        //playerActionMap.Enable();
        Debug.Log("Player inputs enabled");
    }

    public void Pause(InputAction.CallbackContext context)                   // ======================== PAUSING [NewInputSystem]
    {
        if (canPause == true)
        {
            if (GameIsPaused == false)
            {
                rideauUI.SetBool("Paused", true);           // A curtain animation should start when pausing. This is only a placeholder.
                DisablePlayerInputs();
                isPaused = true;
                canPause = true;
                Pause();
            }
            else
            {
                Resume();
                isPaused = false;
                rideauUI.SetBool("Resumed", true);
                EnablePlayerInputs();
            }
        }
    }

    public void Resume()
    {
        isPaused = false;
        EnablePlayerInputs();
        gameUI.SetActive(true);
        //Chrono.TimerPaused();     Not used for now, but stays here just in case.

        pauseMenuUI.SetActive(false);
        confMenuBox.SetActive(false);
        confQuitBox.SetActive(false);
        settingsMenu.SetActive(false);

        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {  
        gameUI.SetActive(false);
        //Chrono.TimerPaused();

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
