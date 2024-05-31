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

    [SerializeField] float characterIconPos;
    [SerializeField] float bossIconPos;

    [SerializeField] Image characterIcon;
    [SerializeField] Image bossIcon;

    [SerializeField] Sprite danger;
    [SerializeField] Sprite neutral;
    [SerializeField] Sprite winning;

    [SerializeField] Sprite bossWinning;
    [SerializeField] Sprite bossNeutral;
    [SerializeField] Sprite bossLosing;

    [SerializeField] float iconMinDistance;// = 50f; // Distance minimale entre les icônes
    [SerializeField] float iconMaxDistance;// = -5f; // Distance maximale entre les icônes
    [SerializeField] float minIconPosX;// = 170f; // Position minimale de l'icône du boss
    [SerializeField] float maxIconPosX; //= 600f; // Position maximale de l'icône du boss

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
        distanceText.text = "DISTANCE : " + (distInt - 4) + " m";

        float normalizedDistance = Mathf.Clamp01((distX - iconMinDistance) / (iconMaxDistance - iconMinDistance));
        float newIconPosX = Mathf.Lerp(minIconPosX, maxIconPosX, normalizedDistance);

        bossIcon.rectTransform.anchoredPosition = new Vector2(newIconPosX, bossIcon.rectTransform.anchoredPosition.y);


        if (distInt <= 12)
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
