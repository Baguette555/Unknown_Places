using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class TpTutoToLodge : MonoBehaviour
{
    [SerializeField] bool inTriggerZone = false;

    [SerializeField] GameObject Player;
    [SerializeField] SpriteRenderer spriteInteract;

    [Header("TUTORIAL Spawnpoint")]
    [SerializeField] GameObject TutoRespawnPoint, BlockedWall;

    [SerializeField] private float TutoSpawnPoint_y;
    [SerializeField] private float TutoSpawnPoint_x;
    Vector2 TutoRespawnVector;

    public CinemachineVirtualCamera TutoCam;
    public CinemachineVirtualCamera LodgeCam;

    [Header("Transition")]
    public Animator transition;

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
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            spriteInteract.color = new Color(spriteInteract.color.r, spriteInteract.color.g, spriteInteract.color.b, 0.7f);
            inTriggerZone = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            spriteInteract.color = new Color(spriteInteract.color.r, spriteInteract.color.g, spriteInteract.color.b, 0f);
            inTriggerZone = false;
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (context.performed && inTriggerZone)
        {
            BlockedWall.SetActive(false);
            transition.SetTrigger("Start");
            StartCoroutine(Transi());
            Debug.Log("TPd to Tuto");
            // TP back to intermission
        }
    }

    IEnumerator Transi()
    {
        yield return new WaitForSeconds(1);
        TutoCam.Priority = 0;
        LodgeCam.Priority = 10;
        Player.transform.position = TutoRespawnVector;
    }
}
