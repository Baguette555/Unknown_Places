using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class charMovement : MonoBehaviour
{
    private AudioSource audioJump;
    // ----------------------- MOVEMENT
    [Header("Scaling - Speed Movement - Animator")]
    [SerializeField] float scale = 3f;
    [SerializeField] float mouvement_speed = 0.03f;
    [SerializeField] Animator Animator_player;
    [SerializeField] SpriteRenderer sprite_renderer;
    [SerializeField] bool canMove = true;

    // ----------------------- JUMPING
    [Header("Jumping and game physics")]
    [SerializeField] Animator playerAnimator;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rb;

    // ----------------------- PARTICLES
    [Header("Particules")]
    [SerializeField] ParticleSystem PT_Jump;

    // ----------------------- DASHING
    [Header("Dashing proprieties")]
    [SerializeField] bool canDash = true;
    [SerializeField] bool isDashing;
    [SerializeField] float dashSpeed = 15f;
    [SerializeField] float dashingTime = 0.4f;
    [SerializeField] float dashingCooldown = 1f;
    [SerializeField] float tm;
    private IEnumerator coroutine;

    void go_left()                  //Movement functions (right)
    {
        transform.Translate(-mouvement_speed, 0, 0, Space.World);       // Using the movement_speed to move right or left
        Animator_player.SetBool("Bool_Run", true);                      // Animation plays for the duration of go_left
        sprite_renderer.flipX = true;                                   // Going left makes the 2D character flip
    }

    void go_right()                 //Movement functions (left)
    {
        transform.Translate(mouvement_speed, 0, 0, Space.World);
        Animator_player.SetBool("Bool_Run", true);
        sprite_renderer.flipX = false;                                  // Important: Going right does not need to flip the character; its already looking the right way.
    }

    void scale_down()               //Scaling functions (down)
    {
        transform.localScale += new Vector3(-scale * Time.deltaTime, -scale * Time.deltaTime, -scale * Time.deltaTime);
        Animator_player.SetBool("Bool_Fall", true);
    }

    void scale_up()                 //Scaling functions (up)
    {
        transform.localScale += new Vector3(scale * Time.deltaTime, scale * Time.deltaTime, scale * Time.deltaTime);
        Animator_player.SetBool("Bool_Jump", true);
    }

    void sliding()               //Animation functions 1: Sliding
    {
        Animator_player.SetBool("Bool_Slide", true);
    }

    void crouching()               //Animation functions 2: Crouch
    {
        Animator_player.SetBool("Bool_Crouch", true);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing)
        {
            return;
        }
    // Controls for movement, animations and specials

        else if (Input.GetKey(KeyCode.KeypadMinus) && canMove == true)              // 1. Special: Scaling - using minus            // Exercise purposes: 1= Bonus content / 2= Movement / 3= One & Two (keypad)
        {
            scale_down();
        }
        else if (Input.GetKey(KeyCode.KeypadPlus) && canMove == true)          // Special: Scaling + using plus 
        {
            scale_up();
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && canMove == true)           // 2. Movement: Going left using the left arrow
        {
            go_left();
        }
        else if (Input.GetKey(KeyCode.RightArrow) && canMove == true)          // Movement: Going right using the right arrow
        {
            go_right();
        }
        else if (Input.GetKey(KeyCode.Keypad1) && canMove == true)          // 3. Doing the slide animation when 1 is pressed on the keypad
        {
            sliding();
        }
        else if (Input.GetKey(KeyCode.Keypad2) && canMove == true)          // Doing the slide animation when 1 is pressed on the keypad
        {
            crouching();
        }
        else if (Input.GetKey(KeyCode.LeftShift) && canMove == true)        // 4. Dashing when L.Shift is pressed
        {
            StartCoroutine(Dash());
            Animator_player.SetBool("Bool_Dash", true);
        }


        if (Input.GetKeyUp(KeyCode.LeftArrow))              // Animations that stops when releasing given control.
        {
            Animator_player.SetBool("Bool_Run", false);
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            Animator_player.SetBool("Bool_Run", false);
        }
        else if (Input.GetKeyUp(KeyCode.KeypadMinus))
        {
            Animator_player.SetBool("Bool_Fall", false);
        }
        else if (Input.GetKeyUp(KeyCode.KeypadPlus))
        {
            Animator_player.SetBool("Bool_Jump", false);
        }
        else if (Input.GetKeyUp(KeyCode.Keypad1))
        {
            Animator_player.SetBool("Bool_Slide", false);
        }
        else if (Input.GetKeyUp(KeyCode.Keypad2))
        {
            Animator_player.SetBool("Bool_Crouch", false);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Animator_player.SetBool("Bool_Dash", false);
        }

        else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            PT_Jump.Stop();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && canMove == true)
        {
            PT_Jump.Play();
        }
    }
    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        tm = Time.time;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        if (spriteRenderer.flipX == true)
        {
            rb.velocity = Vector2.left * dashSpeed;
        }
        else
        {
            rb.velocity = Vector2.right * dashSpeed;
        }

        yield return new WaitForSeconds(dashingTime);
        Animator_player.SetBool("Bool_Dash", false);
        rb.gravityScale = originalGravity;
        isDashing = false;
        rb.velocity = new Vector2(transform.localScale.x * 0, 0f);
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
    public void CineCantMove()
    {
        canMove = false;
        Debug.Log("canMove to false");
    }

    public void CineMove()
    {
        canMove = true;
        Debug.Log("canMove to true");
    }
}