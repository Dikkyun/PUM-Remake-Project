using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectActivator : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
