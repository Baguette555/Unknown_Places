using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterControls : MonoBehaviour
{
    [Header("pwease :pleading_face:")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] SpriteRenderer sprite_renderer;

    [Header("isGrounded?! :( faut pas gronder")]
    [SerializeField] bool EnSaut = false;
    [SerializeField] GameObject SolDetection1;
    [SerializeField] float jumpForce = 5f;

    [Header("[[OLD]] Jumping Script Properties")]
    [SerializeField] float velocity = 5f;

    [Header("Movement and speed")]
    [SerializeField] float mouvement_speed = 5f;
    [SerializeField] bool canMove = true;
    Vector2 move;


    [Header("Health Points")]
    [SerializeField] int health = 3;

    [Header("Dashing proprieties")]
    [SerializeField] bool canDash = true;
    public bool isDashing;
    [SerializeField] float dashSpeed = 15f;
    [SerializeField] float dashingTime = 0.4f;
    [SerializeField] float dashingCooldown = 1f;
    [SerializeField] float tm;
    private IEnumerator coroutine;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing)
        {
            return;
        }
        if (Input.GetKey(KeyCode.LeftArrow) && canMove == true)
        {
            go_left();
        }
        if (Input.GetKey(KeyCode.RightArrow) && canMove == true)
        {
            go_right();
        }
        if (Input.GetKey(KeyCode.LeftShift) && canMove == true && canDash == true)
        {
            StartCoroutine(Dash());
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && EnSaut == false)
        {
            //rb.velocity = Vector2.up * velocity;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            EnSaut = true;

        }
    }
    private void OnTriggerEnter2D(Collider2D SolDetection1)                 // ============== JUMP : GROUND DETECTION
    {
        EnSaut = false;
        canDash = true;
    }
    private void ExitTriggerEnter2D(Collider2D SolDetection1)
    {
        EnSaut = true;
    }                                                                       // =======================================

    void go_left()                                                          // ============== MOVEMENT : LEFT & RIGHT
    {
        //transform.Translate(-mouvement_speed, 0, 0, Space.World);
        //rb.velocity = new Vector2(-mouvement_speed, rb.velocity.y);
        //rb.AddForce(Vector2.left * mouvement_speed);
        //rb.velocity = new Vector2(move.x * mouvement_speed, rb.velocity.y);

        transform.position += Vector3.left * mouvement_speed * Time.deltaTime;
        sprite_renderer.flipX = true;

    }

    void go_right()
    {
        //transform.Translate(mouvement_speed, 0, 0, Space.World);
        //rb.velocity = new Vector2(mouvement_speed, rb.velocity.y);
        //rb.AddForce(Vector2.right * mouvement_speed);

        transform.position += Vector3.right * mouvement_speed * Time.deltaTime;
        sprite_renderer.flipX = false;
    }                                                                       // =======================================

    IEnumerator Dash()                                                      // ============== DASHING : COROUTINE
    {
        canDash = false;
        isDashing = true;
        tm = Time.time;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        if (sprite_renderer.flipX == true)
        {
            rb.velocity = Vector2.left * dashSpeed;
        }
        else
        {
            rb.velocity = Vector2.right * dashSpeed;
        }

        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        rb.velocity = new Vector2(transform.localScale.x * 0, 0f);
        yield return new WaitForSeconds(dashingCooldown);
        if (EnSaut == false)
        {
            canDash = true;
        }
    }                                                                        // =======================================
}
