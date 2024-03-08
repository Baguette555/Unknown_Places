using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private float startPos;
    private Rigidbody2D rb;
    [SerializeField] float fallSpeed = 2f; 
    [SerializeField] float riseSpeed = 1f;

    private void Start()
    {
        startPos = 2; //transform.position.y
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    private void Update()
    {
        if (rb.gravityScale != 0 && transform.position.y < 2)
        {
            transform.position = new Vector2(transform.position.x, 2); // "Limiter" à la position de départ mouais bon pas fou quoi
            rb.gravityScale = 0; // Fait en sorte que ça descende quand on monte dessus.
            rb.velocity = Vector2.zero; // idfk what this does tbf
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            rb.gravityScale = 1; // lock pas à 2 ???
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Invoke("StartRising", 0.5f); // Délai avant de commencer à remonter
        }
        if (rb.gravityScale != 0 && transform.position.y < 2)
        {
            transform.position = new Vector2(transform.position.x, 2); // Limiter la remontée à la position de départ
            rb.gravityScale = 0; // Désactiver la gravité pendant la remontée
            rb.velocity = Vector2.zero; // Arrêter tout mouvement
        }
    }

    private void StartRising()
    {
        rb.gravityScale = 0; // Désactiver la gravité lors de la remontée
        rb.velocity = new Vector2(0, riseSpeed); // Appliquer une vélocité vers le haut pour remonter
        if (rb.gravityScale != 0 && transform.position.y < 2)
        {
            transform.position = new Vector2(transform.position.x, 2); // Limiter la remontée à la position de départ
            rb.gravityScale = 0; // Désactiver la gravité pendant la remontée
            rb.velocity = Vector2.zero; // Arrêter tout mouvement
        }
    }
}
/*{
   private float startPos;
   private bool isFalling = false;
   public float fallSpeed = 2f; // Vitesse de descente de la plateforme
   private Rigidbody2D rb;

   private void Start()
   {
       startPos = transform.position.y;
       rb = GetComponent<Rigidbody2D>();
       rb.bodyType = RigidbodyType2D.Kinematic;
   }

   private void Update()
   {
       if (!isFalling && transform.position.y < startPos)
       {
           // Si la plateforme n'est pas en train de tomber et est en dessous de sa position d'origine, remonte lentement.
           float newY = Mathf.Lerp(transform.position.y, startPos, Time.fixedDeltaTime * fallSpeed);
           rb.MovePosition(new Vector2(transform.position.x, newY));
       }
   }

   private void OnCollisionEnter2D(Collision2D collision)
   {
       if (collision.transform.CompareTag("Player"))
       {
           rb.bodyType = RigidbodyType2D.Dynamic;
           isFalling = true;
       }
   }

   private void OnCollisionExit2D(Collision2D collision)
   {
       if (collision.transform.CompareTag("Player"))
       {
           isFalling = false;
       }
   }
} */

/*public class FallingPlatform : MonoBehaviour
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
}*/
