using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;


public class NPCInteraction2 : MonoBehaviour
{
    public Flowchart flowchart;
    public string act;
    public GameObject Screen;

    [Header("William Control")]
    public GameObject npc1;
    public GameObject npc2;
    public GameObject npc3;
    public GameObject npc4;

    [Header("Teleport Control")]
    public GameObject TB4;

    private bool playerInRange = false;
    private bool dialogFinished = false;

    public void Start()
    {
        Screen.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.Space))
        {
            flowchart.ExecuteBlock(act);
            StartCoroutine(WaitForDialogToEnd());
            playerInRange = false;
        }
    }

    IEnumerator WaitForDialogToEnd()
    {
        
        while (flowchart.GetExecutingBlocks().Count > 0)
        {
            yield return null;  
        }

        
        HandleNPCVisibility();
    }

    void HandleNPCVisibility()
    {
        if (gameObject.name == "NPC1")
        {
            npc1.SetActive(false);
            npc2.SetActive(true);
            
        }
        else if (gameObject.name == "NPC2")
        {
            npc1.SetActive(false);  
            npc2.SetActive(false);  
            npc3.SetActive(true);   
        }
        else if (gameObject.name == "NPC3")
        {
            npc1.SetActive(false);
            npc2.SetActive(false);
            npc3.SetActive(false);
            npc4.SetActive(true);
            TB4.SetActive(false); 
        }
    }


    //public void NoColliderNpc()
    //{
    //    NpcTargetCollider.enabled = false;
    //}

    //public void ColliderNpc()
    //{
    //    NpcTargetCollider.enabled = true;
    //}

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Screen.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Screen.SetActive(false);
        }
    }
}
