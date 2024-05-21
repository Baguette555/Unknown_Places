using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoad : MonoBehaviour
{
    [Header("Défini automatiquement, pas besoin d'y toucher.")]
    [SerializeField] private GameObject originalGameObject;
    private void Awake()
    {
        //GameObject child = originalGameObject.transform.GetChild(0).gameObject; // Get the child of the UI canvas


        Scene currentScene = SceneManager.GetActiveScene(); // get the scene name to destroy the timer when main menu is entered
        string nameScene = currentScene.name;
        if (nameScene == "SCN_MainMenu")
        {
            Destroy(this.gameObject);
            //Destroy(child);
        }

        originalGameObject = this.gameObject;   // else it does not destroy
        DontDestroyOnLoad(originalGameObject);
        //DontDestroyOnLoad(child);
    }
}
