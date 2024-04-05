using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using System.Threading;
using UnityEngine.Rendering.UI;

public class dashCooldownImage : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] Image image;
    [SerializeField] float speed;
    [SerializeField] bool dashReady = true;
    float currentValue;


    // ======================== INITIALISATION DES COULEURS DES IMAGES ========================
    private void Awake()
    {
        dashReady = true;
        image.color = new Color(0, 255, 0, 0f);
        text.color = new Color(255, 255, 255, 0f);
    }
    // ========================================================================================

    // ========================== DETECTION DU L.SHIFT POUR COOLDOWN ==========================
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashReady == true)
        {
            StartCoroutine(Cooldown());
        }
    }
    // ========================================================================================

    // ============================ COUROUTINE DU COOLDOWN ============================
    IEnumerator Cooldown()
    {
        dashReady = false;
        yield return new WaitForSeconds(0.4f);  // Dashing Time

        currentValue = 0;
        text.color = new Color(255, 255, 255, 1f);
        image.color = new Color(0, 255, 0, 1f);

        while (currentValue < 100) // If timer less than 100 (not fully completed),
        {
            currentValue += speed * Time.deltaTime / 1f;    // Refill the timer as time passes
            text.text = ((int)currentValue).ToString() + "%";   // And add % purely decorative
            image.fillAmount = currentValue / 100;          // So the images fills more and more
            yield return null;              // idfk what this does
        }
        dashReady = true;
        text.text = "PRET";
        yield return new WaitForSeconds(0.175f);    // Wait for the user to see that the dash is ready and can be used again
        for (float i = 1; i >= 0; i -= (Time.deltaTime)*5)
        {
            // set color with i as alpha
            image.color = new Color(1, 1, 1, i);
            text.color = new Color(1, 1, 1, i);
            yield return null;
        }
        currentValue = 0;
        image.color = new Color(0, 255, 0, 0f);
        text.color = new Color(255, 255, 255, 0f);

        /*if (currentValue < 100)
        {
            text.color = new Color(255, 255, 255, 1f);
            image.color = new Color(0, 255, 0, 1f);
            currentValue += speed * Time.deltaTime;
            text.text = ((int)currentValue).ToString() + "%";
        }
        else
        {
            text.text = "PRET !";
            currentValue = 0;
            yield return new WaitForSeconds(0.3f);
            image.color = new Color(0, 255, 0, 0f);
            text.color = new Color(255, 255, 255, 0f);

        }
        image.fillAmount = currentValue / 100;*/
    }
    // ================================================================================
}
