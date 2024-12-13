using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 3f;

    // Update is called once per frame
    void Update()
    {
        // You can add any condition to trigger the level load if necessary
    }

    public void LoadBossLevel()
    {
        // Start coroutine to load the Boss1 scene
        StartCoroutine(LoadLevel("Boss1"));
    }

    IEnumerator LoadLevel(string levelName)
    {
        // Play animation for transition
        transition.SetTrigger("Start");

        // Wait for the transition to complete
        yield return new WaitForSeconds(transitionTime);

        // Load the scene by name (Boss1)
        SceneManager.LoadScene(levelName);
    }
}
