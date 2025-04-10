using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class ConversationTrigger : MonoBehaviour
{
    public NPCConversation myConversation;
    private bool PlayerInRange = false; // To check if the player is in range
    public GameObject InteractionHint; // UI 'E' hint
    private bool isTalking = false; // Reference to the conversation UI
    [SerializeField] private PlayerController playerController; // Reference to the PlayerController script


    private void OnEnable()
    {
        ConversationManager.OnConversationEnded += OnConversationEnd; // Subscribe to the event
    }

    private void OnDisable()
    {
        ConversationManager.OnConversationEnded -= OnConversationEnd; // Unsubscribe from the event
    }

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

    private void Update()
    {
        if (PlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            isTalking = true; // Set the talking state to true
            ConversationManager.Instance.StartConversation(myConversation);
            InteractionHint.SetActive(false); // Hide the interaction hint after starting the conversation
            playerController.enabled = false;
        }
    }

    private void OnConversationEnd()
    {
        playerController.enabled = true; // Re-enable the player controller when the conversation ends
        isTalking = false; // Reset the talking state
    }
}
