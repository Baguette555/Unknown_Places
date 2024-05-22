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
}
