using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public List<Character> playerCharacters;
    public List<Character> enemyCharacters;
    public GameObject buttonPrefab;  // Add this line to define buttonPrefab
    private int currentCharacterIndex = 0;
    private bool playerTurn = true;
    private bool actionSelected = false;
    private Character selectedTarget;

    void Start()
    {
        StartCoroutine(TurnCycle());
    }

    IEnumerator TurnCycle()
    {
        while (true)
        {
            if (playerTurn)
            {
                // Player's turn logic
                yield return StartCoroutine(PlayerTurn());
            }
            else
            {
                // Enemy's turn logic
                yield return StartCoroutine(EnemyTurn());
            }
            playerTurn = !playerTurn;
            yield return null;
        }
    }

    IEnumerator PlayerTurn()
    {
        Debug.Log("Player's turn");

        // Select the active character
        Character activeCharacter = playerCharacters[currentCharacterIndex];

        // Enable UI for action selection
        EnableActionUI(activeCharacter);

        // Wait until an action is selected
        yield return new WaitUntil(() => actionSelected);

        // Perform the selected action
        PerformAction(activeCharacter);

        // Disable action UI
        DisableActionUI();

        actionSelected = false;
        currentCharacterIndex = (currentCharacterIndex + 1) % playerCharacters.Count;
    }

    IEnumerator EnemyTurn()
    {
        Debug.Log("Enemy's turn");

        // Example: select the first enemy character to act
        Character activeEnemy = enemyCharacters[currentCharacterIndex];
        // Perform an action, such as attacking the first player character
        activeEnemy.Attack(playerCharacters[0]);

        currentCharacterIndex = (currentCharacterIndex + 1) % enemyCharacters.Count;

        yield return new WaitForSeconds(1f); // Simulate time taken for the enemy to act
    }

    public void OnActionSelected()
    {
        actionSelected = true;
    }

    public void SelectTarget(Character target)
    {
        selectedTarget = target;
        actionSelected = true;
    }

    void EnableActionUI(Character activeCharacter)
    {
        // Show UI buttons and set their callbacks to SelectTarget
        foreach (Character enemy in enemyCharacters)
        {
            // Create a button for each enemy and set up the callback
            GameObject button = Instantiate(buttonPrefab);
            button.GetComponent<TargetButton>().targetCharacter = enemy;
            button.GetComponent<TargetButton>().turnManager = this;
            // Add the button to your UI canvas/panel
            button.transform.SetParent(GameObject.Find("Canvas").transform, false); // Make sure you have a Canvas in your scene
        }
    }

    void PerformAction(Character activeCharacter)
    {
        // Perform the attack on the selected target
        activeCharacter.Attack(selectedTarget);
    }

    void DisableActionUI()
    {
        // Hide UI buttons and remove their callbacks
        foreach (Transform child in GameObject.Find("Canvas").transform)
        {
            Destroy(child.gameObject);
        }
    }
}
