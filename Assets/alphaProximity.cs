using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alphaProximity : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteInteractText;
    public DialogueTrigger DialogueTrigger;

    private void Start()
    {
        spriteInteractText = GetComponent<SpriteRenderer>();
        spriteInteractText.color = new Color(spriteInteractText.color.r, spriteInteractText.color.g, spriteInteractText.color.b, 0f);
    }

    void Update()
    {
        if(DialogueTrigger.inTrigger == true)
        {
            spriteInteractText.color = new Color(spriteInteractText.color.r, spriteInteractText.color.g, spriteInteractText.color.b, 0.7f);
        }
        else
        {
            spriteInteractText.color = new Color(spriteInteractText.color.r, spriteInteractText.color.g, spriteInteractText.color.b, 0f);
        }
    }
}
