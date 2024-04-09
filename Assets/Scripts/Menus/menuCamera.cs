using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuCamera : MonoBehaviour
{
    [SerializeField] GameObject ImageManager;

    private void Awake()
    {
        ImageManager = gameObject.GetComponent<GameObject>();
    }

    public void MenuToSettingsCamera()
    {
        ImageManager.transform.Translate(Vector2.right * Time.deltaTime, 0);
    }

    public void SettingsToMainMenuCamera()
    {
        ImageManager.transform.Translate(Vector2.left * Time.deltaTime, 0);
    }
}
