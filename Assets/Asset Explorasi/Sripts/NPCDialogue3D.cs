using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System.Collections;

public class NPCDialogue3D : MonoBehaviour
{
    public GameObject bubbleChat;
    public TextMeshProUGUI dialogueText;
    [TextArea] public string[] dialogues;
    private int currentDialogueIndex;
    public float typingSpeed = 0.05f;
    private bool dialogueStarted = false; // Untuk cek apakah dialog sudah dimulai di awal

    private void Start()
    {
        bubbleChat.SetActive(false);
        ShowDialogue(); // Menampilkan dialog saat game dimulai
    }

    private void Update()
    {

        if (bubbleChat.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            NextDialogue();
        }
    }

    public void ShowDialogue()
    {
        if (!dialogueStarted && dialogues.Length > 0)
        {
            dialogueStarted = true;
            bubbleChat.SetActive(true);
            StartCoroutine(TypeText(dialogues[currentDialogueIndex]));
        }
    }

    private void NextDialogue()
    {
        if (currentDialogueIndex < dialogues.Length - 1)
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
        bubbleChat.SetActive(false);
        currentDialogueIndex = 0;
        dialogueStarted = false; // Reset agar dialog bisa dimulai lagi
    }

    private IEnumerator TypeText(string text)
    {
        dialogueText.text = "";
        foreach (char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        // Jika ini dialog terakhir, bubble chat akan hilang setelah teks selesai
        //if (currentDialogueIndex == dialogues.Length - 1)
        //{
        //    HideDialogue();
        //}
    }
}
