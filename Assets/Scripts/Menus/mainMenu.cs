using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class mainMenu : MonoBehaviour
{
    public void TitleButton()
    {
        this.transform.Rotate(5, 6, 7, Space.World);
    }

    public void ButtonPlay()
    {
        SceneManager.LoadScene("TerrainJeuAlpha1");
    }

    public void QuitGame()
    {
        Debug.Log("Jeu quitté");
        Application.Quit();
    }
}
