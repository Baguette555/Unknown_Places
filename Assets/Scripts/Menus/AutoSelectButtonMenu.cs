using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AutoSelectButtonMenu : MonoBehaviour
{
    public GameObject panel; 
    public Button defaultButton; // Le bouton à sélectionner automatiquement lorsque le panneau est ouvert

    private GameObject lastSelectedButton; // Pour mémoriser le dernier bouton sélectionné avant l'ouverture du panneau
    private bool panelWasActiveLastFrame = false;

    void Update()
    {
        if (panel.activeSelf)
        {
            if (!panelWasActiveLastFrame)
            {
                lastSelectedButton = EventSystem.current.currentSelectedGameObject;
                if (defaultButton != null && defaultButton.gameObject.activeInHierarchy && defaultButton.interactable)
                {
                    EventSystem.current.SetSelectedGameObject(defaultButton.gameObject);
                }
            }
        }
        else if (panelWasActiveLastFrame && lastSelectedButton != null)
        {
            if (((GameObject)lastSelectedButton).activeInHierarchy)
            {
                EventSystem.current.SetSelectedGameObject(lastSelectedButton);
            }
            lastSelectedButton = null;
        }

        panelWasActiveLastFrame = panel.activeSelf;
    }
}