using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [Header("Set the falling and rising speed of the platform.")]
    public GameObject Platform;
    private float startPos;
    private float XstartPos;
    private Rigidbody2D rb;
    [SerializeField] float fallSpeed = 2.5f;
    [SerializeField] float riseSpeed = 1.5f;

    Vector2 platformRespawnPoint;

    [Header("[Debug purposes] Booleans")]
    [SerializeField] bool isRising = false;
    [SerializeField] bool playerOnPlatform = false;

    [Header("Easter Egg")]
    [SerializeField] AudioSource sound;
    public bool soundPlayed = false; // Public in order to reset the sound when retrying

    private void Start()
    {
        Platform = this.gameObject;
        startPos = transform.position.y;
        XstartPos = transform.position.x;
        Debug.Log(startPos);
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;

        platformRespawnPoint = new Vector2(XstartPos, startPos);
    }

    private void OnTriggerEnter2D(Collider2D collision)             // Using Trigger and not Collision for the better flemme d'écrire en anglais là
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = false;
        }
    }

    private void Update()
    {
        if (playerOnPlatform)
        {
            // Si le joueur est sur la plateforme, elle descend
            rb.velocity = new Vector2(0, -fallSpeed);
        }
        else if (!isRising && transform.position.y <= startPos)
        {
            // La plateforme remonte
            isRising = true;
            StartCoroutine(RisePlatform());
        }

        if (rb.velocity.y > riseSpeed + 0.5f)
        {
            if (soundPlayed == false)
            {
                sound.Play();
                soundPlayed = true;
            }
        }
    }

    private IEnumerator RisePlatform()
    {
        while (transform.position.y < startPos)
        {
            if (playerOnPlatform)
            {
                isRising = false; // Annuler la remontée
                yield break;
            }

            rb.velocity = new Vector2(0, riseSpeed);
            yield return null;
        }

        rb.velocity = Vector2.zero;
        isRising = false;
    }

    public void PlatformReset()
    {
        playerOnPlatform = false;
        soundPlayed = false;
        rb.velocity = new Vector2(0, 0);
        Platform.transform.position = platformRespawnPoint;
    }
}
