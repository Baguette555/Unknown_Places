using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int playerHealth = 3;
    [SerializeField] Image healthImage;

    [SerializeField] Sprite hearts0;
    [SerializeField] Sprite hearts1;
    [SerializeField] Sprite hearts2;
    [SerializeField] Sprite hearts3;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O) && playerHealth >= 0)
        {
            playerHealth -= 1;
        }
        if (Input.GetKeyDown(KeyCode.I) && playerHealth < 3)
        {
            playerHealth += 1;
        }
        if (playerHealth == 3)
        {
            healthImage.sprite = hearts3;
        }
        if (playerHealth <= 2)
        {
            healthImage.sprite = hearts2;
        }
        if (playerHealth <= 1)
        {
            healthImage.sprite = hearts1;
        }
        if (playerHealth <= 0)
        {
            healthImage.sprite = hearts0;
        }
    }

    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
    }
}