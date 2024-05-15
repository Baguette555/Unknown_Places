using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.TimeZoneInfo;

public class stopVideo : MonoBehaviour
{
    [Header("Changer la valeur en secondes du temps de la vidéo jouée.")]
    [SerializeField] float videoTime;

    void Start()
    {
        StartCoroutine(stopVideoClip());
    }

    IEnumerator stopVideoClip()
    {
        yield return new WaitForSeconds(videoTime);
        this.gameObject.SetActive(false);
    }
}
