using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Fungus;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

[System.Serializable]
public class PlayerMoveData
{
    public Transform targetpostransform;
    public Vector3 targetPos;
}

public class BattleSystem : MonoBehaviour
{
    [Header("Fungus")]
    public Flowchart flowchart;
    public string wonSituation;
    public string LostSituation;
    public string perpindahanScene;

    [Header("Boss Battle Setting")]
    public GameObject player; // Direct reference to the player GameObject
    public Animator playerAnim; 
    public GameObject enemy;  // Direct reference to the enemy GameObject
    Unit playerUnit;
    Unit enemyUnit;
    public GameObject playerDialoguePanel; // Reference to the player dialogue panel
    public GameObject enemyDialoguePanel;  // Reference to the enemy dialogue panel

    public Text playerDialogueText;
    public Text enemyDialogueText;
    public BattleHUD enemyHUD;

    public Button attackButton;
    public Button argumentsButton;
    public Button silenceButton;
    public Button option1Button;
    public Button option2Button;
    public Button option3Button;

    public BattleState state;

    private bool buffNoGaugeIncrease = false;
    private bool buffSilenceDecreasesGauge = false;

    [SerializeField] private int enemyDialogueIndex = 0;
    [SerializeField] private int playerActions = 0;
    private int attackActions = 0; // Track the number of attack actions

    public PlayerMoveData[] playerMovedata;

    // Screen shake component reference
    private ScreenShake screenShake;

    [SerializeField]
    private string[] enemyDialogues = {
        "The worlds are true horror...",
        "DONT COME NEAR ME!",
        "My Efforts are FUTILE!",
        "Why is The World So Cruel!?",
        "Is it my fault?"
    };

    [SerializeField]
    private string[] argumentQuestions = {
        "What is the point of life?",
        "What are the things i should think of?",
        "Why everyone is so unreliable?",
        "Why destiny hates me?",
        "how do i love my life?"
    };

    [SerializeField]
    private string[] argumentsForHorror = {
        "It's a harsh reality.",
        "Stay strong.",
        "Find solace in yourself."
    };

    [SerializeField]
    private string[] argumentsForDontComeNear = {
        "We can help you.",
        "Don't be afraid.",
        "Let's talk it out."
    };

    [SerializeField]
    private string[] argumentsForFutile = {
        "Keep trying.",
        "You have potential.",
        "Don't give up."
    };

    [SerializeField]
    private string[] argumentsForCruel = {
        "The world is tough.",
        "Stay resilient.",
        "Overcome the cruelty."
    };

    [SerializeField]
    private string[] argumentsForFault = {
        "It's not your fault.",
        "They're Cruel.",
        "Don't think about it."
    };

    [SerializeField]
    private string[] argumentsForLife = {
        "You make your own purpose",
        "Only Time will tell.",
        "stick around and find it"
    };

    [SerializeField]
    private string[] argumentsForThink = {
        "Focus on what matters.",
        "Be mindful of your thoughts.",
        "Overthink everything."
    };

    [SerializeField]
    private string[] argumentsForUnreliable = {
        "Trust those who prove themselves.",
        "Rely on yourself first.",
        "No one can be trusted."
    };

    [SerializeField]
    private string[] argumentsForDestiny = {
        "Create your own destiny.",
        "Fate is what you make of it.",
        "You are doomed."
    };

    [SerializeField]
    private string[] argumentsForLoveLife = {
        "Find joy in small things.",
        "Appreciate every moment.",
        "Life is too hard to love."
    };

    void Awake()
    {
        playerAnim = player.GetComponent<Animator>();
    }

    void Start()
    {
        state = BattleState.START;

        // Initialize the screen shake reference
        screenShake = FindObjectOfType<ScreenShake>();

        SetupBattle();
    }

    void SetupBattle()
    {
        playerUnit = player.GetComponent<Unit>();
        enemyUnit = enemy.GetComponent<Unit>();
        enemyUnit.currentGauge = 20;

        // Hide the player and enemy dialogue panels at the start
        playerDialoguePanel.SetActive(false);
        enemyDialoguePanel.SetActive(false);

        playerDialogueText.text = "";  // Clear player dialogue text
        enemyDialogueText.text = "";   // Clear enemy dialogue text

        enemyHUD.SetHUD(enemyUnit);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        playerDialogueText.text = "You Have Taken a Step...";
        yield return new WaitForSeconds(2f);

        attackActions++; // Increment attack actions
        playerActions++;

        if (buffNoGaugeIncrease)
        {
            buffNoGaugeIncrease = false; // Use the buff
        }
        else
        {
            IncreaseEnemyGauge(10);
        }

        PlayerMove();

        // Trigger screen shake when player attacks
        screenShake.StartShake();

        CheckEndBattleConditions();
    }

    void ShowArgumentsOptions(string[] argumentsDialogues)
    {
        attackButton.gameObject.SetActive(false);
        argumentsButton.gameObject.SetActive(false);
        silenceButton.gameObject.SetActive(false);

        option1Button.gameObject.SetActive(true);
        option2Button.gameObject.SetActive(true);
        option3Button.gameObject.SetActive(true);

        option1Button.GetComponentInChildren<Text>().text = argumentsDialogues[0];
        option2Button.GetComponentInChildren<Text>().text = argumentsDialogues[1];
        option3Button.GetComponentInChildren<Text>().text = argumentsDialogues[2];

        option1Button.onClick.RemoveAllListeners();
        option2Button.onClick.RemoveAllListeners();
        option3Button.onClick.RemoveAllListeners();

        option1Button.onClick.AddListener(() => OnArgumentOptionSelected(argumentsDialogues, 0));
        option2Button.onClick.AddListener(() => OnArgumentOptionSelected(argumentsDialogues, 1));
        option3Button.onClick.AddListener(() => OnArgumentOptionSelected(argumentsDialogues, 2));
    }

    IEnumerator PlayerArguments()
    {
        int randomIndex = UnityEngine.Random.Range(0, argumentQuestions.Length);
        string selectedQuestion = argumentQuestions[randomIndex];

        // Show the enemy dialogue panel and set the question
        enemyDialoguePanel.SetActive(true);
        enemyDialogueText.text = selectedQuestion;

        // Ensure player dialogue panel is hidden
        playerDialoguePanel.SetActive(false);
        playerDialogueText.text = "";

        // Now show the arguments options
        switch (selectedQuestion)
        {
            case "He's a liar, right?":
                ShowArgumentsOptions(argumentsForLife);
                break;
            case "I will always be like this...":
                ShowArgumentsOptions(argumentsForThink);
                break;
            case "Why everyone is so unreliable?":
                ShowArgumentsOptions(argumentsForUnreliable);
                break;
            case "What do you want?":
                ShowArgumentsOptions(argumentsForDestiny);
                break;
            case "Let's give up on this...":
                ShowArgumentsOptions(argumentsForLoveLife);
                break;
            default:
                Debug.LogError("Selected question does not match any case.");
                break;
        }

        yield return null;
    }

    void OnArgumentOptionSelected(string[] argumentsDialogues, int optionIndex)
    {

        // Hide enemy dialogue panel
        enemyDialoguePanel.SetActive(false);

        // Show player dialogue panel and set the player's chosen option
        playerDialoguePanel.SetActive(true);
        playerDialogueText.text = argumentsDialogues[optionIndex];

        // After the choice, show buff text for 2 seconds and then hide the player dialogue
        StartCoroutine(ShowBuffText());

        Debug.Log("Argument selected: " + argumentsDialogues[optionIndex]);

        // Gauge logic...
        //if (argumentsDialogues[optionIndex] == "stick around and find it" ||
        //    argumentsDialogues[optionIndex] == "Overthink everything." ||
        //    argumentsDialogues[optionIndex] == "No one can be trusted." ||
        //    argumentsDialogues[optionIndex] == "You are doomed." ||
        //    argumentsDialogues[optionIndex] == "Life is too hard to love.")
        //{
        //    IncreaseEnemyGauge(15);
        //}
        if (argumentsDialogues[optionIndex] == "For me, you are the liar here." ||
            argumentsDialogues[optionIndex] == "That's for the better I guess." ||
            argumentsDialogues[optionIndex] == "Then you can end yourself." ||
            argumentsDialogues[optionIndex] == "I hate the way you think." ||
            argumentsDialogues[optionIndex] == "Do it yourself.")
        {
            IncreaseEnemyGauge(15);
        }
        else
        {
            DecreaseEnemyGauge(10);
            AssignRandomBuff(); // Assign a buff for correct answer
        }

        option1Button.gameObject.SetActive(false);
        option2Button.gameObject.SetActive(false);
        option3Button.gameObject.SetActive(false);

        attackButton.gameObject.SetActive(true);
        argumentsButton.gameObject.SetActive(true);
        silenceButton.gameObject.SetActive(true);

        playerActions++;
        ContinueGameAfterArgument();
    }

    void AssignRandomBuff()
    {
        int randomBuff = UnityEngine.Random.Range(0, 2);
        if (randomBuff == 0)
        {
            buffNoGaugeIncrease = true;
            playerDialogueText.text = "Buff: Next action won't increase enemy's gauge!";
        }
        else
        {
            buffSilenceDecreasesGauge = true;
            playerDialogueText.text = "Buff: Next silence will decrease enemy's gauge by 10!";
        }
    }

    IEnumerator PlayerSilence()
    {
        playerDialogueText.text = "You chose to do nothing...";
        yield return new WaitForSeconds(2f);

        playerActions++;

        if (buffSilenceDecreasesGauge)
        {
            DecreaseEnemyGauge(10);
            buffSilenceDecreasesGauge = false; // Use the buff
        }

        CheckEndBattleConditions();
    }

    void CheckEndBattleConditions()
    {
        if (attackActions >= 6)
        {
            //playerDialogueText.text = "You won the battle!";
            flowchart.ExecuteBlock(wonSituation);
            DisableActionButtons();
        }
        else if (enemyUnit.currentGauge >= 25)
        {
            //playerDialogueText.text = "You were defeated.";
            flowchart.ExecuteBlock(LostSituation);
            DisableActionButtons();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    void DisableActionButtons()
    {
        attackButton.gameObject.SetActive(false);
        argumentsButton.gameObject.SetActive(false);
        silenceButton.gameObject.SetActive(false);
        option1Button.gameObject.SetActive(false);
        option2Button.gameObject.SetActive(false);
        option3Button.gameObject.SetActive(false);
    }

    void ContinueGameAfterArgument()
    {
        if (enemyUnit.currentGauge >= 25)
        {
            //playerDialogueText.text = "You were defeated.";
            flowchart.ExecuteBlock(LostSituation);
            DisableActionButtons();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            attackButton.gameObject.SetActive(false);
            argumentsButton.gameObject.SetActive(false);
            silenceButton.gameObject.SetActive(false);
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        print("EnemyTurn");
        enemyDialogueText.text = enemyDialogues[enemyDialogueIndex];
        enemyDialogueIndex = (enemyDialogueIndex + 1) % enemyDialogues.Length;

        yield return new WaitForSeconds(2f);
        print("Ubah ke player turn");
        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void IncreaseEnemyGauge(int amount)
    {
        enemyUnit.currentGauge += amount;
        enemyHUD.SetGauge(enemyUnit.currentGauge);
    }

    public void DecreaseEnemyGauge(int amount)
    {
        enemyUnit.currentGauge -= amount;
        enemyHUD.SetGauge(enemyUnit.currentGauge);
    }

    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            playerDialogueText.text = "You won the battle!";
        }
        else if (state == BattleState.LOST)
        {
            playerDialogueText.text = "You were defeated.";
        }
    }

    void PlayerTurn()
    {
       
            playerDialogueText.text = "Choose an action: Action, Arguments, or Silence";
            attackButton.gameObject.SetActive(true);
            argumentsButton.gameObject.SetActive(true);
            silenceButton.gameObject.SetActive(true);
       

    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        attackButton.gameObject.SetActive(false);
        argumentsButton.gameObject.SetActive(false);
        silenceButton.gameObject.SetActive(false);
        StartCoroutine(PlayerAttack());
        screenShake.StartShake(); // Trigger screen shake immediately when the attack button is pressed
    }

    public void OnArgumentsButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerArguments());
    }

    public void OnSilenceButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        attackButton.gameObject.SetActive(false);
        argumentsButton.gameObject.SetActive(false);
        silenceButton.gameObject.SetActive(false);
        StartCoroutine(PlayerSilence());
    }

    public void PlayerMove()
    {
        //play animasi jalan
        playerAnim.Play("Move");
        //player.transform.position = playerMovedata[attackActions].targetPos;
        player.transform.DOMove(playerMovedata[attackActions].targetpostransform.position, 1f).OnComplete(() =>
        {
            //stop animasi jalan
            playerAnim.Play("Idle");
        });
    }

    IEnumerator ShowBuffText()
    {
        yield return new WaitForSeconds(2f); // Wait for 2 seconds
        playerDialoguePanel.SetActive(false); // Hide the player dialogue panel
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(perpindahanScene);
    }
}
