using UnityEngine;

public class SettingsFullscreen : MonoBehaviour
{
    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
