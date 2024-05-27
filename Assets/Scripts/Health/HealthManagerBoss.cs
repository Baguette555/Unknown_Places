using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class HealthManagerBoss : MonoBehaviour
{
    public int playerHealth = 3;
    [SerializeField] Image healthImage;

    [SerializeField] Sprite hearts0;
    [SerializeField] Sprite hearts1;
    [SerializeField] Sprite hearts2;
    [SerializeField] Sprite hearts3;

    public discordManagerBoss discordManager;

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
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
}