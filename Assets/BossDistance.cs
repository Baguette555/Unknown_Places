using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossDistance : MonoBehaviour
{
    public TextMeshProUGUI distanceText;
    //public GameObject boss;
    public Transform boss;
    public Transform player;

    [SerializeField] Image characterIcon;

    [SerializeField] Sprite danger;
    [SerializeField] Sprite neutral;
    [SerializeField] Sprite winning;

    [SerializeField] Sprite bossWinning;
    [SerializeField] Sprite bossNeutral;
    [SerializeField] Sprite bossLosing;



    void Start()
    {
        distanceText.text = "DISTANCE : ";
    }

    void FixedUpdate()
    {
        float dist = Vector3.Distance(boss.position, player.position);      // Baser sur x : ça marche pas des masses
        Debug.Log(dist);
        int distInt = (int)dist;
        //int distInt = Convert.ToInt32(dist);
        Debug.Log(distInt);
        distanceText.text = "DISTANCE : " + (distInt - 17) + " m";
    }
}
