using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public DialogueManager dialogueManager;
    public GameObject dialogueTrigger;
    public GameObject dialogueUI;

    public PauseMenu pauseMenu; // Used to disable/enable player's input
    public NewControls NewControls;
    [SerializeField] bool inTrigger;

    public bool dialogueStarted = false;
    [SerializeField] private GameObject player;    // To check if the player is in da collider

    [SerializeField] private SpriteRenderer spriteInteract;
    [SerializeField] private float alphaTime;

    private void Start()
    {
        spriteInteract.color = new Color(spriteInteract.color.r, spriteInteract.color.g, spriteInteract.color.b, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            spriteInteract.color = new Color(spriteInteract.color.r, spriteInteract.color.g, spriteInteract.color.b, 0.7f);
            inTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            spriteInteract.color = new Color(spriteInteract.color.r, spriteInteract.color.g, spriteInteract.color.b, 0f);
            inTrigger = false;
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (pauseMenu.isPaused == false)
        {
            if (context.performed && inTrigger)
            {
                if (dialogueStarted == false)
                {
                    pauseMenu.DisablePlayerInputs();
                    dialogueUI.SetActive(true);
                    TriggerDialogue();
                }
                else
                {
                    dialogueManager.DisplayNextSentences();
                }
            }
        }
    }
    void TriggerDialogue()
    {
        dialogueStarted = true;
        if (dialogueTrigger.CompareTag("DialogueTrigger"))
        {
            dialogueManager.animator.SetBool("IsOpen", true);
        }
        DialogueManager.instance.StartDialogue(dialogue);
    }

    /*private void OnTriggerExit2D(Collider2D collision)
    {
        dialogueManager.EndDialogue();
    }*/
}
