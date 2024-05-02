using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class NewControls : MonoBehaviour
{
    [Header("Déjà défini automatiquement. Changer si ça ne fonctionne pas.")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] SpriteRenderer sprite_renderer;

    [Header("isGrounded?! :( faut pas gronder")]
    public Transform groundCheck;
    public LayerMask groundLayer;

    [Header("Movement and speed")]
    private float horizontal;
    public float speed = 8f;
    public float jumpForce = 9f;
    private bool isFacingRight = true;
    private Vector2 move;
    public bool canFlip = true;
    [SerializeField] Animator playerAnimator;

    [Header("Dashing proprieties")]
    public bool canDash = true;                 // Public for dashCooldownImage
    public bool isDashing;
    [SerializeField] float dashSpeed = 30f;
    [SerializeField] float dashingTime = 0.4f;
    [SerializeField] float dashingCooldown = 1f;
    [SerializeField] float dashGravity = 0f;

    [SerializeField] float startDashTime = 0.3f;

    private float waitTime;
    private float normalGravity;
    private IEnumerator coroutine;

    [Header("Dash Trail Renderer")]
    [SerializeField] TrailRenderer trail;

    [Header("Boots properties")]
    public bool hasBoots = false;
    // Activer un particle machin pour les bottes. à voir plus tard ofc

    [Header("Script de l'image de cooldown.")]
    public dashCooldownImage dashCooldownImage;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        normalGravity = rb.gravityScale;
        isFacingRight = true;

        Scene currentScene = SceneManager.GetActiveScene();
        int sceneBuildIndex = currentScene.buildIndex;
        if(currentScene.buildIndex >= 4)
        {
            hasBoots = true;
        }
    }
    void Start()
    {
        sprite_renderer = GetComponent<SpriteRenderer>();
        move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (hasBoots == true)
        {
            Debug.Log("Bottes actives. Lancer une anim.");
            // Démarrer animation des bottes ?
            // Particules permanantes pour les bottes : faire comprendre que les bottes brillent
        }
    }

    void Update()
    {
        waitTime = Time.deltaTime;
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        if(isDashing)
        {
            return;
        }
        if (!isFacingRight && horizontal > 0f)
        {
            Flip();
        }
        else if (isFacingRight && horizontal < 0f)
        {
            Flip();
        }
    }

    private bool IsGrounded()                                               // ============== JUMP : GROUND DETECTION [NEW]
    {
        playerAnimator.SetBool("Jumping", false);                      // Animation stops for the jump
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()                                                      // ============== FLIP [NEW]
    {
        if (canFlip == true)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
        if(context.performed)
        {
            playerAnimator.SetBool("Running", true);                      // Animation plays for the running sprites
            playerAnimator.SetBool("Jumping", false);                      // Animation stops for the jump
        }
        else if (context.canceled)
        {
            playerAnimator.SetBool("Running", false);
        }
    }

    public void Jump(InputAction.CallbackContext context)                   // ============== JUMP [NEW]
    {
        if (context.performed && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            playerAnimator.SetBool("Jumping", true);                      // Animation plays for the jump
        }

        if (context.canceled && rb.velocity.y > 0f)
        {
            playerAnimator.SetBool("Jumping", false);                      // Animation stops for the jump
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.3f);
        }
    }


    public void Dash(InputAction.CallbackContext context)                  // ============== NEW DASHING SYSTEM
    {
        if (context.performed && canDash == true && isDashing == false)
        {
            if(isFacingRight && canDash == true && isDashing == false)
            {
                StartCoroutine(Dash(Vector2.right));
                dashCooldownImage.DashImage();
            }
            else if(!isFacingRight && canDash == true && isDashing == false) 
            {
                StartCoroutine(Dash(Vector2.left));
                dashCooldownImage.DashImage();
            }
        }
        else
        {
            // Play SFX "not ready yet"
            // Show little text "not ready yet"
            Debug.Log("Dash not ready yet.");
        }
    }

    IEnumerator Dash(Vector2 direction)                                                      // ============== DASHING : COROUTINE
    {
        canDash = false;
        isDashing = true;
        dashingTime = startDashTime; // Reset the dash timer.

        while (dashingTime > 0f)
        {
            dashingTime -= Time.deltaTime;

            rb.velocity = direction * (dashSpeed/2f);
            yield return null; // Returns out of the coroutine this frame so we don't hit an infinite loop.
        }

        rb.velocity = new Vector2(0f, 0f); // Stop dashing.

        isDashing = false;
        yield return new WaitForSeconds(1.17f);
        canDash = true;
    }
}
