using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoad : MonoBehaviour
{
    [Header("Défini automatiquement, pas besoin d'y toucher.")]
    [SerializeField] private GameObject originalGameObject;


    private void Awake()
    {
        originalGameObject = this.gameObject;
        DontDestroyOnLoad(originalGameObject);
    }

    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        int sceneBuildIndex = currentScene.buildIndex;
        string sceneName = currentScene.name;
        Debug.Log(sceneName + sceneBuildIndex);

        if(sceneBuildIndex == 4)        // Cuts the "intro" music to let the "game" music play (note: didnt had time for the game music, sorry all)
        {
            Destroy(GameObject.Find("TutorialMusic"));
        }

        // =====================================================================================
        // ================== USING BUILD INDEXES TO DESTROY OLD LEVELS TIME ===================
        if (sceneBuildIndex == 6) // Using BuildIndex works better than sceneName
        { 
            Destroy(GameObject.Find("### PermaUI_CH01_LV01 ###"));
        }
        if (sceneBuildIndex == 8)
        {
            Destroy(GameObject.Find("### PermaUI_CH01_LV02 ###"));
        }
        if (sceneName == "SCN_CH02_LV02") // 10 ?
        {
            Destroy(GameObject.Find("### PermaUI_CH02_LV01 ###"));
        }
        // =====================================================================================
    }
}
