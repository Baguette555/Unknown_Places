using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class dashCooldownImage : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI text;
    [SerializeField] Image image;
    [SerializeField] float speed = 100f;
    float currentValue;

    private void Update()
    {
            if (currentValue < 100)
            {
                image.enabled = true;
                currentValue += speed * Time.deltaTime;
                text.text = ((int)currentValue).ToString();
            }
            else
            {
                text.text = "PRET";
            }
            image.fillAmount = currentValue / 100;
            if (currentValue >= 100)
            {
                image.enabled = false;
                currentValue = 0;
                image.fillAmount = 0;
            }
    }
}
