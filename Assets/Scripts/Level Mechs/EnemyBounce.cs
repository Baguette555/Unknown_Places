using UnityEngine;

public class EnemyBounce : MonoBehaviour
{
    [Header("Bounce force. Default at 5f.")]
    [SerializeField] float bounce = 5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounce, ForceMode2D.Impulse);
        }
    }
}
