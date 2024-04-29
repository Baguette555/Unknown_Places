using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsResolutions : MonoBehaviour
{
    public TMPro.TMP_Dropdown resolutionDropdown;
    private bool isFullscreen;
    [SerializeField] GameObject resolutionsDrop;
    [SerializeField] Button FullscreenButton;

    Resolution[] resolutions;

    void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height + " @ " + resolutions[i].refreshRateRatio + "hz";
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)          // Fonctionne uniquement en mode Fenêtré
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetQuality(int qualityIndex)                // Qualité graphique du jeu
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    public void ShowResolution(bool activeResolution)       // Afficher les résolutions que lorsque le mode Fullscreen est activé /// PAS FONCTIONNEL POUR L'INSTANT
    {
        activeResolution = !activeResolution;
        if (isFullscreen == true)
        {
            resolutionsDrop.SetActive(false);
        }
        else
        {
            resolutionsDrop.SetActive(true);
        }
    }
}
