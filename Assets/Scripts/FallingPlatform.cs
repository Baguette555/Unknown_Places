using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private float startPos;

    private void Start()
    {
        startPos = transform.position.y;        // Le script ne fait pas en sorte que la plateforme choppe la position et aussi faut faire en sorte qu'elle se stoppe quand tu pars de la plateforme
    }
    private void Update()
    {
        Debug.Log(startPos);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
        else
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        }
    }
}
