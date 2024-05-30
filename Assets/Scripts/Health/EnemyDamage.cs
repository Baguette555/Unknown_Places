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
            Debug.Log("Player to spike, boots to false");
            healthManager.TakeDamage(1);
        }
        else if(collision.gameObject.tag == "Player" && newControls.hasBoots == true && ennemy.tag == "Breakable")  // if player has boots and the spike can be destroyed
        {
            Debug.Log("Player to spike, boots to true, and I'm Breakable. I will now die for the nation. Bye bye world.");
            healthManager.TakeDamage(0);
            Destroy(ennemy);
        }
        else if(collision.gameObject.tag == "Player" && newControls.hasBoots == true && ennemy.tag == "Untagged")   // if player has boots but not spikes: deal damage.
        {
            Debug.Log("Player to spike, boots to false, and not tagged Breakable, so I break player's legs.");
            healthManager.TakeDamage(1);
        }
    }
}
