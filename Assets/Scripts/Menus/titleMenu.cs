using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class titleMenu : MonoBehaviour
{
    public Button title;
    private bool flipped = true;
    private int timesFlipped;

    [SerializeField] AudioSource sound;
    [SerializeField] Animator easterEgg;
    public void titleButton()
    {
        flipped = !flipped;
        timesFlipped += 1;
        //title.transform.Translate(0, -5, 0, Space.World);
        if (flipped == false)
        {
            title.transform.localScale = new Vector2(-1.5f, 2.75f);
        }
        else if (flipped == true)
        {
            title.transform.localScale = new Vector2(1.5f, 2.75f);
        }

        if (timesFlipped == 10)
        {
            easterEgg.SetTrigger("PlayEE");
            sound.Play();
            timesFlipped = 0;
        }
    }
}
