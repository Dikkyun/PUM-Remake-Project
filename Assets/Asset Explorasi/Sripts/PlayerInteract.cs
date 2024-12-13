using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            float interactRange = 2f;
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            foreach (Collider collider in colliderArray)
            {
                if (collider.TryGetComponent(out NpcInteract npcInteracttable))
                {
                    npcInteracttable.Interact();
                } 
            }
        }
    }

    public NpcInteract GetInteractableObject()
    {
        float interactRange = 2f;
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out NpcInteract npcInteracttable))
            {
                return npcInteracttable;
            }
        }
        return null;
    }
}
