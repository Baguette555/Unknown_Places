using UnityEngine;

public class UnlockBoots : MonoBehaviour
{
    public NewControls NewControls;

    public void DashUnlocked()
    {
        NewControls.hasBoots = true;
    }
}
