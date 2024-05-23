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
        originalGameObject = this.gameObject;
        DontDestroyOnLoad(originalGameObject);
        //DontDestroyOnLoad(child); // Only the root can be used for DontDestroyOnLoad.
    }

    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        int sceneBuildIndex = currentScene.buildIndex;
        string sceneName = currentScene.name;
        Debug.Log(sceneName + sceneBuildIndex);

        if (sceneBuildIndex == 6)       // BUILD INDEX >>>>>>>>>>>>>>
        {
            Destroy(GameObject.Find("### PermaUI_CH01_LV01 ###"));
        }
        if (sceneBuildIndex == 8)
        {
            Destroy(GameObject.Find("### PermaUI_CH01_LV02 ###"));
        }
        if (sceneName == "SCN_CH02_LV02")
        {
            Destroy(GameObject.Find("### PermaUI_CH02_LV01 ###"));
        }
    }
}
