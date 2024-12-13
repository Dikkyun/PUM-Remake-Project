using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System.Collections;

public class NPCDialogue : MonoBehaviour
{
    public GameObject bubbleChat; 
    public TextMeshProUGUI dialogueText;
    [TextArea] public string[] dialogues;
    private int currentDialogueIndex;
    public float typingSpeed = 0.05f;
    private bool isPlayerNearby = false;

    private void Start()
    {
        bubbleChat.SetActive(false); 
    }

    private void Update()
    {
        if(isPlayerNearby && Input.GetKeyDown(KeyCode.LeftShift))
        {
            ShowDialogue();
        }

        if (bubbleChat.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            NextDialogue();
        }
    }

    public void ShowDialogue()
    {
        if (dialogues.Length > 0)
        {
            StartCoroutine(TypeText(dialogues[currentDialogueIndex]));
            bubbleChat.SetActive(true);
        }
    }

    private void NextDialogue()
    {
        if (currentDialogueIndex < dialogues.Length-1)
        {
            currentDialogueIndex++;
            StartCoroutine(TypeText(dialogues[currentDialogueIndex]));
        }
        else
        {
            HideDialogue();
        }
    }

    public void HideDialogue()
    {
        bubbleChat.SetActive(false );
        currentDialogueIndex = 0;
    }

    private IEnumerator TypeText(string text)
    {
        dialogueText.text = "";
        foreach (char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            HideDialogue();
        }
    }
}
