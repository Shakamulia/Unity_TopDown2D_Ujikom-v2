using DialogueEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcManager : MonoBehaviour
{
    [Header ("Dialogue Refrence")]
    public NPCConversation NPCConversation;
    public NPCConversation questConversation;

    private void Start()
    {
        // subscribe event selesai dialog
        ConversationManager.OnConversationEnded += OnConversationEnded;
    }

    //panggil method ini saat berinteraksi dengan NPC pertama
    public void StartFirstConversation()
    {
        //mulai dialog
        ConversationManager.Instance.StartConversation(NPCConversation);
    }

    //callback saat dialog pertama selesai
    void OnConversationEnded()
    {
        //unsubscribe event sebelumnya
        ConversationManager.OnConversationEnded -= OnConversationEnded;
        //mulai dialog event
        ConversationManager.Instance.StartConversation(questConversation);
        //subscribe event baru untuk quest
        ConversationManager.OnConversationEnded += OnQuestConversationEnded;
    }

    //callback saat dialog quest selesai
    void OnQuestConversationEnded()
    {
        Debug.Log("Quest Started!");
        //unsubscribe event
        ConversationManager.OnConversationEnded -= OnQuestConversationEnded;
    }

    private void OnDestroy()
    {
        //unsubcribe event saat destroy
        ConversationManager.OnConversationEnded -= OnConversationEnded;
        ConversationManager.OnConversationEnded -= OnQuestConversationEnded;
    }
}
