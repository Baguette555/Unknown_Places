using UnityEngine;

public class dashInSpecialWall : MonoBehaviour
{
    public NewControls newControls;
    [SerializeField] GameObject specialWallCollider;

    void Start()
    {
        specialWallCollider = this.gameObject;
        GetComponent<Collider2D>().enabled = true;
    }

    void Update()
    {
        if (newControls.isDashing == true && (gameObject.tag == "SpecialWall"))
        {
            GetComponent<Collider2D>().enabled = false;
        }
        else
        {
            GetComponent<Collider2D>().enabled = true;
        }
    }
}
