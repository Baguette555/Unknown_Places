using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuCamera : MonoBehaviour
{
    public Grid grid;
    public void SettingsCamera()
    {
        grid.transform.Translate(0, 200, 0, Space.World);
    }
    public void MainMenuCamera()
    {
        grid.transform.Translate(0, -200, 0, Space.World);
    }
}
