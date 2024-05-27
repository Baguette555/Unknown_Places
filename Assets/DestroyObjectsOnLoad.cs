using UnityEngine;

public class DestroyObjectsOnLoad : MonoBehaviour
{
    // Used to destroy everything that need to be destroyed when restarting the game / going back to main menu.
    void Start()
    {
        Destroy(GameObject.Find("### PermaUI ###"));              // UI used for chrono
        Destroy(GameObject.Find("### PermaUI_CH01_LV01 ###"));    // UI used for chapter 1 level 1 chrono
        Destroy(GameObject.Find("### PermaUI_CH01_LV02 ###"));    // UI used for chapter 1 level 2 chrono
        Destroy(GameObject.Find("TutorialMusic"));                // Music
    }
}
