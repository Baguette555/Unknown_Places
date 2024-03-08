using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dashInSpecialWall : MonoBehaviour
{
    public CharacterControls characterControls;
    [SerializeField] GameObject specialWallCollider;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Collider2D>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (characterControls.isDashing == true && (gameObject.tag == "SpecialWall"))
        {
            GetComponent<Collider2D>().enabled = false;
        }
        else
        {
            GetComponent<Collider2D>().enabled = true;
        }
    }
}
