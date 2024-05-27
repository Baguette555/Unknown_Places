using UnityEngine;

public class EnemyDamageBoss : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] GameObject ennemy;
    public HealthManagerBoss healthManager;
    public NewControls newControls;

    private void Awake()
    {
        ennemy = this.gameObject;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && newControls.hasBoots == false)
        {
            healthManager.TakeDamage(1);
        }
        else if(collision.gameObject.tag == "Player" && newControls.hasBoots == true && ennemy.tag == "Breakable")
        {
            healthManager.TakeDamage(0);
            Destroy(ennemy);
        }
        else if(collision.gameObject.tag == "Player" && newControls.hasBoots == true && ennemy.tag == "Untagged")
        {
            healthManager.TakeDamage(1);
        }
    }
}
