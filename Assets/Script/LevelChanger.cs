using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public Animator animator;
    private int levelToLoad;
    private bool transitionAllowed = true;

    // Update is called once per frame
    public void Update()
    {
        if (transitionAllowed && Input.GetMouseButtonDown(0))
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        }
    }

    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    // Call this method to enable or disable scene transitions
    public void SetTransitionAllowed(bool allowed)
    {
        transitionAllowed = allowed;
    }

    public void ChangeSceneWithoutMouseInput()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        // Check if the next scene is not "GameScene" before triggering the fade
        if (nextSceneIndex != SceneManager.sceneCountInBuildSettings - 1)
        {
          FadeToLevel(nextSceneIndex);
        }
        else
        {
            // Transition not allowed after reaching "GameScene"
            Debug.Log("Transition not allowed after reaching GameScene.");
        }
    }
}