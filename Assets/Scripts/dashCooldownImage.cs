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

    float currentValue;

    private void Awake()
    {
        image.color = new Color(0, 255, 0, 0f);
        text.color = new Color(255, 255, 255, 0f);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(Cooldown());
        }
    }
    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(0.4f);  // Dashing Time

        currentValue = 0;
        text.color = new Color(255, 255, 255, 1f);
        image.color = new Color(0, 255, 0, 1f);

        while (currentValue < 100) 
        {
            currentValue += speed * Time.deltaTime / 1f;
            text.text = ((int)currentValue).ToString() + "%";
            image.fillAmount = currentValue / 100;
            yield return null;
        }
        text.text = "PRET";
        yield return new WaitForSeconds(0.175f);
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





    /*[SerializeField] private float indicatorTimer = 1f;
    [SerializeField] private float maxIndcatorTimer = 1f;

    [SerializeField] private Image radialIndcatorUI = null;

    [SerializeField] private KeyCode selectKey = KeyCode.LeftShift;


    private bool shouldUpdate = false;

    private void Update()
    {
        if(Input.GetKeyDown(selectKey))
        {
            indicatorTimer -= Time.deltaTime;
            radialIndcatorUI.enabled = true;
            radialIndcatorUI.fillAmount = indicatorTimer;

            if(indicatorTimer <= 0)
            {
                indicatorTimer = maxIndcatorTimer;
                radialIndcatorUI.fillAmount = maxIndcatorTimer;
                radialIndcatorUI.enabled = false;
            }
        }
    }*/
}
