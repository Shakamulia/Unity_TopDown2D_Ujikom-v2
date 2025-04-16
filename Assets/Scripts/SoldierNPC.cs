using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class SoldierNPC : MonoBehaviour
{
    public NPCConversation conversation;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            ConversationManager.Instance.StartConversation(conversation);
        }
    }
}
