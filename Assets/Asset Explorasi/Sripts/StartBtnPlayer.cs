using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class StartBtnPlayer : MonoBehaviour
{
    public GameObject SpaceUI;
    public Flowchart flowchartStart;

    public bool CanTrStartUI = false;

    void Start()
    {
        //SpaceUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (CanTrStartUI)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SpaceUI.SetActive(false);
                flowchartStart.ExecuteBlock("StartUI");
                CanTrStartUI = false;
            }
            
        }
    }

    public void TrStartUI(bool value)
    {
        CanTrStartUI = value;
    }
}
