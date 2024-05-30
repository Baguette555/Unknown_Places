using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class controllerOptions : MonoBehaviour
{
    [Header("Panels")]
    public GameObject Panel;    // Settings options
    bool visible = false;

    [Header("Audio sources")]
    public AudioSource audiosource; // Global Music audio manager
    public AudioSource SFX; // SFX audio manager

    [Header("Sliders")]
    public Slider SliderV;  // Slider for global volume
    public Slider SliderSFX;    // Slider for SFX volume

    [Header("Toggles")]
    public Toggle fullScreenBox; // Toggle for the Fullscreen

    [Header("Back Button")]
    public GameObject backButton;

    [Header("Texts for volume values")]
    public TMP_Text TxtVolume;
    public TMP_Text TxtSFX;

    private GameObject originalFirstSelected;

    private void Start()
    {
        SliderChange();
        SliderChangeSFX();

        if (EventSystem.current != null)
        {
            originalFirstSelected = EventSystem.current.firstSelectedGameObject;
        }
    }

    public void EscapePause(InputAction.CallbackContext context)
    {
        if (context.performed && visible)
        {
            visible = false;
            Panel.SetActive(false);
            RestoreOriginalFocus();
        }
    }

    public void OpenOptionsPanel()
    {
        visible = true;
        Panel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        //EventSystem.current.SetSelectedGameObject(backButton);
    }

    public void RestoreOriginalFocus()
    {
        if (originalFirstSelected != null)
        {
            EventSystem.current.SetSelectedGameObject(originalFirstSelected);
        }
    }

    public void SliderChange()
    {
        audiosource.volume = SliderV.value;
        TxtVolume.text = "Volume " + (audiosource.volume * 100).ToString("0") + "%";
    }

    public void SliderChangeSFX()
    {
        SFX.volume = SliderSFX.value;
        TxtSFX.text = "SFX " + (SFX.volume * 100).ToString("0") + "%";
    }
}
