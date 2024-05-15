using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public NewControls NewControls;
    public Dialogue dialogue;
    public DialogueManager dialogueManager;
    public GameObject dialogueTrigger;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TriggerDialogue();
        }
    }
    void TriggerDialogue()
    {
        if (dialogueTrigger.CompareTag("DialogueTrigger"))
        {
            dialogueManager.animator.SetBool("IsOpen", true);
        }
        DialogueManager.instance.StartDialogue(dialogue);
    }
}
