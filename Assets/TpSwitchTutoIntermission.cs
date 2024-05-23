using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TpSwitchTutoIntermission : MonoBehaviour
{
    [SerializeField] bool inTutorial, inTriggerZone = false;

    [SerializeField] GameObject Player;

    [Header("TUTORIAL Spawnpoint")]
    [SerializeField] GameObject TutoRespawnPoint;

    [SerializeField] private float TutoSpawnPoint_y;
    [SerializeField] private float TutoSpawnPoint_x;
    Vector2 TutoRespawnVector;

    [Header("ARTISTS LODGE Spawnpoint")]
    [SerializeField] GameObject LodgeRespawnPoint;

    [SerializeField] private float LodgeSpawnPoint_y;
    [SerializeField] private float LodgeSpawnPoint_x;
    Vector2 LodgeRespawnVector;
   
    private void Awake()
    {
        Player = GameObject.Find("### Player ###");
    }

    private void Start()
    {
        // ================================================================= Tuto spawn point
        TutoSpawnPoint_y = TutoRespawnPoint.transform.position.y;
        TutoSpawnPoint_x = TutoRespawnPoint.transform.position.x;
        TutoRespawnVector = new Vector2(TutoSpawnPoint_x, TutoSpawnPoint_y);

        // ================================================================= Lodge spawn point
        LodgeSpawnPoint_y = this.transform.position.y;
        LodgeSpawnPoint_x = this.transform.position.x;
        LodgeRespawnVector = new Vector2(LodgeSpawnPoint_x, LodgeSpawnPoint_y);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inTriggerZone = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inTriggerZone = false;
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (context.performed && inTriggerZone)
        {
            inTutorial = !inTutorial;
            if (!inTutorial)
            {
                Player.transform.position = LodgeRespawnVector;
                Debug.Log("TPd back in artists' lodge");
                // TP to tuto
            }
            else if (inTutorial)
            {
                Player.transform.position = TutoRespawnVector;
                Debug.Log("TPd to Tuto");
                // TP back to intermission
            }
        }
    }
}
