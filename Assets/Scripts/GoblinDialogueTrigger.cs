using UnityEngine;
using DialogueEditor;

public class GoblinDialogueTrigger : MonoBehaviour
{
    public NPCConversation normalConversation;
    public NPCConversation repeatConversation;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (ConversationManager.Instance.GetBool("HasTalkedToGoblin"))
            {
                ConversationManager.Instance.StartConversation(repeatConversation);
            }
            else
            {
                ConversationManager.Instance.StartConversation(normalConversation);
                ConversationManager.Instance.SetBool("HasTalkedToGoblin", true);
            }
        }
    }
}
