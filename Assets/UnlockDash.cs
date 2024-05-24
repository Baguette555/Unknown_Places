using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDash : MonoBehaviour
{
    public NewControls NewControls;

    public void DashUnlocked()
    {
        NewControls.isAbleToDash = true;
        NewControls.canDash = true;
    }
}
