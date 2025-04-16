using UnityEngine;
using DialogueEditor;

public class QuestTrigger : MonoBehaviour
{
    [Header("Dialogue Reference")]
    public NPCConversation conversation;
    public NPCConversation Questconversation;
    private bool isFistDialogueDone = false;
    private bool isInTrigger = false;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isFistDialogueDone)
        {
            isInTrigger = true;
            StartFirstConversation();
            //ConversationManager.Instance.StartConversation(conversation);
            //ConversationManager.OnConversationEnded += OnConversationEnded;
        }
             //else
            //{
            //    //langsung mulai dialog quest jika sudah yang pertama
            //    ConversationManager.Instance.StartConversation(Questconversation);
            //}
    }

    void StartFirstConversation()
    {
        ConversationManager.Instance.StartConversation(conversation);
        ConversationManager.OnConversationEnded += OnFirstConversationEnded;
    }

    void OnFirstConversationEnded()
    {
        ConversationManager.OnConversationEnded -= OnFirstConversationEnded;
        isFistDialogueDone = true;

        //Mulai dialog quest
        ConversationManager.Instance.StartConversation(Questconversation);
    }

    //void OnConversationEnded()
    //{
    //    //Tandai dialog pertama sudah selesai
    //    isFistConversationCompleted = true;
    //    //Unsubscribe event
    //    ConversationManager.OnConversationEnded -= OnConversationEnded;
    //    //Mulai dialog quest
    //    ConversationManager.Instance.StartConversation(Questconversation);
    //}

    private void OnDestroy()
    {
        //Unsubscribe event saat destroy
        ConversationManager.OnConversationEnded -= OnFirstConversationEnded;
    }

}