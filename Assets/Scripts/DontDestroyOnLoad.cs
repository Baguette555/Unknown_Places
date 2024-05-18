using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    [Header("Défini automatiquement, pas besoin d'y toucher.")]
    [SerializeField] private GameObject originalGameObject;
    private void Awake()
    {
        originalGameObject = this.gameObject;
        DontDestroyOnLoad(originalGameObject);
        GameObject child = originalGameObject.transform.GetChild(0).gameObject;
        DontDestroyOnLoad(child);
    }
}
