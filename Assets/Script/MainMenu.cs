using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        ScoreManager.Instance.Score = 0;
        SceneManager.LoadScene("game");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
