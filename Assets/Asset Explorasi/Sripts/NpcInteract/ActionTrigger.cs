using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTrigger : MonoBehaviour
{
    [SerializeField] private SceneAction sceneAction = null;
    public GameObject objectUI;
    //private bool playerInRange = false;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = sceneAction.GetActionIcon();
        objectUI.SetActive(false);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            sceneAction.Interact();
            this.gameObject.SetActive(false);
        }
    }

    
}
