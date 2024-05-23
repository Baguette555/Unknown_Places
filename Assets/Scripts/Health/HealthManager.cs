using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using TMPro;

public class HealthManager : MonoBehaviour
{
    public int playerHealth = 3;
    [SerializeField] Image healthImage;

    [SerializeField] Sprite hearts0;
    [SerializeField] Sprite hearts1;
    [SerializeField] Sprite hearts2;
    [SerializeField] Sprite hearts3;

    public SpawnPoint spawnPoint;
    [SerializeField] Animator deathTransition;
    public discordManager discordManager;
    public NewControls NewControls;

    private bool deathAdded = false;
    private int deaths;
    [SerializeField] TextMeshProUGUI deathCounter;


    private void Awake()
    {
        Debug.Log("Awake:" + SceneManager.GetActiveScene().name);
    }

    public void GainHealth(InputAction.CallbackContext context)
    {
        if (context.performed && playerHealth < 3)
        {
            playerHealth += 1;
        }
    }
    public void LoseHealth(InputAction.CallbackContext context)
    {
        if (context.performed && playerHealth > 0)
        {
            playerHealth -= 1;
        }
    }

    void FixedUpdate()
    {
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
            StartCoroutine(deathRespawn());
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void TakeDamage(int damage)
    {
        if (playerHealth > 0)
        {
            playerHealth -= damage;
            discordManager.updateActivity();
            Debug.Log("HPs :" + playerHealth);
        }
    }

    IEnumerator deathRespawn()
    {
        if (deathAdded == false)
        {
            deaths = deaths + 1;
            deathAdded = true;
        }
        Debug.Log("Player died");
        deathCounter.text = "Death N°" + deaths;
        NewControls.speed = 0f;
        deathTransition.SetBool("ded", true);

        yield return new WaitForSeconds(0.8f);
        playerHealth = 3; // healed back
        spawnPoint.RespawnToSpawnPoint();
        yield return new WaitForSeconds(0.5f);
        NewControls.speed = 8f;
        deathAdded = false;

        deathTransition.SetBool("ded", false);
    }
}