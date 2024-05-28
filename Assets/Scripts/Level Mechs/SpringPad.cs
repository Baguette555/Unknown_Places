using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringPad : MonoBehaviour
{
    [SerializeField] GameObject BounceTrigger;
    [SerializeField] float bounce = 2f;
    public NewControls newControls;

    private void Start()
    {
        BounceTrigger.transform.Find("BounceTrigger");
    }
    private void OnCollisionEnter2D(Collision2D BounceTrigger)
    {
        if(BounceTrigger.gameObject.CompareTag("Player"))
        {
            newControls.playerAnimator.SetBool("Falling", false);
            newControls.playerAnimator.SetBool("Landing", false);
            newControls.playerAnimator.SetBool("Jumping", true);
            BounceTrigger.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounce, ForceMode2D.Impulse);
        }
    }
}
