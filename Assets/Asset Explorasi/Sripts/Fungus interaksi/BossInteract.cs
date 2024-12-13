using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Fungus;

public class BossInteract : MonoBehaviour
{
    public Flowchart flowchart;
    public string act;
    public GameObject bossInteract;

    private bool playerInRange = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange == true)
        {
            flowchart.ExecuteBlock(act);
            playerInRange = false;
            bossInteract.SetActive(false);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    public void ScreenBoss()
    {
        SceneManager.LoadScene("boss1");
    }
}
