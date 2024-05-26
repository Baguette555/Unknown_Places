using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] GameObject ennemy;
    public HealthManager healthManager;
    public NewControls newControls;

    private void Awake()
    {
        ennemy = this.gameObject;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && newControls.hasBoots == false)   // if player has no boots, deal damage in any case.
        {
            healthManager.TakeDamage(1);
        }
        else if(collision.gameObject.tag == "Player" && newControls.hasBoots == true && ennemy.tag == "Breakable")  // if player has boots and the spike can be destroyed
        {
            healthManager.TakeDamage(0);
            Destroy(ennemy);
        }
        else if(collision.gameObject.tag == "Player" && newControls.hasBoots == true && ennemy.tag == "Untagged")   // if player has boots but not spikes: deal damage.
        {
            healthManager.TakeDamage(1);
        }
    }
}
