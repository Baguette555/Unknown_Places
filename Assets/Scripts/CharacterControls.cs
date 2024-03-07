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

    [Header("Jumping Script Properties")]
    [SerializeField] float velocity = 10f;

    [Header("Scaling - Speed Movement - Animator")]
    [SerializeField] float mouvement_speed = 0.03f;
    [SerializeField] bool canMove = true;

    [Header("Dashing proprieties")]
    [SerializeField] bool canDash = true;
    [SerializeField] bool isDashing;
    [SerializeField] float dashSpeed = 15f;
    [SerializeField] float dashingTime = 0.4f;
    [SerializeField] float dashingCooldown = 1f;
    [SerializeField] float tm;
    private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

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
        if (Input.GetKey(KeyCode.LeftShift) && canMove == true)
        {
            StartCoroutine(Dash());
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb.velocity = Vector2.up * velocity;
        }
    }

    void go_left()
    {
        transform.Translate(-mouvement_speed, 0, 0, Space.World);       // Using the movement_speed to move right or left
        sprite_renderer.flipX = true;

    }

    void go_right()
    {
        transform.Translate(mouvement_speed, 0, 0, Space.World);
        sprite_renderer.flipX = false;
    }

    IEnumerator Dash()
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
        canDash = true;
    }
}
