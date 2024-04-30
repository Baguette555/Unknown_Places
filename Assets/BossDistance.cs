using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
    [SerializeField] Image bossIcon;

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

    void Update()
    {
        float playerX = player.position.x;
        float bossX = boss.position.x;

        float distX = Mathf.Abs(playerX - bossX);

        int distInt = Mathf.RoundToInt(distX);
        distanceText.text = "DISTANCE : " + (distInt - 6) + " m";

        if(distInt <= 12)
        {
            characterIcon.sprite = danger;
            bossIcon.sprite = bossWinning;
        }
        else if(distInt >= 13 && distInt <= 26) 
        {
            characterIcon.sprite = neutral;
            bossIcon.sprite = bossNeutral;
        }
        else
        {
            characterIcon.sprite = winning;
            bossIcon.sprite = bossLosing;
        }


    }
}
