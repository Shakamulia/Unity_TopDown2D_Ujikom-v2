using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Dialogue dialogueData; // Reference to the Dialogue scriptable object
    public GameObject InteractionHint; // UI 'E' hint
    private bool PlayerInRange = false; // To check if the player is in range

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerInRange = true;
            InteractionHint.SetActive(true); // Show the interaction hint
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerInRange = false;
            InteractionHint.SetActive(false); // Hide the interaction hint
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            StartDialogue();
        }
    }

    private void StartDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogueData);
    }
}
