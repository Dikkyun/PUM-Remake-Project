using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attach this script to each button
public class TargetButton : MonoBehaviour
{
    public Character targetCharacter;
    public TurnManager turnManager;

    public void OnClick()
    {
        turnManager.SelectTarget(targetCharacter);
    }
}

