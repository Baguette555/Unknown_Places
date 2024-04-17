using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

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
    private float speed = 8f;
    private float jumpForce = 9f;
    private bool isFacingRight = true;
    private Vector2 move;

    [Header("Dashing proprieties")]
    [SerializeField] bool canDash = true;
    public bool isDashing;
    [SerializeField] float dashSpeed = 30f;
    [SerializeField] float dashingTime = 0.4f;
    [SerializeField] float dashingCooldown = 1f;
    [SerializeField] float dashGravity = 0f;
    private float waitTime;
    private float normalGravity;
    private IEnumerator coroutine;

    [Header("Dash Trail Renderer")]
    [SerializeField] TrailRenderer trail;

    [Header("Boots properties")]
    public bool hasBoots = false;
    // Activer un particle machin pour les bottes. à voir plus tard ofc

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
        canDash = true;
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()                                                      // ============== FLIP [NEW]
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)                   // ============== JUMP [NEW]
    {
        if (context.performed && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.3f);
        }
    }


    public void Dash(InputAction.CallbackContext context)                  // ============== NEW DASHING SYSTEM (NOT WORKING IDK WHY)
    {
        if (context.performed && canDash == true && isDashing == false)
        {
            StartCoroutine(Dash());
            /*if(waitTime >= dashingCooldown)
            {
                waitTime = 0f;
                Invoke("Dashing", 0);
            }*/
        }
    }
    /*public void Dash() 
    {
        canDash = false;
        isDashing = true;
        rb.gravityScale = dashGravity;

        if (move.x == 0)
        {
            if (isFacingRight)
            {
                rb.velocity = new Vector2(transform.localScale.x * dashSpeed, 0);
            }
            if (!isFacingRight)
            {
                rb.velocity = new Vector2(-transform.localScale.x * dashSpeed, 0);
            }
        }
        else
        {
            rb.velocity = new Vector2(move.x * dashSpeed, 0);
        }
        Invoke("StopDash", dashingTime);
    }
    public void StopDash()
    {
        canDash = true;
        isDashing = false;
        rb.gravityScale = normalGravity;
    }*/

    IEnumerator Dash()                                                      // ============== DASHING : COROUTINE
    {
        canDash = false;
        isDashing = true;
        rb.gravityScale = 0f;
        if (isFacingRight)
        {
            rb.velocity = new Vector2(transform.localScale.x * dashSpeed, 0);
        }
        if (!isFacingRight)
        {
            rb.velocity = new Vector2(-transform.localScale.x * dashSpeed, 0);
        }

        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = normalGravity;
        isDashing = false;
        rb.velocity = new Vector2(transform.localScale.x * 0, 0f);
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
