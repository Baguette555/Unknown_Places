using UnityEngine;

public class gameMusic : MonoBehaviour
{
    private void Awake()
    {
        GameObject[] Musique = GameObject.FindGameObjectsWithTag("Musique");
        if(Musique.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
