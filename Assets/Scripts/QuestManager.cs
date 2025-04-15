using DialogueEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{

    public static QuestManager Instance;
    public GameObject item;
    public List<Transform> ChestSpawnPoint = new List<Transform>(); 

    public NPCConversation questConversation;

    public void Awake()
    {
        Instance = this;
        ConversationManager.OnConversationEnded += OnConversationEnded;
    }

    void OnConversationEnded()
    {
        bool questAccepted = ConversationManager.Instance.GetBool("QuestAccepted");

        if (questAccepted)
        {
            // Logic for when the quest is accepted
            Debug.Log("Quest Accepted!");
            StartQuest();
            SpawnQuestItem();
        }
        else
        {
            // Logic for when the quest is not accepted
            Debug.Log("Quest Not Accepted!");
        }
    }

    public void SpawnQuestItem()
    {
        foreach (Transform spawnPoint in ChestSpawnPoint)
        {
            Instantiate(item, spawnPoint.position, Quaternion.identity);
        }
        Debug.Log($"Buku Dispawn di {ChestSpawnPoint.Count} lokasi");
    }

    public void StartQuest()
    {
        // Logic to start the quest
        Debug.Log("Quest Started! : Temukan 5 Barang");
        // You can add more logic here, like updating quest status, etc.
    }

    public void AddItem()
    {
        int items = ConversationManager.Instance.GetInt("ItemCollected");
        items++;
        ConversationManager.Instance.SetInt("ItemCollected", items);

        if (items >= 5)
        {
            // Logic for when the quest is completed
            Debug.Log("Quest Completed!");
            CompleteQuest();
        }

    }

    public void CompleteQuest()
    {
        // Logic to complete the quest
        Debug.Log("Quest Completed! : Temukan 5 Barang");
        // You can add more logic here, like updating quest status, etc.
    }
}
