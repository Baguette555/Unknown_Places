using UnityEngine;
using UnityEngine.UI;

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
