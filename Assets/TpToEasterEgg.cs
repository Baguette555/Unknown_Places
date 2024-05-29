using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TpToEasterEgg : MonoBehaviour
{
    [SerializeField] GameObject Player;

    [Header("TUTORIAL Spawnpoint")]
    [SerializeField] GameObject RoomSpawnPoint;

    [SerializeField] private float RoomSpawnPoint_y;
    [SerializeField] private float RoomSpawnPoint_x;
    Vector2 RoomVector;

    [Header("Transition")]
    public Animator transition;

    private void Awake()
    {
        Player = GameObject.Find("### Player ###");
    }

    private void Start()
    {
        // ================================================================= Easter Egg Room Spawn Point
        RoomSpawnPoint_y = RoomSpawnPoint.transform.position.y;
        RoomSpawnPoint_x = RoomSpawnPoint.transform.position.x;
        RoomVector = new Vector2(RoomSpawnPoint_x, RoomSpawnPoint_y);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Transi());
        }
    }


    IEnumerator Transi()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        Player.transform.position = RoomVector;
    }
}
