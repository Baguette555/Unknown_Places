using UnityEngine;

public class bossChase : MonoBehaviour
{
    [SerializeField] float bossSpeed = 4f;
    Rigidbody2D bossRb;
    Transform target;
    Vector2 moveDirection;
    public HealthManagerBoss healthManager;

    private void Awake()
    {
        bossRb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        target = GameObject.Find("### Player ###").transform;
    }

    void Update()
    {
        if(target)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            moveDirection = direction;
        }
    }

    private void FixedUpdate()
    {
        if(target)
        {
            bossRb.velocity = new Vector2(moveDirection.x, moveDirection.y) * bossSpeed;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            healthManager.playerHealth = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            healthManager.playerHealth = 0;
        }
    }
}
