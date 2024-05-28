using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public FallingPlatform FallingPlatform;

    [SerializeField] GameObject Player;
     
    [SerializeField] private float spawnPoint_y;
    [SerializeField] private float spawnPoint_x;
    Vector2 respawnPoint;

    private void Awake()
    {
        Player = GameObject.Find("### Player ###");
    }

    private void Start()
    {
        spawnPoint_y = this.transform.position.y;
        spawnPoint_x = this.transform.position.x;
        respawnPoint = new Vector2(spawnPoint_x, spawnPoint_y);
    }

    public void RespawnToSpawnPoint()
    {
        FallingPlatform.PlatformReset();
        Player.transform.position = respawnPoint;
    }
}
