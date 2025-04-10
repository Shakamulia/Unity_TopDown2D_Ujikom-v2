using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI dialogueText;
    public GameObject[] choiceButton;
    public GameObject Dialogue;
    private Dialogue currentDialouge;

    // This method is called when the dialogue manager is enabled
    public void StartDialogue(Dialogue dialogue)
    {
        Dialogue.SetActive(true); // Show the dialogue UI
        currentDialouge = dialogue;
        NameText.text = dialogue.npcName;
        dialogueText.text = dialogue.sentence[0];

        for (int i = 0; i < dialogue.choices.Length; i++)
        {
            choiceButton[i].SetActive(true);
            // Ambil TextMeshProUGUI yang ada di dalam tombol
            TextMeshProUGUI choiceText = choiceButton[i].GetComponentInChildren<TextMeshProUGUI>();
            choiceText.text = dialogue.choices[i];

            int index = i;
            Button btn = choiceText.GetComponentInChildren<Button>();
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => OnChoiceSelected(index));
        }
    }

    public void OnChoiceSelected(int choiceIndex)
    {
        if(currentDialouge.choiceMissionIDs.Length > choiceIndex)
        {
            int missionID = currentDialouge.choiceMissionIDs[choiceIndex];
            FindObjectOfType<MissionManager>().GiveMission(missionID);
        }   

        foreach (var button in choiceButton)
        {
            button.SetActive(false);
        }
    }
}
