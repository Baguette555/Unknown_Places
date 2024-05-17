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
    [Header("ActionMap : Pour désactiver / réactiver les touches")]
    public bool canJump = true;
    public bool canMove = true;
    public bool canFlip = true; // Movement and speed
    public bool canDash = true; // Dashing proprieties
    public PauseMenu pauseMenu;

    [Header("Déjà défini automatiquement. Changer si ça ne fonctionne pas.")]
    [SerializeField] public Rigidbody2D rb;
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
    // canFlip a été déplacé en haut
    public Animator playerAnimator;

    [Header("Dashing proprieties")]
    [SerializeField] bool isAbleToDash = false; // CHECK IF PLAYER IS ABLE TO DASH

    // canDash a été déplacé en haut
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
        isFacingRight = true;                       // À voir avec certaines scènes.    update: quelles "certaines scènes" ?? c'est important de noter finalement

        Scene currentScene = SceneManager.GetActiveScene();
        int sceneBuildIndex = currentScene.buildIndex;
        // =================================================================================
        // ============================================ CHECK IF PLAYER IS ABLE TO USE BOOTS
        if (currentScene.buildIndex >= 6)
        {
            hasBoots = true;
        }
        // ============================================ CHECK IF PLAYER IS ABLE TO USE DASH
        if (currentScene.buildIndex >= 3)
        {
            isAbleToDash = true;
        }
        else
        {
            isAbleToDash = false;
        }
        // =================================================================================
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

        if (!IsGrounded() && rb.velocity.y <= 0)           // L'animation de saut doit s'arrêter lorsque la vitesse de saut est à 0
        {
            playerAnimator.SetBool("Falling", true);                      // Animation plays for falling
            playerAnimator.SetBool("Jumping", false);                      // Animation stops for the jump
        }
        if (rb.velocity.y > 0.5f)
        {
            playerAnimator.SetBool("Jumping", true);
        }
    }

    private bool IsGrounded()                                               // ============== JUMP : GROUND DETECTION [NEW]
    {
        playerAnimator.SetBool("Falling", false);                      // Animation stops for falling
        playerAnimator.SetBool("Landing", true);                      // Animation starts for the landing
        StartCoroutine(LandingCooldown());
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    IEnumerator LandingCooldown()
    {
        yield return new WaitForSeconds(0.04f);
        playerAnimator.SetBool("Landing", false);                      // Animation stops for the landing
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
        if (canMove)
        {
            horizontal = context.ReadValue<Vector2>().x;
            if (context.performed)
            {
                playerAnimator.SetBool("Running", true);                      // Animation plays for the running sprites
            }
            else if (context.canceled)
            {
                playerAnimator.SetBool("Running", false);
            }
        }
    }

    public void Jump(InputAction.CallbackContext context)                   // ============== JUMP [NEW]
    {
        if (canJump)
        {
            if (context.performed && IsGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                playerAnimator.SetBool("Jumping", true);                      // Animation plays for the jump
            }

            if (context.canceled && rb.velocity.y > 0f)
            {
                playerAnimator.SetBool("Jumping", false);                      // Animation stops for the jump
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.2f);
            }
        }
    }


    public void Dash(InputAction.CallbackContext context)                  // ============== NEW DASHING SYSTEM
    {
        if (canDash)
        {
            if (context.performed && isDashing == false)
            {
                if (isFacingRight && canDash == true && isDashing == false)
                {
                    StartCoroutine(Dash(Vector2.right));
                    dashCooldownImage.DashImage();
                }
                else if (!isFacingRight && canDash == true && isDashing == false)
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
