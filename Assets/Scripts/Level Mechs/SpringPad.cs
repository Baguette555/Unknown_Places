using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringPad : MonoBehaviour
{
    [SerializeField] float bounce = 2f;
    public NewControls newControls;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            newControls.playerAnimator.SetBool("Falling", false);
            newControls.playerAnimator.SetBool("Landing", false);
            newControls.playerAnimator.SetBool("Jumping", true);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounce, ForceMode2D.Impulse);
        }
    }
}
