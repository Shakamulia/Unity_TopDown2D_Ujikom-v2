using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class ConversationTrigger : MonoBehaviour
{
    public NPCConversation firstConversation;
    public NPCConversation questConversation;
    private bool PlayerInRange = false; // To check if the player is in range
    private bool isTalking = false; // Reference to the conversation UI
    private bool isFirstDialogueDone = false; // To check if the first dialogue is done

    public GameObject InteractionHint; // UI 'E' hint
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
            if (!isFirstDialogueDone)
            {
                StartFirstConversation();
            }
            else
            {
                StartQuestConversation();
            }
            isTalking = true; // Set the talking state to true
            ConversationManager.Instance.StartConversation(firstConversation);
            InteractionHint.SetActive(false); // Hide the interaction hint after starting the conversation
            playerController.enabled = false;
        }
    }

    // Method untuk memulai dialog pertama
    private void StartFirstConversation()
    {
        playerController.enabled = false;
        InteractionHint.SetActive(false);
        ConversationManager.Instance.StartConversation(firstConversation);
    }

    // Method untuk memulai dialog quest
    private void StartQuestConversation()
    {
        playerController.enabled = false;
        InteractionHint.SetActive(true);
        ConversationManager.Instance.StartConversation(questConversation);
    }

    private void OnConversationEnd()
    {
        playerController.enabled = true; // Re-enable the player controller when the conversation ends
        //isTalking = false; // Reset the talking state

        if(!isFirstDialogueDone)
        {
            isFirstDialogueDone = true; // Set the first dialogue as done
        }
    }
}
