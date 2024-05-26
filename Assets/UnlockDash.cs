using UnityEngine;

public class UnlockDash : MonoBehaviour
{
    public NewControls NewControls;

    public void DashUnlocked()
    {
        NewControls.isAbleToDash = true;
        NewControls.canDash = false;    // Will be reactivated once the pop-up is closed.
    }
}
