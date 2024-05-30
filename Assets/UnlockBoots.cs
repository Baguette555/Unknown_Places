using UnityEngine;

public class UnlockBoots : MonoBehaviour
{
    public NewControls NewControls;

    public void BootsUnlocked()
    {
        Debug.Log("Boots unlocked");
        NewControls.hasBoots = true;
    }
}
