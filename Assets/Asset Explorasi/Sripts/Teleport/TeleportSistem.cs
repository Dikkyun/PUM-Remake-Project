using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSistem : MonoBehaviour
{
    public Transform targetLocation;
    public float teleportDelay = 2f;

    private bool isTeleporting = false;
    private bool isPlayerInZone = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isPlayerInZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = false;
        }
    }

    private void Update()
    {
        if (isPlayerInZone && !isTeleporting && Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(Teleport());
        }
    }

    private IEnumerator Teleport()
    {
        isTeleporting = true;

        yield return new WaitForSeconds(teleportDelay);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = targetLocation.position;

        yield return new WaitForSeconds(1f);
        isTeleporting=false;
    }
}
