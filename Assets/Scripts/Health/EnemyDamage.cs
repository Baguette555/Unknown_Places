using System.Collections;
using System.Collections.Generic;
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
        if(collision.gameObject.tag == "Player" && newControls.hasBoots == false)
        {
            healthManager.TakeDamage(1);
        }
        else if(collision.gameObject.tag == "Player" && newControls.hasBoots == true)
        {
            healthManager.TakeDamage(0);
            Destroy(ennemy);
        }
    }
}
