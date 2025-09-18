using UnityEngine;
using Fungus;

public class InteractTrigger : MonoBehaviour
{
    public Flowchart flowchart;
    public string act;
    public GameObject Screen;

    private bool playerInRange = false;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.Space))
        {
            flowchart.ExecuteBlock(act);
            playerInRange = false;
        }
    }
}
